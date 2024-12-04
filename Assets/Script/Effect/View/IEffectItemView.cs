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

namespace gaw241201.View
{
    public interface IEffectItemView : ITransform
    {
        bool IsAutoEnd { get; }
        UniTask Enter(CancellationToken cancellationToken);
        UniTask End(CancellationToken cancellationToken);
    }
}