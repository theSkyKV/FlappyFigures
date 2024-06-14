using Project.Services.Paths;
using Project.Services.PauseSystems;
using Project.Services.SaveSystems;

namespace Project.Services
{
	public class Service
	{
		public readonly Path Path;
		public readonly ISaveSystem SaveSystem;
		public readonly IPauseSystem PauseSystem;
		public readonly AudioSettings.AudioSettings AudioSettings;

		public Service()
		{
			Path = new Path();
			SaveSystem = new YandexGamesSaveSystem();
			PauseSystem = new DefaultPauseSystem();
			AudioSettings = new AudioSettings.AudioSettings();
		}
	}
}