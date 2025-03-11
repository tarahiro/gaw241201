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
    public class FreeInputEntererView : MonoBehaviour
    {
        [SerializeField] FreeInputTextDisplayView _freeInputTextDisplayView;

        [Inject] FreeInputInputView _freeInputInputView;

        public async UniTask Enter()
        {
            Log.Comment("FreeInputView‚ÉEnter");
            _freeInputInputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
            _freeInputTextDisplayView.Enter().Forget();
        }

        public void Exit()
        {
            _freeInputInputView.Exit();
            _freeInputTextDisplayView.Exit().Forget();
        }
    }
}