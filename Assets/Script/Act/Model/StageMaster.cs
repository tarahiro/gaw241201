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
    public class StageMaster : IStageMaster
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Group { get; set; }
        public int WaveCount { get; set; }
        public string[] AddedRestrictedCharIdList { get; set; }

        public StageMaster(int index, string id, string group, int waveCount, string[] addedRestrictedCharList)
        {
            Index = index;
            Id = id;
            Group = group;
            WaveCount = waveCount;
            AddedRestrictedCharIdList = addedRestrictedCharList;
        }
    }
}