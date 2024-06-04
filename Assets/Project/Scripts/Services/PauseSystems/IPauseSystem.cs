using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Services.PauseSystems
{
    public interface IPauseSystem
    {
		bool IsPaused { get; }
		void Register(IPauseHandler handler);
		void UnRegister(IPauseHandler handler);
		void SetPause(bool isPaused);
    }
}
