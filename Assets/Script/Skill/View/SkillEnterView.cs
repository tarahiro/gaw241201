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
    public class SkillEnterView : MonoBehaviour, IMenuRootView
    {
        [Inject] SkillIndexInputView _inputView;
        [Inject] SkillMenuView _menuView;
        [Inject] IndexVariantHundlerSkill _indexVariantHundlerSkill;

        CompositeDisposable _disposable;


        bool isLoop = true;

        Subject<SkillArgs.Data> _ended = new Subject<SkillArgs.Data>();
        public IObservable<SkillArgs.Data> Ended => _ended;


        /*
        public async UniTask Enter(SkillArgs args)
        {

            await _menuView.Enter(args);
            await _inputView.Enter(args.MenuItemIndex, args.DataList.Count, args.CancellationToken);
        }
        */

        public void EnterRoot()
        {
            _inputView.Enter(this.GetCancellationTokenOnDestroy()).Forget();
        }

        public void EndRoot()
        {
            _inputView.Exit();
        }


    }
}