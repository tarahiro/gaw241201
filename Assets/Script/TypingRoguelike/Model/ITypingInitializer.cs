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
    public interface ITypingInitializer
    {
        void InitializeQuestion(ITypingMaster master);
        IObservable<string> SampleInputted { get; }
    }
}