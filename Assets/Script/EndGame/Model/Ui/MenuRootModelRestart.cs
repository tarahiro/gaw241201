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
    public class MenuRootModelRestart : IMenuModelGate, IEndGameCore
    {
        IMenuModelGate _underlying;

        public MenuRootModelRestart(IMenuModelGate menuModelGate)
        {
            _underlying = menuModelGate;
        }

        public void Enter(EndGameConst.Key bodyId)
        {
            Log.DebugLog("Enter: " + typeof(MenuRootModelRestart).FullName);
            MenuStart();
        }

        public void MenuStart() => _underlying.MenuStart();
        public IObservable<Unit> Started => _underlying.Started;
        public void MenuEnd() => _underlying.MenuEnd();
        public IObservable<Unit> MenuEnded => _underlying.MenuEnded;
    }
}