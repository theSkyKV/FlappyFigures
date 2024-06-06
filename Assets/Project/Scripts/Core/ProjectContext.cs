using System.Collections.Generic;
using System.Linq;
using Project.Config.Loaders;
using Project.Entities.Figures;
using Project.Services;
using Project.Services.SaveSystems;
using UnityEngine;

namespace Project.Core
{
	public class ProjectContext : MonoBehaviour
	{
		public Service Service { get; private set; }

		public static ProjectContext Instance { get; private set; }

		public FigureInfo Figure { get; private set; }

		public SaveData Data { get; set; }

		private FigureInfoLoader _figureInfoLoader;
		private BaseSaveDataLoader _baseSaveDataLoader;

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
		}

		public void UpdateFigure(FigureType type)
		{
			Figure = _figureInfos.FirstOrDefault(f => f.Type == type);
		}

		private void LoadData()
		{
			Data = Service.SaveSystem.Load() ?? _baseSaveDataLoader.Load();
		}
	}
}