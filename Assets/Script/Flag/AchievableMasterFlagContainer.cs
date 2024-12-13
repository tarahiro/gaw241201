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
    public class AchievableMasterFlagContainer<T> : IAchievableMasterFlagProvider<T> where T : IIndexable, IIdentifiable
    {
        List<string> _flagContainer = new List<string>()
        {
            "a","i"
        };

        public bool IsContainskey(string Id)
        {
            return _flagContainer.Contains(Id);
        }
    }
}