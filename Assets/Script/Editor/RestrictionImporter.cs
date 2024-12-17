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
    internal sealed class RestrictionImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            RestrictedCharList = 2,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Restriction", false, 2)]
        static void ImportMenuRestriction()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Restriction imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(RestrictionMasterData.c_DataName, RestrictionMasterData.c_DataName));

            var RestrictionDataList = new List<RestrictionMasterData.Record>();

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
                        RestrictionDataList.Add(new RestrictionMasterData.Record(index, id)
                        {
                            SettableRestrictedCharList = !(sheet[row, (int)Columns.RestrictedCharList].IsEmpty)?
                            sheet[row, (int)Columns.RestrictedCharList].String.Split(',').Select(s => s[0]).ToArray():
                            new char[0]
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<RestrictionMasterData, RestrictionMasterData.Record, IMasterDataRecord<IRestrictionMaster>>(MasterDataConst.DataPath + RestrictionMasterData.c_DataName, RestrictionDataList);
        }
    }
#endif
}