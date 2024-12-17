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
    public interface IStageMaster : IIndexable, IIdentifiable, IGroupable
    {
        int WaveCount { get; }
        string[] AddedRestrictedCharIdList { get; }
    }
}