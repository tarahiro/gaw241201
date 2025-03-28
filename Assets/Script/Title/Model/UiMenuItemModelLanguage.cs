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
    public class UiMenuItemModelLanguage : IUiMenuItemModel
    {
        [Inject] LanguageModel _languageModel;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public void Enter()
        {
            _entered.OnNext(Unit.Default);
        }

        public void Decide(int index)
        {
            _languageModel.SetLanguage(EnumUtil.NoToType<LanguageConst.AvailableLanguage>(index));
            _exited.OnNext(default);
        }
    }
}