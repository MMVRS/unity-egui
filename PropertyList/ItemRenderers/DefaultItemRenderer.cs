#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class DefaultItemRenderer<I> : PropertyListItemRenderer<I>
    {
        public override void OnEGUI()
        {
            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    var height = EGUI.ButtonHeight05;
                    var title = Title ?? $"{Index}: {Item}";
                    EGUI.Label(title, height, FontStyle.Bold);
                
                    RenderViewButton(height);
                    RenderUpButton(height);
                    RenderDownButton(height);
                    RenderDeleteButton(height);
                });
            });
        }
    }
}

#endif