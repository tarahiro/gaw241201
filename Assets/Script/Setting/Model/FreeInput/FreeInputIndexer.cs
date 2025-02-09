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
            UpdateFocus(false,textLength);
        }

        int _index = 0;

        void UpdateFocus(bool unfocusOption, int index)
        {
            if (unfocusOption)
            {
                _unfocused.OnNext(_index);
            }

            if (index < FlagConst.c_NameMaxLength)
            {
                _index = index;
            }
            else
            {
               _index = FlagConst.c_NameMaxLength - 1;
            }
            _focused.OnNext(_index);
        }

        public void Exit()
        {
            _unfocused?.OnNext(_index);
            _index = 0;
        }

        public bool TryNextFocus()
        {
            if(_index >= FlagConst.c_NameMaxLength - 1)
            {
                return false;
            }
            else
            {
                UpdateFocus(true, _index + 1);
                return true;
            }
        }

    }
}