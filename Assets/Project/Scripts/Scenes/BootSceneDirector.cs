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

		private void Awake()
		{
			_bootPanel.UpdateLoadingValue(0);
			Load();
			_bootPanel.UpdateLoadingValue(0.5f);
			SceneManager.LoadScene(1);
			_bootPanel.UpdateLoadingValue(1);
		}

		private void Load()
		{
			_projectContext.Init();
		}
	}
}