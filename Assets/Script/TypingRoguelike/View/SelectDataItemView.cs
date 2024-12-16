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

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        Vector3 _targetLocalPosition;

        public string GetText()
        {
            return _tmp.text;
        }

        public void SetText(string text)
        {
            _tmp.text = text;
        }

        /*

        public void SetPosition(Vector3 targetLocalPosition)
        {
            _targetLocalPosition = targetLocalPosition;
            LateSetPosition().Forget();
        }
        */

        public void SetPosition(TextMeshProUGUI tmp, int index, Vector3 offset)
        {
            LateSetPosition(tmp, index, offset).Forget();
        }


        async UniTask LateSetPosition(TextMeshProUGUI tmp, int index, Vector3 offset)
        {
            await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, _cancellationTokenSource.Token);
            transform.localPosition = tmp.GetCharacterLocalPosition(index) + offset;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}