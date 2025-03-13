using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class FreeInputEntererView : IDisposable
    {
        [Inject] FreeInputTextDisplayView _freeInputTextDisplayView;
        [Inject] FreeInputInputView _freeInputInputView;

        public FreeInputEntererView(FreeInputTextDisplayView freeInputTextDisplayView,FreeInputInputView freeInputInputInputView)
        {
            _freeInputTextDisplayView = freeInputTextDisplayView;
            _freeInputInputView = freeInputInputInputView;
        }

        CancellationTokenSource _cts = new CancellationTokenSource();

        public async UniTask Enter()
        {
            Log.Comment("FreeInputView‚ÉEnter");
            _freeInputInputView.Enter(_cts.Token).Forget();
            _freeInputTextDisplayView.Enter().Forget();
        }

        public void Exit()
        {
            _freeInputInputView.Exit();
            _freeInputTextDisplayView.Exit().Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}