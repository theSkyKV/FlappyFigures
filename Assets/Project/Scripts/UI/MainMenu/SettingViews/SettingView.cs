using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.MainMenu.SettingViews
{
	public class SettingView : MonoBehaviour
	{
		[SerializeField]
		private Slider _slider;

		public event Action<float> ValueChanged;

		private void OnEnable()
		{
			_slider.onValueChanged.AddListener(OnValueChanged);
		}

		private void OnDisable()
		{
			_slider.onValueChanged.RemoveListener(OnValueChanged);
		}

		private void OnValueChanged(float value)
		{
			ValueChanged?.Invoke(value);
		}

		public void UpdateValue(float value)
		{
			_slider.value = value;
		}
	}
}