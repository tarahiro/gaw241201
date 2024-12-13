using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class RoguelikeRestrictInputHundler : ICorrectInputHundlable, IPenaltiableView
    {
        [Inject] ICorrectInputHundlable _correctInputHundlable;

        Subject<Unit> _penaltied = new Subject<Unit>();
        public IObservable<Unit> Penaltied => _penaltied;



        public void OnCorrectInput(List<char> questionCharList, int charIndex, out bool isEndLoop)
        {
            _penaltied.OnNext(Unit.Default);
            _correctInputHundlable.OnCorrectInput(questionCharList, charIndex, out isEndLoop);
        }

    }
}