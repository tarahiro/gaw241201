using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public interface ITypingView
    {
        UniTask Enter(TypingViewArgs args);
        void EndLoop();
        IObservable<char> KeyEntered { get; }
        IObservable<Unit> Exited { get; }
    }
}