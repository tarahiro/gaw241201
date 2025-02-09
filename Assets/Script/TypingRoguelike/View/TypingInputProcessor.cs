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

        public void ProcessInput()
        {
            for (int i = 0; i < Input.inputString.Length; i++)
            {
                _keyEntered.OnNext(Input.inputString[i]);
            }
        }
    }
}