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
    public class TitleEnterModel : IAdapterManagerToModel
    {
       Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;

        public async UniTask Enter()
        {
            Log.Comment("タイトル開始");
            _entered.OnNext(Unit.Default);
        }
    }
}