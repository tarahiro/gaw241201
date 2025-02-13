using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.View;


namespace gaw241201.Presenter
{
    public class MonitorPresenter : IPostInitializable
    {
        [Inject] ILoopInitializer _loopInitializer;

        [Inject] StartMonitorModel _startModel;
        [Inject] MonitorView _view;
        [Inject] CmdMonitorView _cmdMonitorView;
        [Inject] SettingMonitorView _settingMonitorView;
        [Inject] SettingMonitorModel _settingMonitorModel;
        [Inject] CmdHaltModel _haltModel;
        [Inject] IChangeValueMonitorBySettings _changeValueMonitorBySettings;

        [Inject] SettingRootHundler _settingRootHundler;

        CompositeDisposable _disposable = new CompositeDisposable();
        public void PostInitialize()
        {
            _loopInitializer.LoopInitialized.Subscribe(_ => _startModel.StartMonitor("Setting")).AddTo(_disposable);

            _startModel.Entered.Subscribe( _view.Enter).AddTo(_disposable);
            _cmdMonitorView.Detected.Subscribe(_ => _haltModel.Halt()).AddTo(_disposable);
            _settingMonitorView.Detected.Subscribe(_ => _settingRootHundler.Enter().Forget()).AddTo(_disposable) ;
            _changeValueMonitorBySettings.OnChangedValue.Subscribe(_settingMonitorModel.OnChangeFlagsBySetting).AddTo(_disposable);
        }
    }
}