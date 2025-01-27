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
        Subject<SettingConst.Direction> _cursorMoved = new Subject<SettingConst.Direction>();
        public IObservable<SettingConst.Direction> CursorMoved => _cursorMoved;

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
                _cursorMoved.OnNext(SettingConst.Direction.Up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _cursorMoved.OnNext(SettingConst.Direction.Down);
            }
        }
    }
}