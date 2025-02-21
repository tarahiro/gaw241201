using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        [Inject] ICancellationTokenPure _cancellationTokenSource;

        public string GetText()
        {
            return _tmp.text;
        }

        public void SetText(string text)
        {
            _tmp.text = text;
        }


        public void SetPosition(TextMeshProUGUI tmp, int index, Vector3 offset)
        {
            LateSetPosition(tmp, index, offset).Forget();
        }


        async UniTask LateSetPosition(TextMeshProUGUI tmp, int index, Vector3 offset)
        {
            _cancellationTokenSource.SetNew();
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, _cancellationTokenSource.Token);
            transform.localPosition = tmp.GetCharacterLocalPosition(index) + offset;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}