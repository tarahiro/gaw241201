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
    public class FreeInputTextDisplayView : MonoBehaviour
    {
        [SerializeField] List<InputCharacter> _characterList;

        public void SetText(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                _characterList[i].SetCharacter(text[i]);
            }
        }

        public void Focus(int index)
        {
            Log.Comment("Focus");
            _characterList[index].Focus();
        }

        public void Unfocus(int index)
        {
            _characterList[index].UnFocus();
        }
    }
}