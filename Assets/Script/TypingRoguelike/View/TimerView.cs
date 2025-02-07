using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] RectTransform _timerNeedle;
        CancellationToken _ct;

        public void EnterTimer(TimerArgs args)
        {
            _ct = args.CancellationToken;
            Show();
            UpdateTimer(0);
        }


        public void UpdateTimer(float ratio)
        {
            _timerNeedle.localRotation = Quaternion.Euler(0, 0, -360f * ratio);
        }

        public void EndTimer()
        {
            UnShow();
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