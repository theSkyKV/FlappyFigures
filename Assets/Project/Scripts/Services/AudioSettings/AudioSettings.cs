using System;

namespace Project.Services.AudioSettings
{
	public class AudioSettings
	{
		public float MusicVolume { get; private set; }
		public float SoundVolume { get; private set; }

		public event Action<float> MusicVolumeUpdated;
		public event Action<float> SoundVolumeUpdated;

		public void UpdateMusicVolume(float value)
		{
			if (value < 0)
			{
				MusicVolume = 0;
			}
			else if (value > 1)
			{
				MusicVolume = 1;
			}
			else
			{
				MusicVolume = value;
			}

			MusicVolumeUpdated?.Invoke(MusicVolume);
		}

		public void UpdateSoundVolume(float value)
		{
			if (value < 0)
			{
				SoundVolume = 0;
			}
			else if (value > 1)
			{
				SoundVolume = 1;
			}
			else
			{
				SoundVolume = value;
			}

			SoundVolumeUpdated?.Invoke(SoundVolume);
		}
	}
}