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

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class TypingRoguelikeDebugManager : ITickable, IStartable
    {
        [Inject] ILeetMasterDataProvider _leetMasterDataProvider;
        [Inject] IWordMasterDataProvider _wordMasterDataProvider;
        [Inject] IAchievableMasterFlagRegisterer _achievableMasterFlagRegisterer;

        public void Start()
        {

        }

        public void Tick()
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Log.Comment("�����I�ɑS�f�o�b�O�R�}���h�ǉ�");

                for (int i = 0; i < _leetMasterDataProvider.Count; i++)
                {
                    _achievableMasterFlagRegisterer.RegisterFlag(FlagConst.ContainableMasterKey.Leet, _leetMasterDataProvider.TryGetFromIndex(i).Id);
                }
                for (int i = 0; i < _wordMasterDataProvider.Count; i++)
                {
                    _achievableMasterFlagRegisterer.RegisterFlag(FlagConst.ContainableMasterKey.Word, _wordMasterDataProvider.TryGetFromIndex(i).Id);
                }
            }
        }
    }
#endif
}