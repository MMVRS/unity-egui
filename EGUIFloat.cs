using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void Float(float value, Action<float> onChanged)
        {
            var valueNew = EditorGUILayout.FloatField(value);
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
        
        public static void Float(float value, int width, Action<float> onChanged)
        {
            var valueNew = EditorGUILayout.FloatField(value, GUILayout.Width(width));
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
    }
}