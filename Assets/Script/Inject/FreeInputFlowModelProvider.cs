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
        [Inject] FreeInputFactoryBirth _birthFreeInputFactory;
        public IFreeInputGateFlowModel GetGateModel(FreeInputConst.FreeInputCategory category)
        {
            switch (category)
            {
                case FreeInputConst.FreeInputCategory.NameFreeInput:
                    return _nameFreeInputFactory.GetGateModel();

                case FreeInputConst.FreeInputCategory.TimeFreeInput:
                    return _timeFreeInputFactory.GetGateModel();

                case FreeInputConst.FreeInputCategory.BirthDateFreeInput:
                    return _birthFreeInputFactory.GetGateModel();

                default:
                    Log.DebugAssert("–¢‘Î‰ž‚Ìcategory‚Å‚·:" + category);
                        return null;
            }
        }

        public List<IFreeInputGateFlowModel> _freeInputGateModelList()
        {
            List<IFreeInputGateFlowModel> _returnable = new List<IFreeInputGateFlowModel>();

            _returnable.Add(_nameFreeInputFactory.GetGateModel());
            _returnable.Add(_timeFreeInputFactory.GetGateModel());
            _returnable.Add(_birthFreeInputFactory.GetGateModel());

            return _returnable;
        }
    }
}