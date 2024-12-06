using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEngine.UI.InputField;

namespace gaw241201
{
    public class SaveDataConst
    {

        public readonly static FlagConst.Key[] SavableKeys = new FlagConst.Key[]
        {
           FlagConst.Key. ApplicationTime,
             FlagConst.Key. InputTime,
            FlagConst.Key.  Name,
             FlagConst.Key. NameLower,
            FlagConst.Key.BirthDate,
            FlagConst.Key.IsSaveDataExist,
        };
    }
}