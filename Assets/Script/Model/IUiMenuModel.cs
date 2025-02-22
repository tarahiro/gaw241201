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
    public interface IUiMenuModel
    {
        public int ItemIndex { get; }
        public int MaxItemRange { get; }
        public bool IsEnable { get; }
        public IObservable<int> FocusChanged { get; }
        public IObservable<int> Decided { get; }
        public IObservable<int> Entered { get; }
        public void MoveFocus(int menuIndex);
        public void Enter();
        public void Exit();

        public void Decide();
    }
}