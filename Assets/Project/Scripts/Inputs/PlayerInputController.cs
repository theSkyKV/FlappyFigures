using System;
using Project.Core;
using Project.Entities.Figures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Inputs
{
	public class PlayerInputController : MonoBehaviour
	{
		private PlayerInput _playerInput;
		private Figure _figure;

		private float _verticalForce;

		public event Action FirstTimeClicked;

		public void Init(Figure figure)
		{
			var settings = ProjectContext.Instance.GameSettings;
			_playerInput = new PlayerInput();
			_figure = figure;
			_verticalForce = settings.VerticalForce;
			_playerInput.Figure.AddForce.performed += OnFirstTimeClicked;
			_playerInput.Enable();
		}

		private void OnDestroy()
		{
			_playerInput.Disable();
			_playerInput.Figure.AddForce.performed -= OnAddedForce;
		}

		private void OnAddedForce(InputAction.CallbackContext ctx)
		{
			_figure.AddForce(_verticalForce);
		}

		private void OnFirstTimeClicked(InputAction.CallbackContext ctx)
		{
			OnAddedForce(ctx);
			FirstTimeClicked?.Invoke();
			_playerInput.Figure.AddForce.performed += OnAddedForce;
			_playerInput.Figure.AddForce.performed -= OnFirstTimeClicked;
		}
	}
}