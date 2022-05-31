#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class EnumItemRenderer<T> : PropertyListItemRenderer<T> where T : Enum
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.Enum(Item, (int)EGUI.ButtonHeight02, value => { SetItem((T)value); });
                RenderUpButton();
                RenderDownButton();
                RenderDeleteButton();
            });
        }
    }
}

#endif