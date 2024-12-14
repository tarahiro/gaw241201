using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using gaw241201.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tarahiro.MasterData;

namespace gaw241201
{
    public class AvailableWordDataProvider : AvaliableMasterDataProvider<IMasterDataRecord<IWordMaster>>
    {
        protected override FlagConst.ContainableMasterKey _containableMasterKey { get => FlagConst.ContainableMasterKey.Word; }
    }
}