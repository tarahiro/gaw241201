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
    public class RegisterFlagOrderProcessor
    {
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        public void ProcessRegisterOrder(string order)
        {
            switch (order)
            {
                case "ReadTime":
                    DateTime dateTime = DateTime.Now;
                    int hour = dateTime.Hour;
                    int minute = dateTime.Minute;
                    int second = dateTime.Second;

                    string s = "";
                    s += hour.ToString("D2");
                    s += minute.ToString("D2");
                    s += second.ToString("D2");
                    _globalFlagRegisterer.RegisterFlag("ApplicationTime", s);
                    break;

                default:
                    Log.DebugAssert(order + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    break;
            }
        }
    }
}