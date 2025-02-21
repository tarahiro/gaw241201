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

        //Unitask��Subject�̕ϊ����g���Ă��ꂢ�ɂ�����
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)

        {   /*TextSequenceModel<T>�Ƃ̋��ʕ���*/
            Log.Comment(bodyId + "��Group�J�n");

            _cts.SetNew();
            List<ITypingRoguelikeSingleSequenceMaster> _thisGroup = _groupMasterGettable.GetGroupMaster(bodyId);
            /*���ʕ����I���*/

            var master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();
            _conditionProvider.Initialize(master);


            if (_conditionProvider.IsEnableScore())
            {
                _scoreGeneratable.RegisterRequiredScore(_thisGroup, master);
                _pointGettable.InitializePoint();
            }
            
             /*TextSequenceModel<T>�Ƃ̋��ʕ���*/
            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                _singleTextSequenceEnterable.EnterTextSequence(_thisGroup[i], _cts.Token, out _isEnded);
                await UniTask.WaitUntil(() => _isEnded);
                if (_conditionProvider.IsEnableWave())
                {
                    _waveClearModel.ClearWave();
                }
            }

            Log.Comment(bodyId + "��Group�I��");
            /*���ʕ����I���*/
        }


        /*TextSequenceModel<T>�Ƃ̋��ʕ���*/
        public void EndSingle()
        {
            Log.Comment("�I�������m");
            _isEnded = true;


            _typedFlagRegisterer.End();
            if (_conditionProvider.IsEnableTimeUp())
            {
                //�^�C�}�[�X�g�b�v����
                _timerEnded.OnNext(Unit.Default);
            }
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
        /*���ʕ����I���*/
    }
}