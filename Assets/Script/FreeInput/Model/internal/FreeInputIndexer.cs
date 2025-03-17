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
        public FreeInputIndexer(int maxLength)
        {
            _maxLength = maxLength;
        }

        int _maxLength;


        Subject<int> _focused = new Subject<int>();
        public IObservable<int> Focused => _focused;

        Subject<int> _unfocused = new Subject<int>();
        public IObservable<int> Unfocused => _unfocused;

        Subject<int> _updateIndex = new Subject<int>();
        public IObservable<int> UpdateIndex => _updateIndex;

        bool _isTextFixed = true;

        public void Enter(int textLength)
        {
            _isTextFixed = false;
            if (textLength < _maxLength)
            {
                UpdateFocus(false, textLength);
            }
            else
            {
                Index = _maxLength - 1;
                IsFocusExist = false;
            }
        }

        int _index = 0;

        public int Index
        {
            get { return _index; }
            private set
            {
                _index = value;
                _updateIndex.OnNext(_index);
            }
        }

        public bool IsFocusExist { get; private set; } = true;

        void UpdateFocus(bool unfocusOption, int index)
        {
            //本来はFixした時点で関係クラスをすべて消すべき
            if (!_isTextFixed)
            {
                if (unfocusOption)
                {
                    _unfocused.OnNext(Index);
                }

                if (index < _maxLength)
                {
                    Index = index;
                }
                else
                {
                    Index = _maxLength - 1;
                }
                IsFocusExist = true;
                _focused.OnNext(Index);
            }
        }

        public void Exit()
        {
            _unfocused.OnNext(Index);
            Index = 0;
            _isTextFixed = true;
        }

        public bool TryNextFocus()
        {
            if(Index < _maxLength - 1)
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