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
    public class FreeInputEndableDisplayView : MonoBehaviour, IFreeInputEndableDisplayView
    {
        [SerializeField] GameObject _endableObject;

        private void Start()
        {
            Endable(false).Forget();
        }

        public async UniTask Endable(bool b)
        {
            _endableObject.SetActive(b);
        }
    }
}