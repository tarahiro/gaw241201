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
        [SerializeField] BlinkableCursor _underBar;

        [Inject] IFreeInputMessagePublisher _messagePublisher;
        
        CancellationTokenSource _cancellationTokenSource;


        public void SetCharacter(char c)
        {
            SoundManager.PlaySE("Key");
            _tmp.text = c.ToString();
        }

        public void ClearCharacter()
        {
            _tmp.text = null;
        }

        public void Focus()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _underBar.StartBlink();

            _messagePublisher.OnType(Camera.main.WorldToScreenPoint(transform.position));
           // _gazable.Gaze(Camera.main.WorldToScreenPoint(transform.position));
           
        }

        public void UnFocus()
        {
            _cancellationTokenSource.Cancel();
            _underBar.StopBlink();

        }


        public void Enter(IFreeInputMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
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