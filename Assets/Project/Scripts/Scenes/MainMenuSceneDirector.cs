using Project.Core;
using Project.UI.MainMenu;
using UnityEngine;

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
			ProjectContext.Instance.Service.AudioSettings.MusicVolumeUpdated += UpdateMusicVolume;
			UpdateMusicVolume(ProjectContext.Instance.Service.AudioSettings.MusicVolume);
		}

		private void OnDestroy()
		{
			ProjectContext.Instance.Service.AudioSettings.MusicVolumeUpdated -= UpdateMusicVolume;
		}

		private void UpdateMusicVolume(float value)
		{
			_audioSource.volume = value;
		}
	}
}