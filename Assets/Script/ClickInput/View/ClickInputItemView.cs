using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.Ui;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using MessagePipe;

namespace gaw241201.View
{
    public class ClickInputItemView : MonoBehaviour
    {
        int _index;

        Subject<int> _onClicked = new Subject<int>();
        public IObservable<int> OnClicked => _onClicked;


        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _tmp;
        [SerializeField] TranslationTextView _translationTextView;

        [Inject]
        public void Construct(ISubscriber<int> subscriber)
        {
            _translationTextView.Construct(subscriber);
        }


        public void Initialize(int index, string s)
        {
            _index = index;
            _tmp.text = s;
            _button.onClick.AddListener(() => _onClicked.OnNext(_index));
        }


        public void Exit()
        {
            _button.interactable = false;
        }
    }
}