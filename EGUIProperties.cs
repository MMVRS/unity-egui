#if UNITY_EDITOR

using Build1.UnityEGUI.Properties;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static Property Enabled(bool value)               { return new Property(PropertyType.Enabled, value); }
        public static Property Width(int value)                  { return new Property(PropertyType.Width, value); }
        public static Property Height(int value)                 { return new Property(PropertyType.Height, value); }
        public static Property Size(int width, int height)       { return new Property(PropertyType.Size, new Vector2Int(width, height)); }
        public static Property OffsetX(int value)                { return new Property(PropertyType.OffsetX, value); }
        public static Property Padding(RectOffset value)         { return new Property(PropertyType.Padding, value); }
        public static Property FontStyle(FontStyle value)        { return new Property(PropertyType.FontStyle, value); }
        public static Property TextAnchor(TextAnchor value)      { return new Property(PropertyType.TextAnchor, value); }
        public static Property StretchedWidth(bool value = true) { return new Property(PropertyType.StretchedWidth, value); }
    }
}

#endif