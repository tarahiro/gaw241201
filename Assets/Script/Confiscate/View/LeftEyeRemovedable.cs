using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class LeftEyeRemovedable : MonoBehaviour, IRemovedable
    {
        [SerializeField] GameObject _eyeBlank;

        public void RemoveParts()
        {
            _eyeBlank.SetActive(true);
        }
    }
}