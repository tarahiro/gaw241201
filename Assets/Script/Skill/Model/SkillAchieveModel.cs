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
    public class SkillAchieveModel : IFlowModel
    {
        [Inject] ILeetMasterDataProvider _leetProvider;
        [Inject] IWordMasterDataProvider _wordProvider;
        [Inject] IAchievableMasterFlagProvider _achievableMasterFlagProvider;
        [Inject] IAchievableMasterFlagRegisterer _achievableMasterFlagRegisterer;

        const int c_selectedCount = 3;

        [Inject] SkillAchieveArgsDataFactory _argsFactory;

        Subject<SkillArgs> _entered = new Subject<SkillArgs>();
        public IObservable<SkillArgs> Entered => _entered;
        CancellationTokenSource _cts;
        bool _isEnd;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog("スキル獲得開始");
            _cts = new CancellationTokenSource();
            _isEnd = false;

            //どのスキル(Leet,Word)を獲得するか抽選

            List<List<int>> _availableIndexList = new List<List<int>>();
            int sum = 0;

            foreach(FlagConst.ContainableMasterKey key in Enum.GetValues(typeof(FlagConst.ContainableMasterKey)))
            {
                _availableIndexList.Add(new List<int>());

                for (int i = 0; i < GetCount(key); i++)
                {
                    if (!_achievableMasterFlagProvider.IsContainskey(key, GetMasterId(key, i))) {
                        _availableIndexList[(int)key].Add(i);
                    }
                }

                sum += _availableIndexList[(int)key].Count;
            }

            Const.RandomIndexList(out var randomList, sum);

            List<SkillArgs.Data> _enterableArgs = new List<SkillArgs.Data>();

            for(int i = 0; i < c_selectedCount; i++)
            {
                if(randomList[i] < _availableIndexList[(int)FlagConst.ContainableMasterKey.Leet].Count)
                {
                    _enterableArgs.Add(_argsFactory.Create(_leetProvider.TryGetFromIndex(_availableIndexList[(int)FlagConst.ContainableMasterKey.Leet][randomList[i]]).GetMaster()));

                }
                else
                {
                    _enterableArgs.Add(_argsFactory.Create(_wordProvider.TryGetFromIndex(
                        _availableIndexList[(int)FlagConst.ContainableMasterKey.Word][randomList[i] - _availableIndexList[(int)FlagConst.ContainableMasterKey.Leet].Count]).GetMaster()));
                }

            }

            _entered.OnNext(new SkillArgs(_cts.Token, _enterableArgs));

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

        public int GetCount(FlagConst.ContainableMasterKey masterKey)
        {
            switch (masterKey)
            {
                case FlagConst.ContainableMasterKey.Leet:
                    return _leetProvider.Count;

                case FlagConst.ContainableMasterKey.Word:
                    return _wordProvider.Count;

                default:
                    Log.DebugAssert(masterKey + ": 不正な値です");
                    return -1;
            }
        }

        public string GetMasterId(FlagConst.ContainableMasterKey masterKey, int index)
        {
            switch (masterKey)
            {
                case FlagConst.ContainableMasterKey.Leet:
                    return _leetProvider.TryGetFromIndex(index).Id;

                case FlagConst.ContainableMasterKey.Word:
                    return _wordProvider.TryGetFromIndex(index).Id;

                default:
                    Log.DebugAssert(masterKey + ": 不正な値です");
                    return "error";
            }
        }
    }
}