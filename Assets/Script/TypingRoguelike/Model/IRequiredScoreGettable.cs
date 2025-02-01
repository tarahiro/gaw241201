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
    public interface IRequiredScoreGeneratable
    {
        void Initialize();
        void RegisterRequiredScore(List<ITypingRoguelikeSingleSequenceMaster> _thisGroup, ITypingRoguelikeMaster master);
        IObservable<int> RequiredScoreGenerated { get; }
    }
}