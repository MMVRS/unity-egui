#if UNITY_EDITOR

using System;
using Editor.Parameters;
using UnityEngine;

namespace Editor
{
    public static partial class EGUI
    {
        public static int       TitleH1FontSize  { get; set; } = 20;
        public static FontStyle TitleH1FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH1Color     { get; set; } = Color.white;

        public static int       TitleH2FontSize  { get; set; } = 18;
        public static FontStyle TitleH2FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH2Color     { get; set; } = Color.white;

        public static int       TitleH3FontSize  { get; set; } = 14;
        public static FontStyle TitleH3FontStyle { get; set; } = FontStyle.Bold;
        public static Color     TitleH3Color     { get; set; } = Color.white;

        public static Color LabelErrorColor { get; set; } = Color.red;

        /*
         * Titles.
         */

        public static void Title(string title, EGUITitleType type, int offsetX)
        {
            Title(title, type, new Vector2(offsetX, 0));
        }

        public static void Title(string title, EGUITitleType type, bool stretchHeight = false, TextAnchor textAlignment = TextAnchor.UpperLeft)
        {
            Title(title, type, Vector2.zero, stretchHeight, textAlignment);
        }
        
        public static void Title(string title, EGUITitleType type, int offsetX, bool stretchHeight, TextAnchor textAlignment)
        {
            Title(title, type, new Vector2(offsetX, 0), stretchHeight, textAlignment);
        }

        public static void Title(string title, EGUITitleType type, Vector2 offset, bool stretchHeight = false, TextAnchor textAlignment = TextAnchor.UpperLeft)
        {
            var fontSize = type switch
            {
                EGUITitleType.H1 => TitleH1FontSize,
                EGUITitleType.H2 => TitleH2FontSize,
                EGUITitleType.H3 => TitleH3FontSize,
                _                => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var fontStyle = type switch
            {
                EGUITitleType.H1 => TitleH1FontStyle,
                EGUITitleType.H2 => TitleH2FontStyle,
                EGUITitleType.H3 => TitleH3FontStyle,
                _                => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var color = type switch
            {
                EGUITitleType.H1 => TitleH1Color,
                EGUITitleType.H2 => TitleH2Color,
                EGUITitleType.H3 => TitleH3Color,
                _                => throw new ArgumentOutOfRangeException(nameof(type), type, null)
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

        public static void Label(string text)                          { GUILayout.Label(text, LabelBuildStyle(EGUILabelType.Default)); }
        public static void Label(string text, EGUILabelType type)      { GUILayout.Label(text, LabelBuildStyle(type)); }
        public static void Label(string text, int width)               { GUILayout.Label(text, LabelBuildStyle(EGUILabelType.Default), GUILayout.Width(width)); }
        public static void Label(string text, int width, float height) { GUILayout.Label(text, LabelBuildStyle(EGUILabelType.Default), GUILayout.Width(width), GUILayout.Height(height)); }

        public static void Label(string text, FontStyle fontStyle)
        {
            var style = LabelBuildStyle(EGUILabelType.Default);
            style.fontStyle = fontStyle;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(EGUILabelType.Default);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, FontStyle fontStyle, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(EGUILabelType.Default);
            style.alignment = alignment;
            style.fontStyle = fontStyle;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }

        public static void Label(string text, float height, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(EGUILabelType.Default);
            style.stretchWidth = stretchedWidth;
            style.alignment = alignment;
            GUILayout.Label(text, style, GUILayout.Height(height));
        }
        
        public static void Label(string text, int width, float height, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(EGUILabelType.Default);
            style.stretchWidth = stretchedWidth;
            style.alignment = alignment;
            GUILayout.Label(text, style, GUILayout.Width(width), GUILayout.Height(height));
        }

        public static void Label(string text, EGUILabelType type, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(type);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style);
        }
        
        public static void Label(string text, float height, EGUILabelType type, bool stretchedWidth, TextAnchor alignment)
        {
            var style = LabelBuildStyle(type);
            style.alignment = alignment;
            style.stretchWidth = stretchedWidth;
            GUILayout.Label(text, style, GUILayout.Height(height));
        }

        private static GUIStyle LabelBuildStyle(EGUILabelType type)
        {
            var color = type switch
            {
                EGUILabelType.Default => GUI.skin.label.normal.textColor,
                EGUILabelType.Error   => LabelErrorColor,
                _                     => throw new ArgumentOutOfRangeException(nameof(type), type, null)
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