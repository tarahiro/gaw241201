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
    public class SettingFreeInputItemView: SettingItemView
    {
        [SerializeField] FreeInputTextDisplayView _freeInputTextDisplayView;

        [Inject] FreeInputInputView _freeInputInputView;

        public override async UniTask Enter()
        {
            Log.Comment("FreeInputView‚ÉEnter");
            await base.Enter();
            _freeInputInputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void Exit()
        {
            _freeInputInputView.Exit();
        }
    }
}