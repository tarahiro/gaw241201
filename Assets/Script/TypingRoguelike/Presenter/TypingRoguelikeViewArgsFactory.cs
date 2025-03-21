using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class TypingRoguelikeViewArgsFactory
    {
        [Inject] MessageKeyHundler _messageKeyHundler;


        public TypingViewArgs Create(ModelArgs<ITypingRoguelikeSingleSequenceMaster> modelArgs)
        {
            return new TypingViewArgs(_messageKeyHundler.HundleKey(modelArgs.Master.DisplayText.GetTranslatedText(_languageIndex)), 
                _messageKeyHundler.HundleKey(modelArgs.Master.QuestionText.GetTranslatedText(_languageIndex)), modelArgs.CancellationToken);
        }

        [Inject] ISubscriber<int> _subscriber;
        int _languageIndex = 0;
        public void Initialize()
        {
            _subscriber.Subscribe(x => SetLanguage(x));
        }
        public void SetLanguage(int languageIndex)
        {
            _languageIndex = languageIndex;
        }
    }
}