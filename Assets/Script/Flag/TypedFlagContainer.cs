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
    public class TypedFlagContainer
    {
        bool _isTypedNameRegistered = false;
        string _typedName;

        public void RegisterTypedName(string name)
        {
            Log.DebugLog("Register");
            _typedName = name;
            _isTypedNameRegistered = true;
        }

        public bool TryGetTypedName(out string typedName)
        {
            if (_isTypedNameRegistered)
            {
                typedName = _typedName;
                return true;
            }
            else
            {
                typedName = "";
                return false;
            }
        }

        public enum TypedKey
        {
            TypedName,
        }
    }
}