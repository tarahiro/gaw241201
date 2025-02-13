using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SettingMonitorDisplayView : MonoBehaviour
    {
        [SerializeField] GameObject _guide;
        [SerializeField] Animator _animator;


        private void Start()
        {
            _guide.SetActive(false);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            _guide.SetActive(true);
        }

        public void Highlight()
        {
            _animator.SetTrigger("Highlight");
        }

        public void Lowlight()
        {
            _animator.SetTrigger("Idle");
        }
    }
}