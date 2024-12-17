using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    //MasterDataに限定しない方がいいかもしれない。使う側はMasterDataであることを知る必要はないため
    public interface IAchievableMasterFlagProvider
    {
        List<string> RegisteredId(FlagConst.ContainableMasterKey key);

        bool IsContainskey(FlagConst.ContainableMasterKey key, string Id);
    }
}