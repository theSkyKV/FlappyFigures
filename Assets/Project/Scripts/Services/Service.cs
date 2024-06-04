using Project.Services.Paths;
using Project.Services.SaveSystems;

namespace Project.Services
{
	public class Service
	{
		public readonly Path Path;
		public readonly ISaveSystem SaveSystem;

		public Service()
		{
			Path = new Path();
			SaveSystem = new JsonSaveSystem(Path.JsonSaveData);
		}
	}
}