using Cysharp.Threading.Tasks;
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
    public class ActViewArgs
    {
        public int WaveCount { get; private set; }
        public List<char> RestrictedCharList { get; private set; }

        public ActViewArgs(int waveCount, List<char> restrictedCharList)
        {
            WaveCount = waveCount;
            RestrictedCharList = restrictedCharList;
        }
    }
}