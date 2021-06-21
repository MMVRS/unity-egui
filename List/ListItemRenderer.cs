#if UNITY_EDITOR

using System.Collections.Generic;

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
    }
}

#endif