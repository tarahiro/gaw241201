using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;
using gaw241201.Model;

namespace gaw241201
{
    public class SkillAchieveModel : ICategoryEnterableModel, IMenuModelStartable, IMenuModelEndable
    {
        [Inject] ISkillChoicesDecidable _skillChoicesDecideable;
        [Inject] IAchievableMasterFlagRegisterer _achievableMasterFlagRegisterer;
        [Inject] SkillMenuModel _skillMenuModel;
        [Inject] SkillMenuItemProvider _itemProvider;



        [Inject] ICancellationTokenPure _cts;
        bool _isEnd;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog("スキル獲得開始");
            _cts.SetNew();
            _isEnd = false;


            List<SkillArgs.Data> list = _skillChoicesDecideable.DecideChoices(bodyId);

            for(int i = 0; i < list.Count; i++)
            {
                _itemProvider.ProvideRaw(i).SetData(list[i]);
            }

            _onNumberDecided.OnNext(list.Count);
            _skillMenuModel.Enter();
            MenuStart();
            await UniTask.WaitUntil(() => _isEnd);
        }

        public void End(SkillArgs.Data args)
        {
            _achievableMasterFlagRegisterer.RegisterFlag(args.Key, args.Id);
            _skillMenuModel.Exit();
            MenuEnd();
            _isEnd = true;
        }


        public void ForceEndFlow()
        {
            _cts.Cancel();
        }


        public void MenuStart()
        {
            _started.OnNext(Unit.Default);
        }
        Subject<Unit> _started = new Subject<Unit>();
        public IObservable<Unit> Started => _started;

        public void MenuEnd()
        {
            _exited.OnNext(Unit.Default);
        }
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> MenuEnded => _exited;


        Subject<int> _onNumberDecided = new Subject<int>();
        public IObservable<int> OnNumberDecided => _onNumberDecided;

    }
}