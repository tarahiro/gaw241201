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
    public class SettingUiModel
    {
        [Inject] SettingTabListFactory _settingTabListFactory;

        public int TabIndex { get; private set; }

        List<IUiMenuModel> _settingTabModelList = new List<IUiMenuModel>();

        Subject<SettingTabEnterArgs> _tabChanged = new Subject<SettingTabEnterArgs>();
        public IObservable<SettingTabEnterArgs> TabChanged => _tabChanged;
        public void Initialize()
        {
            _settingTabModelList = _settingTabListFactory.Create();
            foreach (var tab in _settingTabModelList)
            {
                tab.Initialize();
            }
        }
        CancellationTokenSource _tabChangeCts;

        public void ChangeTab(SettingConst.TabDirection direction)
        {
            Log.DebugLog("ModelÇ≈TabêÿÇËë÷Ç¶");
            var target = TabIndex + (int)direction;
            if (target < 0) target += _settingTabModelList.Count;
            if (target >= _settingTabModelList.Count) target -= _settingTabModelList.Count;

            TabIndex = target;

            _tabChangeCts = new CancellationTokenSource();
            _tabChanged.OnNext(new SettingTabEnterArgs(target, Current.ItemIndex, _tabChangeCts.Token));
        }

        public IUiMenuModel Current { get { return _settingTabModelList[TabIndex]; } }

        public void GetSettingUiState(out int _tabIndex, out int _itemIndex)
        {
            _tabIndex = TabIndex;
            _itemIndex = Current.ItemIndex;
        }
    }
}