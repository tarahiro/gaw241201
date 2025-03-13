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
       [SerializeField] FreeInputEntererView _freeInputEntererView;

        public override async UniTask Enter()
        {
            await base.Enter();
            await _freeInputEntererView.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            _freeInputEntererView.Exit();
        }
    }
}