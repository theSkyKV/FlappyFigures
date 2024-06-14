using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Project.Core;
using Project.Entities.Figures;
using Project.Entities.Obstacles;
using Project.Entities.Obstacles.Spawners;
using Project.Inputs;
using Project.Services.PauseSystems;
using Project.UI.Level;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Project.Scenes
{
	public class LevelSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private PlayerInputController _playerInputController;

		[SerializeField]
		private Transform _playerSpawnPoint;

		[SerializeField]
		private ObstacleSpawner _obstacleSpawner;

		[SerializeField]
		private List<DeadZone> _borders;

		[SerializeField]
		private LevelPanel _levelPanel;

		private List<Obstacle> _obstacles;

		private Figure _figure;

		private bool _firstTimeClicked;
		private float _gameSpeed;
		private float _terminalGameSpeed;
		private float _gameSpeedScaler;
		private int _score;
		private int _resurrectionNumber;
		private float _immortalTimeAfterResurrection;
		private float _timer;
		private float _immortalTimerStart;

		private IPauseSystem PauseSystem => ProjectContext.Instance.Service.PauseSystem;

		[DllImport("__Internal")]
		private static extern void ShowResurrectAdvExtern();

		private void Awake()
		{
			var settings = ProjectContext.Instance.GameSettings;
			_gameSpeed = settings.BaseGameSpeed;
			_terminalGameSpeed = settings.TerminalGameSpeed;
			_gameSpeedScaler = settings.GameSpeedScaler;
			_immortalTimeAfterResurrection = settings.ImmortalTimeAfterResurrection;

			SpawnPlayer();

			_playerInputController.Init(_figure);

			_obstacles = new List<Obstacle>();
			_obstacleSpawner.Init();

			_levelPanel.UpdateScore(_score);

			_firstTimeClicked = false;
			_timer = 0;
		}

		private void OnEnable()
		{
			_playerInputController.FirstTimeClicked += OnFirstTimeClicked;
			_obstacleSpawner.ScoreZoneTriggered += AddScore;
			_obstacleSpawner.DeadZoneTriggered += OnDeadZoneTriggered;
			_obstacleSpawner.ObstacleCreated += OnObstacleCreated;
			foreach (var border in _borders)
			{
				border.Triggered += OnDeadZoneTriggered;
			}

			_levelPanel.ContinueButtonClicked += OnContinueButtonClicked;
			_levelPanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
			_levelPanel.PauseButtonClicked += OnPauseButtonClicked;
			_levelPanel.RestartButtonClicked += OnRestartButtonClicked;
			_levelPanel.ResurrectButtonClicked += OnResurrectButtonClicked;
		}

		private void OnDisable()
		{
			_playerInputController.FirstTimeClicked -= OnFirstTimeClicked;
			_obstacleSpawner.ScoreZoneTriggered -= AddScore;
			_obstacleSpawner.DeadZoneTriggered -= OnDeadZoneTriggered;
			_obstacleSpawner.ObstacleCreated -= OnObstacleCreated;
			foreach (var border in _borders)
			{
				border.Triggered -= OnDeadZoneTriggered;
			}

			_levelPanel.ContinueButtonClicked -= OnContinueButtonClicked;
			_levelPanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
			_levelPanel.PauseButtonClicked -= OnPauseButtonClicked;
			_levelPanel.RestartButtonClicked -= OnRestartButtonClicked;
			_levelPanel.ResurrectButtonClicked -= OnResurrectButtonClicked;
		}

		private void Update()
		{
			if (PauseSystem.IsPaused || !_firstTimeClicked)
			{
				return;
			}

			IncreaseGameSpeed();

			foreach (var obstacle in _obstacles)
			{
				obstacle.transform.Translate(Vector2.left * (Time.deltaTime * _gameSpeed));
			}

			_timer += Time.deltaTime;
		}

		private void IncreaseGameSpeed()
		{
			_gameSpeed += Time.deltaTime / _gameSpeedScaler;
			_gameSpeed = _gameSpeed > _terminalGameSpeed ? _terminalGameSpeed : _gameSpeed;
		}

		private void AddScore()
		{
			_score++;
			_levelPanel.UpdateScore(_score);
		}

		private void OnDeadZoneTriggered()
		{
			SetPause(true);
			var canResurrect = _resurrectionNumber > 0;
			_levelPanel.ActivateGameOver(canResurrect);
		}

		private void OnObstacleCreated(Obstacle obstacle)
		{
			_obstacles.Add(obstacle);
		}

		private void SpawnPlayer()
		{
			var figureInfo = ProjectContext.Instance.Figure;
			_figure = new GameObject(figureInfo.Name).AddComponent<Figure>();
			_figure.Init(figureInfo);
			_figure.transform.position = _playerSpawnPoint.position;
			_resurrectionNumber = figureInfo.LifeCount - 1;
		}

		private void OnFirstTimeClicked()
		{
			_firstTimeClicked = true;
			_figure.Activate();
			_obstacleSpawner.Spawn();
			_levelPanel.HideInfo();
		}

		private void OnContinueButtonClicked()
		{
			SetPause(false);
			_levelPanel.DeactivatePause();
		}

		private void OnMainMenuButtonClicked()
		{
			SetPause(false);
			SceneManager.LoadScene(1);
		}

		private void OnPauseButtonClicked()
		{
			SetPause(true);
			_levelPanel.ActivatePause();
		}

		private void OnRestartButtonClicked()
		{
			SetPause(false);
			SceneManager.LoadScene(2);
		}

		private float _musicVolume;

		private void OnResurrectButtonClicked()
		{
			_musicVolume = ProjectContext.Instance.Service.AudioSettings.MusicVolume;
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = 0;
			EventSystem.current.SetSelectedGameObject(null);
			ShowResurrectAdvExtern();
		}

		public void OnAdvClosedOrFailed()
		{
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = _musicVolume;
		}

		public void OnRewardReceived()
		{
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = _musicVolume;
			Resurrect();
		}

		private void Resurrect()
		{
			_resurrectionNumber--;
			_figure.transform.position = _playerSpawnPoint.position;
			_figure.ResetFigure();
			_levelPanel.DeactivateGameOver();
			SetPause(false);
			StartCoroutine(SetImmortal());
		}

		private IEnumerator SetImmortal()
		{
			_immortalTimerStart = _timer;
			_figure.SetImmortal(true);
			yield return new WaitWhile(IsImmortal);
			_figure.SetImmortal(false);
		}

		private bool IsImmortal()
		{
			return _timer - _immortalTimerStart < _immortalTimeAfterResurrection;
		}

		private void SetPause(bool isPaused)
		{
			if (!isPaused)
			{
				PauseSystem.SetPause(false);
				return;
			}

			SaveRecord();
			PauseSystem.SetPause(true);
		}

		private void SaveRecord()
		{
			var saveData = ProjectContext.Instance.Data;
			var figure = ProjectContext.Instance.Figure;
			var currentRecord = saveData.Records[figure.Type];
			if (currentRecord >= _score)
			{
				return;
			}

			saveData.Records[figure.Type] = _score;
			ProjectContext.Instance.Service.SaveSystem.Save(saveData);
		}

		private void OnApplicationQuit()
		{
			SaveRecord();
		}
	}
}