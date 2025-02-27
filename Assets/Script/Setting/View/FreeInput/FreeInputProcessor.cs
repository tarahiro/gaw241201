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
    public class FreeInputProcessor : IInputProcessable
    {
        Subject<char> _keyEntered = new Subject<char>();
        public IObservable<char> KeyEntered => _keyEntered;

        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;

        Subject<Unit> _deleted = new Subject<Unit>();
        public IObservable<Unit> Deleted => _deleted;


        List<IInputExecutor> _executorList;

        [Inject]
        public FreeInputProcessor(InputExecutorCommand executor)
        {
            _executorList = new List<IInputExecutor>();

            executor.Initialize(InputConst.Command.Decide);
            executor.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(executor);

        }
        public void ProcessInput()
        {
            for (int i = 0; i < Input.inputString.Length; i++)
            {
                _keyEntered.OnNext(Input.inputString[i]);
            }
            /*
            if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Return))
            {
                Log.DebugLog("FreeInputProcessor: Enter");
                _decided.OnNext(Unit.Default);
            }
            */

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _deleted.OnNext(Unit.Default);
            }
            foreach (var item in _executorList)
            {
                item.TryExecute();
            }
        }
    }
}