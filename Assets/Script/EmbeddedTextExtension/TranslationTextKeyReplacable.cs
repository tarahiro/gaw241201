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
    public class TranslationTextKeyReplacable : ITranslatableText
    {
        MessageKeyHundler _messageKeyHundler;
        ITranslatableText _underlying;

        public TranslationTextKeyReplacable(MessageKeyHundler messageKeyHundler, ITranslatableText underlying)
        {
            _messageKeyHundler = messageKeyHundler;
            _underlying = underlying;
        }

        public string GetTranslatedText(int languageIndex)
        {
            return _messageKeyHundler.HundleKeyToTranslatableText(_underlying).GetTranslatedText(languageIndex);
        }
    }
}