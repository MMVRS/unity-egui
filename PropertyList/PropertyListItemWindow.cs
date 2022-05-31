#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public abstract class PropertyListItemWindow<I>
    {
        public abstract Vector2Int Size { get; }

        public I Item { get; private set; }
        
        protected string           Title { get; private set; }   
        protected int              Index { get; private set; }
        protected IReadOnlyList<I> Items => _items;

        private List<I> _items;
        
        /*
         * Public.
         */
        
        internal void Init(string title, int index, I item, List<I> items)
        {
            Title = title;
            Item = item;
            Index = index;

            _items = items;
        }

        public abstract void OnEGUI();
        public virtual  void OnFocusLost() { }
    }
}

#endif