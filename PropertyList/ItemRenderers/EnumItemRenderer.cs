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
                EGUI.Enum(Item, EGUI.ButtonHeight02, value => { SetItem((T)value); });
                
                TryRenderButton(ButtonType.Up);
                TryRenderButton(ButtonType.Down);
                TryRenderButton(ButtonType.Delete);
            });
        }
    }
}

#endif