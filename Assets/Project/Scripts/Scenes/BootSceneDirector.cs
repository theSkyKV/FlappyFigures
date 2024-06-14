using Project.Core;
using Project.Services.AudioSettings;
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

		[SerializeField]
		private AudioController _audioController;

		private void Start()
		{
			_projectContext.AllDataLoaded += OnAllDataLoaded;
			_bootPanel.Init();
			_projectContext.Init();
			_audioController.Init();
		}

		private void OnDestroy()
		{
			_projectContext.AllDataLoaded -= OnAllDataLoaded;
		}

		private void OnAllDataLoaded()
		{
			SceneManager.LoadSceneAsync(1);
		}
	}
}