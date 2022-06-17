#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class StringItemRenderer : PropertyListItemRenderer<string>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.TextField(Item, (int)EGUI.ButtonHeight02, TextAnchor.MiddleLeft, SetItem);
                RenderUpButton(EGUI.ButtonHeight02);
                RenderDownButton(EGUI.ButtonHeight02);
                RenderDeleteButton(EGUI.ButtonHeight02);
            });
        }
    }
}

#endif