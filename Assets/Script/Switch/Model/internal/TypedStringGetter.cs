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
    public class TypedStringGetter : IByStringGetter
    {
        TypedFlagContainer _container;

        public TypedStringGetter(TypedFlagContainer container ) {
            _container = container;
        }

        public string ByStringGet(string byKey)
        {
            _container.TryGetValue(EnumUtil.KeyToType<TypedFlagContainer.TypedKey>(byKey), out var s);
            return s;
        }

    }
}