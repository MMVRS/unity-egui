#if UNITY_EDITOR

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public sealed class IntItemRenderer : PropertyListItemRenderer<int>
    {
        public override void OnEGUI()
        {
            EGUI.Horizontally(() =>
            {
                EGUI.Int(Item, EGUI.Height(EGUI.ButtonHeight02))
                    .OnChange(SetItem);
                
                TryRenderButton(ButtonType.Up, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Down, EGUI.ButtonHeight02);
                TryRenderButton(ButtonType.Delete, EGUI.ButtonHeight02);
            });
        }
    }
}

#endif