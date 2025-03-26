using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Threading;

namespace gaw241201.View
{
    public class UiMenuInputView : IInputView
    {
        IInputView _inputView;
        public IObservable<bool> BlockEnabled => _inputView.BlockEnabled;

        public UiMenuInputView(IInputViewFactory factory, UiMenuInputProcessor skillInputProcessor)
        {
            _inputView = factory.Create(skillInputProcessor, ActiveLayerConst.InputLayer.GameOver);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            Log.DebugLog("Enter: " + typeof(UiMenuInputView).FullName);
            await _inputView.Enter(ct);
        }

        public void Exit() => _inputView.Exit();
    }
}