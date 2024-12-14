using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class TypingRoguelikeSingleSequenceStarter: ISingleTextSequenceEnterable<ITypingMaster>, ITimerStartableModel
    {
        [Inject] ModelArgsFactory<ITypingMaster> _modelArgsFactory;
        [Inject] ITypingInitializer _questionInitializer;

        Subject<ModelArgs<ITypingMaster>> _entered = new Subject<ModelArgs<ITypingMaster>>();
        Subject<TimerArgs> _timerStarted = new Subject<TimerArgs>();
        public IObservable<ModelArgs<ITypingMaster>> Entered => _entered;
        public IObservable<TimerArgs> TimerStarted => _timerStarted;

        public void EnterTextSequence(ITypingMaster master, CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "ŠJŽn");
            isEnded = false;

            _questionInitializer.InitializeQuestion(master);
            _timerStarted.OnNext(new TimerArgs(20f, ct));
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
        }
    }
}