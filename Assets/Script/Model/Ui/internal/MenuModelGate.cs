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
    public class MenuModelGate : IMenuModelGate
    {
        IUiMenuModel _menuModel;
        public MenuModelGate(IUiMenuModel menuModel)
        {
            _menuModel = menuModel;
        }

        public void MenuStart()
        {
            _started.OnNext(Unit.Default);
            _menuModel.Enter();
        }
        Subject<Unit> _started = new Subject<Unit>();
        public IObservable<Unit> Started => _started;

        public void MenuEnd()
        {
            _exited.OnNext(Unit.Default);
        }
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> MenuEnded => _exited;
    }
}