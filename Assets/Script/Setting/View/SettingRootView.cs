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
    public class SettingRootView : MonoBehaviour
    {
        [Inject] SettingInputView _settingInputView;
        [Inject] SettingTabManager _settingTabManager;

        [SerializeField] GameObject _root;

        public async UniTask Enter(SettingEnterArgs args)
        {
            _root.SetActive(true);
            _settingInputView.Enter().Forget();
            _settingTabManager.Enter(args);
        }

        public async UniTask Exit(SettingExitArgs args)
        {
            _settingInputView.Exit();
            _root.SetActive(false);
        }
    }
}