using System.Threading.Tasks;
using Project.Core;
using Project.UI.Boot;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scenes
{
	public class BootSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private ProjectContext _projectContext;

		[SerializeField]
		private BootPanel _bootPanel;

		private async void Awake()
		{
			_bootPanel.Init();
			await Task.Delay(500);
			Load();
			await Task.Delay(500);
			SceneManager.LoadSceneAsync(1);
		}

		private void Load()
		{
			_projectContext.Init();
		}
	}
}