using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TypingInitializer : ITypingInitializer
    {
        [Inject] EnterKeyHundler _enterKeyHundler;
        [Inject] IQuestionTextDisplayModel _questionTextGenerator;

        Subject<string> _sampleInputted = new Subject<string>();
        public IObservable<string> SampleInputted => _sampleInputted;
        public void InitializeQuestion(ITypingMaster master)
        {
            _sampleInputted.OnNext(master.JpText);
            string romanText  = string.Concat(master.RomanText, "@");

            _enterKeyHundler.Initialize(romanText);

            _questionTextGenerator.GenerateQuestionText(romanText, 0);
        }
    }
}