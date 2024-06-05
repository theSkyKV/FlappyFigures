using System.Collections.Generic;
using System.Linq;
using Project.Config.Loaders;
using Project.Entities.Figures;
using Project.Services;
using UnityEngine;

namespace Project.Core
{
	public class ProjectContext : MonoBehaviour
	{
		public Service Service { get; private set; }

		public static ProjectContext Instance { get; private set; }

		public FigureInfo Figure { get; private set; }

		private FigureInfoLoader _figureInfoLoader;

		private List<FigureInfo> _figureInfos;
		public IReadOnlyList<FigureInfo> FigureInfos => _figureInfos;

		public void Init()
		{
			Instance = this;
			DontDestroyOnLoad(this);

			Service = new Service();
			Service.AudioSettings.UpdateMusicVolume(0.5f);
			Service.AudioSettings.UpdateSoundVolume(0.5f);

			_figureInfoLoader = new FigureInfoLoader(Service.Path.FigureInfo);
			_figureInfos = _figureInfoLoader.GetAll();
			Figure = _figureInfos[0];
		}

		public void UpdateFigure(FigureType type)
		{
			Figure = _figureInfos.FirstOrDefault(f => f.Type == type);
		}
	}
}