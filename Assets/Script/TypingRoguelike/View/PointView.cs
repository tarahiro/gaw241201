using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class PointView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _tmp;

        public void UpdatePoint(int point)
        {
            _tmp.text = point.ToString();
        }

        public void Initialize()
        {
            Show();
        }

        GameObject _root;

        void Start()
        {
            _root = transform.Find("Root").gameObject;
            UnShow();
        }

        void Show()
        {
            _root.SetActive(true);
        }

        void UnShow()
        {
            _root.SetActive(false);

        }
    }
}