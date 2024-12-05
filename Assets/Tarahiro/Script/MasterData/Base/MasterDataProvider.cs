using System.Collections;
using UnityEngine;
using Tarahiro;
using VContainer;

namespace Tarahiro.MasterData
{
    public abstract class MasterDataProvider<DataType, InterfaceType>
        : IMasterDataProvider<InterfaceType>
        where InterfaceType : IIndexable, IIdentifiable
        where DataType : InterfaceType
    {
        protected MasterDataOrderedDictionary<DataType, InterfaceType> m_Dictionary;



        [Inject]
        public MasterDataProvider(string filepath)
        {
            Log.Comment(filepath + "のData読み込み");
            m_Dictionary = ResourceUtil.GetResource<MasterDataOrderedDictionary<DataType, InterfaceType>>(filepath);
        }

        public InterfaceType TryGetFromIndex(int index)
        {
            return m_Dictionary.TryGetFromIndex(index);
        }

        public InterfaceType TryGetFromId(string id)
        {
            return m_Dictionary.TryGetFromId(id);
        }

        public int Count { get { return m_Dictionary.Count; } }
    }
}