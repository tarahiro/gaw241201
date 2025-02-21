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
    public class TypingRoguelikeModel : ICategoryEnterableModel, ITimerEndableModel
    {
        [Inject] IGroupMasterGettable<ITypingRoguelikeSingleSequenceMaster> _groupMasterGettable;
        [Inject] ISingleTextSequenceEnterable<ITypingRoguelikeSingleSequenceMaster> _singleTextSequenceEnterable;

        [Inject] ITypingRoguelikeMasterDataProvider _masterDataProvider;
        [Inject] IPointGettable _pointGettable;
        [Inject] IRequiredScoreGeneratable _scoreGeneratable;

        [Inject] TypingRoguelikeConditionProvider _conditionProvider;
        [Inject] TypedFlagRegisterer _typedFlagRegisterer;

        [Inject] WaveClearModel _waveClearModel;


        [Inject] ICancellationTokenPure _cts;

        Subject<Unit> _timerEnded = new Subject<Unit>();
        public IObservable<Unit> TimerEnded => _timerEnded;

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)

        {   /*TextSequenceModel<T>との共通部分*/
            Log.Comment(bodyId + "のGroup開始");

            _cts.SetNew();
            List<ITypingRoguelikeSingleSequenceMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(bodyId);
            /*共通部分終わり*/

            var master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();
            _conditionProvider.Initialize(master);


            if (_conditionProvider.IsEnableScore())
            {
                _scoreGeneratable.RegisterRequiredScore(_thisGroup, master);
                _pointGettable.InitializePoint();
            }
            
             /*TextSequenceModel<T>との共通部分*/
            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                _singleTextSequenceEnterable.EnterTextSequence(_thisGroup[i], _cts.Token, out _isEnded);
                await UniTask.WaitUntil(() => _isEnded);
                if (_conditionProvider.IsEnableWave())
                {
                    _waveClearModel.ClearWave();
                }
            }

            Log.Comment(bodyId + "のGroup終了");
            /*共通部分終わり*/
        }


        /*TextSequenceModel<T>との共通部分*/
        public void EndSingle()
        {
            Log.Comment("終了を検知");
            _isEnded = true;


            _typedFlagRegisterer.End();
            if (_conditionProvider.IsEnableTimeUp())
            {
                //タイマーストップ処理
                _timerEnded.OnNext(Unit.Default);
            }
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
        /*共通部分終わり*/
    }
}