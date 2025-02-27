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
    public class SkillMenuItemModel : IUiMenuItemModel
    {
        public void Enter()
        {
            _entered.OnNext(_data);
        }

        SkillArgs.Data _data;

        Subject<SkillArgs.Data> _setted = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Setted => _setted;


        Subject<SkillArgs.Data> _entered = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Entered => _entered;


        public void SetData(SkillArgs.Data data)
        {
            _data = data;
            _setted.OnNext(_data);
        }


    }
}