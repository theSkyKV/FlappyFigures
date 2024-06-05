using Project.UI.Buttons;
using UnityEngine;

namespace Project.UI.MainMenu
{
	public class MainMenuPanel : MonoBehaviour
	{
		[SerializeField]
		private ButtonsPanel _buttonsPanel;

		[SerializeField]
		private AdditionalInfoPanel _additionalInfoPanel;

		public void Init()
		{
			_additionalInfoPanel.Init();
		}

		private void OnEnable()
		{
			_buttonsPanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
		}

		private void OnDisable()
		{
			_buttonsPanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
		}

		private void OnMainMenuButtonClicked(MainMenuButton button)
		{
			_additionalInfoPanel.ChangeActivePanel(button.ManagedPanel);
		}
	}
}