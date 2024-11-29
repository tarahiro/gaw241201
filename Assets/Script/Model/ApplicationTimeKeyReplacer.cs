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
            Log.Comment("ApplicationTime��������");

            string value = _flagProvider.GetFlag("ApplicationTime");
            string replaceTo = "";

            TimeInDay applicationTid = CreateTimeInDay(value);

            replaceTo += applicationTid.Hour.ToString() + "��";
            replaceTo += applicationTid.Minute.ToString() + "��";
            replaceTo += applicationTid.Second.ToString() + "�b";

            return replaceTo;
        }
    }
}