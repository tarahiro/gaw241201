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
    public class MessageKeyProcessor
    {
        [Inject] IGlobalFlagProvider _flagProvider;
        
        public string ProcessKey(string message)
        {
            Log.Comment("Message����Key�̌����J�n");
            string returnMessage = message;

            //Time
            string timeKey = "<key=ApplicationTime>";
            if (returnMessage.Contains(timeKey))
            {
                Log.Comment("ApplicationTime��������");

                string value = _flagProvider.GetFlag("ApplicationTime");
                string replaceTo = "";

                TimeInDay applicationTid = CreateTimeInDay(value);

                replaceTo += applicationTid.Hour.ToString() + "��";
                replaceTo += applicationTid.Minute.ToString() + "��";
                replaceTo += applicationTid.Second.ToString() + "�b";

                returnMessage = returnMessage.Replace(timeKey, replaceTo);
            }

            //DiffSecond

            string diffSecondKey = "<key=DiffSecond>";
            if (returnMessage.Contains(diffSecondKey))
            {
                Log.Comment("DiffSecond��������");

                TimeInDay applicationTid = CreateTimeInDay(_flagProvider.GetFlag("ApplicationTime"));
                TimeInDay inputTid = CreateTimeInDay(_flagProvider.GetFlag("InputTime"));

                int diff = Mathf.Abs(applicationTid.GetAllSecond() - inputTid.GetAllSecond());
                

                returnMessage = returnMessage.Replace(diffSecondKey, diff.ToString());
            }

            return returnMessage;
        }

        class TimeInDay
        {
            internal int Hour;
            internal int Minute;
            internal int Second;

            internal TimeInDay(int hour, int minute, int second)
            {
                Hour = hour;
                Minute = minute;
                Second = second;
            }

            internal int GetAllSecond()
            {
                return Hour*3600 + Minute*60 + Second;
            }
        }

        TimeInDay CreateTimeInDay(string value)
        {
            return new TimeInDay(
               int.Parse(value.Substring(0, 2)),
               int.Parse(value.Substring(2, 2)),
               int.Parse(value.Substring(4, 2))
                );
        }

        
    }
}