#if UNITY_EDITOR

using System;
using UnityEditor;

namespace Editor
{
    public static partial class EGUI
    {
        public static void Enum(ref Enum value)
        {
            value = EditorGUILayout.EnumPopup(value);
        }

        public static void Enum(Enum value, Action<Enum> onChange)
        {
            var valueNew = EditorGUILayout.EnumPopup(value);
            if (!Equals(valueNew, value))
                onChange.Invoke(valueNew);
        }
        
        public static void Enum(Enum value, int height, Action<Enum> onChange)
        {
            var heightTemp = EditorStyles.popup.fixedHeight;
            EditorStyles.popup.fixedHeight = height;

            var valueNew = EditorGUILayout.EnumPopup(value);

            EditorStyles.popup.fixedHeight = heightTemp;

            if (!Equals(valueNew, value))
                onChange.Invoke(valueNew);
        }
    }
}

#endif