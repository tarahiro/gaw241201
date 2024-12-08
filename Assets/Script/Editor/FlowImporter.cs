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
using System.IO;
using Tarahiro.Editor;
using System;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    internal sealed class FlowImporter
    {
        const string c_XmlPathSuffix = ".xml";
        enum Columns
        {
            Index = 0,
            Id = 1,
            Category = 2,
            BodyId = 3,
            Condition = 4,
            ConditionArg = 5,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Flow", false, 2)]
        static void ImportMenuFlow()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Flow imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            foreach (FlowMasterConst.FlowMasterLabel v in Enum.GetValues(typeof(FlowMasterConst.FlowMasterLabel)))
            {
                string file = EditorUtil.XmlPath(FlowMasterData.c_DataName, v.ToString());

                var book = XmlImporter.ImportWorkbook(file);

                var FlowDataList = new List<FlowMasterData.Record>();

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
                            FlowDataList.Add(new FlowMasterData.Record(index, id)
                            {
                                SettableCategory = sheet[row, (int)Columns.Category].String,
                                SettableBodyId = sheet[row, (int)Columns.BodyId].String,
                                SettableCondition = sheet[row, (int)Columns.Condition].String,
                                SettableConditionArg = sheet[row, (int)Columns.ConditionArg].String,
                            });
                        }
                    }
                }

                // データ出力
                XmlImporter.ExportOrderedDictionary<FlowMasterData, FlowMasterData.Record, IMasterDataRecord<IFlowMaster>>(
                    MasterDataConst.DataPath + FlowMasterData.c_DataName + "/" + v.ToString(),
                    FlowDataList);
            }
        }
    }
#endif
}