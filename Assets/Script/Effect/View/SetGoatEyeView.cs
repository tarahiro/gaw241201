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
    public class SetGoatEyeView : MonoBehaviour, IEffectItemView
    {
        public bool IsAutoEnd => true;


        IChangableEye _changable;

        public SetGoatEyeView Construct(IChangableEye robbable)
        {
            _changable = robbable;

            return this;
        }
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _changable.ChangeParts(EffectConst.EyeParts.Goat, EffectConst.WhichEye.Both);
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }
    }
}