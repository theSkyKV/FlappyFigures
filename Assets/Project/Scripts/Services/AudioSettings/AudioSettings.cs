using System;

namespace Project.Services.AudioSettings
{
	public class AudioSettings
	{
		private float _musicVolume;
		private float _soundVolume;

		public float MusicVolume
		{
			get => _musicVolume;
			set
			{
				if (value < 0)
				{
					_musicVolume = 0;
				}
				else if (value > 1)
				{
					_musicVolume = 1;
				}
				else
				{
					_musicVolume = value;
				}

				MusicVolumeUpdated?.Invoke(_musicVolume);
			}
		}

		public float SoundVolume
		{
			get => _soundVolume;
			set
			{
				if (value < 0)
				{
					_soundVolume = 0;
				}
				else if (value > 1)
				{
					_soundVolume = 1;
				}
				else
				{
					_soundVolume = value;
				}

				SoundVolumeUpdated?.Invoke(_soundVolume);
			}
		}

		public event Action<float> MusicVolumeUpdated;
		public event Action<float> SoundVolumeUpdated;
	}
}