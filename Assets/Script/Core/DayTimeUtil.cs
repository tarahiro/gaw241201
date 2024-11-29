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
    public static class DayTimeUtil
    {
        public class TimeInDay
        {
            public int Hour;
            public int Minute;
            public int Second;

            internal TimeInDay(int hour, int minute, int second)
            {
                Hour = hour;
                Minute = minute;
                Second = second;
            }

            public int GetAllSecond()
            {
                return Hour * 3600 + Minute * 60 + Second;
            }
        }

        public static TimeInDay CreateTimeInDay(string value)
        {
            return new TimeInDay(
               int.Parse(value.Substring(0, 2)),
               int.Parse(value.Substring(2, 2)),
               int.Parse(value.Substring(4, 2))
                );
        }
    }
}