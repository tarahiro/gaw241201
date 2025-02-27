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
    public class UiEnterViewTemplate : MonoBehaviour, IMenuRootView
    {
        [Inject] IInputView _inputView;

        public void EnterRoot()
        {
            _inputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void EndRoot()
        {
            _inputView.Exit();
        }
    }
}