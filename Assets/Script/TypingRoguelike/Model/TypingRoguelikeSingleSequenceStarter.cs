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
    public class TypingRoguelikeSingleSequenceStarter: ISingleTextSequenceEnterable<ITypingRoguelikeSingleSequenceMaster>, ITimerStartableModel
    {
        [Inject] ModelArgsFactory<ITypingRoguelikeSingleSequenceMaster> _modelArgsFactory;
        [Inject] ITypingInitializer _typingInitializer;

        Subject<ModelArgs<ITypingRoguelikeSingleSequenceMaster>> _entered = new Subject<ModelArgs<ITypingRoguelikeSingleSequenceMaster>>();
        Subject<TimerArgs> _timerStarted = new Subject<TimerArgs>();
        public IObservable<ModelArgs<ITypingRoguelikeSingleSequenceMaster>> Entered => _entered;
        public IObservable<TimerArgs> TimerStarted => _timerStarted;

        public void EnterTextSequence(ITypingRoguelikeSingleSequenceMaster master, CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "�J�n");
            isEnded = false;

            _typingInitializer.InitializeTyping(master);
            _timerStarted.OnNext(new TimerArgs(TypingUtil.RemoveBracketsAndContents(master.RomanText).Length * master.Time, ct));
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
        }
    }
}