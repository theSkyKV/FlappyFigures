using Project.UI.MainMenu.AdditionalPanels;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Buttons
{
	public class MainMenuButton : MonoBehaviour
	{
		[SerializeField]
		private AdditionalPanelBase _managedPanel;

		[SerializeField]
		private Button _button;

		public AdditionalPanelBase ManagedPanel => _managedPanel;
		public Button Button => _button;
	}
}