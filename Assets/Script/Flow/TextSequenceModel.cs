using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public abstract class TextSequenceModel<T> : ICategoryEnterableModel where T : IIdentifiable, IIndexable,IGroupable 
    {
        [Inject] IGroupMasterGettable<T> _groupMasterGettable;
        [Inject] ISingleTextSequenceEnterable<T> _singleTextSequenceEnterable;

        [Inject] ICancellationTokenPure _cts;

        //Unitask��Subject�̕ϊ����g���Ă��ꂢ�ɂ�����
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "��Group�J�n");

            _cts.SetNew();
            List<T> _thisGroup = _groupMasterGettable.GetGroupMaster(bodyId);

            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                _singleTextSequenceEnterable.EnterTextSequence(_thisGroup[i], _cts.Token, out _isEnded);
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "��Group�I��");
        }


        public void EndSingle()
        {
            Log.Comment("�I�������m");
            _isEnded = true;
        }

        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
    }
}