using Cysharp.Threading.Tasks;
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
    public class UiMenuItemModelLanguage : IUiMenuItemModel
    {
        [Inject] LanguageModel _languageModel;
        public void Enter()
        {
            _languageModel.SetLanguage(EnumUtil.NoToType<LanguageConst.AvailableLanguage>(((int)_languageModel.Language + 1) % LanguageConst.AvailableLanguageNumber));
        }
    }
}