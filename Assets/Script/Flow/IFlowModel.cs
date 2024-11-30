using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public interface IFlowModel
    {
       UniTask EnterFlow(string bodyId);
#if ENABLE_DEBUG
        void ForceEndFlow();
        string ForceGetCategory { get; }
#endif
    }
}