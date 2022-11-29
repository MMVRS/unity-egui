#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public abstract class PropertyListItemRenderer<I>
    {
        public    I                      Item            { get; private set; }
        public    PropertyListItemAction Action          { get; private set; } = PropertyListItemAction.None;
        public    ButtonType             Buttons         => _buttons;
        
        protected int                    Index           { get; private set; }
        protected int                    IndexUnfiltered { get; private set; }

        protected IReadOnlyList<I> Items           => _items;
        protected IReadOnlyList<I> ItemsUnfiltered => _itemsUnfiltered;

        private List<I>    _items;
        private List<I>    _itemsUnfiltered;
        private ButtonType _buttons;

        /*
         * Internal.
         */

        internal void Init(I item, int index, int indexUnfiltered, List<I> items, List<I> itemsUnfiltered, ButtonType buttons)
        {
            Item = item;
            
            Index = index;
            IndexUnfiltered = indexUnfiltered;

            _items = items;
            _itemsUnfiltered = itemsUnfiltered;
            _buttons = buttons;
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
         * Buttons.
         */

        public void TryRenderButton(ButtonType button)
        {
            if ((_buttons & button) == button)
                RenderButton(button);
        }
        
        public void TryRenderButton(ButtonType button, int height)
        {
            if ((_buttons & button) == button)
                RenderButton(button, height);
        }

        public void RenderButton(ButtonType button)
        {
            RenderButton(button, EGUI.ButtonHeight04);
        }
        
        public void RenderButton(ButtonType button, int height)
        {
            switch (button)
            {
                case ButtonType.Up:
                    EGUI.Button("↑", EGUI.Size(30, height), EGUI.Padding(new RectOffset(2, 0, 0, 0))).OnClick(SetAction, PropertyListItemAction.Up);
                    break;
                case ButtonType.Down:
                    EGUI.Button("↓", EGUI.Size(30, height), EGUI.Padding(new RectOffset(2, 0, 0, 0))).OnClick(SetAction, PropertyListItemAction.Down);
                    break;
                case ButtonType.Delete:
                    EGUI.Button("×", EGUI.Size(30, height), EGUI.Padding(new RectOffset(1, 1, 0, 2))).OnClick(SetAction, PropertyListItemAction.Delete);
                    break;
                case ButtonType.Details:
                    EGUI.Button("...", EGUI.Size(30, height)).OnClick(SetAction, PropertyListItemAction.Details);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }
    }
}

#endif