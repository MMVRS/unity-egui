#if UNITY_EDITOR

using UnityEditor;

namespace Editor
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