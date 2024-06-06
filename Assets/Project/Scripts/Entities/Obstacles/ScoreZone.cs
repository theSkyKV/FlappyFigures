using System;
using Project.Entities.Figures;
using UnityEngine;

namespace Project.Entities.Obstacles
{
	public class ScoreZone : MonoBehaviour
	{
		public event Action Triggered;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.GetComponent<Figure>())
			{
				return;
			}

			Triggered?.Invoke();
		}
	}
}