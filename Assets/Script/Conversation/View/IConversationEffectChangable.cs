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
    public interface IConversationEffectChangable<T> where T : Enum
    {
        void SetEffect(T key);
        void ResetEffect(T key);
    }
}