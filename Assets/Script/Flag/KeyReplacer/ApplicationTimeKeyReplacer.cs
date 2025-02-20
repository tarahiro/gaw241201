using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.DayTimeUtil;

namespace gaw241201
{
    public class ApplicationTimeKeyReplacer : IKeyReplacer
    {
        [Inject] IGlobalFlagProvider _flagProvider;
        [Inject] ILanguageMessageMasterDataProvider _languageMasterDataProvider;
        [Inject] LanguageModel _languageModel;
        public string ReplaceTo(FlagConst.MessageKey key)
        {
            Log.Comment("ApplicationTimeèëÇ´ä∑Ç¶");

            string value = _flagProvider.GetFlag(FlagConst.Key.ApplicationTime);
            string replaceTo = "";

            TimeInDay applicationTid = CreateTimeInDay(value);

            int _languageIndex = (int)_languageModel.Language;

            replaceTo += applicationTid.Hour.ToString() + _languageMasterDataProvider.TryGetFromId("Hour").GetMaster().Message.GetTranslatedText(_languageIndex);
            replaceTo += applicationTid.Minute.ToString() + _languageMasterDataProvider.TryGetFromId("Minute").GetMaster().Message.GetTranslatedText(_languageIndex);
            replaceTo += applicationTid.Second.ToString() + _languageMasterDataProvider.TryGetFromId("Second").GetMaster().Message.GetTranslatedText(_languageIndex);

            return replaceTo;
        }
    }
}