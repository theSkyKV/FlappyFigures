using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Level
{
	public class LevelPanel : MonoBehaviour
	{
		[SerializeField]
		private PausePanel _pausePanel;

		[SerializeField]
		private GameOverPanel _gameOverPanel;

		[SerializeField]
		private TMP_Text _score;

		[SerializeField]
		private Button _pauseButton;

		[SerializeField]
		private TMP_Text _infoText;

		public event Action ContinueButtonClicked;
		public event Action MainMenuButtonClicked;
		public event Action PauseButtonClicked;
		public event Action RestartButtonClicked;
		public event Action ResurrectButtonClicked;

		private void OnEnable()
		{
			_pausePanel.ContinueButtonClicked += OnContinueButtonClicked;
			_pausePanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
			_pauseButton.onClick.AddListener(OnPauseButtonClicked);
			_gameOverPanel.RestartButtonClicked += OnRestartButtonClicked;
			_gameOverPanel.MainMenuButtonClicked += OnMainMenuButtonClicked;
			_gameOverPanel.ResurrectButtonClicked += OnResurrectButtonClicked;
			_infoText.gameObject.SetActive(true);
			DeactivatePause();
			DeactivateGameOver();
		}

		private void OnDisable()
		{
			_pausePanel.ContinueButtonClicked -= OnContinueButtonClicked;
			_pausePanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
			_pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
			_gameOverPanel.RestartButtonClicked -= OnRestartButtonClicked;
			_gameOverPanel.MainMenuButtonClicked -= OnMainMenuButtonClicked;
			_gameOverPanel.ResurrectButtonClicked -= OnResurrectButtonClicked;
		}

		private void OnContinueButtonClicked()
		{
			ContinueButtonClicked?.Invoke();
		}

		private void OnMainMenuButtonClicked()
		{
			MainMenuButtonClicked?.Invoke();
		}

		private void OnPauseButtonClicked()
		{
			PauseButtonClicked?.Invoke();
		}

		private void OnRestartButtonClicked()
		{
			RestartButtonClicked?.Invoke();
		}

		private void OnResurrectButtonClicked()
		{
			ResurrectButtonClicked?.Invoke();
		}

		public void UpdateScore(int value)
		{
			_score.text = value.ToString();
		}

		public void ActivatePause()
		{
			_pausePanel.gameObject.SetActive(true);
			_pauseButton.gameObject.SetActive(false);
		}

		public void DeactivatePause()
		{
			_pausePanel.gameObject.SetActive(false);
			_pauseButton.gameObject.SetActive(true);
		}

		public void ActivateGameOver(bool canResurrect)
		{
			_gameOverPanel.gameObject.SetActive(true);
			if (canResurrect)
			{
				_gameOverPanel.ShowResurrectButton();
			}
			else
			{
				_gameOverPanel.HideResurrectButton();
			}

			_pauseButton.gameObject.SetActive(false);
		}

		public void DeactivateGameOver()
		{
			_gameOverPanel.gameObject.SetActive(false);
			_pauseButton.gameObject.SetActive(true);
		}

		public void HideInfo()
		{
			StartCoroutine(Hide());
		}

		private IEnumerator Hide()
		{
			var color = _infoText.color;
			var waiter = new WaitForEndOfFrame();
			while (color.a > 0)
			{
				color.a -= Time.deltaTime / 3;
				_infoText.color = color;
				yield return waiter;
			}

			_infoText.gameObject.SetActive(false);
		}
	}
}