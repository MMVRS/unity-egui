#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class IntItemRenderer : PropertyListItemRenderer<int>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.Int(Item, EGUI.Height(EGUI.ButtonHeight02), EGUI.TextAnchor(TextAnchor.MiddleLeft))
                    .OnChange(SetItem);
                
                TryRenderButton(ButtonType.Up, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Down, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Delete, EGUI.ButtonHeight02);
            });
        }
    }
}

#endif