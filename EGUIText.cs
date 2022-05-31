#if UNITY_EDITOR

using System;
using Build1.UnityEGUI.Types;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int TitleOffsetX1 { get; set; } = 5;

        public static int       TitleH1FontSize  { get; set; } = 20;
        public static FontStyle TitleH1FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH1Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static int       TitleH2FontSize  { get; set; } = 18;
        public static FontStyle TitleH2FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH2Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static int       TitleH3FontSize  { get; set; } = 14;
        public static FontStyle TitleH3FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH3Color     { get; set; } = EditorGUIUtility.isProSkin ? Color.white : Color.black;

        public static Color LabelErrorColor { get; set; } = Color.red;

        /*
         * Titles.
         */

        public static void Title(string title, TitleType type, int offsetX)
        {
            Title(title, type, new Vector2(offsetX, 0));
        }

        public static void Title(string title, TitleType type, bool stretchHeight = false, TextAnchor textAlignment = TextAnchor.UpperLeft)
        {
            Title(title, type, Vector2.zero, stretchHeight, textAlignment);
        }

        public static void Title(string title, TitleType type, int offsetX, bool stretchHeight, TextAnchor textAlignment)
        {
            Title(title, type, new Vector2(offsetX, 0), stretchHeight, textAlignment);
        }

        public static void Title(string title, TitleType type, Vector2 offset, bool stretchHeight = false, TextAnchor textAlignment = TextAnchor.UpperLeft)
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

            var guiStyle = new GUIStyle
            {
                fontSize = fontSize,
                fontStyle = fontStyle,
                normal =
                {
                    textColor = color
                },
                contentOffset = offset,
                alignment = textAlignment,
                stretchHeight = stretchHeight
            };

            GUILayout.Label(title, guiStyle);
        }

        /*
         * Labels.
         */

        public static void Label(string text)                          { GUILayout.Label(text, LabelBuildStyle(LabelType.Default)); }
        public static void Label(string text, LabelType type)          { GUILayout.Label(text, LabelBuildStyle(type)); }
        public static void Label(string text, int width)               { GUILayout.Label(text, LabelBuildStyle(LabelType.Default), GUILayout.Width(width)); }
        public static void Label(string text, int width, float height) { GUILayout.Label(text, LabelBuildStyle(LabelType.Default), GUILayout.Width(width), GUILayout.Height(height)); }

        public static void Label(string text, FontStyle fontStyle)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.fontStyle = fontStyle;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, FontStyle fontStyle, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, float height, FontStyle fontStyle)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.fontStyle = fontStyle;
            GUILayout.Label(text, style, GUILayout.Height(height));
        }
        
        public static void Label(string text, float height, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.stretchWidth = stretchedWidth;
            style.alignment = alignment;
            GUILayout.Label(text, style, GUILayout.Height(height));
        }

        public static void Label(string text, int width, float height, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(LabelType.Default);
            style.stretchWidth = stretchedWidth;
            style.alignment = alignment;
            GUILayout.Label(text, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void Label(string text, LabelType type, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(type);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, float height, LabelType type, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(type);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style, GUILayout.Height(height));
        }

        private static GUIStyle LabelBuildStyle(LabelType type)
        {
            var color = type switch
            {
                LabelType.Default => GUI.skin.label.normal.textColor,
                LabelType.Error   => LabelErrorColor,
                _                 => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            return new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = color
                }
            };
        }
    }
}

#endif