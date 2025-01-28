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
        [SerializeField] GameObject HighlightedObject;
        [SerializeField] GameObject LowlightedObject;

        public void Highlight()
        {
            HighlightedObject.SetActive(true);
            LowlightedObject.SetActive(false);
        }

        public void Lowlight()
        {
            HighlightedObject.SetActive(false);
            LowlightedObject.SetActive(true);
        }
    }
}