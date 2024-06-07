namespace Project.Config.Entities
{
	public class GameSettings
	{
		public float BaseGameSpeed { get; set; }
		public float TerminalGameSpeed { get; set; }
		public float GameSpeedScaler { get; set; }
		public float ImmortalTimeAfterResurrection { get; set; }
		public float VerticalForce { get; set; }
		public ObstacleSettings ObstacleSettings { get; set; }
	}
}