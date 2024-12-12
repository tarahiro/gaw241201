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
    //名前が適切でないかも。PointableよりCorrectableとか、正解の入力をしたときに何かがおこることが分かる名前にしたい
    public interface IPointableView
    {
        IObservable<Unit> Pointed { get; }
    }
}