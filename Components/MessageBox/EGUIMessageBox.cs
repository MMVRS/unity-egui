#if UNITY_EDITOR

using UnityEditor;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void MessageBox(string message, MessageType messageType)
        {
            EditorGUILayout.HelpBox(message, messageType);
        }
    }
}

#endif