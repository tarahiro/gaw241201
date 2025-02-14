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
    public class SelectInputInputView : IInputView
    {
        IInputView _inputView;

        public IObservable<bool> BlockEnabled => _inputView.BlockEnabled;
        [Inject]
        public SelectInputInputView(InputViewFactory factory, SelectInputProcessor settingMenuInputProcessor)
        {
            _inputView = factory.Create(settingMenuInputProcessor, ActiveLayerConst.InputLayer.SettingMenu);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            await _inputView.Enter(ct);
        }

        public void Exit() => _inputView.Exit();
    }
}