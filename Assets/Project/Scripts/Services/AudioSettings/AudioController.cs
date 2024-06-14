using Project.Core;
using UnityEngine;

namespace Project.Services.AudioSettings
{
	public class AudioController : MonoBehaviour
	{
		[SerializeField]
		private AudioSource _audioSource;

		public void Init()
		{
			DontDestroyOnLoad(this);
			DontDestroyOnLoad(_audioSource.gameObject);

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