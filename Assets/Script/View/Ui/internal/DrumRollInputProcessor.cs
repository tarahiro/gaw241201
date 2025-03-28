using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class DrumRollInputProcessor : IInputProcessable, IIndexerInputtableView
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

        public DrumRollInputProcessor(
            InputExecutorCommand executor,
            InputExecutorDiscreteDirectionHorizontal executorHorizontal,
            IDisposablePure disposable)
        { 

            _executorList = new List<IInputExecutor>();

            executor.Initialize(InputConst.Command.Decide);
            executor.Inputted.Subscribe(_ => _decided.OnNext(default));
            _executorList.Add(executor);

            executorHorizontal.Inputted.Subscribe(x => _indexerMoved.OnNext(x)).AddTo(disposable);
            _executorList.Add(executorHorizontal);

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