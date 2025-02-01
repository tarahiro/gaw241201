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

namespace gaw241201
{
    public interface ITypingStarter
    {
        void Initialize();
        void StartTyping(ITypingRoguelikeSingleSequenceMaster master, TypingRoguelikeConditionProvider conditionProvider);
        IObservable<string> SampleInputted { get; }
        IObservable<List<char>> RestrictionDataLoaded { get; }
    }
}