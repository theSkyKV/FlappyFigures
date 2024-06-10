using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Level
{
	public class PausePanel : MonoBehaviour
	{
		[SerializeField]
		private Button _continueButton;

		[SerializeField]
		private Button _mainMenuButton;

		public event Action ContinueButtonClicked;
		public event Action MainMenuButtonClicked;

		private void OnEnable()
		{
			_continueButton.onClick.AddListener(OnContinueButtonClicked);
			_mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
		}

		private void OnDisable()
		{
			_continueButton.onClick.RemoveListener(OnContinueButtonClicked);
			_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
		}

		private void OnContinueButtonClicked()
		{
			ContinueButtonClicked?.Invoke();
		}

		private void OnMainMenuButtonClicked()
		{
			MainMenuButtonClicked?.Invoke();
		}
	}
}