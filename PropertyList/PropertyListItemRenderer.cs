#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public abstract class PropertyListItemRenderer<I>
    {
        public I Item { get; private set; }
        
        internal PropertyListItemAction Action { get; private set; } = PropertyListItemAction.None;
        
        protected string           Title { get; private set; }   
        protected int              Index { get; private set; }
        protected IReadOnlyList<I> Items => _items;

        private List<I> _items;

        /*
         * Internal.
         */

        internal void Init(string title, int index, I item, List<I> items)
        {
            Title = title;
            Item = item;
            Index = index;

            _items = items;
        }

        public abstract void OnEGUI();

        /*
         * Protected.
         */
        
        protected void SetItem(I item)
        {
            _items[Index] = item;
        }
        
        /*
         * Private.
         */
        
        private void SetAction(PropertyListItemAction action)
        {
            Action = action;
        }

        /*
         * View Button.
         */

        protected void RenderViewButton()
        {
            RenderViewButton(EGUI.ButtonHeight02);
        }

        protected void RenderViewButtonThin()
        {
            RenderViewButton(EGUI.ButtonHeight05);
        }

        protected void RenderViewButton(float height)
        {
            EGUI.Button("...", 30, height, new RectOffset(2, 0, 0, 0), SetAction, PropertyListItemAction.Details);
        }

        /*
         * Up Button.
         */

        protected void RenderUpButton()
        {
            RenderUpButton(EGUI.ButtonHeight02);
        }

        protected void RenderUpButtonThin()
        {
            RenderUpButton(EGUI.ButtonHeight05);
        }

        protected void RenderUpButton(float height)
        {
            EGUI.Button("↑", 30, height, new RectOffset(2, 0, 0, 0), SetAction, PropertyListItemAction.Up);
        }

        /*
         * Down Button.
         */

        protected void RenderDownButton()
        {
            RenderDownButton(EGUI.ButtonHeight02);
        }

        protected void RenderDownButtonThin()
        {
            RenderDownButton(EGUI.ButtonHeight05);
        }

        protected void RenderDownButton(float height)
        {
            EGUI.Button("↓", 30, height, new RectOffset(2, 0, 0, 0), SetAction, PropertyListItemAction.Down);
        }

        /*
         * Delete Button.
         */

        protected void RenderDeleteButton()
        {
            RenderDeleteButton(EGUI.ButtonHeight02);
        }

        protected void RenderDeleteButtonThin()
        {
            RenderDeleteButton(EGUI.ButtonHeight05);
        }

        protected void RenderDeleteButton(float height)
        {
            EGUI.Button("×", 30, height, new RectOffset(1, 1, 0, 2), SetAction, PropertyListItemAction.Delete);
        }
    }
}

#endif