using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class ActViewArgsListFactory
    {
        public List<ActViewArgs> Create(List<ModelArgs<IStageMasterRegisteredRestrictedCharList>> list)
        {
            var returnable = new List<ActViewArgs>();

            for(int i = 0; i < list.Count; i++)
            {
                returnable.Add(new ActViewArgs(list[i].Master.WaveCount, list[i].Master.RestrictedCharList));
            }

            return returnable;
        }
    }
}