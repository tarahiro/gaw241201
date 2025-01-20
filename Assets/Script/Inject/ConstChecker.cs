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
#if ENABLE_DEBUG
    public class ConstChecker : IInitializable
    {
        public void Initialize()
        {
            if(LanguageConst.AvailableLanguageNumber == EnumUtil.GetTypeNum<LanguageConst.AvailableLanguage>())
            {
                Log.DebugLog("Language��Const�`�F�b�N����");
            }
            else
            {
                Log.DebugAssert("Language��Const���s���ł�");
            }
        }
    }
#endif
}