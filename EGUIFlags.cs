#if UNITY_EDITOR

using System;
using System.Linq;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static void CheckboxFlags<T>(T value, Action<T, bool> onChange) where T: Enum
        {
            Horizontally(() =>
            {
                foreach (var flag in System.Enum.GetValues(typeof(T)).Cast<T>())
                {
                    Checkbox(flag.ToString(), value.HasFlag(flag), value => onChange(flag, value));
                }
            });
        }
    }
}

#endif
