#if UNITY_EDITOR

using System;
using Build1.UnityEGUI.Types;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int       FoldoutH3FontSize  { get; set; } = 14;
        public static FontStyle FoldoutH3FontStyle { get; set; } = FontStyle.Bold;
        public static Color     FoldoutH3Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static void Foldout(string title, FoldoutType type, ref bool foldout)
        {
            var fontSize = type switch
            {
                FoldoutType.Default => EditorStyles.foldout.fontSize,
                FoldoutType.H3      => FoldoutH3FontSize,
                _                   => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var fontStyle = type switch
            {
                FoldoutType.Default => EditorStyles.foldout.fontStyle,
                FoldoutType.H3      => FoldoutH3FontStyle,
                _                   => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var color = type switch
            {
                FoldoutType.Default => EditorStyles.foldout.normal.textColor,
                FoldoutType.H3      => FoldoutH3Color,
                _                   => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var foldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                normal = { textColor = color },
                onNormal = { textColor = color },
                hover = { textColor = color },
                onHover = { textColor = color },
                active = { textColor = color },
                onActive = { textColor = color },
                focused = { textColor = color },
                onFocused = { textColor = color },
                fontSize = fontSize,
                fontStyle = fontStyle,
                contentOffset = new Vector2(3, 0)
            };
            foldout = EditorGUILayout.Foldout(foldout, title, foldoutStyle);
        }
    }
}

#endif