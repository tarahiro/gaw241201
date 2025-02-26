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
        [Inject] ISubscriber<FlagConst.Key, string> _subscriber;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;
        [Inject] FreeInputUnfixedText _freeInputUnfixedText;
        [Inject] IDisposablePure _disposablePure;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        Subject<string> _valueChanged = new Subject<string>();
        public IObservable<string> ValueChanged => _valueChanged;

        string _playerName;


        public void Initialize()
        {
            _subscriber.Subscribe(FlagConst.Key.Name, OnSetFlag).AddTo(_disposablePure);
        }

        public void  Enter()
        {
            Log.Comment("ProfileItemPlayerNameにEnter");

            //Initializerを別クラスに分けるかも
            _freeInputUnfixedText.Enter(_playerName);
            _entered.OnNext(Unit.Default);
        }

        public void End()
        {
            _freeInputUnfixedText.Exit();
            _exited.OnNext(Unit.Default);
        }

        public void OnSetFlag(string s)
        {
            Log.DebugLog("メッセージ受け取り:" + s);

            _playerName = s;
            _valueChanged.OnNext(s);
        }

        public void Decide(string text)
        {
            Log.DebugLog("ProfileItemPlayeName:Decide");
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.Name, text);
            End();
        }
    }
}