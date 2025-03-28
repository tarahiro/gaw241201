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
    public static class SwitchConst
    {
        [System.Serializable]
        public enum TargetCategory
        {
            Flow,
            Conversation,
        }

        [System.Serializable]
        public enum ByCategory
        {
            Typed,
            Language,
        }

    }
}