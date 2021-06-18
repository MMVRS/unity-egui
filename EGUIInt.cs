#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void Int(int value, Action<int> onChanged)
        {
            var valueNew = EditorGUILayout.DelayedIntField(value);
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
        
        public static void Int(int value, int width, Action<int> onChanged)
        {
            var valueNew = EditorGUILayout.DelayedIntField(value, GUILayout.Width(width));
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
    }
}

#endif