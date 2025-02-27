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
    public class SelectInputProcessor : IInputProcessable
    {
        Subject<int> _lrInputted = new Subject<int>();
        public IObservable<int> LrInputted => _lrInputted;

        Subject<Unit>  _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;
        List<IInputExecutor> _executorList = new List<IInputExecutor>();

        [Inject]
        public SelectInputProcessor(InputExecutorCommand command,
            InputExecutorDiscreteDirectionHorizontal horizontal)
        {
            command.Initialize(InputConst.Command.Decide);
            command.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(command);

            horizontal.Inputted.Subscribe(x => _lrInputted.OnNext(x));
            _executorList.Add(horizontal);
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