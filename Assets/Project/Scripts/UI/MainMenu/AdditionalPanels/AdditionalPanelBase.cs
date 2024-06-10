using UnityEngine;

namespace Project.UI.MainMenu.AdditionalPanels
{
	public abstract class AdditionalPanelBase : MonoBehaviour
	{
		public virtual void Activate()
		{
			gameObject.SetActive(true);
		}

		public virtual void Deactivate()
		{
			gameObject.SetActive(false);
		}
	}
}