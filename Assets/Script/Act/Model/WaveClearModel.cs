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
    public class WaveClearModel
    {
        Subject<Unit> _waveCleared = new Subject<Unit>();
        public IObservable<Unit> WaveCleared => _waveCleared;

        public void ClearWave()
        {
            _waveCleared.OnNext(Unit.Default);
        }
    }
}