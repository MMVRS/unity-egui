#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.Properties
{
    public struct Property
    {
        public readonly PropertyType type;

        public readonly int        valueInt;
        public readonly bool       valueBool;
        public readonly Vector2Int valueVector2Int;
        public readonly RectOffset valueRectOffset;
        public readonly FontStyle  valueFontStyle;
        public readonly TextAnchor valueTextAnchor;

        public Property(PropertyType type, int value)
        {
            this.type = type;

            valueInt = value;
            valueBool = default;
            valueVector2Int = default;
            valueRectOffset = default;
            valueFontStyle = default;
            valueTextAnchor = default;
        }

        public Property(PropertyType type, bool value)
        {
            this.type = type;

            valueInt = default;
            valueBool = value;
            valueVector2Int = default;
            valueRectOffset = default;
            valueFontStyle = default;
            valueTextAnchor = default;
        }

        public Property(PropertyType type, Vector2Int value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = default;
            valueVector2Int = value;
            valueRectOffset = default;
            valueFontStyle = default;
            valueTextAnchor = default;
        }

        public Property(PropertyType type, RectOffset value)
        {
            this.type = type;

            valueInt = default;
            valueBool = default;
            valueVector2Int = default;
            valueRectOffset = value;
            valueFontStyle = default;
            valueTextAnchor = default;
        }

        public Property(PropertyType type, FontStyle value)
        {
            this.type = type;

            valueInt = default;
            valueBool = default;
            valueVector2Int = default;
            valueRectOffset = default;
            valueFontStyle = value;
            valueTextAnchor = default;
        }

        public Property(PropertyType type, TextAnchor value)
        {
            this.type = type;

            valueInt = default;
            valueBool = default;
            valueVector2Int = default;
            valueRectOffset = default;
            valueFontStyle = default;
            valueTextAnchor = value;
        }
    }
}

#endif