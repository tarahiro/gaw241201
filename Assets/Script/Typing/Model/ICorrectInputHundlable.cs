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
    public interface ICorrectInputHundlable
    {
        void OnCorrectInput(string questionCharList, int charIndex, out bool isEndLoop);
    }
}