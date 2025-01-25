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
    public class LoopInitializer : ILoopInitializer
    {
        Subject<Unit> _loopInitialized = new Subject<Unit>();
        public IObservable<Unit> LoopInitialized => _loopInitialized;
        public void InitializeLoop()
        {
            Log.Comment("InitializeLoop");

            SoundManager.PlayBGM("Main");
            _loopInitialized.OnNext(Unit.Default);
        }
    }
}