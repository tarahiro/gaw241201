using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEditor;
using UnityEngine;
namespace gaw241201.Editor
{
#if UNITY_EDITOR
    public class AllImporter
    {
        [MenuItem("Assets/Tables/Import gaw241201", false,0 )]
        static void ImportMenuItem()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            FlowImporter.Import();
            ActImporter.Import();
            ConversationImporter.Import();
            LeetImporter.Import();
            RestrictionImporter.Import();
            TypingImporter.Import();
            TypingRoguelikeImporter.Import();
            WordImporter.Import();
            SwitchImporter.Import();

            stopwatch.Stop();
            Log.DebugLog($"Message imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }
    }
#endif
}