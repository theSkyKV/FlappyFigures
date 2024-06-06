using System;
using Project.Entities.Figures;
using Project.Entities.Obstacles;

namespace Project.Utils
{
	public class EnumHelper
	{
		public readonly int FigureTypesCount;
		public readonly int ObstacleTypesCount;
		public readonly int ObstacleOrientationsCount;

		private static EnumHelper _instance;
		public static EnumHelper Instance => _instance ??= new EnumHelper();

		private EnumHelper()
		{
			FigureTypesCount = Enum.GetNames(typeof(FigureType)).Length;
			ObstacleTypesCount = Enum.GetNames(typeof(ObstacleType)).Length;
			ObstacleOrientationsCount = Enum.GetNames(typeof(ObstacleOrientation)).Length;
		}
	}
}