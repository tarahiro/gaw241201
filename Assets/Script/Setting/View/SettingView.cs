using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class SettingView : MonoBehaviour
    {
        [SerializeField] GameObject _root;

        public async UniTask Enter(SettingEnterArgs args)
        {
            _root.SetActive(true); 
        }

        public async UniTask Exit(SettingExitArgs args)
        {
            _root.SetActive(false);
        }
    }
}