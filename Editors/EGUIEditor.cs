#if UNITY_EDITOR

using UnityEditor;

namespace Build1.UnityEGUI.Editors
{
    public abstract class EGUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            OnEGUI();
        }

        protected abstract void OnEGUI();
    }
}

#endif