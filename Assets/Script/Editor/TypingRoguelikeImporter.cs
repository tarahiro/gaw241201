﻿using System.Collections;
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
    internal sealed class TypingRoguelikeImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            GroupList = 2,
            RestrictionId = 3,
            TimePerChar = 4,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import TypingRoguelike", false, 2)]
        static void ImportMenuTypingRoguelike()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"TypingRoguelike imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(TypingRoguelikeMasterData.c_DataName, TypingRoguelikeMasterData.c_DataName));

            var TypingRoguelikeDataList = new List<TypingRoguelikeMasterData.Record>();

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
                        TypingRoguelikeDataList.Add(new TypingRoguelikeMasterData.Record(index, id)
                        {
                            SettableGroupList = sheet[row, (int)Columns.GroupList].String.Split(','),
                            SettableRestrictionId = sheet[row, (int)Columns.RestrictionId].String,
                            SettableTimePerChar = sheet[row,(int)Columns.TimePerChar].Float
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<TypingRoguelikeMasterData, TypingRoguelikeMasterData.Record, IMasterDataRecord<ITypingRoguelikeMaster>>(MasterDataConst.DataPath + TypingRoguelikeMasterData.c_DataName, TypingRoguelikeDataList);
        }
    }
#endif
}