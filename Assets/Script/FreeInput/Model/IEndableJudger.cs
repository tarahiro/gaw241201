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
    public interface IEndableJudger
    {
        public bool IsEnterable();

        public void CatchUpdateFocus(int index);

        IObservable<bool> EnterableStateUpdated { get; }
    }
}