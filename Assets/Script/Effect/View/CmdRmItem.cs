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
    public class CmdRmItem : MonoBehaviour, IEffectItemView
    {

        [SerializeField] CmdLine _cmdLinePrefab;
        [SerializeField] Transform _lineRoot;

        public bool IsAutoEnd => false;
        CmdLine _currentCmdLine;
        int _lineIndex = 0;

        const int c_maxDisplayLineNumber = 20;

        CancellationTokenSource ContinuousCts;

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => OnExit());

            while (_lineIndex < c_maxDisplayLineNumber && !cancellationToken.IsCancellationRequested)
            {
                await ShowLine(_lineIndex, cancellationToken);
                _lineIndex++;
            }

            OnExit();
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
            _currentCmdLine.Unfoucus();
        }

        void OnExit()
        {
            ContinuousCts = new CancellationTokenSource();
            ContinuousLineLoop(ContinuousCts.Token).Forget();

        }

        const float c_textShowTime = 1f;
        const float c_waitForNextLineTime = .5f;

        const float c_lineInterval = 18f;

        async UniTask ContinuousLineLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) 
            {
                await ShowLine(_lineIndex, cancellationToken);
                _lineIndex++;
            }
        }

        async UniTask ShowLine(int lineIndex, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _currentCmdLine.Unfoucus());
            _currentCmdLine = Instantiate(_cmdLinePrefab, _lineRoot);

            if(_lineIndex > c_maxDisplayLineNumber)
            {
                _lineRoot.GetComponent<RectTransform>().anchoredPosition += Vector2.up * c_lineInterval;
            }

            _currentCmdLine.GetComponent<RectTransform>().anchoredPosition = Vector2.down * c_lineInterval * lineIndex;

            _currentCmdLine.Construct(GetLineText(lineIndex));
            await UniTask.WaitForSeconds(c_textShowTime * WaitCoeff(lineIndex), cancellationToken: cancellationToken);
            _currentCmdLine.SetLine();
            await UniTask.WaitForSeconds(c_waitForNextLineTime * WaitCoeff(lineIndex), cancellationToken: cancellationToken);

            _currentCmdLine.Unfoucus();
        }


        const string c_deleteFilePrefix = "Delete File: ";
        const string c_deleteFileSuffix = "%";

        string GetLineText(int lineIndex)
        {
            if(lineIndex == 0)
            {
                return "rm rf /";
            }
            else
            {
                return c_deleteFilePrefix + ((lineIndex - 1) / 100f).ToString("00.00") + c_deleteFileSuffix;
            }
        }

        const float c_maxWaitTimeCoeff = 1f;
        const float c_minWaitTimeCoeff = .01f;
        const float c_waitTimeCoeffChangeIndex = 10;

        float WaitCoeff(int lineIndex)
        {
            float c = Mathf.Max((c_waitTimeCoeffChangeIndex - lineIndex) / (float)c_waitTimeCoeffChangeIndex, 0f);
            c = c * c;
            return c_minWaitTimeCoeff + (c_maxWaitTimeCoeff - c_minWaitTimeCoeff) * c;
        }

    }
}