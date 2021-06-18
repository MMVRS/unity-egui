#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Editor.List;
using Editor.Parameters;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static partial class EGUI
    {
        public static int PropertyLabelWidth     { get; set; } = 200;
        public static int PropertyTextAreaHeight { get; set; } = 100;
        
        private static readonly string[] BoolDropdownItems = { bool.FalseString, bool.TrueString };
        
        /*
         * Property Set.
         */
        
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

        public static void Property(object instance, string value, string propertyName, StringRenderMode mode = StringRenderMode.Field, string[] items = null)
        {
            PropertyBase(instance, value, propertyName, -1, value =>
            {
                switch (mode)
                {
                    case StringRenderMode.Field:
                        return GUILayout.TextField(value);

                    case StringRenderMode.Area:
                        return GUILayout.TextArea(value, GUILayout.Height(PropertyTextAreaHeight));

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
            PropertyBase(instance, value, propertyName, -1, value =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => EditorGUILayout.IntField(value),
                    NumericRenderMode.Slider => EditorGUILayout.IntSlider(value, min, max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            });
        }

        public static void Property(object instance, uint value, string propertyName, NumericRenderMode mode = NumericRenderMode.Field, uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            PropertyBase(instance, value, propertyName, -1, value =>
            {
                return mode switch
                {
                    NumericRenderMode.Field  => (uint)EditorGUILayout.IntField((int)value),
                    NumericRenderMode.Slider => (uint)EditorGUILayout.IntSlider((int)value, (int)min, (int)max),
                    _                        => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                };
            });
        }

        /*
         * Properties Enum.
         */

        public static void Property(object instance, Enum value, string propertyName, Action<Enum, Enum> onChanged = null)
        {
            var valueNew = PropertyBase(instance, value, propertyName, -1, valueImpl => EditorGUILayout.EnumPopup(valueImpl));
            if (!Equals(valueNew, value))
                onChanged?.Invoke(valueNew, value);
        }

        public static void Property(object instance, Enum value, string propertyName, params object[] items)
        {
            PropertyBase(instance, value, propertyName, -1, value =>
            {
                var index = Array.IndexOf(items, value);
                index = EditorGUILayout.Popup(index, items.Select(i => FormatCameCase(i.ToString())).ToArray());
                return (Enum)items[index];
            });
        }

        /*
         * Properties Bool.
         */

        public static void Property(object instance, bool value, string propertyName, BooleanRenderMode mode = BooleanRenderMode.Toggle, int height = -1)
        {
            PropertyBase(instance, value, propertyName, height, value =>
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
         * Properties Base.
         */

        private static T PropertyBase<T>(object instance, T value, string propertyName, int height, Func<T, T> onRender)
        {
            var type = instance.GetType();
            var label = Regex.Replace(propertyName, "(\\B[A-Z])", " $1");

            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception($"Property [{propertyName}] not found for [{type.FullName}].");

            var valueGot = (T)property.GetValue(instance);
            if (!Equals(value, valueGot))
                throw new Exception("Values not equal.");

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

            var valueNew = onRender.Invoke(valueGot);

            EditorGUILayout.EndHorizontal();
            
            property.SetValue(instance, valueNew);

            return valueNew;
        }

        /*
         * Properties List.
         */

        public static void PropertyList<I, R>(object instance, IList<I> items, string propertyName) where R : EGUIListItemRenderer<I>
        {
            PropertyList<I, R>(instance, items, propertyName, null, null, null);
        }
        
        public static void PropertyList<I, R>(object instance, IList<I> items, string propertyName, EGUIListAddDelegate<I> onAdd, Func<I, bool> onDelete) where R : EGUIListItemRenderer<I>
        {
            PropertyList<I, R>(instance, items, propertyName, null, onAdd, onDelete);
        }
        
        public static void PropertyList<I, R>(object instance, IList<I> items, string propertyName, Action<R> onItemRenderer, EGUIListAddDelegate<I> onAdd, Func<I, bool> onDelete) where R : EGUIListItemRenderer<I>
        {
            var type = instance.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception($"Property [{propertyName}] not found for [{type.FullName}].");

            var itemsGot = (List<I>)property.GetValue(instance);
            if (!ReferenceEquals(itemsGot, items))
                throw new Exception("Items collections not the same.");

            var list = new EGUIList<I, R>
            {
                Label = FormatCameCase(propertyName),
                Items = itemsGot
            };
            list.onCreated += i => { property.SetValue(instance, i); };
            list.onAdd += onAdd;
            list.onDelete += onDelete;
            list.onItemRenderer += onItemRenderer;
            list.Build();
        }
    }
}

#endif