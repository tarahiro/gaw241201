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
    public class GameOverExhibitionInputProcessor : IInputProcessable
    {

        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;

        Subject<Unit> _canceled= new Subject<Unit>();
        public IObservable<Unit> Canceled => _canceled;

        List<IInputExecutor> _executorList;

        [Inject]
        public GameOverExhibitionInputProcessor(CommandInputExecutor executor)
        {
            _executorList = new List<IInputExecutor>();

            executor.Initialize(InputConst.Command.Decide);
            executor.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(executor);

        }

        public void ProcessInput()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                _decided.OnNext(Unit.Default);
            }*/

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _canceled.OnNext(Unit.Default);
            }

            foreach (var item in _executorList)
            {
                item.TryExecute();
            }
        }
    }
}