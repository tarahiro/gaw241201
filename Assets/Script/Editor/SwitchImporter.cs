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
using static gaw241201.SwitchConst;
using Tarahiro.Editor;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Switch" を置換
    //ISwitchMasterに合わせてフィールドを追加
    internal sealed class SwitchImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            TargetCategory = 2,
            ByCategory = 3,
            ByKey = 4,
            InitialConditionAndValue = 5,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Switch", false, 2)]
        static void ImportMenuSwitch()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Switch imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(SwitchMasterData.c_DataName, SwitchMasterData.c_DataName));

            var SwitchDataList = new List<SwitchMasterData.Record>();

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
                        SwitchDataList.Add(new SwitchMasterData.Record(index, id)
                        {
                            SettableTargetCategory = EnumUtil.KeyToType<TargetCategory>(sheet[row, (int)Columns.TargetCategory].String),
                            SettableByCategory = EnumUtil.KeyToType<ByCategory>(sheet[row, (int)Columns.ByCategory].String),
                            SettableByKey = sheet[row,(int)Columns.ByKey].String,
                            SettableConditionAndValueList = GetConditionAndValueList(sheet, row, (int)Columns.InitialConditionAndValue)
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<SwitchMasterData, SwitchMasterData.Record, IMasterDataRecord<ISwitchMaster>>(MasterDataConst.DataPath + SwitchMasterData.c_DataName, SwitchDataList);
        }

        public static List<ConditionAndValue> GetConditionAndValueList(IWorksheet sheet, int row, int startColumn)
        {
            string[] Conditions = EditorUtil.GetStringArrayFromCells(sheet,row, startColumn,2);
            string[] Values = EditorUtil.GetStringArrayFromCells(sheet, row, startColumn + 1, 2);

            List<ConditionAndValue> returnable = new List<ConditionAndValue>();

            for (int i = 0; i < Conditions.Length; i++)
            {
                returnable.Add(new ConditionAndValue(
                    Conditions[i],
                    Values.Length > i ? Values[i] : ""));
            }

            return returnable;

        }
    }
#endif
}