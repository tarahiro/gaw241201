using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Linq;

namespace gaw241201
{
    [CreateAssetMenu()]
    [System.Serializable]
    public class DummySaveData :ScriptableObject, ISaveData
    {
        [SerializeField] List<Pair> data;

        public void SaveString(string key, string value)
        {
            Log.DebugWarning("DummyÇ»ÇÃÇ≈Ç»Ç…Ç‡ÉZÅ[ÉuÇ≥ÇÍÇ»Ç¢");
        }

        public bool TryGetString(string key, out string value)
        {
            FlagConst.Key sKey = EnumUtil.KeyToType<FlagConst.Key>(key);
            if (data.Exists(x => x.Key == sKey))
            {
                value = data.Find(x => x.Key == sKey).Value;
                return true;
            }
            else
            {
                value = string.Empty;
                return false;
            }
        }

        public void DeleteSave()
        {
            Log.DebugWarning("DummyÇ»ÇÃÇ≈Ç»Ç…Ç‡è¡Ç¶Ç»Ç¢");
        }

        [System.Serializable]
        public class Pair
        {
            [SerializeField] public FlagConst.Key Key;
            [SerializeField] public string Value;
        }
    }
}