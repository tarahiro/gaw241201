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
using Tarahiro.Sound;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    internal sealed class TypingImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            DisplayText = 2,
            QuestionText =  DisplayText + LanguageConst.AvailableLanguageNumber,
            TypingGroup = QuestionText + LanguageConst.AvailableLanguageNumber,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Typing", false, 2)]
        static void ImportMenuTyping()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Typing imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(TypingMasterData.c_DataName, TypingMasterData.c_DataName));

            var TypingDataList = new List<TypingMasterData.Record>();

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
                        TypingDataList.Add(new TypingMasterData.Record(index, id)
                        {
                            SettableDisplayText = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet, row, (int)Columns.DisplayText),
                            SettableQuestionText = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet, row, (int)Columns.QuestionText),
                            SettableTypingGroup = sheet[row, (int)Columns.TypingGroup].String,
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<TypingMasterData, TypingMasterData.Record, IMasterDataRecord<ITypingMaster>>(MasterDataConst.DataPath + TypingMasterData.c_DataName, TypingDataList);
        }
    }
#endif
}