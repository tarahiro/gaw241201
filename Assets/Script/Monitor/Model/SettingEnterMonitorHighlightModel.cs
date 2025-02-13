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
    public class SettingEnterMonitorHighlightModel
    {
        ISubscriber<FlagConst.Key, string> _subscriber;

        Subject<Unit> _highlighted  = new Subject<Unit>();

        Subject<Unit> _lowlighted  = new Subject<Unit>();
        public IObservable<Unit> Highlighted=> _highlighted;
        public IObservable<Unit> Lowlighted=> _lowlighted;

        bool _isHighlighted = false;
        public void Highlight()
        {
            _isHighlighted = true;
            _highlighted.OnNext(Unit.Default);
        }

        public void TryLowlight()
        {
            if (_isHighlighted)
            {
                _isHighlighted = false;
                _lowlighted.OnNext(Unit.Default);
            }
        }

        [Inject]
        public SettingEnterMonitorHighlightModel(ISubscriber<FlagConst.Key, string> subscriber)
        {
            _subscriber = subscriber;
            _subscriber.Subscribe(FlagConst.Key.OnEnterSettingConversation, OnChangeFlag);
        }

        string _value = "";

        void OnChangeFlag(string value)
        {
            if(value != "" && _value != value)
            {
                Highlight();
            }

            if(value == "" && _value != value)
            {
                TryLowlight();
            }
            _value = value;
        }
    }
}