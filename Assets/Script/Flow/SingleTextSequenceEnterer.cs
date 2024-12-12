using Cysharp.Threading.Tasks;
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
    public class SingleTextSequenceEnterer<T> : ISingleTextSequenceEnterable<T> where T : IIdentifiable, IIndexable, IGroupable
    {
        [Inject] ModelArgsFactory<T> _modelArgsFactory;
        Subject<ModelArgs<T>> _entered = new Subject<ModelArgs<T>>();
        public IObservable<ModelArgs<T>> Entered => _entered;

        public void EnterTextSequence(T master, CancellationToken ct, out bool isEnded)
        {
            Log.Comment(master.Id + "ŠJŽn");
            isEnded = false;
            _entered.OnNext(_modelArgsFactory.Create(master, ct));
        }
    }
}