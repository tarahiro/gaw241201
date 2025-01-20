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
    public class ConfiscateLeftEyeView : MonoBehaviour, IEffectItemView
    {
        public bool IsAutoEnd => true;


        IRemovedable _removedable;
        IRobbable _robbable;

        public ConfiscateLeftEyeView Construct(IRemovedable removedable, IRobbable robbable)
        {
            _removedable = removedable;
            _robbable = robbable;

            return this;
        }

        public virtual async UniTask Enter(CancellationToken cancellationToken)
        {
            _removedable.RemoveParts();
            _robbable.RobParts();
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
        }
    }
}