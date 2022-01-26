#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.List
{
    public abstract class ListItemRenderer<I>
    {
        public int            Index  { get; private set; }
        public I              Item   { get; private set; }
        public ListItemAction Action { get; private set; } = ListItemAction.None;

        protected IReadOnlyList<I> ListItems => _listItems;

        private List<I> _listItems;

        /*
         * Public.
         */

        public void Init(int index, I item, List<I> listItems)
        {
            _listItems = listItems;
            Index = index;
            Item = item;
        }

        public void OnEGUI(I item)
        {
            Item = item;
            OnEGUI();
        }

        public abstract void OnEGUI();

        /*
         * Protected.
         */

        protected void SetAction(ListItemAction action)
        {
            Action = action;
        }

        protected void SetItem(I item)
        {
            _listItems[Index] = item;
        }

        protected void RenderViewButton()
        {
            EGUI.Button("View", 50, EGUI.ButtonHeight02, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.View);
        }

        protected void RenderUpButton()
        {
            EGUI.Button("↑", 30, EGUI.ButtonHeight02, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.Up);
        }

        protected void RenderDownButton()
        {
            EGUI.Button("↓", 30, EGUI.ButtonHeight02, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.Down);
        }

        protected void RenderDeleteButton()
        {
            EGUI.Button("×", 30, EGUI.ButtonHeight02, new RectOffset(1, 1, 0, 2), SetAction, ListItemAction.Delete);
        }
    }
}

#endif