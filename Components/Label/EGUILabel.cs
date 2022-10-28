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
        public static float LabelHeightDefault { get; set; } = 19;
        public static float LabelHeight01      { get; set; } = 30;
        public static float LabelHeight02      { get; set; } = 25;
        public static float LabelHeight03      { get; set; } = 22;
        public static float LabelHeight04      { get; set; } = 19;

        public static Color LabelColorError { get; set; } = Color.red;

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
                _                                  => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var style = new GUIStyle(GUI.skin.label)
            {
                normal =
                {
                    textColor = color
                }
            };

            List<GUILayoutOption> options = null;
            var defaultHeightAdded = false;

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
                        defaultHeightAdded = true;
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

            GUILayoutOption[] optionsArray;

            if (options == null)
            {
                optionsArray = new[] { GUILayout.Height(LabelHeightDefault) };
            }
            else
            {
                if (!defaultHeightAdded)
                    options.Add(GUILayout.Height(LabelHeightDefault));

                optionsArray = options.ToArray();
            }

            GUILayout.Label(text, style, optionsArray);
        }
    }
}

#endif