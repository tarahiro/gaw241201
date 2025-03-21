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
    public class UiMenuEnterView : MonoBehaviour, IMenuRootView
    {
        [Inject] UiMenuInputView _inputView;

        [SerializeField] GameObject _root;

        void Start()
        {
            _root.SetActive(false);
        }

        public void EnterRoot()
        {
            _inputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
            _root.SetActive(true);
        }

        public void EndRoot()
        {
            _inputView.Exit();
            _root.SetActive(false);
        }
    }
}