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

        List<IInputExecutor> _executorList;

        [Inject]
        public TitleInputView(InputExecutorCommand executor)
        {
            _executorList = new List<IInputExecutor>();

            executor.Initialize(InputConst.Command.Decide);
            executor.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(executor);

        }

        public async UniTask Enter()
        {
            _isEnable = true;
            while (_isEnable)
            {
                await UniTask.Yield(PlayerLoopTiming.PreUpdate);
                ProcessInput();
            }
        }

        public void Exit()
        {
            _isEnable = false;
        }


        void ProcessInput()
        {
            foreach (var item in _executorList)
            {
                item.TryExecute();
            }
        }
    }
}