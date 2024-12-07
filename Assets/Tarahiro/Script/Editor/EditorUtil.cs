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
        public static string XmlPath(string fileName)
        {
            return EditorConst.c_XmlPathPrefix + fileName + "/" + fileName + EditorConst.c_XmlPathSuffix;
        }
    }
}