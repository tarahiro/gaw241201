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
    public class AdvancedItemRoguelike : IUiMenuItemModel
    {

        [Inject] ISubscriber<FlagConst.Key, string> _subscriber;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        public bool IsEnterable;

        Subject<bool> _valueChanged = new Subject<bool>();
        public IObservable<bool> ValueChanged => _valueChanged;
        [Inject]
        IDisposablePure _disposablePure;

        bool _isRoguelikeEnabled;


        public void Initialize() 
        {
            _subscriber.Subscribe(FlagConst.Key.IsRoguelikeEnabled, OnSetFlag).AddTo(_disposablePure);
        }

        public void Enter()
        {
            _entered.OnNext(Unit.Default);
        }

        public void End()
        {
            if (_isRoguelikeEnabled)
            {
                _globalFlagRegisterer.RegisterFlag(FlagConst.Key.IsRoguelikeEnabled, Tarahiro.Const.c_false);
            }
            else
            {
                _globalFlagRegisterer.RegisterFlag(FlagConst.Key.IsRoguelikeEnabled, Tarahiro.Const.c_true);

            }
        }

        public void OnSetFlag(string s)
        {
            Log.DebugLog("メッセージ受け取り: " + s);

            bool b = s == Tarahiro.Const.c_true;
            _isRoguelikeEnabled = b;
            _valueChanged.OnNext(b);
        }
    }
}