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
    public class QuestionInitializer : IQuestionInitializer
    {
        [Inject] TypingTextView _item;

        public void InitializeQuestion(TypingViewArgs _viewArgsList, List<char> questionCharList, int charIndex)
        {

            // 問題の初期状態を設定
            char[] characters = _viewArgsList.QuestionText.ToCharArray();
            foreach (char character in characters)
            {
                questionCharList.Add(character);
            }
            questionCharList.Add('@');

            //表示テキストを反映
            _item.SetSampleText(_viewArgsList.SampleText);
        }
    }
}