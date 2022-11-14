#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.Other;
using Build1.UnityEGUI.Types;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int       FoldoutH3FontSize  { get; set; } = 14;
        public static FontStyle FoldoutH3FontStyle { get; set; } = UnityEngine.FontStyle.Bold;
        public static Color     FoldoutH3Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        private static readonly Dictionary<string, FoldInfo> _infos = new();
        
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

        public static void GetFoldInfo(object instance, out FoldInfo info)
        {
            info = GetFoldInfo(instance);
        }
        
        public static void GetFoldInfo(object instance, int id, out FoldInfo info)
        {
            info = GetFoldInfo(instance, id);
        }
        
        public static FoldInfo GetFoldInfo(object instance)
        {
            return GetFoldInfo(instance.GetType().FullName);
        }
        
        public static FoldInfo GetFoldInfo(object instance, int id)
        {
            return GetFoldInfo($"{instance.GetType().FullName}_{id}");
        }
        
        public static FoldInfo GetFoldInfo(string key)
        {
            if (_infos.TryGetValue(key, out var folds)) 
                return folds;
            
            folds = new FoldInfo();
            _infos.Add(key, folds);
            return folds;
        }
    }
}

#endif