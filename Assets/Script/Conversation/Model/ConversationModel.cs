using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class ConversationModel : TextSequenceModel<IConversationMaster> {
        /*
        [Inject] IConversationMasterDataProvider _masterDataProvider;
        [Inject] ConversationModelArgsFactory _modelArgsFactory;

        CancellationTokenSource _cts = new CancellationTokenSource();
        Subject<ConversationModelArgs> _entered = new Subject<ConversationModelArgs>();
        public IObservable<ConversationModelArgs> Entered => _entered;

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "のConversationGroup開始");
            _cts = new CancellationTokenSource();
            List<IConversationMaster> _thisConversationGroup = new List<IConversationMaster>();

            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if(_masterDataProvider.TryGetFromIndex(i).GetMaster().Group == bodyId)
                {
                    _thisConversationGroup.Add(_masterDataProvider.TryGetFromIndex(i).GetMaster());
                }
            }

            for (int i = 0; i < _thisConversationGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                Log.Comment(_thisConversationGroup[i].Id + "のConversation開始");
                _isEnded = false;

                _entered.OnNext(_modelArgsFactory.Create(_thisConversationGroup[i],_cts.Token));
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "のConversationGroup終了");
        }

        public void EndSIngle()
        {
            Log.Comment("終了を検知");
            _isEnded = true;
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

#endif
        */
    }
}