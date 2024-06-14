using System;
using Project.Core;
using Project.Entities.Figures;
using Project.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.MainMenu
{
	public class MainMenuPanel : MonoBehaviour
	{
		[SerializeField]
		private ButtonsPanel _buttonsPanel;

		[SerializeField]
		private AdditionalInfoPanel _additionalInfoPanel;

		[SerializeField]
		private Button _resetDataButton;

		public event Action StartButtonClicked;
		public event Action QuitButtonClicked;
		public event Action<FigureType> OpenNowButtonClicked;

		public void Init()
		{
			_additionalInfoPanel.Init();
		}

		private void OnEnable()
		{
			_additionalInfoPanel.OpenNowButtonClicked += OnOpenNowButtonClicked;
			_buttonsPanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
			_buttonsPanel.StartButtonClicked += OnStartButtonClicked;
			_buttonsPanel.QuitButtonClicked += OnQuitButtonClicked;
			_resetDataButton.onClick.AddListener(OnResetDataButtonClicked);
		}

		private void OnDisable()
		{
			_additionalInfoPanel.OpenNowButtonClicked -= OnOpenNowButtonClicked;
			_buttonsPanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
			_buttonsPanel.StartButtonClicked -= OnStartButtonClicked;
			_buttonsPanel.QuitButtonClicked -= OnQuitButtonClicked;
			_resetDataButton.onClick.RemoveListener(OnResetDataButtonClicked);
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

		private void OnResetDataButtonClicked()
		{
			ProjectContext.Instance.Service.SaveSystem.Reset();
			//ProjectContext.Instance.LoadData();
		}

		private void OnOpenNowButtonClicked(FigureType type)
		{
			OpenNowButtonClicked?.Invoke(type);
		}

		public void UpdateFigureInfo()
		{
			_additionalInfoPanel.UpdateFigureInfo();
		}
	}
}