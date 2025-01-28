using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SettingInputView
    {
        Subject<SettingConst.MenuDirection> _cursorMoved = new Subject<SettingConst.MenuDirection>();
        Subject<SettingConst.TabDirection> _lrInputted = new Subject<SettingConst.TabDirection>();
        public IObservable<SettingConst.MenuDirection> CursorMoved => _cursorMoved;
        public IObservable<SettingConst.TabDirection> LrInputted => _lrInputted;

        bool _isEnable = false;

        public async UniTask Enter()
        {
            _isEnable = true;
            while (_isEnable)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                ProcessInput();
            }
        }

        public void Exit()
        {
            _isEnable = false;
        }


        void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _cursorMoved.OnNext(SettingConst.MenuDirection.Up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _cursorMoved.OnNext(SettingConst.MenuDirection.Down);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _lrInputted.OnNext(SettingConst.TabDirection.Left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _lrInputted.OnNext(SettingConst.TabDirection.Right);
            }
        }
    }
}