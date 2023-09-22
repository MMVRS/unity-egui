#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.Properties;
using Build1.UnityEGUI.PropertyList.ItemRenderers;
using Build1.UnityEGUI.PropertyWindow;
using Build1.UnityEGUI.Types;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.PropertyList
{
    public sealed class PropertyList<I>
    {
        internal LayoutType LayoutType { get; set; } = LayoutType.Vertical;
        internal string     Label      { get; set; }
        internal List<I>    Items      { get; set; }
        internal int        Padding    { get; set; } = 10;

        private Type         _itemRendererType;
        private ButtonType[] _itemRendererButtons = RenderingItemRenderer.ButtonsAll;

        private Type _windowRendererType;

        private string     _title;
        private Property[] _titleProperties;
        private bool       _titleShow = true;
        private bool       _countShow = true;
        private bool       _panelShow = true;

        private Action<List<I>>                     _onCreate;
        private Action                              _onFilters;
        private Func<I, string>                     _onItemTitle;
        private Action<PropertyListItemRenderer<I>> _onItemRender;
        private Func<I>                             _onItemAdd;
        private Func<I, I>                          _onItemCopy;
        private Func<bool>                          _onItemAddAvailable;
        private Func<bool>                          _onItemAddValidation;
        private Func<I, bool>                       _onItemDelete;
        private Func<I, bool>                       _onItemFilter;
        private Action<I>                           _onItemDetails;
        private Action<I, int>                      _onItemIndexChanged;
        private bool                                _isReadOnly;

        private int    _pageSize;
        private object _pageStorageKey;
        private int    _page;
        private int    _pagesTotal;

        private static readonly Dictionary<object, int> _pageStorage = new();

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

        public PropertyList<I> NoTitle()
        {
            _titleShow = false;
            return this;
        }

        public PropertyList<I> Title(string title, params Property[] properties)
        {
            _title = title;
            _titleProperties = properties;
            return this;
        }

        public PropertyList<I> NoCount()
        {
            _countShow = false;
            return this;
        }

        public PropertyList<I> NoPanel()
        {
            _panelShow = false;
            return this;
        }

        public PropertyList<I> OnFilters(Action handler)
        {
            _onFilters = handler;
            return this;
        }

        public PropertyList<I> ItemRenderer<R>() where R : PropertyListItemRenderer<I>
        {
            _itemRendererType = typeof(R);
            return this;
        }

        public PropertyList<I> ItemRenderer<R>(params ButtonType[] buttons) where R : PropertyListItemRenderer<I>
        {
            _itemRendererType = typeof(R);
            _itemRendererButtons = buttons;
            return this;
        }

        public PropertyList<I> ItemButtons(params ButtonType[] buttons)
        {
            _itemRendererButtons = buttons;
            return this;
        }

        public PropertyList<I> OnItemRender(Action<PropertyListItemRenderer<I>> handler)
        {
            _onItemRender = handler;
            return this;
        }

        public PropertyList<I> OnItemTitle(Func<I, string> handler)
        {
            _onItemTitle = handler;
            return this;
        }

        public PropertyList<I> OnItemFilter(Func<I, bool> handler)
        {
            _onItemFilter = handler;
            return this;
        }

        public PropertyList<I> WindowRenderer<W>() where W : PropertyWindow<I>
        {
            _windowRendererType = typeof(W);
            return this;
        }

        public PropertyList<I> OnItemAdd(Func<I> handler)
        {
            _onItemAdd = handler;
            return this;
        }

        public PropertyList<I> OnItemCopy(Func<I, I> handler)
        {
            _onItemCopy = handler;
            return this;
        }

        public PropertyList<I> OnItemAddAvailable(Func<bool> handler)
        {
            _onItemAddAvailable = handler;
            return this;
        }

        public PropertyList<I> OnItemAddValidation(Func<bool> handler)
        {
            _onItemAddValidation = handler;
            return this;
        }

        public PropertyList<I> OnItemIndexChanged(Action<I, int> handler)
        {
            _onItemIndexChanged = handler;
            return this;
        }

        public PropertyList<I> OnItemDelete(Func<I, bool> handler)
        {
            _onItemDelete = handler;
            return this;
        }

        public PropertyList<I> OnItemDetails(Action<I> handler)
        {
            _onItemDetails = handler;
            return this;
        }

        public PropertyList<I> ReadOnly()
        {
            _isReadOnly = true;
            return this;
        }

        public PropertyList<I> Pager(int pageSize, object pageStorageKey)
        {
            _pageSize = pageSize;
            _pageStorageKey = pageStorageKey;
            _pageStorage.TryGetValue(_pageStorageKey, out _page);
            return this;
        }

        public void Build()
        {
            if (_titleShow || _countShow)
            {
                EGUI.Horizontally(() =>
                {
                    if (_titleShow)
                        EGUI.Label(_title ?? Label, _titleProperties);

                    if (_countShow)
                    {
                        EGUI.Space();
                        EGUI.Label("Count:");
                        EGUI.Enabled(false, () => { EGUI.Int(Items?.Count ?? 0, EGUI.Width(50)); });
                    }
                });

                EGUI.Space(3);
            }

            GUIStyle style;

            if (_panelShow)
            {
                style = new GUIStyle(EditorStyles.helpBox)
                {
                    padding = new RectOffset(Padding, Padding, Padding, Padding)
                };
            }
            else
            {
                style = GUIStyle.none;
            }

            switch (LayoutType)
            {
                case LayoutType.Horizontal:
                    EGUI.Horizontally(style, () =>
                    {
                        RenderFilters();
                        RenderList();
                    });
                    break;
                case LayoutType.Vertical:
                    EGUI.Vertically(style, () =>
                    {
                        RenderFilters();
                        RenderList();
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pagerSet = _pageSize > 0;
            var itemAdditionAvailable = !_isReadOnly && (_onItemAddAvailable == null || _onItemAddAvailable.Invoke());
            if (pagerSet || itemAdditionAvailable)
            {
                EGUI.Space(3);
                EGUI.Horizontally(() =>
                {
                    EGUI.Space(30);

                    if (pagerSet)
                    {
                        EGUI.Space();

                        EGUI.Button("←", EGUI.Size(30, 22), EGUI.Padding(new RectOffset(0, 0, 0, 2))).OnClick(Prev);
                        EGUI.Label($"{_page + 1}/{_pagesTotal}", EGUI.Size(45, 22), EGUI.FontStyle(FontStyle.Normal), EGUI.TextAnchor(TextAnchor.MiddleCenter));
                        EGUI.Button("→", EGUI.Size(30, 22), EGUI.Padding(new RectOffset(0, 0, 0, 2))).OnClick(Next);
                    }

                    EGUI.Space();

                    if (itemAdditionAvailable)
                        EGUI.Button("+", EGUI.Size(30, 22), EGUI.Padding(new RectOffset(0, 0, 0, 2))).OnClick(Add);
                });
                EGUI.Space(3);
            }
        }

        /*
         * Private.
         */

        private void RenderFilters()
        {
            if (_onFilters == null)
                return;

            EGUI.Label("Filters", EGUI.FontStyle(FontStyle.Bold));
            EGUI.Space(1);

            _onFilters?.Invoke();

            EGUI.Space(6);
        }

        private void RenderList()
        {
            var items = Items;
            if (items != null)
            {
                var itemsFiltered = new List<I>();

                for (var i = 0; i < items.Count; i++)
                {
                    if (_onItemFilter?.Invoke(items[i]) == false)
                        continue;

                    itemsFiltered.Add(items[i]);
                }

                items = itemsFiltered;

                if (_pageSize > 0)
                {
                    _pagesTotal = Mathf.CeilToInt(items.Count / (float)_pageSize);

                    if (_page >= _pagesTotal)
                    {
                        _page = 0;
                        _pageStorage[_pageStorageKey] = _page;
                    }

                    var index = _page * _pageSize;
                    var count = Math.Min(_pageSize, items.Count - index);
                    items = items.GetRange(index, count);
                }
            }

            if (items == null || items.Count == 0)
            {
                EGUI.Space(25);
                EGUI.Label("No Items", EGUI.StretchedWidth(), EGUI.TextAnchor(TextAnchor.MiddleCenter));
                EGUI.Space(25);
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                PropertyListItemRenderer<I> itemRenderer;

                var item = items[i];

                if (_itemRendererType != null)
                {
                    itemRenderer = (PropertyListItemRenderer<I>)Activator.CreateInstance(_itemRendererType);
                }
                else if (_onItemRender != null)
                {
                    itemRenderer = new RenderingItemRenderer<I>(_onItemRender);
                }
                else
                {
                    var itemRendererDefault = new DefaultItemRenderer<I>
                    {
                        Title = _onItemTitle?.Invoke(item)
                    };

                    itemRenderer = itemRendererDefault;
                }

                itemRenderer.Init(item, i, Items.IndexOf(item), items, Items, _isReadOnly ? new[] { ButtonType.Details } : _itemRendererButtons);
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
                        index = Items.IndexOf(item);

                        EGUI.PropertyWindow(_windowRendererType, item, index, Items);
                    }
                    else
                    {
                        if (_onItemDetails == null)
                            EGUI.LogError("EGUI: PropertyList details handler not specified");
                        else
                            _onItemDetails.Invoke(item);
                    }

                    break;

                case PropertyListItemAction.Copy:

                    I itemCopy;

                    var type = typeof(I);
                    var isClass = type.IsClass;
                    var isStruct = type.IsValueType && !type.IsPrimitive;

                    if (isClass || isStruct)
                    {
                        var itemJson = JsonConvert.SerializeObject(item);
                        itemCopy = JsonConvert.DeserializeObject<I>(itemJson);
                    }
                    else if (typeof(I) == typeof(string))
                    {
                        itemCopy = (I)(object)string.Copy((string)(object)item);
                    }
                    else if (type.IsPrimitive)
                    {
                        itemCopy = item;
                    }
                    else
                    {
                        throw new NotImplementedException($"Type copying not implemented. Type: {type.FullName}");
                    }

                    if (itemCopy != null)
                        Items.Insert(Items.IndexOf(item) + 1, itemCopy);
                    
                    if (_onItemCopy != null)
                        itemCopy = _onItemCopy.Invoke(itemCopy);

                    if (itemCopy != null)
                    {
                        if (_pageSize > 0)
                        {
                            _page = Mathf.CeilToInt(Items.Count / (float)_pageSize) - 1;
                            _pageStorage[_pageStorageKey] = _page;
                        }
                    }

                    break;
            }
        }

        private void Add()
        {
            var addValid = _onItemAddValidation == null || _onItemAddValidation.Invoke();
            if (!addValid)
                return;

            TryCreate();

            I item = default;

            if (_onItemAdd != null)
                item = _onItemAdd.Invoke();

            Items.Add(item);

            if (_windowRendererType != null)
                ProcessAction(PropertyListItemAction.Details, item);

            if (_pageSize > 0)
            {
                _page = Mathf.CeilToInt(Items.Count / (float)_pageSize) - 1;
                _pageStorage[_pageStorageKey] = _page;
            }
        }

        private void TryCreate()
        {
            if (Items != null)
                return;

            Items = new List<I>();
            _onCreate?.Invoke(Items);
        }

        private void Prev()
        {
            if (_page <= 0)
                return;

            _page--;
            _pageStorage[_pageStorageKey] = _page;
        }

        private void Next()
        {
            if (_page >= _pagesTotal - 1)
                return;

            _page++;
            _pageStorage[_pageStorageKey] = _page;
        }
    }
}

#endif