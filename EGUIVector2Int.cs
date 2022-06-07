#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void Vector2Int(string label, Vector2Int value, Action<Vector2Int> onChanged)
        {
            EditorGUILayout.BeginHorizontal();
            
            GUILayout.Label(label, GUILayout.Width(PropertyLabelWidth));

            var valueNew = EditorGUILayout.Vector2IntField(string.Empty, value);
            
            EditorGUILayout.EndHorizontal();
            
            if (valueNew != value)
                onChanged?.Invoke(valueNew);
        }
    }
}

#endif