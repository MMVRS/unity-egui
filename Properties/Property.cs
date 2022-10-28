#if UNITY_EDITOR

using Build1.UnityEGUI.Components.Label;
using Build1.UnityEGUI.Components.Title;
using UnityEngine;

namespace Build1.UnityEGUI.Properties
{
    public struct Property
    {
        public readonly PropertyType type;
        
        public readonly int          valueInt;
        public readonly bool         valueBool;
        public readonly FontStyle    valueFontStyle;
        public readonly TextAnchor   valueTextAnchor;
        
        public readonly LabelType valueLabelType;
        public readonly TitleType valueTitleType;

        internal Property(PropertyType type, int value)
        {
            this.type = type;
            
            valueInt = value;
            valueBool = default;
            valueFontStyle = default;
            valueTextAnchor = default;
            
            valueLabelType = default;
            valueTitleType = default;
        }
        
        internal Property(PropertyType type, bool value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = value;
            valueFontStyle = default;
            valueTextAnchor = default;
            
            valueLabelType = default;
            valueTitleType = default;
        }

        internal Property(PropertyType type, FontStyle value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = default;
            valueFontStyle = value;
            valueTextAnchor = default;
            
            valueLabelType = default;
            valueTitleType = default;
        }
        
        internal Property(PropertyType type, TextAnchor value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = default;
            valueFontStyle = default;
            valueTextAnchor = value;
            
            valueLabelType = default;
            valueTitleType = default;
        }
        
        internal Property(PropertyType type, LabelType value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = default;
            valueFontStyle = default;
            valueTextAnchor = default;
            
            valueLabelType = value;
            valueTitleType = default;
        }
        
        internal Property(PropertyType type, TitleType value)
        {
            this.type = type;
            
            valueInt = default;
            valueBool = default;
            valueFontStyle = default;
            valueTextAnchor = default;
            
            valueLabelType = default;
            valueTitleType = value;
        }
    }
}

#endif