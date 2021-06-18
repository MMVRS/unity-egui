#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int DropDownHeight01 { get; set; } = 30;
        public static int DropDownHeight02 { get; set; } = 25;
        public static int DropDownHeight03 { get; set; } = 22;

        /*
         * Ref.
         */
        
        public static void DropDown(IEnumerable<string> items, ref int index)
        {
            index = EditorGUILayout.Popup(index, items.Select(i => " " + i).ToArray());
        }

        public static void DropDown(IEnumerable<string> items, ref int index, int height)
        {
            var style = new GUIStyle(EditorStyles.popup)
            {
                fixedHeight = height
            };
            index = EditorGUILayout.Popup(index, items.Select(i => " " + i).ToArray(), style);
        }
        
        /*
         * Callback.
         */

        public static void DropDown(IEnumerable<string> items, int index, Action<int> onChanged)
        {
            var indexNew = EditorGUILayout.Popup(index, items.Select(i => " " + i).ToArray());
            if (indexNew != index)
                onChanged.Invoke(indexNew);
        }

        public static void DropDown(IEnumerable<string> items, int index, int height, Action<int> onChanged)
        {
            var style = new GUIStyle(EditorStyles.popup)
            {
                fixedHeight = height
            };
            var indexNew = EditorGUILayout.Popup(index, items.Select(i => " " + i).ToArray(), style);
            if (indexNew != index)
                onChanged.Invoke(indexNew);
        }
    }
}

#endif