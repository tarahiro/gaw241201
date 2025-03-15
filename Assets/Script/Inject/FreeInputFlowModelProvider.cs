using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using gaw241201.Presenter;

namespace gaw241201.Inject
{
    public class FreeInputFlowModelProvider : IFreeInputSwithcerModel
    {
        [Inject] FreeInputFactoryName _nameFreeInputFactory;
        [Inject] FreeInputFactoryTime _timeFreeInputFactory;
        public IFreeInputGateModel GetGateModel(FreeInputConst.FreeInputCategory category)
        {
            switch (category)
            {
                case FreeInputConst.FreeInputCategory.NameFreeInput:
                    return _nameFreeInputFactory.GetGateModel();

                case FreeInputConst.FreeInputCategory.TimeFreeInput:
                    return _timeFreeInputFactory.GetGateModel();

                default:
                    Log.DebugAssert("–¢‘Î‰ž‚Ìcategory‚Å‚·:" + category);
                        return null;
            }
        }

        public List<IFreeInputGateModel> _freeInputGateModelList()
        {
            List<IFreeInputGateModel> _returnable = new List<IFreeInputGateModel>();

            _returnable.Add(_nameFreeInputFactory.GetGateModel());
            _returnable.Add(_timeFreeInputFactory.GetGateModel());

            return _returnable;
        }
    }
}