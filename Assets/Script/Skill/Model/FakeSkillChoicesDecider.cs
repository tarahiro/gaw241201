using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    //とりあえずbodyIdをstringとして見て、出すようにする
    public class FakeSkillChoicesDecider : ISkillChoicesDecidable
    {
        [Inject] ILeetMasterDataProvider _leetProvider;
        [Inject] IWordMasterDataProvider _wordProvider;
        [Inject] IAchievableMasterFlagProvider _achievableMasterFlagProvider;
        [Inject] SkillAchieveArgsDataFactory _argsFactory;

        const int c_selectedCount = 3;
        public List<SkillArgs.Data> DecideChoices(string bodyId)
        {
            List<List<int>> _availableIndexList = new List<List<int>>();
            List<SkillArgs.Data> _enterableArgs = new List<SkillArgs.Data>();
            int sum = 0;


            switch (bodyId)
            {
                case "Initial":
                    _enterableArgs.Add(_argsFactory.Create(_leetProvider.TryGetFromId("JapaneseEnglish").GetMaster()));
                    _enterableArgs.Add(_argsFactory.Create(_wordProvider.TryGetFromId("cat").GetMaster()));
                    _enterableArgs.Add(_argsFactory.Create(_wordProvider.TryGetFromId("Alice").GetMaster()));
                    break;

                default:

                    foreach (FlagConst.ContainableMasterKey key in Enum.GetValues(typeof(FlagConst.ContainableMasterKey)))
                    {
                        _availableIndexList.Add(new List<int>());

                        for (int i = 0; i < GetCount(key); i++)
                        {
                            if (!_achievableMasterFlagProvider.IsContainskey(key, GetMasterId(key, i)))
                            {
                                _availableIndexList[(int)key].Add(i);
                            }
                        }

                        sum += _availableIndexList[(int)key].Count;
                    }

                    Const.RandomIndexList(out var randomList, sum);

                    for (int i = 0; i < c_selectedCount; i++)
                    {
                        if (randomList[i] < _availableIndexList[(int)FlagConst.ContainableMasterKey.Leet].Count)
                        {
                            _enterableArgs.Add(_argsFactory.Create(_leetProvider.TryGetFromIndex(_availableIndexList[(int)FlagConst.ContainableMasterKey.Leet][randomList[i]]).GetMaster()));

                        }
                        else
                        {
                            _enterableArgs.Add(_argsFactory.Create(_wordProvider.TryGetFromIndex(
                                _availableIndexList[(int)FlagConst.ContainableMasterKey.Word][randomList[i] - _availableIndexList[(int)FlagConst.ContainableMasterKey.Leet].Count]).GetMaster()));
                        }

                    }
                    break;

            }
            return _enterableArgs;

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