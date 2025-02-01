using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TypingRoguelikeSingleSequenceStarter: ISingleTextSequenceEnterable<ITypingRoguelikeSingleSequenceMaster>
    {
        [Inject] ModelArgsFactory<ITypingRoguelikeSingleSequenceMaster> _modelArgsFactory;
        [Inject] ITypingStarter _typingInitializer;
        [Inject] ITimerStartableModel _timerStarter;

        Subject<ModelArgs<ITypingRoguelikeSingleSequenceMaster>> _entered = new Subject<ModelArgs<ITypingRoguelikeSingleSequenceMaster>>();
        public IObservable<ModelArgs<ITypingRoguelikeSingleSequenceMaster>> Entered => _entered;
     
        public void EnterTextSequence(ITypingRoguelikeSingleSequenceMaster master,  CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "ŠJŽn");
            isEnded = false;

            _typingInitializer.StartTyping(master, master.ConditionProvider);
            if (master.ConditionProvider.IsEnableTimeUp())
            {
                _timerStarter.StartTimer(master);
            }
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
        }
    }
}