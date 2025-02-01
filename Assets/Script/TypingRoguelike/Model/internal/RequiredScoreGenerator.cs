using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class RequiredScoreGenerator : IRequiredScoreGeneratable
    {

        Subject<int> _requiredScoreGenerated = new Subject<int>();
        public IObservable<int> RequiredScoreGenerated => _requiredScoreGenerated;

        public void RegisterRequiredScore(List<ITypingRoguelikeSingleSequenceMaster> _thisGroup, ITypingRoguelikeMaster master)
        {
            int textCount = 0;

            foreach (var singleSequence in _thisGroup)
            {
                textCount += TypingUtil.RemoveBracketsAndContents(singleSequence.QuestionText.GetTranslatedText(_languageIndex)).Length;
            }

            _requiredScoreGenerated.OnNext((int)(textCount * master.RequiredScorePerChar));

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