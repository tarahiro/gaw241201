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
    public class SettingMenuInputProcessor : IInputProcessable, IIndexerInputtableView
    {
        [Inject] IndexVariantHundlerSettings _indexVariantHundler;

        Subject<int> _indexerMoved = new Subject<int>();
        Subject<Unit> _decided = new Subject<Unit>();
        Subject<SettingConst.TabDirection> _lrInputted = new Subject<SettingConst.TabDirection>();
        public IObservable<int> IndexerMoved => _indexerMoved;

        public IObservable<Unit> Decided => _decided;

        public IObservable<SettingConst.TabDirection> LrInputted => _lrInputted;

        List<IInputExecutor> _executorList;

        [Inject]
        public SettingMenuInputProcessor(InputExecutorCommand command,
            InputExecutorDiscreteDirectionHorizontal horizontal,
            InputExecutorDiscreteDirectionVertical vertical,
            IDisposablePure disposable)
        {
            _executorList = new List<IInputExecutor>();

            command.Initialize(InputConst.Command.Decide);
            command.Inputted.Subscribe(_ => _decided.OnNext(default)).AddTo(disposable);
            _executorList.Add(command);


            horizontal.Inputted.Subscribe(x => _lrInputted.OnNext((SettingConst.TabDirection)x)).AddTo(disposable);
            _executorList.Add(horizontal);

            vertical.Inputted.Subscribe(x =>
            _indexerMoved.OnNext(_indexVariantHundler.IndexVariant(Vector2Int.up * x))).AddTo(disposable);
            _executorList.Add(vertical);

        }

        public void ProcessInput()
        {
            foreach(var item in _executorList)
            {
                item.TryExecute();
            }
        }

    }
}