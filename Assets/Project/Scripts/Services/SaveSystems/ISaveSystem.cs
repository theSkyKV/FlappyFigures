namespace Project.Services.SaveSystems
{
	public interface ISaveSystem
	{
		void Save(SaveData data);
		SaveData Load();
	}
}