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
using static gaw241201.View.ViewConst;

namespace gaw241201.View
{
    public class WaitEffectItemView :MonoBehaviour, IEffectItemView
    {
        // const float c_waitTime = .5f
        const float c_waitTime = 5f;
        public bool IsAutoEnd => true;
        public virtual async UniTask Enter(CancellationToken cancellationToken)
        {
            Log.Comment("WaitŠJŽn");
            await UniTask.WaitForSeconds(c_waitTime, cancellationToken: cancellationToken);
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }
    }
}