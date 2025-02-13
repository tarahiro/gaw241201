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
    public class SaveDataProvider : ISaveDataProvider
    {
        [Inject] SaveData _saveData;
        [Inject] FakeStaticSaveData _fakeStaticSaveData;
        [Inject] InitialParameter _initialParameter;

        public ISaveData Provide()
        {
            if (_initialParameter.UseDummySaveData)
            {
                if (GlobalStaticFlag.IsSkipTitle)
                {
                    return _fakeStaticSaveData;
                }
                else
                {
                    return _initialParameter.DummySaveData;
                }
            }
            else
            {
                return _fakeStaticSaveData;
            }
        }
    }
}