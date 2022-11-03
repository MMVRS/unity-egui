#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI
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

        public static TR Horizontally<TR>(Func<TR> onHorizontally)
        {
            EditorGUILayout.BeginHorizontal();
            var value = onHorizontally.Invoke();
            EditorGUILayout.EndHorizontal();
            return value;
        }
        
        public static TR Horizontally<T1, TR>(Func<T1, TR> onHorizontally, T1 param01)
        {
            EditorGUILayout.BeginHorizontal();
            var value = onHorizontally.Invoke(param01);
            EditorGUILayout.EndHorizontal();
            return value;
        }
        
        public static TR Horizontally<T1, T2, TR>(Func<T1, T2, TR> onHorizontally, T1 param01, T2 param02)
        {
            EditorGUILayout.BeginHorizontal();
            var value = onHorizontally.Invoke(param01, param02);
            EditorGUILayout.EndHorizontal();
            return value;
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
        
        public static TR Vertically<TR>(Func<TR> onHorizontally)
        {
            EditorGUILayout.BeginVertical();
            var value = onHorizontally.Invoke();
            EditorGUILayout.EndVertical();
            return value;
        }
        
        public static TR Vertically<T1, TR>(Func<T1, TR> onHorizontally, T1 param01)
        {
            EditorGUILayout.BeginVertical();
            var value = onHorizontally.Invoke(param01);
            EditorGUILayout.EndVertical();
            return value;
        }
        
        public static TR Vertically<T1, T2, TR>(Func<T1, T2, TR> onHorizontally, T1 param01, T2 param02)
        {
            EditorGUILayout.BeginVertical();
            var value = onHorizontally.Invoke(param01, param02);
            EditorGUILayout.EndVertical();
            return value;
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

        public static void Line(int thickness = 2, int padding = 0)
        {
            Line(Color.gray, thickness, padding);
        }
        
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
