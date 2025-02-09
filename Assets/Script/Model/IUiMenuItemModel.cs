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
    public interface IUiMenuItemModel
    {
        public IObservable<Unit> Entered { get; }
        bool IsEnterable { get;}

        UniTask Enter();

        void End();
    }
}