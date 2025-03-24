using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class MenuRootModelTitle : IMenuModelGate, IAdapterManagerToModel
    {
        public async UniTask Enter()
        {
            Log.Comment("タイトル開始"); 
            MenuStart();
        }

        IMenuModelGate _underlying;

        public MenuRootModelTitle(IMenuModelGate menuModelGate)
        {
            _underlying = menuModelGate;
        }

        public void MenuStart() => _underlying.MenuStart();
        public IObservable<Unit> Started => _underlying.Started;
        public void MenuEnd() => _underlying.MenuEnd();
        public IObservable<Unit> MenuEnded => _underlying.MenuEnded;
    }
}