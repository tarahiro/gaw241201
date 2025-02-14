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
    public class ChangeEyeViewFactory
    {
        [Inject] IChangableEye _changableEye;

        public IEffectItemView Create(EffectConst.Key key, Transform parent)
        {
            ChangeEyeView changeEyeView =  GameObject.Instantiate(ResourceUtil.GetResource<ChangeEyeView>(EffectViewItemFactory.c_pathPrefix + "ChangeEyeView"), parent);

            switch (key)
            {
                case EffectConst.Key.SetGoatEye:
                    return changeEyeView.Construct(_changableEye, EffectConst.EyeParts.Goat, EffectConst.WhichEye.Both);

                case EffectConst.Key.SetNormalEye:
                    return changeEyeView.Construct(_changableEye, EffectConst.EyeParts.Normal, EffectConst.WhichEye.Both);

                default:
                    Log.DebugAssert("ïsê≥Ç»keyÇ≈Ç∑:" + key);
                    return changeEyeView;
            }
        }
    }
}