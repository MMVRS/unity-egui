#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.Properties;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static IntResult Int(int value)
        {
            return new IntResult(value, EditorGUILayout.DelayedIntField(value));
        }
        
        public static IntResult Int(int value, params Property[] properties)
        {
            if (properties == null || properties.Length == 0)
                return new IntResult(value, EditorGUILayout.DelayedIntField(value));
            
            var options = new List<GUILayoutOption>(properties.Length);
            
            foreach (var property in properties)
            {
                switch (property.type)
                {
                    case PropertyType.Width:
                        options.Add(GUILayout.Width(property.valueInt));
                        break;
                    case PropertyType.Height:
                        options.Add(GUILayout.Height(property.valueInt));
                        break;
                    case PropertyType.Size:
                        options.Add(GUILayout.Width(property.valueVector2Int.x));
                        options.Add(GUILayout.Height(property.valueVector2Int.y));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Property not supported: {property.type}");
                }
            }
            
            return new IntResult(value, EditorGUILayout.DelayedIntField(value, options.ToArray()));
        }
    }
}

#endif