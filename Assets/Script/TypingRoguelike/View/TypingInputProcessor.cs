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
    public class TypingInputProcessor : IInputProcessable
    {
        Subject<char> _keyEntered = new Subject<char>();
        public IObservable<char> KeyEntered => _keyEntered;


        List<IInputExecutor> _executorList;

        [Inject]
        public TypingInputProcessor(InputExecutorKeyStroke keyStroke,
            IDisposablePure disposable)
        {
            _executorList = new List<IInputExecutor>();

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