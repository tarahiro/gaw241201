using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
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

        List<ISettingTabModel> _settingTabModelList = new List<ISettingTabModel>();

        public void Initialize()
        {
            _settingTabModelList = _settingTabListFactory.Create();
            foreach (var tab in _settingTabModelList)
            {
                tab.Initialize();
            }
        }

        public void MoveFocus(SettingConst.Direction direction)
        {
            current.MoveFocus(direction);
        }

        ISettingTabModel current { get { return _settingTabModelList[TabIndex]; } }

        public void GetSettingUiState(out int _tabIndex, out int _itemIndex)
        {
            _tabIndex = TabIndex;
            _itemIndex = current.ItemIndex;
        }
    }
}