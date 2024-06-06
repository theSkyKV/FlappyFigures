using System;
using Project.Utils;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Project.Entities.Obstacles.Spawners
{
	public class ObstacleSpawner : MonoBehaviour
	{
		[SerializeField]
		private Obstacle _obstaclePrefab;

		[SerializeField]
		private GetTrigger _getTrigger;

		[SerializeField]
		private ReleaseTrigger _releaseTrigger;

		[SerializeField]
		private Transform _spawnPoint;

		private ObjectPool<Obstacle> _pool;
		private const int MaxPoolSize = 15;
		private const int DefaultCapacity = 15;

		public event Action ScoreZoneTriggered;
		public event Action DeadZoneTriggered;
		public event Action<Obstacle> ObstacleCreated;

		public void Init()
		{
			_pool = new ObjectPool<Obstacle>(CreateObstacle, OnGetFromPool, OnReleaseToPool,
				OnDestroyPooledObject, true, DefaultCapacity, MaxPoolSize);

			_getTrigger.Triggered += Spawn;
			_releaseTrigger.Triggered += Release;
		}

		private void OnDestroy()
		{
			_getTrigger.Triggered -= Spawn;
			_releaseTrigger.Triggered -= Release;
		}

		public void Spawn()
		{
			_pool.Get();
		}

		public void Release(Obstacle obstacle)
		{
			_pool.Release(obstacle);
		}

		private Obstacle CreateObstacle()
		{
			var obstacle = Instantiate(_obstaclePrefab);
			obstacle.Init();
			obstacle.ScoreZoneTriggered += OnScoreZoneTriggered;
			obstacle.DeadZoneTriggered += OnDeadZoneTriggered;
			ObstacleCreated?.Invoke(obstacle);
			Deactivate(obstacle);

			return obstacle;
		}

		private void OnGetFromPool(Obstacle obstacle)
		{
			obstacle.transform.position = _spawnPoint.transform.position;
			var obstacleType = (ObstacleType) Random.Range(0, EnumHelper.Instance.ObstacleTypesCount);
			switch (obstacleType)
			{
				case ObstacleType.Single:
					obstacle.ToSingle();
					break;
				case ObstacleType.Double:
					obstacle.ToDouble();
					break;
				default:
					obstacle.ToSingle();
					break;
			}

			obstacle.gameObject.SetActive(true);
		}

		private void OnReleaseToPool(Obstacle obstacle)
		{
			Deactivate(obstacle);
		}

		private void OnDestroyPooledObject(Obstacle obstacle)
		{
			obstacle.ScoreZoneTriggered -= OnScoreZoneTriggered;
			obstacle.DeadZoneTriggered -= OnDeadZoneTriggered;
			Destroy(obstacle.gameObject);
		}

		private void Deactivate(Obstacle obstacle)
		{
			obstacle.Deactivate();
			obstacle.gameObject.SetActive(false);
		}

		private void OnScoreZoneTriggered()
		{
			ScoreZoneTriggered?.Invoke();
		}

		private void OnDeadZoneTriggered()
		{
			DeadZoneTriggered?.Invoke();
		}
	}
}