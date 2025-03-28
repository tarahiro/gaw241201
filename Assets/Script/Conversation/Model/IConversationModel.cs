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
    public interface IConversationModel
    {
        UniTask Enter(string bodyId);
        void Initialize(Action<ModelArgs<IConversationMaster>> action, IDisposablePure disposables);
        void EndSingle();
    }
}