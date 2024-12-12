using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using gaw241201;

namespace gaw241201.View
{
    public interface ITimerView
    {
        UniTask Enter(TimerArgs timerArgs);

        IObservable<Unit> TimeUped { get; }
    }
}