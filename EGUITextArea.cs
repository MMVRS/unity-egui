#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        /*
         * Text Field.
         */
        
        public static void TextField(string text, Action<string> onChanged)
        {
            var newText = EditorGUILayout.TextField(text);
            if (newText != text)
                onChanged?.Invoke(newText);
        }

        public static void TextField(string text, out string controlName, Action<string> onChanged)
        {
            controlName = new System.Random().Next(100000).ToString();
            GUI.SetNextControlName(controlName);
            TextField(text, onChanged);
        }
        
        public static void TextField(string text, int height, TextAnchor alignment, out string controlName, Action<string> onChanged)
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