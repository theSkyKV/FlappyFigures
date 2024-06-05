using Project.Core;
using Project.UI.MainMenu.SettingViews;
using UnityEngine;
using AudioSettings = Project.Services.AudioSettings.AudioSettings;

namespace Project.UI.MainMenu.AdditionalPanels
{
	public class SettingsPanel : OtherAdditionalPanel
	{
		[SerializeField]
		private MusicSettingView _musicSettingView;

		[SerializeField]
		private SoundSettingView _soundSettingView;

		private AudioSettings _audioSettings;

		public override void Activate()
		{
			_audioSettings = ProjectContext.Instance.Service.AudioSettings;
			UpdateMusicVolume(_audioSettings.MusicVolume);
			UpdateSoundVolume(_audioSettings.SoundVolume);
			_musicSettingView.ValueChanged += OnMusicVolumeChanged;
			_soundSettingView.ValueChanged += OnSoundVolumeChanged;
			base.Activate();
		}

		public override void Deactivate()
		{
			_musicSettingView.ValueChanged -= OnMusicVolumeChanged;
			_soundSettingView.ValueChanged -= OnSoundVolumeChanged;
			base.Deactivate();
		}

		private void OnMusicVolumeChanged(float value)
		{
			_audioSettings.UpdateMusicVolume(value);
		}

		private void OnSoundVolumeChanged(float value)
		{
			_audioSettings.UpdateSoundVolume(value);
		}

		private void UpdateMusicVolume(float value)
		{
			_musicSettingView.UpdateValue(value);
		}

		private void UpdateSoundVolume(float value)
		{
			_soundSettingView.UpdateValue(value);
		}
	}
}