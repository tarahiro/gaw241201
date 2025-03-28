using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201.View
{
    public class DrumRollInputView : IInputView
    {
        IInputView _inputView;
        public IObservable<bool> BlockEnabled => _inputView.BlockEnabled;

        public DrumRollInputView(IInputViewFactory factory, DrumRollInputProcessor inputProcessor)
        {
            _inputView = factory.Create(inputProcessor, ActiveLayerConst.InputLayer.GameOver);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            await _inputView.Enter(ct);
        }

        public void Exit() => _inputView.Exit();
    }
}