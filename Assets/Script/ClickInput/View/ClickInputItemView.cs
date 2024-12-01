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
    public class ClickInputItemView : MonoBehaviour
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

            await UniTask.WaitUntil(() => !_isWait,cancellationToken:ct);

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