using System;
using Project.Core;
using Project.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Entities.Obstacles
{
	public class Obstacle : MonoBehaviour
	{
		[SerializeField]
		private DeadZone _deadZoneTop;

		[SerializeField]
		private DeadZone _deadZoneBottom;

		[SerializeField]
		private ScoreZone _scoreZone;

		private float _relativeDistanceBetweenDeadZonesMin;
		private float _relativeDistanceBetweenDeadZonesMax;
		private float _relativePosition;
		private float _cameraHeightHalf;

		public event Action ScoreZoneTriggered;
		public event Action DeadZoneTriggered;

		public void Init()
		{
			var mainCamera = Camera.main;
			_cameraHeightHalf = mainCamera != null ? mainCamera.orthographicSize : 0;

			var settings = ProjectContext.Instance.GameSettings.ObstacleSettings;
			_relativeDistanceBetweenDeadZonesMin = settings.RelativeDistanceBetweenDeadZonesMin;
			_relativeDistanceBetweenDeadZonesMax = settings.RelativeDistanceBetweenDeadZonesMax;
			_relativePosition = settings.RelativePosition;

			_scoreZone.Triggered += OnScoreZoneTriggered;
			_deadZoneBottom.Triggered += OnDeadZoneTriggered;
			_deadZoneTop.Triggered += OnDeadZoneTriggered;
		}

		private void OnDestroy()
		{
			_scoreZone.Triggered -= OnScoreZoneTriggered;
			_deadZoneBottom.Triggered -= OnDeadZoneTriggered;
			_deadZoneTop.Triggered -= OnDeadZoneTriggered;
		}

		private void OnScoreZoneTriggered()
		{
			ScoreZoneTriggered?.Invoke();
		}

		private void OnDeadZoneTriggered()
		{
			DeadZoneTriggered?.Invoke();
		}

		public void ToSingle()
		{
			var obstacleOrientation =
				(ObstacleOrientation) Random.Range(0, EnumHelper.Instance.ObstacleOrientationsCount);
			DeadZone deadZone;
			int orientationMultiplier;
			switch (obstacleOrientation)
			{
				case ObstacleOrientation.Top:
					deadZone = _deadZoneTop;
					orientationMultiplier = 1;
					break;
				case ObstacleOrientation.Bottom:
					deadZone = _deadZoneBottom;
					orientationMultiplier = -1;
					break;
				default:
					return;
			}

			var position = GetRandomPosition();
			deadZone.transform.position = new Vector3(position.x, position.y + orientationMultiplier
				* _cameraHeightHalf);
			deadZone.gameObject.SetActive(true);
		}

		public void ToDouble()
		{
			var topTransform = _deadZoneTop.transform;
			var bottomTransform = _deadZoneBottom.transform;
			var position = GetRandomPosition();
			var offset = Random.Range(
				_cameraHeightHalf * _relativeDistanceBetweenDeadZonesMin,
				_cameraHeightHalf * _relativeDistanceBetweenDeadZonesMax);
			topTransform.position =
				new Vector3(position.x, position.y + _cameraHeightHalf + offset);
			bottomTransform.position =
				new Vector3(position.x, position.y - _cameraHeightHalf - offset);

			_deadZoneTop.gameObject.SetActive(true);
			_deadZoneBottom.gameObject.SetActive(true);
		}

		private Vector3 GetRandomPosition()
		{
			var offset = Random.Range(-_cameraHeightHalf * _relativePosition,
				_cameraHeightHalf * _relativePosition);
			var position = transform.position;
			position = new Vector3(position.x, position.y + offset);

			return position;
		}

		public void Deactivate()
		{
			_deadZoneTop.gameObject.SetActive(false);
			_deadZoneBottom.gameObject.SetActive(false);
		}
	}
}