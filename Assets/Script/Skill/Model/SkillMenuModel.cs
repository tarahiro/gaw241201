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
        public int ItemIndex => _uiMenuModel.ItemIndex;
        public int MaxItemRange => 3;
        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;

        Subject<SkillArgs> _entered = new Subject<SkillArgs>();
        public IObservable<SkillArgs> Entered => _entered;

        Subject<SkillArgs.Data> _decided = new Subject<SkillArgs.Data>();   
        public IObservable<SkillArgs.Data> Decided => _decided;


        public void Initialize()
        {
            //ˆê’U‰¼
            List<IUiMenuItemModel> list = new List<IUiMenuItemModel>();
            list.Add(null);
            list.Add(null);
            list.Add(null);

            var uiMenuModel = new UiMenuModel(list);
            uiMenuModel.FocusChanged.Subscribe(_focusChanged);

            _uiMenuModel = uiMenuModel;
        }

        List<SkillArgs.Data> _data;

        public void Enter(CancellationToken ct, List<SkillArgs.Data> data)
        {
            _data = data;
            _entered.OnNext(new SkillArgs(ct, ItemIndex, data));
            Enter();
            MoveFocus(ItemIndex);

        }

        public void MoveFocus(int menuIndex)
        {
            _uiMenuModel.MoveFocus(menuIndex);
        }

        public void Enter()
        {
            _uiMenuModel.Enter();

        }

        public void Exit()
        {
            _uiMenuModel.Exit();
        }

        public void Decide()
        {
            _decided.OnNext(_data[ItemIndex]);
        }
    }
}