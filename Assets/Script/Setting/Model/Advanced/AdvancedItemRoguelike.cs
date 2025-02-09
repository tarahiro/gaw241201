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
        IUiMenuItemModel _uiMenuItemModel;

        [Inject] ISubscriber<bool> _subscriber;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        public IObservable<Unit> Entered => _uiMenuItemModel.Entered;
        public bool IsEnterable => _uiMenuItemModel.IsEnterable;

        Subject<bool> _valueChanged = new Subject<bool>();
        public IObservable<bool> ValueChanged => _valueChanged;

        bool _isRoguelikeEnabled;

        [Inject]
        public AdvancedItemRoguelike(ActiveLayerPublisher activeLayerPublisher)
        {
            _uiMenuItemModel = new UiMenuItemModel(true,activeLayerPublisher);
        }

        public void Initialize() 
        {
            _subscriber.Subscribe(OnSetFlag);
        }

        public async UniTask Enter()
        {
            await _uiMenuItemModel.Enter();
        }

        public void End()
        {
            _uiMenuItemModel.End();

            if (_isRoguelikeEnabled)
            {
                _globalFlagRegisterer.RegisterFlag(FlagConst.Key.IsRoguelikeEnabled, Tarahiro.Const.c_false);
            }
            else
            {
                _globalFlagRegisterer.RegisterFlag(FlagConst.Key.IsRoguelikeEnabled, Tarahiro.Const.c_true);

            }
        }

        public void OnSetFlag(bool b)
        {
            Log.DebugLog("メッセージ受け取り: " + b);
            _isRoguelikeEnabled = b;
            _valueChanged.OnNext(b);
        }
    }
}