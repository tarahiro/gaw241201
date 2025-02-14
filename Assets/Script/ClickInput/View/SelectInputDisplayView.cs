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
    public class SelectInputDisplayView : MonoBehaviour
    {
        [SerializeField] GameObject _root;
        [SerializeField] GameObject _cursor;
        [SerializeField] Transform[] _button;

        private void Start()
        {
            _root.SetActive(false);
        }

        public void Enter(int index)
        {
            _root.SetActive(true);
            _cursor.SetActive(true);
            Focus(index);
        }

        public void Exit()
        {
            _root.SetActive(false);
            _cursor.SetActive(false);
        }

        public void Focus(int index)
        {
            _cursor.transform.localPosition = _button[index].localPosition + Vector3.left * 50f;
        }
    }
}