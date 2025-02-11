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
        [Inject] InitialParameter _initialParameter;

        public ISaveData Provide()
        {
            if (!_initialParameter.IsFakeSaveData)
            {
                return _saveData;
            }
            else
            {
                return _initialParameter.DummySaveData;
            }
        }
    }
}