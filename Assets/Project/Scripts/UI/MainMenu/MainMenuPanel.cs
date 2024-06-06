using System;
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

		public event Action StartButtonClicked;
		public event Action QuitButtonClicked;

		public void Init()
		{
			_additionalInfoPanel.Init();
		}

		private void OnEnable()
		{
			_buttonsPanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
			_buttonsPanel.StartButtonClicked += OnStartButtonClicked;
			_buttonsPanel.QuitButtonClicked += OnQuitButtonClicked;
		}

		private void OnDisable()
		{
			_buttonsPanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
			_buttonsPanel.StartButtonClicked -= OnStartButtonClicked;
			_buttonsPanel.QuitButtonClicked -= OnQuitButtonClicked;
		}

		private void OnMainMenuButtonClicked(MainMenuButton button)
		{
			_additionalInfoPanel.ChangeActivePanel(button.ManagedPanel);
		}

		private void OnStartButtonClicked()
		{
			StartButtonClicked?.Invoke();
		}

		private void OnQuitButtonClicked()
		{
			QuitButtonClicked?.Invoke();
		}
	}
}