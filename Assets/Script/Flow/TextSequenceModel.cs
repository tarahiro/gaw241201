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

        //UnitaskとSubjectの変換を使ってきれいにしたい
        bool _isEnded = false;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "のGroup開始");

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
                Log.Comment(_thisGroup[i].Id + "開始");
                _isEnded = false;

                _entered.OnNext(_modelArgsFactory.Create(_thisGroup[i], _cts.Token));
                await UniTask.WaitUntil(() => _isEnded);
            }

            Log.Comment(bodyId + "のGroup終了");
        }

        public void EndSIngle()
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