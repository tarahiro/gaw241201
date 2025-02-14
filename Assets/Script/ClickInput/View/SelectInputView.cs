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
    public class SelectInputView
    {
        [Inject] SelectInputInputView _inputView;
        [Inject] SelectInputDisplayView _displayView;


        public void Enter(CancellationToken ct)
        {
            _inputView.Enter(ct).Forget();
            _displayView.Enter(0);
        }

        public void Exit()
        {
            _inputView.Exit();
            _displayView.Exit();
        }
    }
}