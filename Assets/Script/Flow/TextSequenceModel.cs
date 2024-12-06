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
    public abstract class TextSequenceModel<T> : IFlowModel where T : IIdentifiable, IIndexable,IGroupable
    {
        [Inject] ModelArgsFactory<T> _modelArgsFactory;
        [Inject] IMasterDataProvider<IMasterDataRecord<T>> _masterDataProvider;

        CancellationTokenSource _cts = new CancellationTokenSource();
        Subject<ModelArgs<T>> _entered = new Subject<ModelArgs<T>>();
        public IObservable<ModelArgs<T>> Entered => _entered;

        //Unitask��Subject�̕ϊ����g���Ă��ꂢ�ɂ�����
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "��Group�J�n");

            _cts = new CancellationTokenSource();
            List<T> _thisGroup = new List<T>();

            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if (_masterDataProvider.TryGetFromIndex(i).GetMaster().Group == bodyId)
                {
                    _thisGroup.Add(_masterDataProvider.TryGetFromIndex(i).GetMaster());
                }
            }


            for (int i = 0; i < _thisGroup.Count && !_cts.IsCancellationRequested; i++)
            {
                Log.Comment(_thisGroup[i].Id + "�J�n");
                _isEnded = false;

                _entered.OnNext(_modelArgsFactory.Create(_thisGroup[i], _cts.Token));
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "��Group�I��");
        }

        public void EndSIngle()
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