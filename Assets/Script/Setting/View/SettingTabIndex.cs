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
    public class SettingTabIndex : MonoBehaviour
    {
        [SerializeField] GameObject LowlightedObject;
        [SerializeField] GameObject _root;

        const float c_lowlightLength = 16f;

        public void Awake()
        {
            Lowlight();
        }

        public void Highlight()
        {
            _root.transform.localPosition = Vector2.zero;
        }

        public void Lowlight()
        {
            _root.transform.localPosition = Vector2.down * c_lowlightLength;
        }

        public void OnSetEnabled(bool b)
        {
            if (b)
            {
                LowlightedObject.SetActive(false);
                Highlight();
            }
            else
            {
                LowlightedObject.SetActive(true);
                Lowlight();
            }
        }
    }
}