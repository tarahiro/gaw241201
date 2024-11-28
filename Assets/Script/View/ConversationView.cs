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
    public class ConversationView
    {
        [Inject] ConversationTextView _textView;
        [Inject] EyesView _eyesView;

        Subject<Unit> _completed = new Subject<Unit>();
        public Subject<Unit> Completed => _completed;


        public async UniTask EnterConversation(ConversationViewArgs args, CancellationToken ct)
        {
            Log.Comment(args.Message + " " + args.Facial + "‚ÌConversation•\Ž¦ŠJŽn");

            _eyesView.SetEye(args.Facial);
            await _textView.Enter(args.Message,ct);

            _completed.OnNext(Unit.Default);
        }
    }
}