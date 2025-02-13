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
    public abstract class SettingTabView : MonoBehaviour
    {
        [SerializeField] SettingTabBodyView _bodyView;
        [SerializeField] SettingTabIndex _indexView;

        public virtual async UniTask Enter(int menuIndex)
        {
            await _bodyView.Enter();
            await _bodyView.SetFocus(menuIndex);
            //_indexView.Highlight();
        }

        public virtual async UniTask SetFocus(int menuIndex)
        {
            MenuIndex = menuIndex;
            await _bodyView.SetFocus(menuIndex);
        }

        public virtual async UniTask Exit()
        {
            await _bodyView.Exit();
            //_indexView.Lowlight();
        }

        public virtual async UniTask Decide(int menuIndex)
        {
            await _bodyView.Decide(menuIndex);
        }

        public virtual int MenuIndex { get; set; }

        public virtual int MaxIndex { get; set; }
    }
}