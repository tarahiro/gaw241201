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
    public class ChangeEyeView : MonoBehaviour, IEffectItemView
    {
        public bool IsAutoEnd => true;

        IChangableEye _changable;
        EffectConst.EyeParts _eyeParts;
        EffectConst.WhichEye _whichEye;

        public ChangeEyeView Construct(IChangableEye robbable, EffectConst.EyeParts eyeParts, EffectConst.WhichEye whichEye)
        {
            _changable = robbable;
            _eyeParts = eyeParts;
            _whichEye = whichEye;

            return this;
        }
        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _changable.ChangeParts(_eyeParts, _whichEye);
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }
    }
}