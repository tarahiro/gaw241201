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
    //�Z�[�u�f�[�^�ɂȂ��t���O������������N���X
    public class FlagInitializer : IFlagInitializable
    {
        [Inject] IGlobalFlagProvider _provider;
        [Inject] IGlobalFlagRegisterer _registerer;

        public void InitializeFlag()
        {
            foreach (FlagConst.Key key in Enum.GetValues(typeof(FlagConst.Key)))
            {
                if (!_provider.IsContainskey(key))
                {
                    _registerer.RegisterFlag(key, FlagConst.InitialValue(key));
                }
            }
        }
    }
}