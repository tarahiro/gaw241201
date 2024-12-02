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
        [Inject] IFacialChangable _facialChangable;

        Subject<Unit> _completed = new Subject<Unit>();
        public Subject<Unit> Completed => _completed;


        public async UniTask EnterConversation(ConversationViewArgs args)
        {
            Log.Comment(args.Message + " " + args.Facial + "‚ÌConversation•\Ž¦ŠJŽn");

            args.CancellationToken.Register(() => _completed.OnNext(Unit.Default));

            _facialChangable.SetFacial(args.Facial);
            await _textView.Enter(args.Message, args.CancellationToken);

            _completed.OnNext(Unit.Default);
        }
    }
}