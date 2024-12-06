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

        const int c_maxDisplayLineNumber = 1;

        CancellationTokenSource ContinuousCts;

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => OnExit());

            while (_lineIndex < c_maxDisplayLineNumber && !cancellationToken.IsCancellationRequested)
            {
                await ShowLine(GetLineText(_lineIndex), WaitCoeff(_lineIndex), cancellationToken);
            }

            OnExit();
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
            ContinuousCts.Cancel();
            _lineIndex++;

            await ShowLine("Process Stopped.", 1f, cancellationToken);
            await UniTask.WaitForSeconds(2f, cancellationToken: cancellationToken);
        }

        void OnExit()
        {
            ContinuousCts = new CancellationTokenSource();
            ContinuousLineLoop(ContinuousCts.Token).Forget();
        }

        const float c_textShowTime = 1f;
        const float c_waitForNextLineTime = .5f;

        const float c_lineInterval = 18f;
        const int c_maxIndex = 10001;

        async UniTask ContinuousLineLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested) 
            {
                if (_lineIndex < c_maxIndex)
                {
                    await ShowLine(GetLineText(_lineIndex), WaitCoeff(_lineIndex), cancellationToken);
                }
                else
                {
                    await ShowLine(GetLineText(c_maxIndex), WaitCoeff(c_maxIndex), cancellationToken);
                }
            }
        }

        async UniTask ShowLine(string lineText, float waitCoeffTime, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _currentCmdLine.Unfoucus());
            _currentCmdLine = Instantiate(_cmdLinePrefab, _lineRoot);

            if(_lineIndex > c_maxDisplayLineNumber)
            {
                _lineRoot.GetComponent<RectTransform>().anchoredPosition += Vector2.up * c_lineInterval;
            }

            _currentCmdLine.GetComponent<RectTransform>().anchoredPosition = Vector2.down * c_lineInterval * _lineIndex;

            _currentCmdLine.Construct(lineText);
            await UniTask.WaitForSeconds(c_textShowTime * waitCoeffTime, cancellationToken: cancellationToken);
            _currentCmdLine.SetLine();
            await UniTask.WaitForSeconds(c_waitForNextLineTime * waitCoeffTime, cancellationToken: cancellationToken);

            _lineIndex++;
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