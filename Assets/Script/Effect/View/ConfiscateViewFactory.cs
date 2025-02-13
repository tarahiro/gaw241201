using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class ConfiscateViewFactory
    {
        [Inject] LeftEyeRemovedable _leftEyeRemovedable;
        [Inject] BothEyeRemovedable _bothEyeRemovedable;
        [Inject] IChangableEye _changableEye;

        public IEffectItemView Create(EffectConst.Key key, Transform parent)
        {
            ConfiscateView returnable = GameObject.Instantiate(ResourceUtil.GetResource<ConfiscateView>(EffectViewItemFactory.c_pathPrefix +"ConfiscateView"), parent);
            switch (key)
            {
                case EffectConst.Key.ConfiscateLeftEye:
                    return returnable.Construct(_leftEyeRemovedable, _changableEye);

                case EffectConst.Key.ConfiscateBothEye:
                    return returnable.Construct(_bothEyeRemovedable, _changableEye);

                default:
                    Log.DebugAssert("ïsê≥Ç»keyÇ≈Ç∑:" + key);
                    return returnable;
            }
        }
    }
}