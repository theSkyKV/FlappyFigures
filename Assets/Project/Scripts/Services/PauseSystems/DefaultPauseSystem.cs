using System.Collections;
using System.Collections.Generic;
using Project.Services.PauseSystems;
using UnityEngine;

namespace Project.Services.PauseSystems
{
    public class DefaultPauseSystem : IPauseSystem
    {
		private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();

		public bool IsPaused { get; private set; }

		public void Register(IPauseHandler handler)
		{
			_handlers.Add(handler);
		}

		public void UnRegister(IPauseHandler handler)
		{
			_handlers.Remove(handler);
		}

		public void SetPause(bool isPaused)
		{
			IsPaused = isPaused;
			foreach (var handler in _handlers)
			{
				handler.SetPause(isPaused);
			}
		}
    }
}
