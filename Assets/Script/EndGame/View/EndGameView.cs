using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using UnityEngine.UI;

namespace gaw241201.View
{
    public class EndGameView : MonoBehaviour
    {
        //[SerializeField] Button _button;
        [SerializeField] GameObject _root;
        [SerializeField] List<GameObject> _label;

        Subject<Unit> _clicked = new Subject<Unit>();
        public IObservable<Unit> Clicked => _clicked;

        private void Start()
        {
            _root.SetActive(false);
            foreach (var v in _label)
            {
                v.SetActive(false);
            }
           // _button.onClick.AddListener(OnClick);
        }

        public void Enter(EndGameConst.Key key)
        {
            Log.Comment(key);
            _label[(int)key].SetActive(true);
            _root.SetActive(true);
        }

        void OnClick()
        {
            _clicked.OnNext(Unit.Default);
        }
    }
}