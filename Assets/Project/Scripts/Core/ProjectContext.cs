using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
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

		private bool _playerInfoLoaded;
		private bool _additionalInfoLoaded;

		public event Action AllDataLoaded;

		public void Init()
		{
			_playerInfoLoaded = false;
			_additionalInfoLoaded = false;
			Instance = this;
			DontDestroyOnLoad(this);

			Service = new Service();

			_baseSaveDataLoader = new BaseSaveDataLoader(Service.Path.BaseSaveData);

			LoadData(true);

			_figureInfoLoader = new FigureInfoLoader(Service.Path.FigureInfo);
			_figureInfos = _figureInfoLoader.GetAll();
			Figure = _figureInfos[0];

			_gameSettingsLoader = new GameSettingsLoader(Service.Path.GameSettings);
			GameSettings = _gameSettingsLoader.Get();
			_additionalInfoLoaded = true;
			OnDataLoaded();
		}

		public void UpdateFigure(FigureType type)
		{
			Figure = _figureInfos.FirstOrDefault(f => f.Type == type);
		}

		public void LoadData(bool fromYandex = false)
		{
			if (fromYandex)
			{
				Service.SaveSystem.Load();
			}
			else
			{
				Data = Service.SaveSystem.Load() ?? _baseSaveDataLoader.Load();
			}
		}

		public void SetDataFromYandex(string json)
		{
			if (string.IsNullOrWhiteSpace(json) || json == "{}")
			{
				Data = _baseSaveDataLoader.Load();
			}
			else
			{
				Data = JsonConvert.DeserializeObject<SaveData>(json) ?? _baseSaveDataLoader.Load();
			}
			
			Service.AudioSettings.MusicVolume = Data.AudioSettings.MusicVolume;
			Service.AudioSettings.SoundVolume = Data.AudioSettings.SoundVolume;
			_playerInfoLoaded = true;
			OnDataLoaded();
		}

		private void OnDataLoaded()
		{
			if (_playerInfoLoaded && _additionalInfoLoaded)
			{
				AllDataLoaded?.Invoke();
			}
		}

		private void OnApplicationQuit()
		{
			Data.AudioSettings = Service.AudioSettings;
			Service.SaveSystem.Save(Data);
		}
	}
}