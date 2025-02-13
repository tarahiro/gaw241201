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
    public class FakeStaticSaveData : ISaveData
    {

        public void SaveString(string key, string value)
        {
            Log.DebugLog("StaticSaveDataSave: " + key + value);
            if (StaticSaveData.Dictionary.ContainsKey(key))
            {
                StaticSaveData.Dictionary[key] = value;
            }
            else
            {
                StaticSaveData.Dictionary.Add(key, value);
            }
        }


        public bool TryGetString(string key, out string value)
        {
            if (StaticSaveData.Dictionary.ContainsKey(key))
            {
                value = StaticSaveData.Dictionary[key];
                Log.DebugLog("StaticSaveDataLoad: " + key + value);
                return true;
            }
            else
            {
                value = "";
                return false;

            }
        }

        public void DeleteSave()
        {
            StaticSaveData.Dictionary = new Dictionary<string, string>();
        }

        internal static class StaticSaveData
        {
            public static Dictionary<string, string> Dictionary = new Dictionary<string, string>();
        }
    }
}