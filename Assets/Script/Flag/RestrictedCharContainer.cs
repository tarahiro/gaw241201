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
    public class RestrictedCharContainer : IRestrictedCharProvider
    {
        List<char> _list = new List<char>();

        public List<char> GetRestrictedChar()
        {
            return _list;
        }
    }
}