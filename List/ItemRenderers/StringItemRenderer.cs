#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.List.ItemRenderers
{
    public sealed class StringItemRenderer : ListItemRenderer<string>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.TextField(Item, (int)EGUI.ButtonHeight02, TextAnchor.MiddleLeft, SetItem);
                EGUI.Button("Up", 60, EGUI.ButtonHeight02, SetAction, ListItemAction.Up);
                EGUI.Button("Down", 60, EGUI.ButtonHeight02, SetAction, ListItemAction.Down);
                EGUI.Button("Delete", 80, EGUI.ButtonHeight02, SetAction, ListItemAction.Delete);
            });
        }
    }
}

#endif