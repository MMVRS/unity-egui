#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void Enabled(bool enabled, Action onEnable)
        {
            var changed = false;
            if (GUI.enabled != enabled)
            {
                GUI.enabled = enabled;
                changed = true;
            }

            onEnable?.Invoke();

            if (changed)
                GUI.enabled = !enabled;
        }
    }
}

#endif