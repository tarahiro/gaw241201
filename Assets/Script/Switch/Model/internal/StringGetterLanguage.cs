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
    public class StringGetterLanguage : IByStringGetter
    {
        LanguageModel _model;

        public StringGetterLanguage(LanguageModel model)
        {
            _model = model;
        }

        public string ByStringGet(string byKey)
        {
            return _model.Language.ToString();
        }
    }
}