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

        public void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _lrInputted.OnNext(1);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _lrInputted.OnNext(-1);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                _decided.OnNext(Unit.Default);
            }
        }
    }
}