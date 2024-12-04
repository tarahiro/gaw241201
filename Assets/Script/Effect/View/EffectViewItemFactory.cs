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

        public IEffectItemView Create(EffectConst.Key key, Transform parent)
        {
            return GameObject.Instantiate<GameObject>(ResourceUtil.GetResource<GameObject>(c_pathPrefix + key), parent).GetComponent<IEffectItemView>();
        }
    }
}