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
    public class TitleExitModel
    {
        [Inject] IMainLoopEntererProvider _adapter;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public void ExitTitle()
        {
            Log.Comment("タイトル終了");
            _adapter.ProvideMainLoopAdapter().Enter().Forget();
            _exited.OnNext(Unit.Default);

        }
    }
}