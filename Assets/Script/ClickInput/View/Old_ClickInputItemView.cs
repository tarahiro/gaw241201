using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using Tarahiro.TInput;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class Old_ClickInputItemView : MonoBehaviour
    {
        IGazable _gazable;
        [SerializeField] List<Button> _button ;

        bool _isWait;
        public void Construct(IGazable gazable)
        {
            _gazable = gazable;
        }

        public async UniTask Enter(CancellationToken ct)
        {
            Log.Comment("ClickInputItemViewŠJŽn");
            foreach (var button in _button)
            {
                button.onClick.AddListener(OnClick);
                button.interactable = true;
            }
            ct.Register(OnExit);

            _isWait = true;

            while (_isWait && !ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                _gazable.Gaze(TTouch.GetInstance().ScreenPointOnThisFrame);
            }


        }

        private void OnClick()
        {
            _isWait = false;
        }

        private void OnExit()
        {
            foreach (var button in _button)
            {
                button.interactable = false;
            }
        }

    }
}