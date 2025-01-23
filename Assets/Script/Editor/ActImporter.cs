using System.Collections;
using UnityEngine;
using Tarahiro;
using Tarahiro.MasterData;
using Tarahiro.Editor.XmlImporter;
using System.Collections.Generic;
using UnityEditor;
using gaw241201;
using gaw241201.Model;
using gaw241201.Model.MasterData;
using Tarahiro.Editor;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    internal sealed class ActImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            BgId = 2,
            StageGroupId = 3,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Act", false, 2)]
        static void ImportMenuAct()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Act imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(ActMasterData.c_DataName, ActMasterData.c_DataName));

            var ActDataList = new List<ActMasterData.Record>();

            var sheet = book.TryGetWorksheet(EditorConst.c_SheetName);
            if (sheet == null)
            {
                Log.DebugWarning($"シート: {EditorConst.c_SheetName} が見つかりませんでした。");
            }
            else
            {
                for (int row = 0; row < sheet.Height; ++row)
                {
                    // Indexの欄が有効な数字だったら読み込み
                    if (int.TryParse(sheet[row, (int)Columns.Index].String, out int index))
                    {
                        string id = sheet[row, (int)Columns.Id].String;
                        ActDataList.Add(new ActMasterData.Record(index, id)
                        {
                            SettableBgId = sheet[row, (int)Columns.BgId].String,
                            SettableStageGroupId = sheet[row, (int)Columns.StageGroupId].String,
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<ActMasterData, ActMasterData.Record, IMasterDataRecord<IActMaster>>(MasterDataConst.DataPath + ActMasterData.c_DataName, ActDataList);
        }
    }
#endif
}