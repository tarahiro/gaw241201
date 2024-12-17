using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using Tarahiro.Editor.XmlImporter;

namespace Tarahiro.Editor
{
    public static class EditorUtil
    {
        public static string XmlPath(string directoryName, string fileName)
        {
            return EditorConst.c_XmlPathPrefix + directoryName + "/" + fileName + EditorConst.c_XmlPathSuffix;
        }

        public static string[] GetStringArray(TableCell cell)
        {
            if (cell.IsEmpty)
            {
                return new string[0];
            }
            else
            {
                return cell.String.Split(',');
            }
        }
    }
}