using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class ConversationModelArgsFactory
    {
        public ConversationModelArgs Create(IConversationMaster master, CancellationToken cancellationToken)
        {
            return new ConversationModelArgs(master, cancellationToken);
        }
    }
}