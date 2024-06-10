using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Boot
{
	public class BootPanel : MonoBehaviour
	{
		[SerializeField]
		private Image _loadingImage;

		private Vector3 _rotation;
		private const float RotationSpeed = -90;

		public void Init()
		{
			_rotation = _loadingImage.transform.eulerAngles;
			_rotation.z = RotationSpeed;
			StartCoroutine(Animate());
		}

		private IEnumerator Animate()
		{
			var waiter = new WaitForEndOfFrame();

			while (true)
			{
				_loadingImage.transform.Rotate(_rotation * Time.deltaTime);
				yield return waiter;
			}
		}
	}
}