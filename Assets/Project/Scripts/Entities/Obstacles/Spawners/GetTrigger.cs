using System;
using UnityEngine;

namespace Project.Entities.Obstacles.Spawners
{
	public class GetTrigger : MonoBehaviour
	{
		public event Action Triggered;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.GetComponent<Obstacle>())
			{
				return;
			}

			Triggered?.Invoke();
		}
	}
}