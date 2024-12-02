using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public static class ResourceUtil
    {
        //将来的にはここでResouces.LoadやAddressableの出し分けをできるようにする

        public static T GetResource<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }


    }
}
