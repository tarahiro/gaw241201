using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class FreeInputDisplayFlowView : MonoBehaviour, IFreeInputDisplayView
    {
        [SerializeField] List<InputCharacter> _characterList;
        [SerializeField] GameObject _root;

        void Start()
        {
            Exit().Forget();
        }

        public void SetCharacter(FreeInputArgs args)
        {
            _characterList[args.Index].SetCharacter(args.Character);
        }

        public async UniTask Enter()
        {
            _root.SetActive(true);
        }

        public async UniTask Exit()
        {
            _root.SetActive(false);
        }

        public void Focus(int index)
        {
            _characterList[index].Focus();
        }

        public void Unfocus(int index)
        {
            _characterList[index].UnFocus();
        }
    }
}