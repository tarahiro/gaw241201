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
    public class SettingTabProfile : SettingTabView
    {
        [Inject] ISettingOrnament _ornament;

        public override UniTask Enter(int menuIndex)
        {
            _ornament.SetNormal();
            return base.Enter(menuIndex);
        }
    }
}