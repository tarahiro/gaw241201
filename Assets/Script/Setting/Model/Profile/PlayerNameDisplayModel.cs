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
    public class PlayerNameDisplayModel
    {
        [Inject] ISubscriber<FlagConst.Key, string> _subscriber;

        Subject<string> _valueChanged = new Subject<string>();
        public IObservable<string> ValueChanged => _valueChanged;

        [Inject] IDisposablePure _disposablePure;

        public void OnSetFlag(string s)
        {
            Log.DebugLog("メッセージ受け取り:" + s);
            _valueChanged.OnNext(s);
        }
        public void Initialize()
        {
            _subscriber.Subscribe(FlagConst.Key.Name, OnSetFlag).AddTo(_disposablePure);
        }
    }
}