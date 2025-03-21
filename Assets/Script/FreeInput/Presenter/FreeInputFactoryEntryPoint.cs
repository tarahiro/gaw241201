using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class FreeInputFactoryEntryPoint : IInitializable
    {
        [Inject] FreeInputFactorySetting _factorySetting;
        [Inject] FreeInputFactoryName _factoryName;
        [Inject] FreeInputFactoryTime _factoryTime;
        [Inject] FreeInputFactoryBirth _factoryBirth;
        public void Initialize()
        {
            _factorySetting.Initialize();
            _factoryName.Initialize();
            _factoryTime.Initialize();
            _factoryBirth.Initialize();
        }
    }
}