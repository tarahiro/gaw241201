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
    public interface IQuestionDisplayTextModel
    {
        void GenerateDisplayQuestionText(string questionChar, int charIndex);
        IObservable<string> TextUpdated { get; }
        IObservable<int> CorrectInputted { get; }
    }
}