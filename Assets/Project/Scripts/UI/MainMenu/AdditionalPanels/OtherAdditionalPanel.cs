using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.MainMenu.AdditionalPanels
{
	public abstract class OtherAdditionalPanel : AdditionalPanelBase
	{
		[SerializeField]
		private Button _closeButton;

		public event Action CloseButtonClicked;

		private void OnEnable()
		{
			_closeButton.onClick.AddListener(OnCloseButtonClicked);
		}

		private void OnDisable()
		{
			_closeButton.onClick.RemoveListener(OnCloseButtonClicked);
		}

		private void OnCloseButtonClicked()
		{
			CloseButtonClicked?.Invoke();
		}
	}
}