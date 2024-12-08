using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro.Editor
{
    public static class EditorUtil
    {
        public static string XmlPath(string directoryName, string fileName)
        {
            return EditorConst.c_XmlPathPrefix + directoryName + "/" + fileName + EditorConst.c_XmlPathSuffix;
        }
    }
}