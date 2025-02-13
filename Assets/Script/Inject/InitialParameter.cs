using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    [CreateAssetMenu]
    [System.Serializable]
    public class InitialParameter : ScriptableObject
    {
        public bool IsFakeMainLoopEnabled = false;
        public FlowMasterConst.FlowMasterLabel FakeMainLoop;
        public LanguageConst.AvailableLanguage Language;
        public StartOptionKey StartOption;

        public bool UseDummySaveData = true;

        [SerializeField]
        public DummySaveData DummySaveData;

        [System.Serializable]
        public enum StartOptionKey
        {
            Normal,
            IsSkipTitle,
        }
    }
}