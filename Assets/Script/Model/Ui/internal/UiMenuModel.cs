using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class UiMenuModel : IUiMenuModel
    {
        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;

        Subject<int> _decided = new Subject<int>();
        public IObservable<int> Decided => _decided;

        Subject<int> _entered = new Subject<int>();
        public IObservable<int> Entered => _entered;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public bool IsEnable { get; private set; } = true;
        public int ItemIndex { get; private set; }
        public int MaxItemRange => _uiMenuItemModelList.Count;

        List<IUiMenuItemModel> _uiMenuItemModelList;

        public UiMenuModel(List<IUiMenuItemModel> uiMenuItemModelList)
        {
            _uiMenuItemModelList = uiMenuItemModelList;
        }
        public void MoveFocus(int menuIndex)
        {
            ItemIndex = menuIndex;
            _focusChanged.OnNext(ItemIndex);
        }
        public void Enter()
        {
            Log.DebugLog("Enter :" + typeof(UiMenuModel).FullName);
            _entered.OnNext(ItemIndex);
        }

        public void Exit()
        {
            _exited.OnNext(Unit.Default);
        }

        public void Decide()
        {
            Log.DebugLog("Decide :" + typeof(UiMenuModel).FullName);
            _decided.OnNext(ItemIndex);
            _uiMenuItemModelList[ItemIndex].Enter();
        }

        public void SetEnable(bool b)
        {
            IsEnable = b;
        }
    }
}
