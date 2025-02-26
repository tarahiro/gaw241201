using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tarahiro.TInput;
using static gaw241201.View.InputConst;
using UnityEngine.Windows;

namespace gaw241201.View
{
    public class InputHundlerCommand : IInputHundlerCommand
    {
        [Inject] PureSingletonInput _input;
        [Inject] PureSingletonKey _key;



        public bool IsInputtedCommand(Command command)
        {
            switch (command)
            {
                case Command.Decide:
                    return _key.IsKeyDown(KeyCode.Return) || _key.IsKeyDown(KeyCode.Space);
                case Command.Cancel:
                    return _key.IsKeyDown(KeyCode.Backspace);
                default:
                    Log.DebugAssert("‘Î‰ž‚µ‚Ä‚¢‚È‚¢ƒRƒ}ƒ“ƒh‚Å‚·:" + command);
                    return false;
            }
        }

        public void NotifyUse(Command command)
        {
            Log.DebugLog("NotifyUse: " + command);
            _input.AvailableInputted();
        }


    }
}