#if UNITY_EDITOR

using System;
using Build1.UnityEGUI.Window;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.Windows
{
    internal sealed class ItemWindowRenderer : EGUIWindow
    {
        internal string Title            { get; private set; }
        internal Action RenderHandler    { get; private set; }

        private Vector2 _scrollPosition = new(0, 1);

        protected override void OnAwake()
        {
            Padding = 10;
        }

        internal void Initialize(string windowTitle, Action renderHandler, Action focusLostHandler)
        {
            Title = windowTitle;
            RenderHandler = renderHandler;

            if (focusLostHandler != null)
                FocusLost += focusLostHandler;
        }
        
        protected override void OnEGUI()
        {
            EGUI.Scroll(ref _scrollPosition, RenderHandler.Invoke);
            EGUI.Space(10);
            EGUI.Horizontally(() =>
            {
                EGUI.Space();
                EGUI.Button("Close", 120, EGUI.ButtonHeight02, Close);
            });
        }
    }
}

#endif