using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SettingFreeInputDisplayView : MonoBehaviour, IFreeInputDisplayView
    {
        [SerializeField] List<InputCharacter> _characterList;
        [SerializeField] GameObject _focusFrame;

        public void SetText(string text)
        {
            for (int i = 0; i < _characterList.Count ; i++)
            {
                if (i < text.Length)
                {
                    _characterList[i].SetCharacter(text[i],false);
                }
                else
                {
                    //‚È‚º‚©ClearCharacter‚ÅãŽè‚­‚¢‚©‚È‚¢
                    _characterList[i].SetCharacter(' ',false);
                }
            }
        }

        public void SetCharacter(FreeInputArgs args)
        {
            _characterList[args.Index].SetCharacter(args.Character);
        }

        public async UniTask Enter()
        {
            _focusFrame.SetActive(true);
        }

        public async UniTask Exit()
        {
            _focusFrame.SetActive(false);
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