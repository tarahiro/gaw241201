using Cysharp.Threading.Tasks;
using gaw241201.Model;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class TypingStarter : ITypingStarter
    {
        [Inject] EnterKeyHundler _enterKeyHundler;
        [Inject] IndexUpdater _indexUpdater;
        [Inject] MessageKeyHundler _messageKeyHundler;
        [Inject] KeyInputProcesser _keyInputProcesser;
        [Inject] IQuestionDisplayTextModel _questionDisplayTextGenerator;
        [Inject] IRestrictedCharProvider _restrictedCharProvider;
        [Inject] IRestrictedCharRegisterer _restrictedCharRegisterer;
        [Inject] ISubscriber<int> _subscriber;
        int _languageIndex = 0;

        Subject<List<char>> _restrictionDataLoaded = new Subject<List<char>>();
        Subject<string> _sampleInputted = new Subject<string>();
        public IObservable<string> SampleInputted => _sampleInputted;
        public IObservable<List<char>> RestrictionDataLoaded => _restrictionDataLoaded;

        public void Initialize()
        {
            _subscriber.Subscribe(x => SetLanguage(x));
        }

        public void StartTyping(ITypingRoguelikeSingleSequenceMaster master, TypingRoguelikeConditionProvider conditionProvider)
        {

            _sampleInputted.OnNext(_messageKeyHundler.HundleKey(master.DisplayText.GetTranslatedText(_languageIndex)));
            string romanText  = string.Concat(_messageKeyHundler.HundleKey(master.QuestionText.GetTranslatedText(_languageIndex)), "@");

            if (conditionProvider.IsEnableRestriction())
            {
                _restrictedCharRegisterer.Register(master.RestrictedCharList);
            }

            List<char> restrictedCharList = _restrictedCharProvider.GetRestrictedChar();
            
            if (conditionProvider.IsEnableRestriction())
            {
                _restrictionDataLoaded.OnNext(restrictedCharList);
            }
            
            _enterKeyHundler.Initialize(romanText, restrictedCharList);
            _indexUpdater.Initialize(romanText);
            _keyInputProcesser.Initialize(conditionProvider.IsEnableRoman(), conditionProvider.IsEnableCapital());

            _indexUpdater.UpdateIndex(0,romanText);
            _questionDisplayTextGenerator.GenerateDisplayQuestionText(romanText,_indexUpdater.GetIndex());
        }

        public void SetLanguage(int languageIndex)
        {
            _languageIndex = languageIndex;
        }
    }
}