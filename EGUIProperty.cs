#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Build1.UnityEGUI.RenderModes;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int PropertyLabelWidth     { get; set; } = 220;
        public static int PropertyTextAreaHeight { get; set; } = 100;

        private static readonly string[] BoolDropdownItems = { bool.FalseString, bool.TrueString };

        /*
         * Property Set.
         */
        
        public static void PropertySet<T>(object instance, string propertyName, T valueNew)
        {
            var type = instance.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception($"Property [{propertyName}] not found for [{type.FullName}].");

            property.SetValue(instance, valueNew);
        }

        public static void PropertySet<T>(object instance, T valueCurrent, string propertyName, T valueNew)
        {
            var type = instance.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception($"Property [{propertyName}] not found for [{type.FullName}].");

            var valueGot = (T)property.GetValue(instance);
            if (!Equals(valueCurrent, valueGot))
                throw new Exception("Values not equal.");

            property.SetValue(instance, valueNew);
        }

        /*
         * Properties String.
         */

        public static void Property(object instance, string value, string propertyName, string tooltip = null)
        {
            Property(instance, value, propertyName, StringRenderMode.Field, PropertyTextAreaHeight, null, tooltip);
        }
        
        public static void Property(object instance, string value, string propertyName, StringRenderMode mode, string tooltip = null)
        {
            Property(instance, value, propertyName, mode, PropertyTextAreaHeight, null, tooltip);
        }

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode, string[] items, string tooltip = null)
        {
            Property(instance, value, propertyName, mode, PropertyTextAreaHeight, items, tooltip);
        }

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode, int height, string[] items = null, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                var valueNew = mode switch
                {
                    StringRenderMode.Field    => GUILayout.TextField(value),
                    StringRenderMode.Area     => GUILayout.TextArea(value, GUILayout.Height(height)),
                    StringRenderMode.DropDown => RenderStringDropDown(value, items),
                    _                         => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };

                if (valueNew is { Length: 0 })
                    valueNew = null;

                return valueNew;
            }, tooltip);
        }

        private static string RenderStringDropDown(string value, string[] items)
        {
            items ??= new string[] { };
            var index = Array.IndexOf(items, value);
            index = EditorGUILayout.Popup(index, items);
            return index != -1 ? items[index] : value;
        }

        /*
         * Properties Numeric.
         */

        public static void Property(object instance, uint value, string propertyName, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueNew =>
            {
                return (uint)EditorGUILayout.LongField(valueNew);
            }, tooltip);
        }
        
        public static void Property(object instance, int value, string propertyName, NumericRenderMode mode = NumericRenderMode.Field, int min = int.MinValue, int max = int.MaxValue, string tooltip = null)
        {
            Property(instance, value, propertyName, propertyName, mode, min, max, tooltip);
        }

        public static void Property(object instance, int value, string propertyName, string propertyDisplayName, NumericRenderMode mode = NumericRenderMode.Field, int min = int.MinValue, int max = int.MaxValue, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyDisplayName, -1, valueNew =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => EditorGUILayout.IntField(valueNew),
                    NumericRenderMode.Slider => EditorGUILayout.IntSlider(valueNew, min, max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            }, tooltip);
        }
        
        public static void Property(object instance, long value, string propertyName, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueNew =>
            {
                return EditorGUILayout.LongField(valueNew);
            }, tooltip);
        }

        public static void Property(object instance, float value, string propertyName, NumericRenderMode mode = NumericRenderMode.Field, float min = float.MinValue, float max = float.MaxValue, string tooltip = null)
        {
            Property(instance, value, propertyName, propertyName, mode, min, max, tooltip);
        }

        public static void Property(object instance, float value, string propertyName, string propertyDisplayName, NumericRenderMode mode = NumericRenderMode.Field, float min = float.MinValue, float max = float.MaxValue, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyDisplayName, -1, valueNew =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => EditorGUILayout.FloatField(valueNew),
                    NumericRenderMode.Slider => EditorGUILayout.Slider(valueNew, min, max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            }, tooltip);
        }

        /*
         * Properties Enum.
         */
        
        public static void Property(object instance, Enum value, string propertyName, string tooltip = null)
        {
            Property(instance, value, propertyName, EnumRenderMode.DropDown, null, tooltip);
        }
        
        public static void Property(object instance, Enum value, string propertyName, Action<Enum, Enum> onChanged, string tooltip = null)
        {
            Property(instance, value, propertyName, EnumRenderMode.DropDown, onChanged, tooltip);
        }

        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode, Action<Enum, Enum> onChanged, string tooltip = null)
        {
            var valueNew = PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode), tooltip);
            if (!Equals(valueNew, value))
                onChanged?.Invoke(valueNew, value);
        }

        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode), tooltip);
        }
        
        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode, int lineSize, float height, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode, lineSize, height), tooltip);
        }
        
        public static void Property(object instance, Enum value, string propertyName, params Enum[] items)
        {
            Property(instance, value, propertyName, items.ToList());
        }
        
        public static void Property(object instance, Enum value, string propertyName, string tooltip, params Enum[] items)
        {
            Property(instance, value, propertyName, items.ToList(), tooltip);
        }
        
        public static void Property(object instance, Enum value, string propertyName, IList<Enum> items)
        {
            Property(instance, value, propertyName, items, null);
        }
        
        public static void Property(object instance, Enum value, string propertyName, IList<Enum> items, string tooltip)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                var index = items.IndexOf(value);
                index = EditorGUILayout.Popup(index, items.Select(i => FormatCameCase(i.ToString())).ToArray());
                return items[index];
            }, tooltip);
        }

        /*
         * Properties Bool.
         */

        public static void Property(object instance, bool value, string propertyName, string tooltip = null)
        {
            Property(instance, value, propertyName, BooleanRenderMode.Toggle, tooltip);
        }
        
        public static void Property(object instance, bool value, string propertyName, BooleanRenderMode mode, string tooltip = null)
        {
            Property(instance, value, propertyName, mode, -1, tooltip);
        }
        
        public static void Property(object instance, bool value, string propertyName, BooleanRenderMode mode, int height, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, height, value =>
            {
                switch (mode)
                {
                    case BooleanRenderMode.Toggle:
                        return EditorGUILayout.Toggle(value);

                    case BooleanRenderMode.Dropdown:
                        var style = new GUIStyle(EditorStyles.popup);
                        if (height != -1)
                            style.fixedHeight = height;
                        
                        var index = EditorGUILayout.Popup(value ? 1 : 0, BoolDropdownItems, style);
                        return bool.Parse(BoolDropdownItems[index]);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            }, tooltip);
        }
        
        /*
         * Properties DateTime.
         */

        public static void Property(object instance, DateTime value, string propertyName, string tooltip = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                var stringCurrent = value.ToString(CultureInfo.InvariantCulture);
                var stringNew = GUILayout.TextField(stringCurrent);
                return DateTime.Parse(stringNew, CultureInfo.InvariantCulture);
            }, tooltip);
        }
        
        /*
         * Properties Base.
         */

        private static T PropertyBase<T>(object instance, T value, string propertyName, string propertyDisplayName, int height, Func<T, T> onRender, string tooltip)
        {
            var type = instance.GetType();
            var label = Regex.Replace(propertyDisplayName, "(\\B[A-Z])", " $1");

            if (!string.IsNullOrWhiteSpace(tooltip))
                label += " *";

            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                LogError($"Instance property not found. Instance: [{instance.GetType().Name}] Property: {propertyName}");
                return value;
            }
            
            // Commented for optimization purposes.
            // Lets see how it goes.
            // var valueGot = (T)property.GetValue(instance);
            // if (!Equals(value, valueGot))
            //     throw new Exception("Values not equal.");

            if (height != -1)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Height(height));
                GUILayout.Label(new GUIContent(label, tooltip), GUILayout.Width(PropertyLabelWidth), GUILayout.Height(height));
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(new GUIContent(label, tooltip), GUILayout.Width(PropertyLabelWidth));
            }

            var valueNew = onRender.Invoke(value);

            EditorGUILayout.EndHorizontal();

            property.SetValue(instance, valueNew);

            return valueNew;
        }
    }
}

#endif