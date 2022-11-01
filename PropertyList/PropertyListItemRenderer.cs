#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public abstract class PropertyListItemRenderer<I>
    {
        public I                      Item   { get; private set; }
        public PropertyListItemAction Action { get; private set; } = PropertyListItemAction.None;

        protected int Index           { get; private set; }
        protected int IndexUnfiltered { get; private set; }

        protected IReadOnlyList<I> Items           => _items;
        protected IReadOnlyList<I> ItemsUnfiltered => _itemsUnfiltered;

        private List<I> _items;
        private List<I> _itemsUnfiltered;

        /*
         * Internal.
         */

        internal void Init(I item, int index, int indexUnfiltered, List<I> items, List<I> itemsUnfiltered)
        {
            Item = item;
            
            Index = index;
            IndexUnfiltered = indexUnfiltered;

            _items = items;
            _itemsUnfiltered = itemsUnfiltered;
        }

        public abstract void OnEGUI();

        /*
         * Protected.
         */

        protected void SetItem(I item)
        {
            _itemsUnfiltered[IndexUnfiltered] = item;
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

        public void RenderDetailsButton()
        {
            RenderDetailsButton(EGUI.ButtonHeight04);
        }

        public void RenderDetailsButton(int height)
        {
            EGUI.Button("...", EGUI.Size(30, height), EGUI.Padding(new RectOffset(2, 0, 0, 0)))
                .OnClick(SetAction, PropertyListItemAction.Details);
        }

        /*
         * Up Button.
         */

        public void RenderUpButton()
        {
            RenderUpButton(EGUI.ButtonHeight04);
        }

        public void RenderUpButton(int height)
        {
            EGUI.Button("↑", EGUI.Size(30, height), EGUI.Padding(new RectOffset(2, 0, 0, 0))).OnClick(SetAction, PropertyListItemAction.Up);
        }

        /*
         * Down Button.
         */

        public void RenderDownButton()
        {
            RenderDownButton(EGUI.ButtonHeight04);
        }

        public void RenderDownButton(int height)
        {
            EGUI.Button("↓", EGUI.Size(30, height), EGUI.Padding(new RectOffset(2, 0, 0, 0))).OnClick(SetAction, PropertyListItemAction.Down);
        }

        /*
         * Delete Button.
         */

        public void RenderDeleteButton()
        {
            RenderDeleteButton(EGUI.ButtonHeight04);
        }

        public void RenderDeleteButton(int height)
        {
            EGUI.Button("×", EGUI.Size(30, height), EGUI.Padding(new RectOffset(1, 1, 0, 2))).OnClick(SetAction, PropertyListItemAction.Delete);
        }
    }
}

#endif