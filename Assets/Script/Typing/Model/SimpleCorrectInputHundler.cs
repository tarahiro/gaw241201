

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
    public class SimpleCorrectInputHundler : ICorrectInputHundlable
    {
        [Inject] IQuestionDisplayTextModel _questionTextGenerator;

        public void OnCorrectInput(string questionCharList, int charIndex, out bool isEndLoop)
        {
            SoundManager.PlaySE("Key");
            if (questionCharList[charIndex] == '@') // 「@」がタイピングの終わりの判定となる。
            {
                isEndLoop = true;
            }
            else
            {
                isEndLoop = false;
                _questionTextGenerator.GenerateDisplayQuestionText(questionCharList, charIndex);
            }
        }
    }
}