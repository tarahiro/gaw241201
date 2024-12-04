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
    public class EffectArgs
    {

        public EffectConst.Key Key { get; private set; }
        public CancellationToken CancellationToken { get; private set; }

        public EffectArgs(EffectConst.Key key, CancellationToken cancellationToken)
        {
            Key = key;
            CancellationToken = cancellationToken;
        }
    }
}