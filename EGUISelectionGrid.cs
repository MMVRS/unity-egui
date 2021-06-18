#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void SelectionGrid(IEnumerable<string> items, ref int selectedIndex, int itemHeight, int itemsPerRow, int spaceTop)
        {
            selectedIndex = SelectionGrid(selectedIndex, items, itemHeight, itemsPerRow, spaceTop);
        }

        private static int SelectionGrid(int selectedIndex, IEnumerable<string> items, int itemHeight, int itemsPerRow, int spaceTop)
        {
            var style = new GUIStyle(GUI.skin.button);
            var lastRect = GUILayoutUtility.GetLastRect();
            var enumerable = items as string[] ?? items.ToArray();
            var height = Mathf.CeilToInt((float)enumerable.Length / itemsPerRow) * itemHeight;
            var y = lastRect.y + lastRect.height + spaceTop;
            var rect = new Rect(lastRect.x, y, lastRect.width, height);
            var index = GUI.SelectionGrid(rect, selectedIndex, enumerable, itemsPerRow, style);
            GUILayout.Space(height);
            return index;
        }
    }
}

#endif