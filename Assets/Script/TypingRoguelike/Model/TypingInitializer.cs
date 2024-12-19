using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] IndexUpdater _indexUpdater;
        [Inject] IQuestionDisplayTextModel _questionDisplayTextGenerator;
        [Inject] IRestrictedCharProvider _restrictedCharProvider;
        [Inject] IRestrictedCharRegisterer _restrictedCharRegisterer;
        Subject<List<char>> _restrictionDataLoaded = new Subject<List<char>>();
        Subject<string> _sampleInputted = new Subject<string>();
        public IObservable<string> SampleInputted => _sampleInputted;
        public IObservable<List<char>> RestrictionDataLoaded => _restrictionDataLoaded;
        public void InitializeTyping(ITypingRoguelikeSingleSequenceMaster master)
        {
            _sampleInputted.OnNext(master.JpText);
            string romanText  = string.Concat(master.RomanText, "@");

            _restrictedCharRegisterer.Register(master.RestrictedCharList);
            List<char> restrictedCharList = _restrictedCharProvider.GetRestrictedChar();

            _enterKeyHundler.Initialize(romanText, restrictedCharList);
            _indexUpdater.Initialize(romanText);

            string s = "RestrictionÇê›íË: ";
            foreach(var c in restrictedCharList)
            {
                s += c + ",";
            }
            Log.Comment(s);
            _restrictionDataLoaded.OnNext(restrictedCharList);

            _indexUpdater.UpdateIndex(0,romanText);
            _questionDisplayTextGenerator.GenerateDisplayQuestionText(romanText,_indexUpdater.GetIndex());
        }
    }
}