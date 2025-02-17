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
    public class SettingMenuInputProcessor : IInputProcessable,IIndexerInputtableView
    {

        [Inject] SettingTabManager _tabManager;

        Subject<int> _indexerMoved = new Subject<int>();
        Subject<Unit> _decided = new Subject<Unit>();
        Subject<SettingConst.TabDirection> _lrInputted = new Subject<SettingConst.TabDirection>();
        public IObservable<int> IndexerMoved => _indexerMoved;

        public IObservable<Unit> Decided => _decided;

        public IObservable<SettingConst.TabDirection> LrInputted => _lrInputted;

        public void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                int index = _tabManager.Current.MenuIndex;
                index--;
                if (index < 0) index = _tabManager.Current.MaxIndex - 1;
                _indexerMoved.OnNext(index);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                int index = _tabManager.Current.MenuIndex;
                index++;
                if (index >= _tabManager.Current.MaxIndex) index = 0;
                _indexerMoved.OnNext(index);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _lrInputted.OnNext(SettingConst.TabDirection.Left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _lrInputted.OnNext(SettingConst.TabDirection.Right);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                _decided.OnNext(Unit.Default);
            }
        }

    }
}