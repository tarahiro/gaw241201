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
using System.Linq;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    internal sealed class LeetImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            Name = 2,
            Description = 3,
            LeetedChar = 4,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Leet", false, 2)]
        static void ImportMenuLeet()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Leet imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(LeetMasterData.c_DataName, LeetMasterData.c_DataName));

            var LeetDataList = new List<LeetMasterData.Record>();

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
                        LeetDataList.Add(new LeetMasterData.Record(index, id)
                        {
                            SettableName = sheet[row, (int)Columns.Name].String,
                            SettableDescription = sheet[row, (int)Columns.Description].String,
                            SettableReplaceToStringList = GetLeetReplaceDataArray(sheet, row, (int)Columns.LeetedChar, 2)
                        }); ;
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<LeetMasterData, LeetMasterData.Record, IMasterDataRecord<ILeetMaster>>(MasterDataConst.DataPath + LeetMasterData.c_DataName, LeetDataList);
        }

        public static LeetReplaceData[] GetLeetReplaceDataArray(IWorksheet sheet, int row, int startColumn, int columnInterval)
        {
            List<LeetReplaceData> returnable = new List<LeetReplaceData>();

            for (int i = 0; startColumn +  1 + i*columnInterval < sheet.Width && !sheet[row, startColumn + i * columnInterval].IsEmpty; i++)
            {
                returnable.Add(new LeetReplaceData(sheet[row, startColumn + i * columnInterval].String[0], sheet[row, startColumn + 1 + i * columnInterval].String.Split(',').ToList()));
            }

            return returnable.ToArray();

        }
    }
#endif
}