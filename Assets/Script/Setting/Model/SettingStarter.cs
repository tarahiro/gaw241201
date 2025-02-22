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
    public class SettingStarter : IMenuModelStartable
    {
        [Inject] SettingUiModel _uiModel;
        [Inject] SettingEventCatcher _eventCatcher;

        Subject<SettingTabEnterArgs> _settingStarted = new Subject<SettingTabEnterArgs>();
        public IObservable<SettingTabEnterArgs> MenuStarted => _settingStarted;

        [Inject] ICancellationTokenPure _cts;

        public void MenuStart()
        {
            _cts.SetNew();

            _uiModel.GetSettingUiState(out var _tabIndex, out var _itemIndex);
            _settingStarted.OnNext(new SettingTabEnterArgs(_tabIndex, _itemIndex, _cts.Token));
            CountFake().Forget();
        }

        //Fake: CancellationToken‚ª•K—v
        async UniTask CountFake()
        {
            await UniTask.WaitForSeconds(1f);
            _eventCatcher.OnEnter();
        }
    }
}