#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyWindow
{
    public abstract class PropertyWindow
    {
        public virtual Vector2Int Size  => EGUI.PropertyWindowSizeDefault;
        public virtual string     Title => GetType().Name;
        
        public abstract void OnEGUI();
        public virtual  void OnFocusLost() { }
    }
    
    public abstract class PropertyWindow<I> : PropertyWindow
    {
        protected I                Item  { get; private set; }
        protected int              Index { get; private set; }
        protected IReadOnlyList<I> Items => _items;

        private List<I> _items;

        /*
         * Public.
         */

        internal void Initialize(int index, I item, List<I> items)
        {
            Item = item;
            Index = index;

            _items = items;
        }
    }
}

#endif