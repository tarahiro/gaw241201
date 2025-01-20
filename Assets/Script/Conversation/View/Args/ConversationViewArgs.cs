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
        public ITranslatableText Message { get; private set; }
        public ConversationViewConst.EyePosition EyePosition { get; private set; }
        public ConversationViewConst.Facial Facial { get; private set; }
        public ConversationViewConst.Impression Impression { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public ConversationViewArgs(ITranslatableText message, ConversationViewConst.EyePosition eyePosition, ConversationViewConst.Facial facial, ConversationViewConst.Impression impression, CancellationToken cancellationToken)
        {
            Message = message;
            EyePosition = eyePosition;
            Facial = facial;
            Impression = impression;

            CancellationToken = cancellationToken;
        }
    }
}