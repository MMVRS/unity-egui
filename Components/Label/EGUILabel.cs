#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.Components.Label;
using Build1.UnityEGUI.Properties;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static float LabelHeightMin { get; set; } = 19;
        public static float LabelHeight01  { get; set; } = 30;
        public static float LabelHeight02  { get; set; } = 25;
        public static float LabelHeight03  { get; set; } = 22;
        public static float LabelHeight04  { get; set; } = 19;

        public static Color LabelColorError { get; set; } = Color.red;

        public static void Label(string text)
        {
            Label(text, LabelType.Default, null);
        }

        public static void Label(string text, params Property[] properties)
        {
            Label(text, LabelType.Default, properties);
        }

        public static void Label(string text, LabelType type, params Property[] properties)
        {
            var color = type switch
            {
                LabelType.Default => GUI.skin.label.normal.textColor,
                LabelType.Error   => LabelColorError,
                _                 => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var style = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = color
                }
            };

            List<GUILayoutOption> options = null;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    switch (property.type)
                    {
                        case PropertyType.Width:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Width(property.valueInt));
                            break;
                        case PropertyType.Height:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Height(property.valueInt));
                            break;
                        case PropertyType.Size:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Width(property.valueVector2Int.x));
                            options.Add(GUILayout.Height(property.valueVector2Int.y));
                            break;
                        case PropertyType.FontStyle:
                            style.fontStyle = property.valueFontStyle;
                            break;
                        case PropertyType.TextAnchor:
                            style.alignment = property.valueTextAnchor;
                            break;
                        case PropertyType.StretchedWidth:
                            style.stretchWidth = property.valueBool;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Property not supported: {property.type}");
                    }
                }
            }

            options ??= new List<GUILayoutOption>();
            options.Add(GUILayout.MinHeight(LabelHeightMin));

            GUILayout.Label(text, style, options.ToArray());
        }
    }
}

#endif