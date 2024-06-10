using System;
using UnityEngine;

namespace Project.Entities.Obstacles.Spawners
{
	public class ReleaseTrigger : MonoBehaviour
	{
		public event Action<Obstacle> Triggered;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent<Obstacle>(out var obstacle))
			{
				return;
			}

			Triggered?.Invoke(obstacle);
		}
	}
}