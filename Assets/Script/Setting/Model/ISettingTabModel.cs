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
    public interface ISettingTabModel
    {
        public int ItemIndex { get; }
        public int MaxItemRange { get; }
        public IObservable<int> FocusChanged { get; }   
        public void Initialize();
        public void MoveFocus(SettingConst.Direction direction);
        public void Enter();
        public void Exit();
    }
}