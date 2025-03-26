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
    public class UiMenuItemModelGameStart : IUiMenuItemModel
    {

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;

        public void Enter()
        {
            Log.DebugLog("GameStart");
            _entered.OnNext(Unit.Default);
        }
    }
}