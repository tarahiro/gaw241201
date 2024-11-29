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


        //Unitask��Subject�̕ϊ����g���Ă��ꂢ�ɂ�����
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "��ConversationGroup�J�n");
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
                Log.Comment(_thisConversationGroup[i].Id + "��Conversation�J�n");
                _isEnded = false;

                _entered.OnNext(_thisConversationGroup[i]);
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "��ConversationGroup�I��");
        }

        public void EndSIngleConversation()
        {
            Log.Comment("�I�������m");
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