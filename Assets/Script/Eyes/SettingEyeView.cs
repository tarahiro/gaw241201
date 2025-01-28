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
    public class SettingEyeView: MonoBehaviour
    {
        [SerializeField] RectTransform _eyeball;
        [SerializeField] RectTransform _eyeContour;

        [SerializeField] GameObject _normalEye;
        [SerializeField] GameObject _realEye;
        [SerializeField] GameObject _goatEye;

        public void SetEyePosition(Vector2 localPosition)
        {
            _eyeball.anchoredPosition = localPosition;
        }

        public void SetScale(float f)
        {
            _eyeContour.localScale = Vector3.one * f;
        }

        public void SetRotation(Vector3 EulerAngle)
        {
            _eyeContour.localRotation = Quaternion.Euler(EulerAngle);
        }

        public void SetEyeParts(EffectConst.EyeParts parts)
        {
            switch (parts)
            {
                case EffectConst.EyeParts.Normal:
                    _normalEye.SetActive(true);
                    _realEye.SetActive(false);
                    _goatEye.SetActive(false);
                    break;

                case EffectConst.EyeParts.Real:
                    _realEye.SetActive(true);
                    break;

                case EffectConst.EyeParts.Goat:
                    _normalEye.SetActive(false);
                    _goatEye.SetActive(true);
                    break;
            }
        }
    }
}