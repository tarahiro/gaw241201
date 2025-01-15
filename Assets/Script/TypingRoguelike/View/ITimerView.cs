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
        void EnterTimer(TimerArgs args);
        void UpdateTimer(float ratio);

        void EndTimer();
    }
}