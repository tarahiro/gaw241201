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
    internal sealed class ConversationImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            Message = 2,
            ConversationGroup = Message + LanguageConst.AvailableLanguageNumber,
            EyePosition = Message + LanguageConst.AvailableLanguageNumber + 1,
            Facial = Message + LanguageConst.AvailableLanguageNumber + 2,
            Impression = Message + LanguageConst.AvailableLanguageNumber + 3,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Conversation", false, 2)]
        static void ImportMenuConversation()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Conversation imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(ConversationMasterData.c_DataName, ConversationMasterData.c_DataName));

            var ConversationDataList = new List<ConversationMasterData.Record>();

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
                        ConversationDataList.Add(new ConversationMasterData.Record(index, id)
                        {
                            SettableConversationGroup = sheet[row, (int)Columns.ConversationGroup].String,
                            SettableMessage = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet,row,(int)Columns.Message),
                            SettableEyePosition = sheet[row, (int)Columns.EyePosition].String,
                            SettableFacial = sheet[row, (int)Columns.Facial].String,
                            SettableImpression = sheet[row, (int)Columns.Impression].String,
                        });; ;
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<ConversationMasterData, ConversationMasterData.Record, IMasterDataRecord<IConversationMaster>>(MasterDataConst.DataPath + ConversationMasterData.c_DataName, ConversationDataList);
        }
    }
#endif
}