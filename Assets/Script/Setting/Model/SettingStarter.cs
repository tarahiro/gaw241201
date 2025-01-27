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
    public class SettingStarter
    {
        [Inject] SettingUiModel _uiModel;

        Subject<SettingEnterArgs> _settingStarted = new Subject<SettingEnterArgs>();
        public IObservable<SettingEnterArgs> SettingStarted => _settingStarted;

        CancellationTokenSource _cts;
        public async UniTask Enter()
        {
            _cts = new CancellationTokenSource();

            _uiModel.GetSettingUiState(out var _tabIndex, out var _itemIndex);
            _settingStarted.OnNext(new SettingEnterArgs(_tabIndex, _itemIndex, _cts.Token));
        }
    }
}