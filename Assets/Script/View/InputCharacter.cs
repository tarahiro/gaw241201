using Cysharp.Threading.Tasks;
using LitMotion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.TextCore.Text;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class InputCharacter : MonoBehaviour
    {
        IGazable _gazable;

        [SerializeField] TextMeshProUGUI _tmp;
        [SerializeField] GameObject _underBar;
        
        CancellationTokenSource _cancellationTokenSource;


        public void SetCharacter(string s)
        {
            _tmp.text = s[0].ToString();
        }

        public void ClearCharacter()
        {
            _tmp.text = null;
        }

        public void Focus()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Blink(_cancellationTokenSource.Token).Forget();

            _gazable.Gaze(transform.position);
           
        }

        public void UnFocus()
        {
            _cancellationTokenSource.Cancel();
            _underBar.SetActive(true);

        }

        async UniTask Blink(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {

                _underBar.SetActive(true);
                await UniTask.WaitForSeconds(0.5f, cancellationToken: ct);
                _underBar.SetActive(false);
                await UniTask.WaitForSeconds(0.2f, cancellationToken: ct);
            }
        }

        public void Enter(IGazable gazable)
        {
            _gazable = gazable;
        }

        public bool TryGetCharacter(out char c)
        {
            if(_tmp.text == "")
            {
                c = '0';
                return false;
            }
            else
            {
                c = _tmp.text[0];
                return true;
            }
        }
    }
}