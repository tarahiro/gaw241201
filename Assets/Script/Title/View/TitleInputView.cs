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
    public class TitleInputView
    {
        bool _isEnable = false;

        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;


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
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                _decided.OnNext(Unit.Default);
            }
        }
    }
}