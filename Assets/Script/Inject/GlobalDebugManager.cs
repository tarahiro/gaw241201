using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class GlobalDebugManager : ITickable, IStartable
    {
        [Inject] FlowHundler _flowHundler;
        [Inject] ISaveDeletable _saveDeletable;
        [Inject] LanguageModel _languageModel;
        [Inject] EnterEffectModel _effectModel;

        public void Start()
        {

        }

        public void Tick()
        {

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _flowHundler.ForceGetCurrentFlow().ForceEndFlow();
                }


                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _saveDeletable.DeleteSaveData();
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    _languageModel.SetLanguage(EnumUtil.NoToType<LanguageConst.AvailableLanguage>(((int)_languageModel.Language + 1) % LanguageConst.AvailableLanguageNumber));
                }

                if (Input.GetKeyDown(KeyCode.E)) {
                    _effectModel.EnterFlow("GlitchLargeAutoEnd").Forget();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _effectModel.EnterFlow("GlitchSmall").Forget();
                }

            }
        }
    }
#endif
}