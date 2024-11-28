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
    public class ConversationView
    {
       public async UniTask EnterConversation(ConversationViewArgs args)
        {
            Log.Comment(args.Message + " " + args.Facial + "‚ÌConversation•\Ž¦ŠJŽn");
        }
    }
}