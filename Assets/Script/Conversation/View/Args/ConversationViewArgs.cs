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

namespace gaw241201.View
{
    public class ConversationViewArgs
    {
        public string Message { get; private set; }
        public FacialConst.Key Facial { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public ConversationViewArgs(string message, FacialConst.Key facial,CancellationToken cancellationToken)
        {
            Message = message;
            Facial = facial;
            CancellationToken = cancellationToken;
        }
    }
}