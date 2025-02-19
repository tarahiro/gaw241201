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
    public class LanguageInitializer : IStartable
    {
        [Inject] LanguageModel _languageModel;

        //将来的にはSteamの言語設定などから取ってくる
        [Inject] LanguageConst.AvailableLanguage _initialLanguage;

        public void Start()
        {
            _languageModel.SetLanguage(_initialLanguage);
        }
    }
}