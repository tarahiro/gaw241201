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

namespace gaw241201.View
{
    public class InputHundlerCommand : IInputHundlerCommand
    {

        public bool IsInputtedCommand(Command command)
        {
            switch (command)
            {
                case Command.Decide:
                    return Tkey.GetInstance().IsKeyDown(KeyCode.Return) || Tkey.GetInstance().IsKeyDown(KeyCode.Space);
                case Command.Cancel:
                    return Tkey.GetInstance().IsKeyDown(KeyCode.Backspace);
                default:
                    Log.DebugAssert("‘Î‰ž‚µ‚Ä‚¢‚È‚¢ƒRƒ}ƒ“ƒh‚Å‚·:" + command);
                    return false;
            }
        }

        public void NotifyUse(Command command)
        {
            Log.DebugLog("NotifyUse: " + command);
            TInput.GetInstance().AvailableInputted();
        }


    }
}