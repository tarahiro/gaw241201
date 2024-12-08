using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.Model.MasterData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class FlowMasterDataDictionaryProvider : IFlowMasterDataDictionaryProvider
    {
        Dictionary<FlowMasterConst.FlowMasterLabel, IFlowMasterDataProvider> _dictionary;
        const string c_assetSuffix = ".asset";

        public FlowMasterDataDictionaryProvider() {
            _dictionary = new Dictionary<FlowMasterConst.FlowMasterLabel, IFlowMasterDataProvider>();
            /*
            string prefix = ResourceUtil.ResourcePath() + FlowMasterData.c_DataPathPrefix;
            var files = Directory.GetFiles(prefix, "*" + c_assetSuffix, SearchOption.TopDirectoryOnly);

            foreach (var item in files)
            {
                string fileName = item.Replace(prefix, "").Replace(c_assetSuffix, "");
                _dictionary.Add(fileName, new FlowMasterDataProvider(item.Replace(ResourceUtil.ResourcePath(),"").Replace(c_assetSuffix,"")));
            }
            */

            foreach(FlowMasterConst.FlowMasterLabel v in Enum.GetValues(typeof(FlowMasterConst.FlowMasterLabel)))
            {
                AddToDictionary(v);
            }
        }

        void AddToDictionary(FlowMasterConst.FlowMasterLabel label)
        {
            _dictionary.Add(label, new FlowMasterDataProvider(FlowMasterData.c_DataName + "/" + label.ToString()));
        }

        public IFlowMasterDataProvider GetProvider(FlowMasterConst.FlowMasterLabel key)
        {
            return _dictionary[key];
        }

    }
}