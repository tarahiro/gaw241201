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
    internal sealed class WordImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            DisplayName = 2,
            Description = DisplayName + LanguageConst.AvailableLanguageNumber,
            ReplaceTo = Description + LanguageConst.AvailableLanguageNumber,
            TagName = ReplaceTo + 1,
            SkillKey = TagName + 1,
            SkillStringArgs = SkillKey + 1,
            SkillFloatArg = SkillStringArgs + 1,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Word", false, 2)]
        static void ImportMenuWord()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Word imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(WordMasterData.c_DataName, WordMasterData.c_DataName));

            var WordDataList = new List<WordMasterData.Record>();

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
                        WordDataList.Add(new WordMasterData.Record(index, id)
                        {
                            SettableDisplayName = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet,row, (int)Columns.DisplayName),
                            SettableDescription = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet, row, (int)Columns.Description),
                            SettableReplaceTo = sheet[row, (int)Columns.ReplaceTo].String,
                            SettableTagName = sheet[row, (int)Columns.TagName].String,
                            SettableSkillKey = sheet[row, (int)Columns.SkillKey].String,
                            SettableSkillStringArgs = sheet[row, (int)Columns.SkillStringArgs].String.Split(','),
                            SettableSkillFloatArg = sheet[row, (int)Columns.SkillFloatArg].Float
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<WordMasterData, WordMasterData.Record, IMasterDataRecord<IWordMaster>>(MasterDataConst.DataPath + WordMasterData.c_DataName, WordDataList);
        }
    }
#endif
}