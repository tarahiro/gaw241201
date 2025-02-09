using Cysharp.Threading.Tasks;
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
    public class UiMenuItemModel : IUiMenuItemModel
    {
        [Inject]
        ActiveLayerPublisher _activeLayerPublisher;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        ActiveLayerConst.InputLayer _inputLayer = ActiveLayerConst.InputLayer.SettingMenuItem;


        public bool IsEnterable { get; private set; }

        bool _isEnd = false;


        //Fake
        public UiMenuItemModel(bool isEnterable, ActiveLayerPublisher activeLayerPublisher = null)
        {
            IsEnterable = isEnterable;
            _activeLayerPublisher = activeLayerPublisher;
        }

        public async UniTask Enter()
        {
            _activeLayerPublisher.Publish(_inputLayer);
            _entered.OnNext(Unit.Default);
            await UniTask.WaitUntil(() => !_isEnd);
        }

        public void End()
        {
            _isEnd = false;
        }
    }
}