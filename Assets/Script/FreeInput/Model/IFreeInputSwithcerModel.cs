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
    public interface IFreeInputSwithcerModel
    {
        public IFreeInputGateFlowModel GetGateModel(FreeInputConst.FreeInputCategory category);
        public List<IFreeInputGateFlowModel> _freeInputGateModelList();
    }
}