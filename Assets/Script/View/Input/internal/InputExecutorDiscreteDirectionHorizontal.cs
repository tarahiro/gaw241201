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
    public class InputExecutorDiscreteDirectionHorizontal : IInputExecutor
    {
        [Inject] IInputHundlerDiscreteDirection _hundler;

        Subject<int> _inputted = new Subject<int>();

        public IObservable<int> Inputted => _inputted;


        public void TryExecute()
        {
            Vector2Int vec = _hundler.InputtedDiscreteDirection();

            if (vec.x != 0)
            {
                _inputted.OnNext(vec.x);

                _hundler.NotifyUse(Vector2Int.right * vec.x);
            }
        }

      
    }
}