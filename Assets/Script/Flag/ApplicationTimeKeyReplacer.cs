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
        public string ReplaceTo(FlagConst.MessageKey key)
        {
            Log.Comment("ApplicationTime書き換え");

            string value = _flagProvider.GetFlag(FlagConst.Key.ApplicationTime);
            string replaceTo = "";

            TimeInDay applicationTid = CreateTimeInDay(value);

            replaceTo += applicationTid.Hour.ToString() + "時";
            replaceTo += applicationTid.Minute.ToString() + "分";
            replaceTo += applicationTid.Second.ToString() + "秒";

            return replaceTo;
        }
    }
}