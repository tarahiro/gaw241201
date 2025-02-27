using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using System.Linq;

namespace Tarahiro
{
    public class PureSingletonKey
    {
        Dictionary<KeyInputState, List<KeyCode>> _keyListArray = new Dictionary<KeyInputState, List<KeyCode>>();

        [Inject] PureSingletonInput _input;


        public void Initialize()
        {
            TryReset();
        }


        public void TryReset()
        {
            foreach (KeyInputState key in Enum.GetValues(typeof(KeyInputState)))
            {
                if (!_keyListArray.ContainsKey(key))
                {
                    _keyListArray.Add(key, new List<KeyCode>());
                }
                else if (_keyListArray[key].Count > 0)
                {
                    _keyListArray[key] = new List<KeyCode>();
                }
            }
        }

        public void AddKey(KeyInputState inputState, KeyCode keycode)
        {
            _keyListArray[inputState].Add(keycode);
        }

        public bool IsKeyDown(KeyCode key)
        {
            return IsKeyState(KeyInputState.Down, key);
        }

        public bool IsKey(KeyCode key)
        {
            return IsKeyState(KeyInputState.Key, key);
        }

        public bool IsKeyUp(KeyCode key)
        {
            return IsKeyState(KeyInputState.Up, key);
        }

        bool IsKeyState(KeyInputState inputState, KeyCode key)
        {
            if (_input.IsAnyAvailableInputted) return false;

            return _keyListArray[inputState].Any(x => x == key);

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

        public enum KeyInputState
        {
            Down,
            Key,
            Up
        }

    }
}
