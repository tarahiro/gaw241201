using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SkillIndexInputView : IIndexerInputtableView
    {
        Subject<int> _indexerMoved = new Subject<int>();
        public IObservable<int> IndexerMoved => _indexerMoved;

        Subject<Unit> _decided = new Subject<Unit>();
        public IObservable<Unit> Decided => _decided;

        bool _isEnable = false;

        int _itemIndex;
        int _maxIndex;

        public async UniTask Enter(int itemIndex, int maxIndex,CancellationToken cancellationToken)
        {
            _itemIndex = itemIndex;
            _maxIndex = maxIndex;

            _isEnable = true;
            while (_isEnable && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                ProcessInput();
            }
        }

        public void Exit()
        {
            _isEnable = false;
        }


        void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Log.DebugLog("InputŒŸ’m");
                int index = _itemIndex;
                index--;
                if (index < 0) index = _maxIndex - 1;
                _indexerMoved.OnNext(index);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Log.DebugLog("InputŒŸ’m");
                int index = _itemIndex;
                index++;
                if (index >= _maxIndex) index = 0;
                _indexerMoved.OnNext(index);
            }
            else if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                _decided.OnNext(Unit.Default);
            }
        }

        public void ChangeFocus(int index)
        {
            _itemIndex = index;
        }
    }
}