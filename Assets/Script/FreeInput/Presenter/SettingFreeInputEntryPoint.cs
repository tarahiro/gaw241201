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
    public class SettingFreeInputEntryPoint : IInitializable
    {
        [Inject] SettingFreeInputFactory _factory;
        public void Initialize()
        {
            _factory.Initialize();
        }
    }
}