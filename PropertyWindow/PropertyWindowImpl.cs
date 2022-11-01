#if UNITY_EDITOR

using Build1.UnityEGUI.Window;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyWindow
{
    public sealed class PropertyWindowImpl : EGUIWindow
    {
        private PropertyWindow _window;
        private Vector2        _scrollPosition = new(0, 1);

        /*
         * Public.
         */

        public PropertyWindowImpl Initialize(PropertyWindow window)
        {
            _window = window;
            return this;
        }

        protected override void OnFocusLost()
        {
            _window.OnFocusLost();
        }

        public new PropertyWindowImpl Show()
        {
            base.Show();
            return this;
        }
        
        /*
         * Protected.
         */
        
        protected override void OnEGUI()
        {
            Padding = 10;
            
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
                EGUI.Button("Close", EGUI.Size(120, EGUI.ButtonHeight02)).OnClick(Close);
            });
        }
    }
}

#endif