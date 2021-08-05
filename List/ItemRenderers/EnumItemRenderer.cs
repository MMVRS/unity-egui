#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.List.ItemRenderers
{
    public sealed class EnumItemRenderer<T> : ListItemRenderer<T> where T : Enum
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.Enum(Item, (int)EGUI.ButtonHeight02, value => { SetItem((T)value); });
                EGUI.Button("Up", 60, EGUI.ButtonHeight02, SetAction, ListItemAction.Up);
                EGUI.Button("Down", 60, EGUI.ButtonHeight02, SetAction, ListItemAction.Down);
                EGUI.Button("Delete", 80, EGUI.ButtonHeight02, SetAction, ListItemAction.Delete);
            });
        }
    }
}

#endif