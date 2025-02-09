using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class ProfileItemPlayerName : IUiMenuItemModel
    {
        IUiMenuItemModel _uiMenuItemModel;

        [Inject] ISubscriber<FlagConst.Key, string> _subscriber;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;
        public IObservable<Unit> Entered => _uiMenuItemModel.Entered;
        public bool IsEnterable => _uiMenuItemModel.IsEnterable;

        Subject<string> _valueChanged = new Subject<string>();
        public IObservable<string> ValueChanged => _valueChanged;

        string _playerName;

        [Inject]
        public ProfileItemPlayerName()
        {
            _uiMenuItemModel = new UiMenuItemModel(true);

        }
        public void Initialize()
        {
            _subscriber.Subscribe(FlagConst.Key.Name, OnSetFlag);
        }

        public async UniTask Enter()
        {
            await _uiMenuItemModel.Enter();
        }

        public void End()
        {
            _uiMenuItemModel.End();

        }

        public void End(string name)
        {
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, name);
            End();
        }

        public void OnSetFlag(string s)
        {
            Log.DebugLog("メッセージ受け取り:" + s);

            _playerName = s;
            _valueChanged.OnNext(s);
        }
    }
}