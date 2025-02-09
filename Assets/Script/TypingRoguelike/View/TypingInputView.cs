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
    public class TypingInputView : IInputView
    {
        IInputView _inputView;

        [Inject]
        public TypingInputView(InputViewFactory factory, TypingInputProcessor settingMenuInputProcessor)
        {
            _inputView = factory.Create(settingMenuInputProcessor, ActiveLayerConst.InputLayer.Typing);
        }

        public async UniTask Enter()
        {
            await _inputView.Enter();
        }

        public void Exit() => _inputView.Exit();

    }
}