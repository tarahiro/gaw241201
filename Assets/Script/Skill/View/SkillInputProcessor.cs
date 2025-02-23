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
    public class SkillInputProcessor : IInputProcessable, IIndexerInputtableView
    {
        [Inject] IndexVariantHundlerSkill _indexVariantHundler;


        Subject<int> _indexerMoved = new Subject<int>();
        Subject<Unit> _decided = new Subject<Unit>();

        public IObservable<int> IndexerMoved => _indexerMoved;

        public IObservable<Unit> Decided => _decided;
        public void ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _indexerMoved.OnNext(_indexVariantHundler.IndexVariant(CursorInputUtil.GetCursorDirection(KeyCode.LeftArrow)));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _indexerMoved.OnNext(_indexVariantHundler.IndexVariant(CursorInputUtil.GetCursorDirection(KeyCode.RightArrow)));
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                _decided.OnNext(Unit.Default);
            }
        }
    }
}