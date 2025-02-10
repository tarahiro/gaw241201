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
        [Inject] SettingMenuInputView _settingInputView;
        [Inject] SettingTabManager _settingTabManager;
        [Inject] RendererHundler _rendererHundler;

        [SerializeField] GameObject _root;

        
        private void Start()
        {
            _root.SetActive(false);            
        }
        

        public async UniTask Enter(SettingTabEnterArgs args)
        {
            _root.SetActive(true);
            _rendererHundler.ActivateRendererFeature(RendererHundler.RendererFeature.ScanLine);
            _settingInputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
            _settingTabManager.EnterTab(args);
        }

        public async UniTask Exit(SettingExitArgs args)
        {
            _settingInputView.Exit();
            _rendererHundler.DeactivateRendererFeature(RendererHundler.RendererFeature.ScanLine);
            _root.SetActive(false);
        }
    }
}