﻿using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;
using gaw241201;
using gaw241201.Model;

namespace gaw241201.Model.MasterData
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    public class TypingRoguelikeMasterDataProvider : MasterDataProvider<TypingRoguelikeMasterData.Record, IMasterDataRecord<ITypingRoguelikeMaster>>, ITypingRoguelikeMasterDataProvider
    {
        public TypingRoguelikeMasterDataProvider() : base()
        {
            Load(TypingRoguelikeMasterData.c_DataName);
        }
    }
}