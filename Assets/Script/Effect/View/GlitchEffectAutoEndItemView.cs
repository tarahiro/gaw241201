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
    public class GlitchEffectAutoEndItemView : GlitchEffectItemView
    {
        [SerializeField] float _glitchTime = 2f;
        public override async UniTask Enter(CancellationToken cancellationToken)
        {
            await base.Enter(cancellationToken);

            await UniTask.WaitForSeconds(_glitchTime, cancellationToken: cancellationToken);
        }
    }
}