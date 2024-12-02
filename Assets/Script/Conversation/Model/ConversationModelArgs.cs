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
    public class ConversationModelArgs
    {
        public IConversationMaster Master { get; set; }
        public CancellationToken CancellationToken { get; set; }

        public ConversationModelArgs(IConversationMaster master, CancellationToken cancellationToken)
        {
            Master = master;
            CancellationToken = cancellationToken;
        }
    }
}