using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Switch" を置換
    public interface ISwitchMasterDataProvider : IMasterDataProvider<IMasterDataRecord<ISwitchMaster>>
    {

    }
}