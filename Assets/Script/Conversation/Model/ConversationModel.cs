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
    public class ConversationModel : IConversationModel, ICategoryEnterableModel
    {
        [Inject] IGroupMasterGettable<IConversationMaster> _groupMasterGettable;
        
        ISingleTextSequenceEnterable<IConversationMaster> _singleTextSequenceEnterable;

        CancellationTokenSource _cts = new CancellationTokenSource();

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        [Inject]
        public ConversationModel(ISingleTextSequenceEnterable<IConversationMaster> singleTextSequenceEnterable, IGroupMasterGettable<IConversationMaster> groupMasterGettable)
        {
            _singleTextSequenceEnterable = singleTextSequenceEnterable;
            _groupMasterGettable = groupMasterGettable;
        }

        public void Initialize(Action<ModelArgs<IConversationMaster>> action, CompositeDisposable disposable)
        {
            _singleTextSequenceEnterable.Entered.Subscribe(action).AddTo(disposable);
        }

        public async UniTask Enter(string bodyId)
        {
            Log.Comment(bodyId + "のGroup開始");

            _cts = new CancellationTokenSource();
            List<IConversationMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(bodyId);

            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                _singleTextSequenceEnterable.EnterTextSequence(_thisGroup[i], _cts.Token, out _isEnded);
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "のGroup終了");

        }


        public async UniTask EnterFlow(string bodyId)
        {
            await Enter(bodyId);
        }

        public void EndSingle()
        {
            Log.Comment("終了を検知");
            _isEnded = true;
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

    }
}