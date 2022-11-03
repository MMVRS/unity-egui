#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.Window
{
    public abstract class EGUIWindow : EditorWindow
    {
        protected int Padding { get; set; } = -1;

        private Type _initializeFlag;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void OnLostFocus()
        {
            OnFocusLost();
        }

        protected virtual void OnFocusLost() { }

        private void OnGUI()
        {
            if (Padding == -1)
                Padding = EGUI.WindowPaddingDefault;
            
            if (_initializeFlag == null)
            {
                OnInitialize();
                _initializeFlag = GetType();
            }

            if (Padding > 0)
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
    }

    public abstract class EGUIWindow<D> : EditorWindow
    {
        protected D   Data    { get; private set; }
        protected int Padding { get; set; } = -1;

        private Type _initializeFlag;
        
        internal void SetData(D data)
        {
            Data = data;
        }
        
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake() { }

        private void OnLostFocus()
        {
            OnFocusLost();
        }

        protected virtual void OnFocusLost() { }

        private void OnGUI()
        {
            if (Padding == -1)
                Padding = EGUI.WindowPaddingDefault;
            
            if (_initializeFlag == null)
            {
                OnInitialize();
                _initializeFlag = GetType();
            }

            if (Padding > 0)
            {
                GUILayout.BeginArea(new Rect(Padding, Padding, position.width - Padding * 2, position.height - Padding * 2.5F));
                OnEGUI(Data);
                GUILayout.EndArea();
            }
            else
            {
                OnEGUI(Data);
            }
        }

        protected virtual  void OnInitialize() { }
        protected abstract void OnEGUI(D data);
    }
}

#endif