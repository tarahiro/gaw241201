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
    public class SkillAchieveModel : ICategoryEnterableModel
    {
        [Inject] ISkillChoicesDecidable _skillChoicesDecideable;
        [Inject] IAchievableMasterFlagRegisterer _achievableMasterFlagRegisterer;



        Subject<SkillArgs> _entered = new Subject<SkillArgs>();
        public IObservable<SkillArgs> Entered => _entered;
        CancellationTokenSource _cts;
        bool _isEnd;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog("スキル獲得開始");
            _cts = new CancellationTokenSource();
            _isEnd = false;


            _entered.OnNext(new SkillArgs(_cts.Token, _skillChoicesDecideable.DecideChoices(bodyId)));

            await UniTask.WaitUntil(() => _isEnd);
        }

        public void End(SkillArgs.Data args)
        {
            _achievableMasterFlagRegisterer.RegisterFlag(args.Key, args.Id);
            _isEnd = true;
        }


        public void ForceEndFlow()
        {
            _cts.Cancel();
        }
    }
}