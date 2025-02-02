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
    public class SkillEnterView : MonoBehaviour
    {
        [Inject] SkillIndexInputView _inputView;
        [Inject] SkillMenuView _menuView;

        CompositeDisposable _disposable;


        bool isLoop = true;

        Subject<SkillArgs.Data> _ended = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Ended => _ended;


        public async UniTask Enter(SkillArgs args)
        {

            await _menuView.Enter(args);
            await _inputView.Enter(args.MenuItemIndex, args.DataList.Count, args.CancellationToken);
        }

        public async UniTask Exit()
        {
            _inputView.Exit();
            await _menuView.Exit();
        }

    }
}