#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.PropertyList.ItemRenderers;
using Build1.UnityEGUI.PropertyList.Windows;
using Build1.UnityEGUI.Types;
using Build1.UnityEGUI.Window;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public sealed class PropertyList<I>
    {
        internal LayoutType LayoutType { get; set; } = LayoutType.Vertical;
        internal string     Label      { get; set; }
        internal List<I>    Items      { get; set; }
        internal int        CountMax   { get; set; } = 100;
        internal int        Padding    { get; set; } = 10;

        private Type _itemRendererType;
        private Type _windowRendererType;

        private PropertyListAddBehavior _addBehavior = PropertyListAddBehavior.Default;

        private Action<List<I>>      _onCreate;
        private Func<I, string>      _onItemTitle;
        private Func<Tuple<bool, I>> _onItemAdd;
        private Func<I, bool>        _onItemDelete;
        private Func<I, bool>        _onItemFilter;
        private Action<I>            _onItemDetails;
        private Action<I, int>       _onItemIndexChanged;

        internal PropertyList(string label, List<I> items)
        {
            Label = label;
            Items = items;
        }

        /*
         * Internal.
         */

        internal PropertyList<I> OnCreate(Action<List<I>> handler)
        {
            _onCreate = handler;
            return this;
        }

        /*
         * Public
         */

        public PropertyList<I> ItemRenderer<R>() where R : PropertyListItemRenderer<I>
        {
            _itemRendererType = typeof(R);
            return this;
        }

        public PropertyList<I> WindowRenderer<W>() where W : PropertyListItemWindow<I>
        {
            _windowRendererType = typeof(W);
            return this;
        }

        public PropertyList<I> OnItemTitle(Func<I, string> handler)
        {
            _onItemTitle = handler;
            return this;
        }

        public PropertyList<I> OnItemAdd(Func<Tuple<bool, I>> handler)
        {
            _onItemAdd = handler;
            return this;
        }
        
        public PropertyList<I> OnItemAdd(PropertyListAddBehavior behavior, Func<Tuple<bool, I>> handler)
        {
            _onItemAdd = handler;
            _addBehavior = behavior;
            return this;
        }

        public PropertyList<I> OnItemDelete(Func<I, bool> handler)
        {
            _onItemDelete = handler;
            return this;
        }

        public PropertyList<I> OnItemFilter(Func<I, bool> handler)
        {
            _onItemFilter = handler;
            return this;
        }

        public PropertyList<I> OnItemDetails(Action<I> handler)
        {
            _onItemDetails = handler;
            return this;
        }
        
        public PropertyList<I> OnItemIndexChanged(Action<I, int> handler)
        {
            _onItemIndexChanged = handler;
            return this;
        }

        public void Build()
        {
            EGUI.Horizontally(() =>
            {
                if (!string.IsNullOrWhiteSpace(Label))
                    EGUI.Label(Label);

                EGUI.Space();
                EGUI.Label("Count:");

                EGUI.Enabled(_onItemAdd != null, () => { EGUI.Int(Items?.Count ?? 0, 50, CountChanged); });
            });

            EGUI.Space(3);

            var style = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(Padding, Padding, Padding, Padding)
            };

            switch (LayoutType)
            {
                case LayoutType.Horizontal:
                    EGUI.Horizontally(style, BuildImpl);
                    break;
                case LayoutType.Vertical:
                    EGUI.Vertically(style, BuildImpl);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (typeof(I).IsEnum || _addBehavior == PropertyListAddBehavior.Disabled)
                return;

            EGUI.Space(3);
            EGUI.Horizontally(() =>
            {
                EGUI.Space();
                EGUI.Button("+", 30, 22, new RectOffset(0, 0, 0, 2), Add);
            });
        }

        /*
         * Private.
         */

        private void BuildImpl()
        {
            if (Items == null || Items.Count == 0)
            {
                EGUI.Space(10);
                EGUI.Label("Empty", true, TextAnchor.MiddleCenter);
                EGUI.Space(10);
                return;
            }

            for (var i = 0; i < Items.Count; i++)
            {
                if (_onItemFilter?.Invoke(Items[i]) == false)
                    continue;

                PropertyListItemRenderer<I> itemRenderer;

                if (_itemRendererType != null)
                    itemRenderer = (PropertyListItemRenderer<I>)Activator.CreateInstance(_itemRendererType);
                else
                    itemRenderer = new DefaultItemRenderer<I>();

                var item = Items[i];
                var title = _onItemTitle?.Invoke(item);

                itemRenderer.Init(title, i, item, Items);
                itemRenderer.OnEGUI();

                ProcessAction(itemRenderer.Action, itemRenderer.Item);
            }
        }

        private void ProcessAction(PropertyListItemAction action, I item)
        {
            int index;

            switch (action)
            {
                case PropertyListItemAction.Up:
                    index = Math.Max(0, Items.IndexOf(item) - 1);
                    Items.Remove(item);
                    Items.Insert(index, item);
                    _onItemIndexChanged?.Invoke(item, index);
                    break;

                case PropertyListItemAction.Down:
                    index = Items.IndexOf(item);
                    if (index < Items.Count - 1)
                    {
                        index = Math.Min(Items.Count, Items.IndexOf(item) + 1);
                        Items.Remove(item);
                        Items.Insert(index, item);
                        _onItemIndexChanged?.Invoke(item, index);
                    }
                    break;

                case PropertyListItemAction.Delete:
                    if (_onItemDelete == null || _onItemDelete.Invoke(item))
                        Items.Remove(item);
                    break;

                case PropertyListItemAction.Details:

                    if (_windowRendererType != null)
                    {
                        var title = _onItemTitle?.Invoke(item);
                        index = Items.IndexOf(item);

                        var window = (PropertyListItemWindow<I>)Activator.CreateInstance(_windowRendererType);
                        window.Init(title, index, item, Items);

                        var size = window.Size;
                        var windowInstance = EGUIWindow.Create<ItemWindowRenderer>(title, size.x, size.y, false);
                        windowInstance.Initialize(title, window.OnEGUI, window.OnFocusLost);
                        windowInstance.Show();
                    }
                    else
                    {
                        if (_onItemDetails == null)
                            EGUI.LogError("EGUI: PropertyList details handler not specified");
                        else
                            _onItemDetails.Invoke(item);
                    }

                    break;
            }
        }

        private void CountChanged(int count)
        {
            count = Mathf.Min(count, CountMax);
            if (count == (Items?.Count ?? 0))
                return;

            TryCreate();

            if (count < Items.Count)
            {
                Items.RemoveRange(count, Items.Count - count);
            }
            else if (count > Items.Count && _onItemAdd != null)
            {
                if (count > Items.Capacity)
                    Items.Capacity = count;

                var countToAdd = count - Items.Count;
                for (var i = 0; i < countToAdd; i++)
                {
                    var itemInfo = _onItemAdd.Invoke();
                    if (itemInfo.Item1)
                        Items.Add(itemInfo.Item2);
                }
            }
        }

        private void Add()
        {
            TryCreate();

            I item = default;
            
            if (_onItemAdd != null)
            {
                var itemInfo = _onItemAdd.Invoke();
                if (!itemInfo.Item1)
                    return;

                item = itemInfo.Item2;
            }
            
            Items.Add(item);

            if (_addBehavior == PropertyListAddBehavior.OpenDetails)
                ProcessAction(PropertyListItemAction.Details, item);
        }

        private void TryCreate()
        {
            if (Items != null)
                return;

            Items = new List<I>();
            _onCreate?.Invoke(Items);
        }
    }
}

#endif