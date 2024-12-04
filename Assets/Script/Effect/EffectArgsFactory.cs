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
    public class EffectArgsFactory
    {
        public EffectArgs Create(string key, CancellationToken cancellationToken)
        {
            return new EffectArgs(EnumUtil.KeyToType<EffectConst.Key>(key), cancellationToken);
        }
    }
}