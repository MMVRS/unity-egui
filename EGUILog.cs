#if UNITY_EDITOR

using UnityEngine;

namespace Editor
{
    public static partial class EGUI
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }
        
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}

#endif