using System.Collections.Generic;
using Project.Entities.Obstacles;
using Project.Entities.Obstacles.Spawners;
using UnityEngine;

namespace Project.Scenes
{
	public class LevelSceneDirector : MonoBehaviour
	{
		[SerializeField]
		private ObstacleSpawner _obstacleSpawner;

		private List<Obstacle> _obstacles;

		private float _gameSpeed = 2.5f;
		private const float TerminalGameSpeed = 4.5f;
		private const float GameSpeedScaler = 120f;

		private void Awake()
		{
			_obstacles = new List<Obstacle>();
			_obstacleSpawner.Init();
		}

		private void OnEnable()
		{
			_obstacleSpawner.ObstacleCreated += OnObstacleCreated;
		}

		private void OnDisable()
		{
			_obstacleSpawner.ObstacleCreated -= OnObstacleCreated;
		}

		private void Start()
		{
			_obstacleSpawner.Spawn();
		}

		private void Update()
		{
			IncreaseGameSpeed();

			foreach (var obstacle in _obstacles)
			{
				obstacle.transform.Translate(Vector2.left * (Time.deltaTime * _gameSpeed));
			}
		}

		private void IncreaseGameSpeed()
		{
			_gameSpeed += Time.deltaTime / GameSpeedScaler;
			_gameSpeed = _gameSpeed > TerminalGameSpeed ? TerminalGameSpeed : _gameSpeed;
		}

		private void OnObstacleCreated(Obstacle obstacle)
		{
			_obstacles.Add(obstacle);
		}
	}
}