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
    public class RoguelikeCorrectInputHundler : ICorrectInputHundlable,IPointableView
    {
        [Inject] ICorrectInputHundlable _correctInputHundlable;

        Subject<Unit> _pointed = new Subject<Unit>();
        public IObservable<Unit> Pointed => _pointed;



        public void OnCorrectInput(List<char> questionCharList, int charIndex, out bool isEndLoop)
        {
            _pointed.OnNext(Unit.Default);
            _correctInputHundlable.OnCorrectInput(questionCharList, charIndex, out isEndLoop);
        }
    }
}