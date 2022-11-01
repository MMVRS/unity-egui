#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.Components.Button
{
    public struct ButtonResult
    {
        private readonly bool clicked;
        
        internal ButtonResult(bool clicked)
        {
            this.clicked = clicked;
        }

        public bool Clicked()
        {
            return clicked;
        }
        
        public void Clicked(out bool value)
        {
            value = clicked;
        }
        
        public void OnClick(Action action)
        {
            if (clicked)
                action();
        }
        
        public void OnClick<T>(Action<T> action, T param)
        {
            if (clicked)
                action(param);
        }
    }
}

#endif