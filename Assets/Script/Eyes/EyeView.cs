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
        [SerializeField] Transform _eyeContour;

        public void SetEyePosition(Vector2 localPosition)
        {
            _eyeball.localPosition = localPosition;
        }

        public void SetScale(float f)
        {
            _eyeball.localScale = Vector3.one * f;
            _eyeContour.localScale = Vector3.one * f;
        }

        public void SetRotation(Vector3 EulerAngle)
        {
            _eyeContour.localRotation = Quaternion.Euler(EulerAngle);   
        }
    }
}