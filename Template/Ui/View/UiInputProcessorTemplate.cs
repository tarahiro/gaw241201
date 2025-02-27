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
    public class UiInputProcessorTemplate : IInputProcessable, IIndexerInputtableView
    {

        [Inject] IndexVariantHundlerTemplate _indexVariantHundler;

        Subject<int> _indexerMoved = new Subject<int>();
        Subject<Unit> _decided = new Subject<Unit>();

        public IObservable<int> IndexerMoved => _indexerMoved;

        public IObservable<Unit> Decided => _decided;

        List<IInputExecutor> _executorList;

        //横移動の想定。縦の場合はverticalにする
        //indexVariantHundlerの中身を書く必要もある
        [Inject]public UiInputProcessorTemplate(InputExecutorCommand command,
          InputExecutorDiscreteDirectionHorizontal horizontal,IDisposablePure disposable)
        {
            _executorList = new List<IInputExecutor>();

            command.Initialize(InputConst.Command.Decide);
            command.Inputted.Subscribe(_ => _decided.OnNext(default)).AddTo(disposable);
            _executorList.Add(command);


            horizontal.Inputted.Subscribe(x =>
            _indexerMoved.OnNext(_indexVariantHundler.IndexVariant(Vector2Int.right * x))).AddTo(disposable);
            _executorList.Add(horizontal);

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