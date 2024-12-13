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
    public class TypingRoguelikeSingleSequenceStarter<T> : ISingleTextSequenceEnterable<T>, ITimerStartableModel, ILeetMasterGettable where T : IIdentifiable, IIndexable, IGroupable
    {
        [Inject] ModelArgsFactory<T> _modelArgsFactory;
        [Inject] AvaliableLeetMasterDataProvider _leetMasterDataProvider;

        Subject<ModelArgs<T>> _entered = new Subject<ModelArgs<T>>();
        Subject<TimerArgs> _timerStarted = new Subject<TimerArgs>();
        Subject<List<ILeetMaster>> _leetMasterGetted = new Subject<List<ILeetMaster>>();
        public IObservable<ModelArgs<T>> Entered => _entered;
        public IObservable<TimerArgs> TimerStarted => _timerStarted;
        public IObservable<List<ILeetMaster>> LeetMasterGetted => _leetMasterGetted;

        public void EnterTextSequence(T master, CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "ŠJŽn");
            isEnded = false;
            _timerStarted.OnNext(new TimerArgs(20f, ct));
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
            _leetMasterGetted.OnNext(_leetMasterDataProvider.GetAvailableLeetMasterDataList());
        }
    }
}