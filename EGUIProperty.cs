#if UNITY_EDITOR

using System;
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
        public static int PropertyLabelWidth     { get; set; } = 200;
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

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode = StringRenderMode.Field)
        {
            Property(instance, value, propertyName, mode, PropertyTextAreaHeight, null);
        }

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode, string[] items)
        {
            Property(instance, value, propertyName, mode, PropertyTextAreaHeight, items);
        }

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode, int height, string[] items = null)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                switch (mode)
                {
                    case StringRenderMode.Field:
                        return GUILayout.TextField(value);

                    case StringRenderMode.Area:
                        return GUILayout.TextArea(value, GUILayout.Height(height));

                    case StringRenderMode.DropDown:
                        items ??= new string[] { };
                        var index = Array.IndexOf(items, value);
                        index = EditorGUILayout.Popup(index, items);
                        return index != -1 ? items[index] : value;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            });
        }

        /*
         * Properties Numeric.
         */

        public static void Property(object instance, int value, string propertyName, NumericRenderMode mode = NumericRenderMode.Field, int min = int.MinValue, int max = int.MaxValue)
        {
            Property(instance, value, propertyName, propertyName, mode, min, max);
        }

        public static void Property(object instance, int value, string propertyName, string propertyDisplayName, NumericRenderMode mode = NumericRenderMode.Field, int min = int.MinValue, int max = int.MaxValue)
        {
            PropertyBase(instance, value, propertyName, propertyDisplayName, -1, valueNew =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => EditorGUILayout.IntField(valueNew),
                    NumericRenderMode.Slider => EditorGUILayout.IntSlider(valueNew, min, max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            });
        }

        public static void Property(object instance, float value, string propertyName, NumericRenderMode mode = NumericRenderMode.Field, float min = float.MinValue, float max = float.MaxValue)
        {
            Property(instance, value, propertyName, propertyName, mode, min, max);
        }

        public static void Property(object instance, float value, string propertyName, string propertyDisplayName, NumericRenderMode mode = NumericRenderMode.Field, float min = float.MinValue, float max = float.MaxValue)
        {
            PropertyBase(instance, value, propertyName, propertyDisplayName, -1, valueNew =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => EditorGUILayout.FloatField(valueNew),
                    NumericRenderMode.Slider => EditorGUILayout.Slider(valueNew, min, max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            });
        }

        /*
         * Properties Enum.
         */
        
        public static void Property(object instance, Enum value, string propertyName, Action<Enum, Enum> onChanged = null)
        {
            Property(instance, value, propertyName, EnumRenderMode.DropDown, onChanged);
        }

        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode, Action<Enum, Enum> onChanged)
        {
            var valueNew = PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode));
            if (!Equals(valueNew, value))
                onChanged?.Invoke(valueNew, value);
        }

        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode));
        }
        
        public static void Property(object instance, Enum value, string propertyName, EnumRenderMode renderMode, int lineSize, float height)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, valueImpl => Enum(valueImpl, renderMode, lineSize, height));
        }
        
        public static void Property(object instance, Enum value, string propertyName, params Enum[] items)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                var index = Array.IndexOf(items, value);
                index = EditorGUILayout.Popup(index, items.Select(i => FormatCameCase(i.ToString())).ToArray());
                return items[index];
            });
        }

        /*
         * Properties Bool.
         */

        public static void Property(object instance, bool value, string propertyName)
        {
            Property(instance, value, propertyName, BooleanRenderMode.Toggle);
        }
        
        public static void Property(object instance, bool value, string propertyName, BooleanRenderMode mode)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                switch (mode)
                {
                    case BooleanRenderMode.Toggle:
                        return EditorGUILayout.Toggle(value);

                    case BooleanRenderMode.Dropdown:
                        var style = new GUIStyle(EditorStyles.popup);
                        var index = EditorGUILayout.Popup(value ? 1 : 0, BoolDropdownItems, style);
                        return bool.Parse(BoolDropdownItems[index]);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            });
        }
        
        public static void Property(object instance, bool value, string propertyName, BooleanRenderMode mode, int height)
        {
            PropertyBase(instance, value, propertyName, propertyName, height, value =>
            {
                switch (mode)
                {
                    case BooleanRenderMode.Toggle:
                        return EditorGUILayout.Toggle(value);

                    case BooleanRenderMode.Dropdown:
                        var style = new GUIStyle(EditorStyles.popup)
                        {
                            fixedHeight = height
                        };
                        var index = EditorGUILayout.Popup(value ? 1 : 0, BoolDropdownItems, style);
                        return bool.Parse(BoolDropdownItems[index]);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            });
        }
        
        /*
         * Properties DateTime.
         */

        public static void Property(object instance, DateTime value, string propertyName)
        {
            PropertyBase(instance, value, propertyName, propertyName, -1, value =>
            {
                var stringCurrent = value.ToString(CultureInfo.InvariantCulture);
                var stringNew = GUILayout.TextField(stringCurrent);
                return DateTime.Parse(stringNew);
            });
        }
        
        /*
         * Properties Base.
         */

        private static T PropertyBase<T>(object instance, T value, string propertyName, string propertyDisplayName, int height, Func<T, T> onRender)
        {
            var type = instance.GetType();
            var label = Regex.Replace(propertyDisplayName, "(\\B[A-Z])", " $1");

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
                GUILayout.Label(label, GUILayout.Width(PropertyLabelWidth), GUILayout.Height(height));
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(label, GUILayout.Width(PropertyLabelWidth));
            }

            var valueNew = onRender.Invoke(value);

            EditorGUILayout.EndHorizontal();

            property.SetValue(instance, valueNew);

            return valueNew;
        }
    }
}

#endif