using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using static PlasticPipe.PlasticProtocol.Messages.NegotiationCommand;

namespace Tarahiro.TInput
{
    public class MonoKey : MonoBehaviour
    {
        List<KeyCode> _keyListDowned = new List<KeyCode>();
        List<KeyCode> _keyListKeyed = new List<KeyCode>();
        List <KeyCode> _keyListUped = new List<KeyCode>();

        private void Update()
        {
            if (_keyListDowned.Count > 0)
            {
                Log.DebugLog("keydownのリストをリセット");
                _keyListDowned = new List<KeyCode>();
            }
            _keyListKeyed = new List<KeyCode>();
            _keyListUped = new List<KeyCode>();

            foreach (KeyCode key in InputtableKeyList)
            {
                if (Input.GetKeyDown(key))
                {
                    Log.DebugLog(key + "のKeyDownを登録");
                    _keyListDowned.Add(key);
                }
                if(Input.GetKey(key)) _keyListKeyed.Add(key);
                if(Input.GetKeyUp(key)) _keyListUped.Add(key);
            }
        }

        public bool IsKeyDown(KeyCode key)
        {
            if (TInput.GetInstance().IsAnyAvailableInputted)
            {
                Log.DebugLog("すでにインプット消化済み" + key);
                return false;
            }
            else
            {
                if (_keyListDowned.Any(x => x == key))
                {
                    Log.DebugLog(key + "がKeyDownされていることを取得");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsKey(KeyCode key)
        {
            if (TInput.GetInstance().IsAnyAvailableInputted) return false;

            return _keyListKeyed.Any(x => x == key);
        }

        public bool IsKeyUp(KeyCode key)
        {
            if (TInput.GetInstance().IsAnyAvailableInputted) return false;

            return _keyListUped.Any(x => x == key);
        }


        public readonly static List<KeyCode> InputtableKeyList = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,

            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z,
            KeyCode.Minus,
            KeyCode.Caret,
            KeyCode.Backslash,
            KeyCode.At,
            KeyCode.LeftBracket,
            KeyCode.Semicolon,
            KeyCode.Colon,
            KeyCode.RightBracket,
            KeyCode.Comma,
            KeyCode.Period,
            KeyCode.Slash,
            KeyCode.Underscore,
            KeyCode.Backspace,
            KeyCode.Return,
            KeyCode.Space,
        };
    }
}
