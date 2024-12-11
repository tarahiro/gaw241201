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

            // ���̏�����Ԃ�ݒ�
            char[] characters = _viewArgsList.QuestionText.ToCharArray();
            foreach (char character in characters)
            {
                questionCharList.Add(character);
            }
            questionCharList.Add('@');

            //�\���e�L�X�g�𔽉f
            _item.SetSampleText(_viewArgsList.SampleText);
        }
    }
}