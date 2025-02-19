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

        Dictionary<TypedKey,string> _typedFlagDictionary = new Dictionary<TypedKey,string>();

        public void Register(TypedKey key, string value)
        {
            if (_typedFlagDictionary.ContainsKey(key))
            {
                _typedFlagDictionary[key] = value;
            }
            else
            {
                _typedFlagDictionary.Add(key, value);
            }
        }

        public bool TryGetValue(TypedKey type, out string value)
        {
            return _typedFlagDictionary.TryGetValue(type, out value);
        }


        /*





        bool _isTypedNameRegistered = false;
        string _typedName;

        bool _isHolderegistered = false;
        string _Holder;

        bool _isAnimalegistered = false;
        string _Animal;

        bool _isRightegistered = false;
        string _Right;

        public void RegisterTypedName(string name)
        {
            Log.DebugLog("Register");
            _typedName = name;
            _isTypedNameRegistered = true;
        }

        public void RegisterHolder(string holder)
        {
            _Holder = holder;
            _isHolderegistered = true;
        }

        public void RegisterAnimal(string Animal)
        {
            _Animal = Animal;
            _isAnimalegistered = true;
        }
        public void RegisterRight(string Right)
        {
            _Right = Right;
            _isRightegistered = true;
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

        public bool TryGetHolder(out string holder)
        {
            if (_isHolderegistered)
            {
                holder = _Holder;
                return true;
            }
            else
            {
                holder = "";
                return false;
            }
        }
        public bool TryGetAnimal(out string Animal)
        {
            if (_isAnimalegistered)
            {
                Animal = _Animal;
                return true;
            }
            else
            {
                Animal = "";
                return false;
            }
        }
        public bool TryGetRight(out string Right)
        {
            if (_isRightegistered)
            {
                Right = _Right;
                return true;
            }
            else
            {
                Right = "";
                return false;
            }
        }
        */

        public enum TypedKey
        {
            Name,
            Holder,
            Animal,
            Direction,
        }
    }
}