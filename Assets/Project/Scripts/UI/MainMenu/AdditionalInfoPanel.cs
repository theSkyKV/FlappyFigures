using System;
using System.Collections.Generic;
using Project.Entities.Figures;
using Project.UI.MainMenu.AdditionalPanels;
using UnityEngine;

namespace Project.UI.MainMenu
{
	public class AdditionalInfoPanel : MonoBehaviour
	{
		[SerializeField]
		private MainAdditionalPanel _mainAdditionalPanel;

		[SerializeField]
		private SettingsPanel _settingsPanel;

		[SerializeField]
		private AchievementsPanel _achievementsPanel;

		[SerializeField]
		private LeaderboardPanel _leaderboardPanel;

		private List<OtherAdditionalPanel> _otherPanels;

		private AdditionalPanelBase _activePanel;

		public event Action<FigureType> OpenNowButtonClicked;

		public void Init()
		{
			_mainAdditionalPanel.Init();
		}

		private void OnEnable()
		{
			ChangeActivePanel(_mainAdditionalPanel);
			_mainAdditionalPanel.OpenNowButtonClicked += OnOpenNowButtonClicked;

			_otherPanels = new List<OtherAdditionalPanel> {_settingsPanel, _achievementsPanel, _leaderboardPanel};
			foreach (var panel in _otherPanels)
			{
				panel.CloseButtonClicked += OnCloseButtonClicked;
				panel.Deactivate();
			}
		}

		private void OnDisable()
		{
			_mainAdditionalPanel.OpenNowButtonClicked -= OnOpenNowButtonClicked;
			foreach (var panel in _otherPanels)
			{
				panel.CloseButtonClicked -= OnCloseButtonClicked;
			}
		}

		private void OnCloseButtonClicked()
		{
			ChangeActivePanel(_mainAdditionalPanel);
		}

		public void ChangeActivePanel(AdditionalPanelBase panel)
		{
			if (_activePanel == panel)
			{
				return;
			}

			if (_activePanel != null)
			{
				_activePanel.Deactivate();
			}

			_activePanel = panel;
			_activePanel.Activate();
		}

		private void OnOpenNowButtonClicked(FigureType type)
		{
			OpenNowButtonClicked?.Invoke(type);
		}

		public void UpdateFigureInfo()
		{
			_mainAdditionalPanel.UpdateInfo();
		}
	}
}