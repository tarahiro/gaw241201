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
    public interface IPointable
    {
        void AddUnitPoint();
        void ReducePenaltyPoint();
        void AddRemainTimePoint(float remainTime);
        IObservable<int> PointUpdated { get; }
    }
}