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
    public interface IFlowSwitchable_Fake
    {
        public IObservable<FlowSwitchArgs_Fake> SwitchFlow { get; }
    }
}