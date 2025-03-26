using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class UiMenuInputProcessor : IInputProcessable, IIndexerInputtableView
    {
        IIndexVariantHundler _indexVariantHundler;


        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;

        Subject<Unit> _canceled = new Subject<Unit>();
        public IObservable<Unit> Canceled => _canceled;

        List<IInputExecutor> _executorList;


        //ç°ÇÕFake
        Subject<int> _indexerMoved = new Subject<int>();
        public IObservable<int> IndexerMoved => _indexerMoved;

        public UiMenuInputProcessor(
            IIndexVariantHundler indexVariantHundler,
            InputExecutorCommand executor,
            InputExecutorDiscreteDirectionVertical executorVertical,
            IDisposablePure disposable)
        {
            _indexVariantHundler = indexVariantHundler;

            _executorList = new List<IInputExecutor>();

            executor.Initialize(InputConst.Command.Decide);
            executor.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(executor);

            executorVertical.Inputted.Subscribe(x =>
            _indexerMoved.OnNext(_indexVariantHundler.IndexVariant(Vector2Int.up * x))).AddTo(disposable);
            _executorList.Add(executorVertical);

        }

        public void ProcessInput()
        {
            foreach (var item in _executorList)
            {
                item.TryExecute();
            }
        }
    }
}