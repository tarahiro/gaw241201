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
    public class TimerStarter : ITimerStartableModel
    {
        Subject<float> _timerStarted = new Subject<float>();
        public IObservable<float> TimerStarted => _timerStarted;

        public void StartTimer(ITypingRoguelikeSingleSequenceMaster master)
        {
            _timerStarted.OnNext(TypingUtil.RemoveBracketsAndContents(master.QuestionText.GetTranslatedText(_languageIndex)).Length * master.Time);
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