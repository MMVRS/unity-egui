#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.Components.Button;
using Build1.UnityEGUI.Properties;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static float ButtonHeightDefault { get; set; } = 19;
        public static int   ButtonHeight01      { get; set; } = 30;
        public static int   ButtonHeight02      { get; set; } = 25;
        public static int   ButtonHeight03      { get; set; } = 22;
        public static int   ButtonHeight04      { get; set; } = 19;

        /*
         * Return.
         */

        public static ButtonResult Button(string label)
        {
            return Button(label, null);
        }
        
        public static ButtonResult Button(string label, params Property[] properties)
        {
            var style = GUI.skin.button;

            List<GUILayoutOption> options = null;
            
            var defaultHeightAdded = false;
            var enabledPreset = false;
            var enabledValue = false;

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    switch (property.type)
                    {
                        case PropertyType.Enabled:
                            enabledPreset = true;
                            enabledValue = property.valueBool;
                            break;
                        case PropertyType.Width:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Width(property.valueInt));
                            break;
                        case PropertyType.Height:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Height(property.valueInt));
                            defaultHeightAdded = true;
                            break;
                        case PropertyType.Size:
                            options ??= new List<GUILayoutOption>();
                            options.Add(GUILayout.Width(property.valueVector2Int.x));
                            options.Add(GUILayout.Height(property.valueVector2Int.y));
                            defaultHeightAdded = true;
                            break;
                        case PropertyType.Padding:
                            style.padding = property.valueRectOffset;
                            break;
                        case PropertyType.TextAnchor:
                            style.alignment = property.valueTextAnchor;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException($"Property not supported: {property.type}");
                    }
                }
            }

            GUILayoutOption[] optionsArray;

            if (options == null)
            {
                optionsArray = new[] { GUILayout.Height(ButtonHeightDefault) };
            }
            else
            {
                if (!defaultHeightAdded)
                    options.Add(GUILayout.Height(ButtonHeightDefault));

                optionsArray = options.ToArray();
            }

            var clicked = false;

            if (enabledPreset)
            {
                Enabled(enabledValue, () =>
                {
                    clicked = GUILayout.Button(label, style, optionsArray);
                });
            }
            else
            {
                clicked = GUILayout.Button(label, style, optionsArray);
            }
            
            return new ButtonResult(clicked);
        }
    }
}

#endif