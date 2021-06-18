#if UNITY_EDITOR

using System.Collections.Generic;

namespace Build1.UnityEGUI.List
{
    public abstract class EGUIListItemRenderer<I>
    {
        public int                        Index  { get; private set; }
        public I                          Item   { get; private set; }
        public EGUIListItemRendererAction Action { get; private set; } = EGUIListItemRendererAction.None;

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

        public void Validate(I item, out bool valid, out string message)
        {
            Item = item;
            valid = OnValidate(out message);
        }

        protected virtual bool OnValidate(out string message)
        {
            message = null;
            return true;
        }

        /*
         * Protected.
         */

        protected void SetAction(EGUIListItemRendererAction action)
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