using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Level
{
	public class GameOverPanel : MonoBehaviour
	{
		[SerializeField]
		private Button _restartButton;

		[SerializeField]
		private Button _mainMenuButton;

		[SerializeField]
		private Button _resurrectButton;

		public event Action RestartButtonClicked;
		public event Action MainMenuButtonClicked;
		public event Action ResurrectButtonClicked;

		private void OnEnable()
		{
			_restartButton.onClick.AddListener(OnRestartButtonClicked);
			_mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
			_resurrectButton.onClick.AddListener(OnResurrectButtonClicked);
		}

		private void OnDisable()
		{
			_restartButton.onClick.RemoveListener(OnRestartButtonClicked);
			_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
			_resurrectButton.onClick.RemoveListener(OnResurrectButtonClicked);
		}

		private void OnRestartButtonClicked()
		{
			RestartButtonClicked?.Invoke();
		}

		private void OnMainMenuButtonClicked()
		{
			MainMenuButtonClicked?.Invoke();
		}

		private void OnResurrectButtonClicked()
		{
			ResurrectButtonClicked?.Invoke();
		}

		public void ShowResurrectButton()
		{
			_resurrectButton.gameObject.SetActive(true);
		}

		public void HideResurrectButton()
		{
			_resurrectButton.gameObject.SetActive(false);
		}
	}
}