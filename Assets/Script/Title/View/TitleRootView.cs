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
    public class TitleRootView : MonoBehaviour
    {
        [Inject] TitleInputView _inputView;

        [SerializeField] GameObject _root;

        private void Start()
        {
            _root.SetActive(false);
        }

        public async UniTask Enter()
        {
            Log.DebugLog("TitleRootViewŠJŽn");
            _root.SetActive(true);
            _inputView.Enter().Forget();
        }

        public async UniTask Exit()
        {
            _root.SetActive(false);
            _inputView.Exit();
        }
        
    }
}