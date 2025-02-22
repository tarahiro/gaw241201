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
    public class SettingTabManager : MonoBehaviour, IMenuView
    {
        [SerializeField] List<SettingTabView> _settingTabView;

        public SettingTabView Current { get; private set; }

        public void EnterTab(int tabIndex)
        {
            Current = _settingTabView[tabIndex];
        }
        public void ChangeTab(int tabIndex)
        {
            Log.DebugLog("View‚ÅTabØ‚è‘Ö‚¦");
            Current.Exit().Forget();
            EnterTab(tabIndex);
        }

        public async UniTask Enter(int itemIndex) => await Current.Enter(itemIndex);
        public async UniTask Exit() => await Current.Exit();
        public async UniTask SetFocus(int itemIndex) => await Current.SetFocus(itemIndex);
        public async UniTask Decide(int itemIndex) => await Current.SetFocus(itemIndex);

    }
}