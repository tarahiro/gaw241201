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

        [Inject] IEyePositionChangable _eyePositionChangable;

        [Inject] IFacialChangable _facialChangable;
        [Inject] IImpressionChangable _impressionChangable;

        Subject<Unit> _completed = new Subject<Unit>();
        ConversationViewArgs? _prevArgs = null;
        public Subject<Unit> Completed => _completed;


        public async UniTask EnterConversation(ConversationViewArgs args)
        {
            Log.Comment(args.Message + " " + args.EyePosition + "‚ÌConversation•\Ž¦ŠJŽn");

            args.CancellationToken.Register(() => _completed.OnNext(Unit.Default));


            ProcessConversationEffect<ConversationViewConst.EyePosition>((int)args.EyePosition, -1, _prevArgs != null ? (int)_prevArgs.EyePosition : -1, _eyePositionChangable);
            ProcessConversationEffect<ConversationViewConst.Facial>((int)args.Facial, (int)ConversationViewConst.Facial.None, _prevArgs != null ? (int)_prevArgs.Facial : -1, _facialChangable);
            ProcessConversationEffect<ConversationViewConst.Impression>((int)args.Impression, (int)ConversationViewConst.Impression.None, _prevArgs != null ? (int)_prevArgs.Impression : -1, _impressionChangable);

         //   _eyePositionChangable.SetEffect(args.EyePosition);





            await _textView.Enter(args.Message, args.CancellationToken);

            _prevArgs = args;
            _completed.OnNext(Unit.Default);
        }

        void ProcessConversationEffect<T>(int key, int none, int prevKey, IConversationEffectChangable<T> effectChangable) where T : Enum
        {

            if (prevKey != key)
            {
                effectChangable.ResetEffect(EnumUtil.NoToType<T>(prevKey));
            }

            if (key != none)
            {
                effectChangable.SetEffect(EnumUtil.NoToType<T>(key));
            }

        }

    }

}