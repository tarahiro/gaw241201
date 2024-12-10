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
    public class TypingView : MonoBehaviour
    {
        [Inject] IGazable _gazable;
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] IKeyCodeToCharConverter _keyToCharConverter;
        [Inject] IQuestionTextGenerator _questionTextGenerator;

        TypingItemView _currentItem;
        const string c_prefabPath = "Prefab/Typing/TypingItemView";

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment(args.SampleText + "ŠJŽn");

            _questionTextGenerator.Initialize();


            _currentItem = Instantiate(ResourceUtil.GetResource<TypingItemView>(c_prefabPath), transform);
            _currentItem.Construct(args,_gazable, _keyInputJudger, _keyToCharConverter, _questionTextGenerator);
            var v = args.CancellationToken.Register(OnExit);

            await _currentItem.Enter(args.CancellationToken);

            v.Dispose();
            if (!args.CancellationToken.IsCancellationRequested)
            {
                OnExit();
            }
        }

        private void OnExit()
        {
            Destroy(_currentItem.gameObject);
            _exited.OnNext(Unit.Default);
        }
    }
}