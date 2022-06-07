#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class DefaultItemRenderer<I> : PropertyListItemRenderer<I>
    {
        internal string Title { get; set; }

        public override void OnEGUI()
        {
            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    var height = EGUI.ButtonHeight05;
                    var title = Title ?? Item.ToString();
                    EGUI.Label(title, height, FontStyle.Normal);

                    RenderDetailsButton(height);
                    RenderUpButton(height);
                    RenderDownButton(height);
                    RenderDeleteButton(height);
                });
            });
        }
    }
}

#endif