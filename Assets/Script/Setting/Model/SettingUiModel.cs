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
        }
        CancellationTokenSource _tabChangeCts;

        public void ChangeTab(SettingConst.TabDirection direction)
        {
            Log.DebugLog("Model��Tab�؂�ւ�");

            do
            {
                TabIndex = TabIndex + (int)direction;
                if (TabIndex < 0) TabIndex += _settingTabModelList.Count;
                if (TabIndex >= _settingTabModelList.Count) TabIndex -= _settingTabModelList.Count;

            } while (!_settingTabModelList[TabIndex].IsEnable);


            _tabChangeCts = new CancellationTokenSource();
            _tabChanged.OnNext(new SettingTabEnterArgs(TabIndex, Current.ItemIndex, _tabChangeCts.Token));
        }

        public IUiMenuModel Current { get { return _settingTabModelList[TabIndex]; } }

        public void GetSettingUiState(out int _tabIndex, out int _itemIndex)
        {
            _tabIndex = TabIndex;
            _itemIndex = Current.ItemIndex;
        }
    }
}