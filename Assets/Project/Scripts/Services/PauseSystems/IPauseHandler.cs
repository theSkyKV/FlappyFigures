using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Services.PauseSystems
{
    public interface IPauseHandler
    {
		void SetPause(bool isPaused);
    }
}
