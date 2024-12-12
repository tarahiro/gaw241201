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
    public interface ISingleTextSequenceEnterable<T> where T : IIdentifiable, IIndexable, IGroupable
    {
        void EnterTextSequence( T master, CancellationToken ct, out bool isEnded);

        IObservable<ModelArgs<T>> Entered { get; }
    }
}