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
        // public IObservable<SettingTabEnterArgs> SettingMenuStartedFake => _settingStarted;

        /*
        Subject<int> _tabStarted = new Subject<int>();
        Subject<int> _menuStarted = new Subject<int>();

        public IObservable<int> TabStarted => _tabStarted;
        public IObservable<int> MenuStarted => _menuStarted;
        */

        Subject<Unit> _started = new Subject<Unit>();
        public IObservable<Unit> Started => _started;

        [Inject] ICancellationTokenPure _cts;

        public void MenuStart()
        {
            _cts.SetNew();

            //_uiModel.GetSettingUiState(out var _tabIndex, out var _itemIndex);

            _started.OnNext(Unit.Default);
            _uiModel.EnterTab();
            _uiModel.Enter();


            /*
            _tabStarted.OnNext(_tabIndex);
            _menuStarted.OnNext(_itemIndex);

            _settingStarted.OnNext(new SettingTabEnterArgs(_tabIndex, _itemIndex, _cts.Token));
            */
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