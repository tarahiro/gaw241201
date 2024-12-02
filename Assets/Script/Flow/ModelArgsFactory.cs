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
    public class ModelArgsFactory<T> where T : IIndexable, IIdentifiable, IGroupable
    {
        public ModelArgs<T> Create(T master, CancellationToken cancellationToken)
        {
            return new ModelArgs<T>(master, cancellationToken);
        }
    }
}