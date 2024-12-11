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
    public interface ITypingViewInitializer
    {
        void InitializeView(TypingViewArgs _viewArgsList,out bool isEndLoop, out List<char> questionCharList, out int charIndex);
    }
}