using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using VContainer.Unity;

namespace Tarahiro
{
    public class PureSingletonInput : ITickable
    {
        public void Tick()
        {
            if (IsAnyAvailableInputted)
            {
                Log.DebugLog("IsAnyAvailableInputtedをリセット");
                IsAnyAvailableInputted = false;
            }
        }

        public bool IsAnyAvailableInputted { get; private set; } = false;

        public void AvailableInputted()
        {
            Log.DebugLog("IsAnyAvailableInputtedをセット");
            IsAnyAvailableInputted = true;
        }
    }
}
