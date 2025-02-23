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
    public class SettingRootView : MonoBehaviour, IMenuRootView
    {
        [Inject] SettingMenuInputView _settingInputView;
        [Inject] RendererHundler _rendererHundler;

        [SerializeField] GameObject _root;

        
        private void Start()
        {
            _root.SetActive(false);            
        }
       

        public void EnterRoot()
        {
            _root.SetActive(true);
            _rendererHundler.ActivateRendererFeature(RendererHundler.RendererFeature.ScanLine);
            _settingInputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void EndRoot()
        {
            _settingInputView.Exit();
            _rendererHundler.DeactivateRendererFeature(RendererHundler.RendererFeature.ScanLine);
            _root.SetActive(false);
        }
    }
}