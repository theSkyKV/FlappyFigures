using Project.Core;
using Project.Services.PauseSystems;
using UnityEngine;

namespace Project.Entities.Figures
{
	public class Figure : MonoBehaviour, IPauseHandler
	{
		private Rigidbody2D _rigidbody;
		private Collider2D _collider;

		private IPauseSystem PauseSystem => ProjectContext.Instance.Service.PauseSystem;

		public void Init(FigureInfo info)
		{
			PauseSystem.Register(this);
			_rigidbody = gameObject.AddComponent<Rigidbody2D>();
			Deactivate();
			_rigidbody.mass = info.Mass;
			_rigidbody.gravityScale = info.GravityScale;
			AddCollider(info.Type);
			AddIcon(info);
		}

		private void OnDestroy()
		{
			PauseSystem.UnRegister(this);
		}

		public void Activate()
		{
			_rigidbody.simulated = true;
		}

		public void Deactivate()
		{
			_rigidbody.simulated = false;
		}

		public void AddForce(float force)
		{
			_rigidbody.AddForce(Vector2.up * force * _rigidbody.gravityScale, ForceMode2D.Impulse);
		}

		private void AddCollider(FigureType type)
		{
			switch (type)
			{
				case FigureType.Square:
					_collider = gameObject.AddComponent<BoxCollider2D>();
					break;
				case FigureType.Triangle:
					var col = gameObject.AddComponent<PolygonCollider2D>();
					_collider = col;
					col.points = new[]
					{
						new Vector2(0, 0.5773587f),
						new Vector2(-0.5f, -0.2886667f),
						new Vector2(0.5f, -0.2886667f)
					};
					break;
				case FigureType.Circle:
					_collider = gameObject.AddComponent<CircleCollider2D>();
					break;
			}
		}

		private void AddIcon(FigureInfo info)
		{
			var sr = new GameObject().AddComponent<SpriteRenderer>();
			sr.transform.SetParent(transform);
			sr.sprite = info.Sprite;
			Vector3 offset;
			switch (info.Type)
			{
				case FigureType.Triangle:
					offset = new Vector3(0, 0.22f, 0);
					break;
				case FigureType.Square:
				case FigureType.Circle:
					offset = Vector3.zero;
					break;
				default:
					offset = Vector3.zero;
					break;
			}

			sr.transform.position = offset;
		}

		public void SetPause(bool isPaused)
		{
			if (isPaused)
			{
				Deactivate();
			}
			else
			{
				Activate();
			}
		}

		public void SetImmortal(bool isImmortal)
		{
			_collider.enabled = !isImmortal;
		}

		public void ResetFigure()
		{
			_rigidbody.velocity = Vector2.zero;
		}
	}
}