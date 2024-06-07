using System.Collections.Generic;
using Project.Core;
using Project.Entities.Figures;
using Project.Entities.Obstacles;
using Project.Entities.Obstacles.Spawners;
using Project.Inputs;
using Project.UI.Level;
using UnityEngine;

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

		private float _gameSpeed;
		private float _terminalGameSpeed;
		private float _gameSpeedScaler;
		private int _score;

		private void Awake()
		{
			var settings = ProjectContext.Instance.GameSettings;
			_gameSpeed = settings.BaseGameSpeed;
			_terminalGameSpeed = settings.TerminalGameSpeed;
			_gameSpeedScaler = settings.GameSpeedScaler;

			SpawnPlayer();

			_playerInputController.Init(_figure);

			_obstacles = new List<Obstacle>();
			_obstacleSpawner.Init();

			_levelPanel.UpdateScore(_score);
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
		}

		private void Update()
		{
			IncreaseGameSpeed();

			foreach (var obstacle in _obstacles)
			{
				obstacle.transform.Translate(Vector2.left * (Time.deltaTime * _gameSpeed));
			}
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
		{ }

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
		}

		private void OnFirstTimeClicked()
		{
			_figure.Activate();
			_obstacleSpawner.Spawn();
		}
	}
}