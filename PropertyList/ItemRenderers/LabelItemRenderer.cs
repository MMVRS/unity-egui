#if UNITY_EDITOR

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class LabelItemRenderer : PropertyListItemRenderer<string>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.Label(Item, EGUI.Height(EGUI.ButtonHeight02));

                TryRenderButton(ButtonType.Up, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Down, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Delete, EGUI.ButtonHeight02);
            });
        }
    }
}

#endif