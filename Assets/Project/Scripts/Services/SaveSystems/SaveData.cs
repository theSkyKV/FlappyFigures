using System.Collections.Generic;
using Project.Entities.Figures;

namespace Project.Services.SaveSystems
{
	public class SaveData
	{
		public Dictionary<FigureType, int> Records { get; set; }
		public Dictionary<FigureType, bool> Availabilities { get; set; }
	}
}