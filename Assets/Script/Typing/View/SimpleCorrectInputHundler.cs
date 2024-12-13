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
    public class SimpleCorrectInputHundler : ICorrectInputHundlable
    {
        [Inject] TypingTextView _item;
        [Inject] IQuestionTextGenerator _questionTextGenerator;

        public void OnCorrectInput(List<char> questionCharList, int charIndex, out bool isEndLoop)
        {
            SoundManager.PlaySE("Key");
            if (questionCharList[charIndex] == '@') // 「@」がタイピングの終わりの判定となる。
            {
                isEndLoop = true;
            }
            else
            {
                isEndLoop = false;
                _item.SetQuestionText(_questionTextGenerator.GenerateQuestionText(questionCharList, charIndex), charIndex, questionCharList.Count);
            }
        }
    }
}