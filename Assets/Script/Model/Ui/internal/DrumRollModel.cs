using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class DrumRollModel
    {
        List<string> _contentsNameList;

        Subject<Unit> _entered = new Subject<Unit>();
        public IObservable<Unit> Entered => _entered;
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;



        int _order = 0;

        Subject<List<string>> _contentsInitialized = new Subject<List<string>>();
        public IObservable<List<string>> ContentsInitialized => _contentsInitialized;

        Subject<int> _orderChanged = new Subject<int>();
        public IObservable<int> IndexChanged => _orderChanged;

        Subject<int> _decided = new Subject<int>();
        public IObservable<int> Decided => _decided;

        public void Initialize(int order, List<string> contentsNameList)
        {
            _contentsNameList = contentsNameList;
            _order = order;

            _contentsInitialized.OnNext(contentsNameList);
            _orderChanged.OnNext(order);
        }

        public void Enter()
        {
            _entered.OnNext(Unit.Default);
        }

        public void Decide()
        {
            _decided.OnNext(_order);
        }

        public void Exit() 
        {
            _exited.OnNext(Unit.Default);
        }

        public void ChangeFocus(int i)
        {
            _order += i;
            if(_order < 0) _order = _contentsNameList.Count - 1;
            if(_order >= _contentsNameList.Count) _order = 0;

            _orderChanged.OnNext(_order);
        }


    }
}