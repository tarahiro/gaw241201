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
    public class BlinkableCursor : MonoBehaviour
    {
        [SerializeField] GameObject _cursor;
        [SerializeField] bool _isDefaultShow = true;

        CancellationTokenSource cancellationTokenSource = null;

        private void LateStart()
        {
            _cursor.SetActive(_isDefaultShow);
        }

        public void StartBlink()
        {
            cancellationTokenSource = new CancellationTokenSource();

            Main(cancellationTokenSource.Token).Forget();
        }

        async UniTask Main(CancellationToken ct)
        {
            ct.Register(() => _cursor.SetActive(_isDefaultShow));

            while (!ct.IsCancellationRequested)
            {

                _cursor.SetActive(true);
                await UniTask.WaitForSeconds(0.5f, cancellationToken: ct);
                _cursor.SetActive(false);
                await UniTask.WaitForSeconds(0.2f, cancellationToken: ct);

            }
        }

        public void StopBlink()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
            }
            _cursor.SetActive(_isDefaultShow);
        }

        public void EraseCursor()
        {
            Log.DebugAssert(cancellationTokenSource == null || cancellationTokenSource.IsCancellationRequested);
            _cursor.SetActive(false);
        }
    }
}