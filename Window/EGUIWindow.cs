#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.Window
{
    public abstract class EGUIWindow : EditorWindow
    {
        protected int Padding { get; set; }

        protected event Action FocusLost;

        private Type _initializeFlag;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void OnGUI()
        {
            if (_initializeFlag == null)
            {
                OnInitialize();
                _initializeFlag = GetType();
            }

            if (Padding != 0)
            {
                GUILayout.BeginArea(new Rect(Padding, Padding, position.width - Padding * 2, position.height - Padding * 2.5F));
                OnEGUI();
                GUILayout.EndArea();
            }
            else
            {
                OnEGUI();
            }
        }

        protected virtual  void OnInitialize() { }
        protected abstract void OnEGUI();
        
        private void OnLostFocus()
        {
            FocusLost?.Invoke();
        }

        /*
         * Static.
         */

        public static T Open<T>(string title, bool utility, bool focus) where T : EGUIWindow
        {
            return GetWindow<T>(utility, title, focus);
        }

        public static T Open<T>(string title, Vector2Int dimensions, bool utility, bool focus, EGUIWindowAnchor anchor = EGUIWindowAnchor.Center) where T : EGUIWindow
        {
            return Open<T>(title, dimensions.x, dimensions.y, utility, focus, anchor);
        }

        public static T Open<T>(string title, int width, int height, bool utility, bool focus, EGUIWindowAnchor anchor = EGUIWindowAnchor.Center) where T : EGUIWindow
        {
            var window = GetWindow<T>(utility, title, focus);
            window.position = GetWindowRect(width, height, anchor);
            return window;
        }

        public static T Create<T>(string title) where T : EGUIWindow
        {
            var window = CreateWindow<T>(title);
            window.Show();
            return window;
        }

        public static T Create<T>(string title, int width, int height, bool show, EGUIWindowAnchor anchor = EGUIWindowAnchor.Center) where T : EGUIWindow
        {
            var window = CreateWindow<T>(title);
            window.position = GetWindowRect(width, height, anchor);
            
            if (show)
                window.Show();
            
            return window;
        }

        public static Rect GetWindowRect(int width, int height, EGUIWindowAnchor anchor)
        {
            switch (anchor)
            {
                case EGUIWindowAnchor.Center:
                    var main = EditorGUIUtility.GetMainWindowPosition();
                    var centerWidth = (main.width - width) * 0.5f;
                    var centerHeight = (main.height - height) * 0.5f;
                    return new Rect(main.x + centerWidth, main.y + centerHeight, width, height);

                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }
        }
    }
}

#endif