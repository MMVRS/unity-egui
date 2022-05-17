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
            EGUI.Button("View", 50, height, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.View);
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
            EGUI.Button("↑", 30, height, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.Up);
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
            EGUI.Button("↓", 30, height, new RectOffset(2, 0, 0, 0), SetAction, ListItemAction.Down);
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
            EGUI.Button("×", 30, height, new RectOffset(1, 1, 0, 2), SetAction, ListItemAction.Delete);
        }
    }
}

#endif