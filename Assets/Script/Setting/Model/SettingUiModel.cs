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

namespace gaw241201
{
    public class SettingUiModel:IUiMenuModel
    {
        [Inject] SettingTabListFactory _settingTabListFactory;

        public int TabIndex { get; private set; }

        List<IUiMenuModel> _settingTabModelList = new List<IUiMenuModel>();

        Subject<int> _tabChanged = new Subject<int>();
        public IObservable<int> TabChanged => _tabChanged;

        Subject<int> _tabEntered = new Subject<int>();
        public IObservable<int> TabEntered => _tabEntered;
        public void Initialize()
        {
            _settingTabModelList = _settingTabListFactory.Create();
        }

        public void ChangeTab(SettingConst.TabDirection direction)
        {
            Log.DebugLog("ModelÇ≈TabêÿÇËë÷Ç¶");

            do
            {
                TabIndex = TabIndex + (int)direction;
                if (TabIndex < 0) TabIndex += _settingTabModelList.Count;
                if (TabIndex >= _settingTabModelList.Count) TabIndex -= _settingTabModelList.Count;

            } while (!_settingTabModelList[TabIndex].IsEnable);


            _tabChanged.OnNext(TabIndex);
            Enter();
        }

        public void EnterTab()
        {
            _tabEntered.OnNext(TabIndex);
        }


        public void GetSettingUiState(out int _tabIndex, out int _itemIndex)
        {
            _tabIndex = TabIndex;
            _itemIndex = Current.ItemIndex;
        }


        public IUiMenuModel Current { get { return _settingTabModelList[TabIndex]; } }
        public int ItemIndex => Current.ItemIndex;
        public int MaxItemRange => Current.MaxItemRange;
        public bool IsEnable => Current.IsEnable;
        public IObservable<int> FocusChanged => Current.FocusChanged;
        public IObservable<int> Decided => Current.Decided;
        public IObservable<int> Entered => Current.Entered;
        public void MoveFocus(int menuIndex) => Current.MoveFocus(menuIndex);
        public void Enter() => Current.Enter();
        public void Exit() => Current.Exit();
        public void Decide() => Current.Decide();
    }
}