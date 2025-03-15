using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class FreeInputGateModel : IFreeInputGateModel
    {
        public IObservable<Unit> Entered => _entered;
        public IObservable<Unit> Exited => _exited;

        public FreeInputGateModel(FreeInputUnfixedText freeInputUnfixedText)
        {
            _freeInputUnfixedText = freeInputUnfixedText;
        }

        Subject<Unit> _entered = new Subject<Unit>();

        Subject<Unit> _exited = new Subject<Unit>();
        FreeInputUnfixedText _freeInputUnfixedText;

        public string InitialText { get; set; } = "";


        public void Enter()
        {
            _freeInputUnfixedText.Enter(InitialText);
            _entered.OnNext(Unit.Default);
        }



        public void Decide(string text)
        {
            _freeInputUnfixedText.Exit();
            _exited.OnNext(Unit.Default);
        }
    }
}