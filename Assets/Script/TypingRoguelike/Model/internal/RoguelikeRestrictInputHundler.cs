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

namespace gaw241201
{
    public class RoguelikeRestrictInputHundler : ICorrectInputHundlable
    {
        [Inject] ICorrectInputHundlable _correctInputHundlable;
        [Inject] IPointable _pointable;



        public void OnCorrectInput(string questionCharList, int charIndex, out bool isEndLoop)
        {
            _pointable.ReducePenaltyPoint();
            _correctInputHundlable.OnCorrectInput(questionCharList, charIndex, out isEndLoop);
        }

    }
}