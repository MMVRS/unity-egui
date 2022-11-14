#if UNITY_EDITOR

using System;
using Build1.UnityEGUI.Components.Title;
using Build1.UnityEGUI.Properties;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int TitleOffsetX1 { get; set; } = 5;

        public static int       TitleH1FontSize  { get; set; } = 20;
        public static FontStyle TitleH1FontStyle { get; set; } = UnityEngine.FontStyle.Bold;
        public static Color     TitleH1Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static int       TitleH2FontSize  { get; set; } = 18;
        public static FontStyle TitleH2FontStyle { get; set; } = UnityEngine.FontStyle.Bold;
        public static Color     TitleH2Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static int       TitleH3FontSize  { get; set; } = 14;
        public static FontStyle TitleH3FontStyle { get; set; } = UnityEngine.FontStyle.Bold;
        public static Color     TitleH3Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static void Title(string title, TitleType type)
        {
            Title(title, type, null);
        }
        
        public static void Title(string title, TitleType type, params Property[] properties)
        {
            var fontSize = type switch
            {
                TitleType.H1 => TitleH1FontSize,
                TitleType.H2 => TitleH2FontSize,
                TitleType.H3 => TitleH3FontSize,
                _            => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var fontStyle = type switch
            {
                TitleType.H1 => TitleH1FontStyle,
                TitleType.H2 => TitleH2FontStyle,
                TitleType.H3 => TitleH3FontStyle,
                _            => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var color = type switch
            {
                TitleType.H1 => TitleH1Color,
                TitleType.H2 => TitleH2Color,
                TitleType.H3 => TitleH3Color,
                _            => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var style = new GUIStyle
            {
                fontSize = fontSize,
                fontStyle = fontStyle,
                normal =
                {
                    textColor = color
                },
            };

            var alignmentSet = false;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    switch (property.type)
                    {
                        case PropertyType.TextAnchor:
                            style.alignment = property.valueTextAnchor;
                            alignmentSet = true;
                            break;

                        case PropertyType.StretchedWidth:
                            style.stretchWidth = property.valueBool;
                            break;
                    
                        case PropertyType.StretchedHeight:
                            style.stretchHeight = property.valueBool;
                            break;

                        case PropertyType.OffsetX:
                            style.contentOffset = new Vector2(property.valueInt, style.contentOffset.y);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException($"Property not supported: {property.type}");
                    }
                } 
            }

            if (!alignmentSet)
                style.alignment = UnityEngine.TextAnchor.UpperLeft;

            GUILayout.Label(title, style);
        }
    }
}

#endif