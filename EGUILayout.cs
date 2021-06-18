#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static partial class EGUI
    {
        /*
         * Horizontal.
         */

        public static void Horizontally(Action onHorizontally)
        {
            EditorGUILayout.BeginHorizontal();
            onHorizontally?.Invoke();
            EditorGUILayout.EndHorizontal();
        }

        public static void Horizontally(GUIStyle style, Action onHorizontally)
        {
            EditorGUILayout.BeginHorizontal(style);
            onHorizontally?.Invoke();
            EditorGUILayout.EndHorizontal();
        }
        
        /*
         * Vertical.
         */

        public static void Vertically(Action onVertically)
        {
            EditorGUILayout.BeginVertical();
            onVertically?.Invoke();
            EditorGUILayout.EndVertical();
        }

        public static void Vertically(GUIStyle style, Action onVertically)
        {
            EditorGUILayout.BeginVertical(style);
            onVertically?.Invoke();
            EditorGUILayout.EndVertical();
        }

        /*
         * Panel.
         */

        public static void Panel(int padding, Action onPanel)
        {
            var style = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(padding, padding, padding, padding)
            };
            EditorGUILayout.BeginVertical(style);
            onPanel?.Invoke();
            EditorGUILayout.EndVertical();
        }

        /*
         * Scroll.
         */

        public static void Scroll(ref Vector2 scrollPosition, Action onScroll)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, false, GUIStyle.none, GUI.skin.verticalScrollbar);
            onScroll?.Invoke();
            GUILayout.EndScrollView();
        }
        
        /*
         * Separators.
         */

        public static void Line(Color color, int thickness = 2, int padding = 0)
        {
            var rect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            rect.height = thickness;
            rect.y += padding / 2;
            rect.x -= 2;
            rect.width += 6;
            EditorGUI.DrawRect(rect, color);
        }
    }
}

#endif