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
        public string ReplaceTo()
        {
            Log.Comment("ApplicationTimeèëÇ´ä∑Ç¶");

            string value = _flagProvider.GetFlag("ApplicationTime");
            string replaceTo = "";

            TimeInDay applicationTid = CreateTimeInDay(value);

            replaceTo += applicationTid.Hour.ToString() + "éû";
            replaceTo += applicationTid.Minute.ToString() + "ï™";
            replaceTo += applicationTid.Second.ToString() + "ïb";

            return replaceTo;
        }
    }
}