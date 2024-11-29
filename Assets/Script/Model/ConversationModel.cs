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
    public class ConversationModel : IFlowModel {

        [Inject] IConversationMasterDataProvider _masterDataProvider;

        Subject<IConversationMaster> _entered = new Subject<IConversationMaster>();


        public IObservable<IConversationMaster> Entered => _entered;
        CancellationTokenSource _cts = new CancellationTokenSource();


        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "のConversationGroup開始");
            _cts = new CancellationTokenSource();
            List<IConversationMaster> _thisConversationGroup = new List<IConversationMaster>();

            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if(_masterDataProvider.TryGetFromIndex(i).GetMaster().ConversationGroup == bodyId)
                {
                    _thisConversationGroup.Add(_masterDataProvider.TryGetFromIndex(i).GetMaster());
                }
            }

            for (int i = 0; i < _thisConversationGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                Log.Comment(_thisConversationGroup[i].Id + "のConversation開始");
                _isEnded = false;

                _entered.OnNext(_thisConversationGroup[i]);
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "のConversationGroup終了");
        }

        public void EndSIngleConversation()
        {
            Log.Comment("終了を検知");
            _isEnded = true;
        }

#if ENABLE_DEBUG
        Subject<Unit> _forceEnded = new Subject<Unit>();
        public IObservable<Unit> ForceEnded => _forceEnded;
        public string ForceGetCategory => "Conversation";
        public void ForceEndFlow()
        {
            EndSIngleConversation();
            _cts.Cancel();
            _forceEnded.OnNext(Unit.Default);
        }

#endif
    }
}