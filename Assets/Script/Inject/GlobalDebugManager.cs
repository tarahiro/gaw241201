using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class GlobalDebugManager : ITickable, IStartable
    {
        [Inject] FlowHundler _flowHundler;
        [Inject] ISaveDeletable _saveDeletable;

        public void Start()
        {

        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _flowHundler.ForceGetCurrentFlow().ForceEndFlow();
            }


            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _saveDeletable.DeleteSaveData();
            }
        }
    }
#endif
}