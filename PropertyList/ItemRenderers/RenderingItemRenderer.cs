#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.PropertyList.ItemRenderers
{
    public abstract class RenderingItemRenderer
    {
        public static RenderingItemRenderer<I> Render<I>(I item, Action<PropertyListItemRenderer<I>> renderingFunction)
        {
            var renderer = new RenderingItemRenderer<I>(renderingFunction);
            renderer.Init(item, 0, 0, null, null, ButtonType.All);
            renderer.OnEGUI();
            return renderer;
        }
    }
    
    public sealed class RenderingItemRenderer<I> : PropertyListItemRenderer<I>
    {
        private readonly Action<PropertyListItemRenderer<I>> _renderingFunction;
        
        internal RenderingItemRenderer(Action<PropertyListItemRenderer<I>> renderingFunction)
        {
            _renderingFunction = renderingFunction;
        }

        public override void OnEGUI()
        {
            EGUI.Panel(10, () =>
            {
                EGUI.Horizontally(() =>
                {
                    _renderingFunction.Invoke(this);
                });
            });
        }
    }
}

#endif