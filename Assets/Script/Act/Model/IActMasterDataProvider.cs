﻿using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;

namespace gaw241201.Model
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    public interface IActMasterDataProvider : IMasterDataProvider<IMasterDataRecord<IActMaster>>
    {

    }
}