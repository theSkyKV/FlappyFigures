namespace Project.Services.PauseSystems
{
	public interface IPauseSystem
	{
		bool IsPaused { get; }
		void Register(IPauseHandler handler);
		void UnRegister(IPauseHandler handler);
		void SetPause(bool isPaused);
	}
}