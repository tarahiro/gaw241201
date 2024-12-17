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
        [Inject] IndexUpdater _indexUpdater;
        [Inject] IQuestionDisplayTextModel _questionDisplayTextGenerator;

        Subject<List<char>> _restrictionDataLoaded = new Subject<List<char>>();
        Subject<string> _sampleInputted = new Subject<string>();
        public IObservable<string> SampleInputted => _sampleInputted;
        public IObservable<List<char>> RestrictionDataLoaded => _restrictionDataLoaded;
        public void InitializeTyping(ITypingRoguelikeSingleSequenceMaster master)
        {
            _sampleInputted.OnNext(master.JpText);
            string romanText  = string.Concat(master.RomanText, "@");

            _enterKeyHundler.Initialize(romanText,master.RestrictedCharList);
            _indexUpdater.Initialize(romanText);

            string s = "Restriction��ݒ�: ";
            foreach(var c in master.RestrictedCharList)
            {
                s += c + ",";
            }
            Log.Comment(s);
            _restrictionDataLoaded.OnNext(master.RestrictedCharList);

            _indexUpdater.UpdateIndex(0,romanText);
            _questionDisplayTextGenerator.GenerateDisplayQuestionText(romanText,_indexUpdater.GetIndex());
        }
    }
}