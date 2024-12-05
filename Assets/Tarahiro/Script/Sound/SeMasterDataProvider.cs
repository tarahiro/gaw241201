using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;

namespace Tarahiro.Sound
{
    public class SeMasterDataProvider : MasterDataProvider<SeMasterData.Record, IMasterDataRecord<ISeMaster>>, ISeMasterDataProvider
    {
        public SeMasterDataProvider(string fileName) : base(fileName)
        {
        }
    }
}