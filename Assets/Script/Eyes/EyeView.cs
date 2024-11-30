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
    public class EyeView : MonoBehaviour
    {
        [SerializeField] Transform _eyeball;

        public void SetEyePosition(Vector2 localPosition)
        {
            _eyeball.localPosition = localPosition;
        }
    }
}