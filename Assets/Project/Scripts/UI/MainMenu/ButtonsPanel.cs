using System;
using Project.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.MainMenu
{
	public class ButtonsPanel : MonoBehaviour
	{
		[SerializeField]
		private Button _startButton;

		[SerializeField]
		private MainMenuButton _settingsButton;

		[SerializeField]
		private MainMenuButton _achievementsButton;

		[SerializeField]
		private MainMenuButton _leaderboardButton;

		[SerializeField]
		private Button _quitButton;

		public event Action<MainMenuButton> MainMenuButtonClicked;
		public event Action StartButtonClicked;
		public event Action QuitButtonClicked;

		private void OnEnable()
		{
			_settingsButton.Button.onClick.AddListener(() => OnMainMenuButtonClick(_settingsButton));
			_achievementsButton.Button.onClick.AddListener(() => OnMainMenuButtonClick(_achievementsButton));
			_leaderboardButton.Button.onClick.AddListener(() => OnMainMenuButtonClick(_leaderboardButton));
			_startButton.onClick.AddListener(OnStartButtonClicked);
			_quitButton.onClick.AddListener(OnQuitButtonClicked);
		}

		private void OnDisable()
		{
			_settingsButton.Button.onClick.RemoveListener(() => OnMainMenuButtonClick(_settingsButton));
			_achievementsButton.Button.onClick.RemoveListener(() => OnMainMenuButtonClick(_achievementsButton));
			_leaderboardButton.Button.onClick.RemoveListener(() => OnMainMenuButtonClick(_leaderboardButton));
			_startButton.onClick.RemoveListener(OnStartButtonClicked);
			_quitButton.onClick.RemoveListener(OnQuitButtonClicked);
		}

		private void OnMainMenuButtonClick(MainMenuButton button)
		{
			MainMenuButtonClicked?.Invoke(button);
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