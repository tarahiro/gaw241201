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
    //MasterData�Ɍ��肵�Ȃ�����������������Ȃ��B�g������MasterData�ł��邱�Ƃ�m��K�v�͂Ȃ�����
    public interface IAchievableMasterFlagProvider
    {
        List<string> RegisteredId(FlagConst.ContainableMasterKey key);

        bool IsContainskey(FlagConst.ContainableMasterKey key, string Id);
    }
}