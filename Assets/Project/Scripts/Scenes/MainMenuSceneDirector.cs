using System.Runtime.InteropServices;
using Project.Core;
using Project.Entities.Figures;
using Project.UI.MainMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Project.Scenes
{
	public class MainMenuSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		private float _musicVolume;
		private FigureType _figureType;

		[DllImport("__Internal")]
		private static extern void ShowFigureOpenAdvExtern();

		private void Awake()
		{
			_mainMenuPanel.Init();
			_mainMenuPanel.StartButtonClicked += OnStartButtonClicked;
			_mainMenuPanel.QuitButtonClicked += OnQuitButtonClicked;
			_mainMenuPanel.OpenNowButtonClicked += OnOpenNowButtonClicked;
		}

		private void OnDestroy()
		{
			_mainMenuPanel.StartButtonClicked -= OnStartButtonClicked;
			_mainMenuPanel.QuitButtonClicked -= OnQuitButtonClicked;
			_mainMenuPanel.OpenNowButtonClicked -= OnOpenNowButtonClicked;
		}

		private void OnStartButtonClicked()
		{
			ProjectContext.Instance.Data.AudioSettings = ProjectContext.Instance.Service.AudioSettings;
			ProjectContext.Instance.Service.SaveSystem.Save(ProjectContext.Instance.Data);
			SceneManager.LoadScene(2);
		}

		private void OnQuitButtonClicked()
		{ }

		private void OnOpenNowButtonClicked(FigureType type)
		{
			_figureType = type;
			_musicVolume = ProjectContext.Instance.Service.AudioSettings.MusicVolume;
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = 0;
			EventSystem.current.SetSelectedGameObject(null);
			ShowFigureOpenAdvExtern();
		}

		public void OnAdvClosedOrFailed()
		{
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = _musicVolume;
		}

		public void OnRewardReceived()
		{
			ProjectContext.Instance.Service.AudioSettings.MusicVolume = _musicVolume;
			ProjectContext.Instance.Data.Availabilities[_figureType] = true;
			ProjectContext.Instance.Service.SaveSystem.Save(ProjectContext.Instance.Data);
			_mainMenuPanel.UpdateFigureInfo();
		}
	}
}