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
    public class RoguelikeCorrectInputHundler : ICorrectInputHundlable
    {
        [Inject] ICorrectInputHundlable _correctInputHundlable;
        [Inject] IPointable _pointable;


        public void OnCorrectInput(string questionCharList, int charIndex, out bool isEndLoop)
        {
            _pointable.AddUnitPoint();
            _correctInputHundlable.OnCorrectInput(questionCharList, charIndex, out isEndLoop);
        }
    }
}