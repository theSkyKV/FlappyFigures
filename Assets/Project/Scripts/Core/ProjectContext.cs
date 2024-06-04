using Project.Services;
using UnityEngine;

namespace Project.Core
{
	public class ProjectContext : MonoBehaviour
	{
		public Service Service { get; private set; }

		public static ProjectContext Instance { get; private set; }

		public void Init()
		{
			Instance = this;
			DontDestroyOnLoad(this);

			Service = new Service();
		}
	}
}