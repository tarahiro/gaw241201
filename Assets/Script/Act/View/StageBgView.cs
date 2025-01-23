using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using TMPro;

namespace gaw241201.View
{
    public class StageBgView : MonoBehaviour, IActBgView
    {
        const string c_prefabPath = "Prefab/StageBg/";

        StageBgItemView _currentItem;

        public void Enter(ActBgViewArgs args)
        {
            _currentItem = Instantiate(ResourceUtil.GetResource<StageBgItemView>(c_prefabPath +  args.BodyId), transform).Construct(args);
            _currentItem.Initialize();
        }

        public void ToNext()
        {
            _currentItem.ToNext();
        }

    }
}