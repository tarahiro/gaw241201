using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;
using gaw241201;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    public class SwitchMasterDataProvider : MasterDataProvider<SwitchMasterData.Record, IMasterDataRecord<ISwitchMaster>>, ISwitchMasterDataProvider
    {
        public SwitchMasterDataProvider() : base()
        {
            Load(SwitchMasterData.c_DataName);
        }
    }
}