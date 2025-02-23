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

        Subject<SkillArgs> _argsSetted = new Subject<SkillArgs>();
        public IObservable<SkillArgs> ArgsSetted => _argsSetted;

        Subject<SkillArgs.Data> _decidedSkill = new Subject<SkillArgs.Data>();   
        public IObservable<SkillArgs.Data> DecidedSkill => _decidedSkill;

        [Inject]
        public SkillMenuModel()
        {
            //ˆê’U‰¼
            List<IUiMenuItemModel> list = new List<IUiMenuItemModel>();
            list.Add(null);
            list.Add(null);
            list.Add(null);


            _uiMenuModel = new UiMenuModel(list);
        }


        List<SkillArgs.Data> _data;

        public void Enter(CancellationToken ct, List<SkillArgs.Data> data)
        {
            _data = data;
            _argsSetted.OnNext(new SkillArgs(data));
            Enter();
            MoveFocus(ItemIndex);

        }


        public int ItemIndex => _uiMenuModel.ItemIndex;
        public int MaxItemRange => 3;
        public bool IsEnable => _uiMenuModel.IsEnable;
        public IObservable<int> FocusChanged => _uiMenuModel.FocusChanged;
        public IObservable<int> Entered => _uiMenuModel.Entered;
        public IObservable<int> Decided => _uiMenuModel.Decided;
        public IObservable<Unit> Exited => _uiMenuModel.Exited;
        public void Enter() => _uiMenuModel.Enter();

        public void Exit() => _uiMenuModel.Exit();

        public void MoveFocus(int menuIndex)
        {
            _uiMenuModel.MoveFocus(menuIndex);
        }
        public void Decide()
        {
            _decidedSkill.OnNext(_data[ItemIndex]);
            _uiMenuModel.Decide();
        }
    }
}