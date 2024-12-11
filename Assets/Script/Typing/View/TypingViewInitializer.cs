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
    public class TypingViewInitializer : ITypingViewInitializer
    {
        [Inject] IQuestionInitializer _questionInitializer;

        public void InitializeView(TypingViewArgs _viewArgsList,out bool IsEndLoop, out List<char> questionCharList, out int charIndex)
        {   
            //èâä˙âª
            IsEndLoop = false;
            questionCharList = new List<char>();
            charIndex = 0;

            _questionInitializer.InitializeQuestion(_viewArgsList, questionCharList, charIndex);
        }
    }

}