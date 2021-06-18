#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void Space()           { GUILayout.FlexibleSpace(); }
        public static void Space(int pixels) { GUILayout.Space(pixels); }
    }
}

#endif