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
    public class SettingTabManager : MonoBehaviour
    {
        [SerializeField] List<SettingTabView> _settingTabView;

        public SettingTabView Current { get; private set; }

        public void EnterTab(SettingTabEnterArgs args)
        {
            Current = _settingTabView[args.TabIndex];
            Current.Enter(args.MenuIndex).Forget();
        }
        public void ChangeTab(SettingTabEnterArgs args)
        {
            Log.DebugLog("ViewÇ≈TabêÿÇËë÷Ç¶");
            Current.Exit().Forget();
            EnterTab(args);
        }

    }
}