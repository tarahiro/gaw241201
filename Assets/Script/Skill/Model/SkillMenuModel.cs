using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class SkillMenuModel : IUiMenuModel
    {
        IUiMenuModel _uiMenuModel;

        [Inject]
        public SkillMenuModel(ISkillMenuItemProvider provider)
        {
            List<IUiMenuItemModel> list = new List<IUiMenuItemModel>();
            for(int i = 0; i < provider.Count; i++)
            {
                list.Add(provider.Provide(i));
            }

            _uiMenuModel = new UiMenuModel(list);
        }

        public int MaxItemRange => _uiMenuModel.MaxItemRange;
        public int ItemIndex => _uiMenuModel.ItemIndex;
        public bool IsEnable => _uiMenuModel.IsEnable;
        public IObservable<int> FocusChanged => _uiMenuModel.FocusChanged;
        public IObservable<int> Entered => _uiMenuModel.Entered;
        public IObservable<int> Decided => _uiMenuModel.Decided;
        public IObservable<Unit> Exited => _uiMenuModel.Exited;
        public void Enter() => _uiMenuModel.Enter();
        public void Exit() => _uiMenuModel.Exit();
        public void MoveFocus(int menuIndex) => _uiMenuModel.MoveFocus(menuIndex);
        public void Decide() => _uiMenuModel.Decide();
    }
}