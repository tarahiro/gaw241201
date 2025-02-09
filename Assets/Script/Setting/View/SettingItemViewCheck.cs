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
    public class SettingItemViewCheck : SettingItemView
    {
        [SerializeField] GameObject _checkObject;

        public override async UniTask Enter()
        {
            _checkObject.SetActive(true);
            await base.Enter();
        }
    }
}