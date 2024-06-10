using System.Collections.Generic;
using System.Linq;
using Project.Config.Entities;
using Project.Config.Loaders;
using Project.Entities.Figures;
using Project.Services;
using Project.Services.SaveSystems;
using UnityEngine;

namespace Project.Core
{
	public class ProjectContext : MonoBehaviour
	{
		public static ProjectContext Instance { get; private set; }

		public Service Service { get; private set; }

		public FigureInfo Figure { get; private set; }

		public SaveData Data { get; set; }

		public GameSettings GameSettings { get; private set; }

		private FigureInfoLoader _figureInfoLoader;
		private BaseSaveDataLoader _baseSaveDataLoader;
		private GameSettingsLoader _gameSettingsLoader;

		private List<FigureInfo> _figureInfos;
		public IReadOnlyList<FigureInfo> FigureInfos => _figureInfos;

		public void Init()
		{
			Instance = this;
			DontDestroyOnLoad(this);

			Service = new Service();

			_baseSaveDataLoader = new BaseSaveDataLoader(Service.Path.BaseSaveData);

			LoadData();
			Service.AudioSettings.MusicVolume = Data.AudioSettings.MusicVolume;
			Service.AudioSettings.SoundVolume = Data.AudioSettings.SoundVolume;

			_figureInfoLoader = new FigureInfoLoader(Service.Path.FigureInfo);
			_figureInfos = _figureInfoLoader.GetAll();
			Figure = _figureInfos[0];

			_gameSettingsLoader = new GameSettingsLoader(Service.Path.GameSettings);
			GameSettings = _gameSettingsLoader.Get();
		}

		public void UpdateFigure(FigureType type)
		{
			Figure = _figureInfos.FirstOrDefault(f => f.Type == type);
		}

		private void LoadData()
		{
			Data = Service.SaveSystem.Load() ?? _baseSaveDataLoader.Load();
		}

		private void OnApplicationQuit()
		{
			Data.AudioSettings = Service.AudioSettings;
			Service.SaveSystem.Save(Data);
		}
	}
}