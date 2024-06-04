using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Boot
{
	public class BootPanel : MonoBehaviour
	{
		[SerializeField]
		private Image _loadingImage;

		public void UpdateLoadingValue(float value)
		{
			_loadingImage.fillAmount = value;
		}
	}
}