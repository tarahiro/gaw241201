using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SelectDataItemView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _tmp;

        public string GetText()
        {
            return _tmp.text;
        }

        public void SetText(string text)
        {
            _tmp.text = text;
        }
    }
}