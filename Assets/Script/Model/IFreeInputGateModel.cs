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
    public interface IFreeInputGateModel : IEnterTimingNotifiable
    {
        public void Enter();
        public void Decide(string value);
    }
}