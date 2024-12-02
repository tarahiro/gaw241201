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
    public class ModelArgs<T> where T : IIndexable,IIdentifiable, IGroupable
    {

        public T Master { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public ModelArgs(T master, CancellationToken cancellationToken)
        {
            Master = master;
            CancellationToken = cancellationToken;
        }
    }
}