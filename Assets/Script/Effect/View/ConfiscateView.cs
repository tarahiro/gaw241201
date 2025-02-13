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
    public class ConfiscateView : MonoBehaviour, IEffectItemView
    {
        public bool IsAutoEnd => true;

        IRemovedable _removedable;
        IChangableEye _changable;

        
        public ConfiscateView Construct(IRemovedable removedable, IChangableEye robbable)
        {
            _removedable = removedable;
            _changable = robbable;

            return this;
        }

        public virtual async UniTask Enter(CancellationToken cancellationToken)
        {
            _removedable.RemoveParts();
            _changable.ChangeParts(EffectConst.EyeParts.Real, EffectConst.WhichEye.Left);
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }
    }
}