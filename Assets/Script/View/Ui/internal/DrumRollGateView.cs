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
    public class DrumRollGateView
    {
        DrumRollDisplayView _displayView;
        DrumRollInputView _inputView;
        ICancellationTokenPure _tokenPure;

        public DrumRollGateView(DrumRollDisplayView displayView, DrumRollInputView inputView, ICancellationTokenPure tokenPure)
        {
            _displayView = displayView;
            _inputView = inputView;
            _tokenPure = tokenPure;
        }

        public async UniTask Enter()
        {
            _displayView.Enter();
            _tokenPure.SetNew();
            _inputView.Enter(_tokenPure.Token).Forget();
        }

        public async UniTask Exit()
        {
            _displayView.Exit();
            _inputView.Exit();
        }
    }
}