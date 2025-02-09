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
        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;

        public bool IsEnterable { get; private set; }

        bool _isEnd = false;

        public UiMenuItemModel(bool isEnterable)
        {
            IsEnterable = isEnterable;
        }

        public async UniTask Enter()
        {
            _entered.OnNext(Unit.Default);
            await UniTask.WaitUntil(() => !_isEnd);
        }

        public void End()
        {
            _isEnd = false;
        }
    }
}