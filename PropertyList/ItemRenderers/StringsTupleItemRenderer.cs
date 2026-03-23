#if UNITY_EDITOR

using UnityEngine;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class StringsTupleItemRenderer : PropertyListItemRenderer<(string, string)>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.TextField(Item.Item1, EGUI.ButtonHeight02, TextAnchor.MiddleLeft, OnProperty1Changed);
                EGUI.TextField(Item.Item2, EGUI.ButtonHeight02, TextAnchor.MiddleLeft, OnProperty2Changed);

                TryRenderButton(ButtonType.Up, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Down, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Delete, EGUI.ButtonHeight02);
            });
        }

        private void OnProperty1Changed(string value)
        {
            SetItem((value, Item.Item2));
        }
        
        private void OnProperty2Changed(string value)
        {
            SetItem((Item.Item1, value));
        }
    }
}

#endif