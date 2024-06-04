using UnityEngine;

namespace Project.UI.Boot
{
	public class BootSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private BootPanel _bootPanel;

		private void Awake()
		{
			_bootPanel.UpdateLoadingValue(0);
			Load();
			_bootPanel.UpdateLoadingValue(1);
		}

		private void Load()
		{ }
	}
}