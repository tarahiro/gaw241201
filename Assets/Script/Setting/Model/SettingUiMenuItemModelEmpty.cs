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
    public class SettingUiMenuItemModelEmpty : IUiMenuItemModel
    {
        IUiMenuItemModel _uiMenuItemModel;

        public IObservable<Unit> Entered => _uiMenuItemModel.Entered;
        public IObservable<Unit> Exited => _uiMenuItemModel.Exited;

        public bool IsEnterable => _uiMenuItemModel.IsEnterable;

        [Inject]
        public SettingUiMenuItemModelEmpty()
        {
            _uiMenuItemModel = new UiMenuItemModel(true);
        }

        public async UniTask Enter()
        {
            await _uiMenuItemModel.Enter();

            //Ç±Ç±Ç≈ÉtÉâÉOèàóù
        }

        public void End()
        {
            _uiMenuItemModel.End();
        }
    }
}