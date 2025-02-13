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
            Log.DebugLog("ItemIndex " + ItemIndex + "Ç…çXêV");
            _focusChanged.OnNext(ItemIndex);
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Decide()
        {
            _decided.OnNext(ItemIndex);
            _uiMenuItemModelList[ItemIndex].Enter().Forget();
        }

        public void SetEnable(bool b)
        {
            IsEnable = b;
        }
    }
}