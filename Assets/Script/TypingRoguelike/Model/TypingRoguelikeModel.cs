using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;

namespace gaw241201
{
    public class TypingRoguelikeModel : IFlowModel, IRequiredScoreGeneratable
    {
        [Inject] IGroupMasterGettable<ITypingRoguelikeSingleSequenceMaster> _groupMasterGettable;
        [Inject] ISingleTextSequenceEnterable<ITypingRoguelikeSingleSequenceMaster> _singleTextSequenceEnterable;

        [Inject] ITypingRoguelikeMasterDataProvider _masterDataProvider;
        [Inject] IPointGettable _pointGettable;


        CancellationTokenSource _cts = new CancellationTokenSource();


        Subject<int> _requiredScoreGenerated = new Subject<int>();
        public IObservable<int> RequiredScoreGenerated => _requiredScoreGenerated;

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)

        {   /*TextSequenceModel<T>との共通部分*/
            Log.Comment(bodyId + "のGroup開始");

            _cts = new CancellationTokenSource();
            List<ITypingRoguelikeSingleSequenceMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(bodyId);
            /*共通部分終わり*/

            RegisterRequiredScore(_thisGroup,bodyId);
            _pointGettable.InitializePoint();
            
             /*TextSequenceModel<T>との共通部分*/
            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                _singleTextSequenceEnterable.EnterTextSequence(_thisGroup[i], _cts.Token, out _isEnded);
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "のGroup終了");
            /*共通部分終わり*/
        }

        void RegisterRequiredScore(List<ITypingRoguelikeSingleSequenceMaster> _thisGroup, string bodyId)
        {
            var master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();
            int textCount = 0;

            foreach (var singleSequence in _thisGroup)
            {
                textCount += TypingUtil.RemoveBracketsAndContents(singleSequence.RomanText).Length;
            }

            _requiredScoreGenerated.OnNext((int)(textCount * master.RequiredScorePerChar));

        }


        /*TextSequenceModel<T>との共通部分*/
        public void EndSingle()
        {
            Log.Comment("終了を検知");
            _isEnded = true;
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
        /*共通部分終わり*/
    }
}