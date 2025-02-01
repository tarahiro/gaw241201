using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
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
        [Inject] StageBgView _stageBgView;

        public void Start()
        {

        }

        public void Tick()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {

                if (Input.GetKeyDown(KeyCode.A))
                {
                    Log.Comment("強制的に全スキル追加");

                    for (int i = 0; i < _leetMasterDataProvider.Count; i++)
                    {
                        _achievableMasterFlagRegisterer.RegisterFlag(FlagConst.ContainableMasterKey.Leet, _leetMasterDataProvider.TryGetFromIndex(i).Id);
                    }
                    for (int i = 0; i < _wordMasterDataProvider.Count; i++)
                    {
                        _achievableMasterFlagRegisterer.RegisterFlag(FlagConst.ContainableMasterKey.Word, _wordMasterDataProvider.TryGetFromIndex(i).Id);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    _stageBgView.Enter(new ActBgViewArgs("Company", 10, new System.Threading.CancellationToken()));
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    _stageBgView.ToNext();
                }
            }
        }
    }
#endif
}