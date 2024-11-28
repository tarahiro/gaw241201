using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class DebugManager : ITickable
    {
        [Inject] FlowHundler _flowHundler;

        string _initialCategory;

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _initialCategory = _flowHundler.ForceGetCurrentFlow().ForceGetCategory;
            }

            if (Input.GetKey(KeyCode.Tab))
            {
                if(_flowHundler.ForceGetCurrentFlow().ForceGetCategory == _initialCategory)
                {
                    _flowHundler.ForceGetCurrentFlow().ForceEndFlow();
                }
            }
        }
    }
#endif
}