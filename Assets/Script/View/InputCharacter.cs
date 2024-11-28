using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] TextMeshProUGUI _tmp;

        public void SetCharacter(char c)
        {
            _tmp.text = c.ToString();
        }

        public void Enter()
        {
            Log.DebugWarning("–¢ŽÀ‘• ‚¿‚©‚¿‚©‚³‚¹‚é");
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