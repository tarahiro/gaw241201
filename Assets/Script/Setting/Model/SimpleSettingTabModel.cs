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
    public class SimpleSettingTabModel : ISettingTabModel
    {
        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;
        public int ItemIndex { get; private set; }

        public int MaxItemRange { get; private set; }

        public void Initialize()
        {

        }
        public void MoveFocus(SettingConst.Direction direction)
        {
            int target = ItemIndex + (int)direction;
            if (target < 0)
            {
                target += MaxItemRange;
            }
            else if (target >= MaxItemRange)
            {
                target -= MaxItemRange;
            }

            ItemIndex = target;
            Log.DebugLog("ItemIndex " + ItemIndex + "Ç…çXêV");
            _focusChanged.OnNext(ItemIndex);
        }
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void SetMaxItemRange(int _max)
        {
            MaxItemRange = _max;
        }
    }
}