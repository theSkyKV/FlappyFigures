using Project.Core;
using Project.UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scenes
{
	public class MainMenuSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private MainMenuPanel _mainMenuPanel;

		[SerializeField]
		private AudioSource _audioSource;

		private void Awake()
		{
			_mainMenuPanel.Init();
			_mainMenuPanel.StartButtonClicked += OnStartButtonClicked;
			_mainMenuPanel.QuitButtonClicked += OnQuitButtonClicked;

			ProjectContext.Instance.Service.AudioSettings.MusicVolumeUpdated += UpdateMusicVolume;
			UpdateMusicVolume(ProjectContext.Instance.Service.AudioSettings.MusicVolume);
		}

		private void OnDestroy()
		{
			_mainMenuPanel.StartButtonClicked -= OnStartButtonClicked;
			_mainMenuPanel.QuitButtonClicked -= OnQuitButtonClicked;
			ProjectContext.Instance.Service.AudioSettings.MusicVolumeUpdated -= UpdateMusicVolume;
		}

		private void UpdateMusicVolume(float value)
		{
			_audioSource.volume = value;
		}

		private void OnStartButtonClicked()
		{
			SceneManager.LoadScene(2);
		}

		private void OnQuitButtonClicked()
		{
			Application.Quit();
		}
	}
}