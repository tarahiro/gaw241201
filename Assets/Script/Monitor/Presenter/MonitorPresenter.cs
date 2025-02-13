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

        [Inject] MonitorModel _startModel;
        [Inject] MonitorView _view;

        [Inject] SettingMonitorInputView _settingMonitorView;
        [Inject] SettingEnterMonitorHighlightModel _settingEnterMonitorHighlightModel;
        [Inject] SettingMonitorDisplayView _settingMonitorDisplayView;
        [Inject] SettingExitMonitorModel _settingMonitorModel;


        [Inject] CmdMonitorView _cmdMonitorView;
        [Inject] CmdHaltModel _haltModel;
        [Inject] IChangeValueMonitorBySettings _changeValueMonitorBySettings;

        [Inject] SettingRootHundler _settingRootHundler;

        CompositeDisposable _disposable = new CompositeDisposable();
        public void PostInitialize()
        {
            _startModel.Entered.Subscribe( _view.Enter).AddTo(_disposable);
            _cmdMonitorView.Detected.Subscribe(_ => _haltModel.Halt()).AddTo(_disposable);
            _settingMonitorView.Detected.Subscribe(_ => _settingRootHundler.Enter().Forget()).AddTo(_disposable) ;

            _settingEnterMonitorHighlightModel.Highlighted.Subscribe(_ => _settingMonitorDisplayView.Highlight()).AddTo(_disposable) ;
            _settingEnterMonitorHighlightModel.Lowlighted.Subscribe(_ => _settingMonitorDisplayView.Lowlight()).AddTo(_disposable) ;

            _changeValueMonitorBySettings.OnChangedValue.Subscribe(_settingMonitorModel.OnChangeFlagsBySetting).AddTo(_disposable);
        }
    }
}