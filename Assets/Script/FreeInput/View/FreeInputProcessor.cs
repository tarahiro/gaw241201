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
    public class FreeInputProcessor : IInputProcessable, IFreeInputProcessor
    {
        Subject<char> _keyEntered = new Subject<char>();
        public IObservable<char> KeyEntered => _keyEntered;

        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;

        Subject<Unit> _deleted = new Subject<Unit>();
        public IObservable<Unit> Deleted => _deleted;


        List<IInputExecutor> _executorList;

        [Inject]
        public FreeInputProcessor(InputExecutorCommand decide, InputExecutorCommand cancel,InputExecutorKeyStroke keyStroke,
            IDisposablePure disposable)
        {
            _executorList = new List<IInputExecutor>();

            decide.Initialize(InputConst.Command.Decide);
            decide.Inputted.Subscribe(_ => _decided.OnNext(default)).AddTo(disposable);
            _executorList.Add(decide);

            cancel.Initialize(InputConst.Command.Cancel);
            cancel.Inputted.Subscribe(_ => _deleted.OnNext(default)).AddTo(disposable);
            _executorList.Add(cancel);

            keyStroke.Inputted.Subscribe(_keyEntered).AddTo(disposable);
            _executorList.Add(keyStroke);

        }
        public void ProcessInput()
        {

            foreach (var item in _executorList)
            {
                item.TryExecute();
            }
        }
    }
}