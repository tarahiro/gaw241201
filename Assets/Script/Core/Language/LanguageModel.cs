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
    public class LanguageModel
    {
        [Inject] LanguageCommandProcessor _languageCommandProcessor;

        public LanguageConst.AvailableLanguage Language { get; private set; }

        public void SetLanguage(LanguageConst.AvailableLanguage language)
        {
            Log.Comment("åæåÍê›íË");
            Language = language;
            _languageCommandProcessor.On((int)Language);
        }
    }
}