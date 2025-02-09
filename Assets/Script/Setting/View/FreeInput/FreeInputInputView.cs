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
    public class FreeInputInputView : IInputView
    {
        IInputView _inputView;

        [Inject]
        public FreeInputInputView(InputViewFactory factory, SettingFreeInputProcessor settingMenuInputProcessor)
        {
            _inputView = factory.Create(settingMenuInputProcessor, ActiveLayerConst.InputLayer.SettingMenuItem);
        }

        public async UniTask Enter()
        {
            await _inputView.Enter();
        }

        public void Exit() => _inputView.Exit();

    }
}