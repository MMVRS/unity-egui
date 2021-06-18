#if UNITY_EDITOR

using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static Rect GetLastRect()
        {
            return GUILayoutUtility.GetLastRect();
        }
        
        public static void CopyToClipboard(string content)
        {
            EditorGUIUtility.systemCopyBuffer = content;
        }

        public static string FormatCameCase(string str)
        {
            return Regex.Replace(str, "(\\B[A-Z])", " $1");
        }

        public static void Focus(string controlName)
        {
            EditorGUI.FocusTextInControl(controlName);
        }
    }
}

#endif