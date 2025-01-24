using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class EffectViewItemFactory
    {
        const string c_pathPrefix = "Prefab/EffectViewItem/";

        [Inject] IRemovedable _removedable;
        [Inject] IChangableEye _robbable;

        public IEffectItemView Create(EffectConst.Key key, Transform parent)
        {
            switch (key)
            {
                case EffectConst.Key.ConfiscateLeftEye:
                    return GameObject.Instantiate<ConfiscateLeftEyeView>(ResourceUtil.GetResource<ConfiscateLeftEyeView>(c_pathPrefix + key), parent)
                        .Construct(_removedable, _robbable);

                case EffectConst.Key.SetGoatEye:
                    return GameObject.Instantiate<SetGoatEyeView>(ResourceUtil.GetResource<SetGoatEyeView>(c_pathPrefix + key), parent)
                        .Construct(_robbable);

                default:
                    return GameObject.Instantiate<GameObject>(ResourceUtil.GetResource<GameObject>(c_pathPrefix + key), parent).GetComponent<IEffectItemView>();
            }
        }
    }
}