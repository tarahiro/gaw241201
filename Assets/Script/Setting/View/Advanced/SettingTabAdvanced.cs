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
    public class SettingTabAdvanced : SettingTabView
    {
        [Inject] ISettingOrnament _ornament;
        public override int MaxIndex { get; set; } = 2;

        public override UniTask Enter(int menuIndex)
        {
            _ornament.SetMad();
            return base.Enter(menuIndex);
        }
    }
}