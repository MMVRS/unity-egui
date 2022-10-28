#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static float TextFieldHeightDefault { get; set; } = 19;
        public static float TextFieldHeight01      { get; set; } = 30;
        public static float TextFieldHeight02      { get; set; } = 25;
        public static float TextFieldHeight03      { get; set; } = 22;
        public static float TextFieldHeight04      { get; set; } = 19;

        /*
         * Text Field.
         */

        public static string TextField(string text)
        {
            return EditorGUILayout.TextField(text, GUILayout.Height(TextFieldHeightDefault));
        }

        public static string TextField(string text, float height)
        {
            return EditorGUILayout.TextField(text, GUILayout.Height(height));
        }

        public static void TextField(string text, Action<string> onChanged)
        {
            var newText = EditorGUILayout.TextField(text);
            if (newText != text)
                onChanged?.Invoke(newText);
        }

        public static void TextField(string text, float height, Action<string> onChanged)
        {
            var newText = EditorGUILayout.TextField(text, GUILayout.Height(height));
            if (newText != text)
                onChanged?.Invoke(newText);
        }

        public static void TextField(string text, float height, TextAnchor alignment, Action<string> onChanged)
        {
            var style = new GUIStyle(EditorStyles.textField)
            {
                alignment = alignment
            };

            var newText = EditorGUILayout.TextField(text, style, GUILayout.Height(height));
            if (newText != text)
                onChanged?.Invoke(newText);
        }

        public static void TextField(string text, out string controlName, Action<string> onChanged)
        {
            controlName = new System.Random().Next(100000).ToString();
            GUI.SetNextControlName(controlName);
            TextField(text, onChanged);
        }

        public static void TextField(string text, float height, TextAnchor alignment, out string controlName, Action<string> onChanged)
        {
            controlName = new System.Random().Next(100000).ToString();
            GUI.SetNextControlName(controlName);

            var style = new GUIStyle(EditorStyles.textField)
            {
                alignment = alignment
            };

            var newText = EditorGUILayout.TextField(text, style, GUILayout.Height(height));
            if (newText != text)
                onChanged?.Invoke(newText);
        }

        /*
         * Text Area.
         */

        public static void TextArea(string content)
        {
            GUILayout.TextArea(content);
        }

        public static void TextArea(string content, float height)
        {
            GUILayout.TextArea(content, GUILayout.Height(height));
        }

        public static void TextArea(string content, float height, int padding)
        {
            var style = new GUIStyle(EditorStyles.textArea);
            style.padding = new RectOffset(padding, padding, padding, padding);
            GUILayout.TextArea(content, style, GUILayout.Height(height));
        }
    }
}

#endif