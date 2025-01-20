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
        Subject<float> _timerStarted = new Subject<float>();
        public IObservable<ModelArgs<ITypingRoguelikeSingleSequenceMaster>> Entered => _entered;
        public IObservable<float> TimerStarted => _timerStarted;

        public void EnterTextSequence(ITypingRoguelikeSingleSequenceMaster master,  CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "開始");
            isEnded = false;

            _typingInitializer.InitializeTyping(master, master.ConditionProvider);
            if (master.ConditionProvider.IsEnableTimeUp())
            {
                Log.Comment("タイマー開始");
                _timerStarted.OnNext(TypingUtil.RemoveBracketsAndContents(master.QuestionText).Length * master.Time);
            }
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
        }
    }
}