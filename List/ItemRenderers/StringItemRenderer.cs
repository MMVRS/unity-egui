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
                RenderUpButton();
                RenderDownButton();
                RenderDeleteButton();
            });
        }
    }
}

#endif