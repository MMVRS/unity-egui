#if UNITY_EDITOR

using Build1.UnityEGUI.Window;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyWindow
{
    internal sealed class PropertyWindowImpl : EGUIWindow
    {
        private PropertyWindow _window;
        private Vector2        _scrollPosition = new(0, 1);

        protected override void OnAwake()
        {
            Padding = 10;
        }
        
        /*
         * Public.
         */

        public void Initialize(PropertyWindow window)
        {
            _window = window;
            
            FocusLost += _window.OnFocusLost;
        }
        
        /*
         * Protected.
         */
        
        protected override void OnEGUI()
        {
            try
            {
                EGUI.Scroll(ref _scrollPosition, _window.OnEGUI);
            }
            catch
            {
                // This is to escape exceptions when recompiling scripts.
                Close();
                return;
            }

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