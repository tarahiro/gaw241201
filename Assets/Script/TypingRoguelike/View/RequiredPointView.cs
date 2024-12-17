using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TMPro;

namespace gaw241201.View
{
    public class RequiredPointView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _tmp;

        public void UpdatePoint(int point)
        {
            _tmp.text = point.ToString();
        }
    }
}