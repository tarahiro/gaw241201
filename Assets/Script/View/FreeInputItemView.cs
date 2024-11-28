using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public abstract class FreeInputItemView : MonoBehaviour
    {
        protected Subject<string> _exited = new Subject<string>();
        public IObservable<string> Exited => _exited;
        public virtual async UniTask Enter(CancellationToken ct)
        {
            _exited.OnNext("Fake");
        }
    }
}