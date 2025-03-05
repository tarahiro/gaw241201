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
    public class FreeInputIndexer
    {
        Subject<int> _focused = new Subject<int>();
        public IObservable<int> Focused => _focused;

        Subject<int> _unfocused = new Subject<int>();
        public IObservable<int> Unfocused => _unfocused;

        Subject<string> _updated = new Subject<string>();
        public IObservable<string> Updated => _updated;

        public void Enter(int textLength)
        {
            if (textLength < FlagConst.c_NameMaxLength)
            {
                UpdateFocus(false, textLength);
            }
            else
            {
                Index = FlagConst.c_NameMaxLength - 1;
                IsFocusExist = false;
            }
        }

        public int Index { get; private set; } = 0;

        public bool IsFocusExist { get; private set; } = true;

        void UpdateFocus(bool unfocusOption, int index)
        {
            if (unfocusOption)
            {
                _unfocused.OnNext(Index);
            }

            if (index < FlagConst.c_NameMaxLength)
            {
                Index = index;
            }
            else
            {
               Index = FlagConst.c_NameMaxLength - 1;
            }
            IsFocusExist = true;
            _focused.OnNext(Index);
        }

        public void Exit()
        {
            _unfocused?.OnNext(Index);
            Index = 0;
        }

        public bool TryNextFocus()
        {
            if(Index < FlagConst.c_NameMaxLength - 1)
            {
                UpdateFocus(true, Index + 1);
                return true;
            }
            else
            {
                _unfocused.OnNext(Index);
                IsFocusExist = false;
                return false;
            }
        }

        public void PrevFocus()
        {
            if (IsFocusExist)
            {
                UpdateFocus(true, Index - 1);
            }
            else
            {
                UpdateFocus(false, Index);
            }
        }

    }
}