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
    public interface ITimerModel
    {
        UniTask Enter(float maxTime);

        void EndTimer();

        IObservable<TimerArgs> Entered { get; }
        IObservable<float> Updated { get; }
        IObservable<Unit> TimeUped { get; }

        IObservable<float> TimeRemained { get; }
    }
}