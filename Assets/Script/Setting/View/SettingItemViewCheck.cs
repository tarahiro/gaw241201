using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SettingItemViewCheck : SettingItemView
    {
        [SerializeField] GameObject _checkObject;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public override async UniTask Enter()
        {
            await base.Enter();
            _exited.OnNext(Unit.Default);
        }

        public void SetValue(bool b)
        {
            _checkObject.SetActive(b);
        }
    }
}