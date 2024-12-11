using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public interface IGroupMasterGettable<T> where T : IIdentifiable, IIndexable, IGroupable
    {
        List<T> GetGroupMaster(string bodyId);
    }
}