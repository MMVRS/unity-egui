#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using Build1.UnityEGUI.Types;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.List
{
    public sealed class EGUIList<I, R> where R : EGUIListItemRenderer<I>
    {
        public LayoutType LayoutType { get; set; } = LayoutType.Vertical;
        public string         Label      { get; set; }
        public List<I>        Items      { get; set; }
        public int            CountMax   { get; set; } = 100;
        public int            Padding    { get; set; } = 10;

        public event Action<List<I>>        onCreated;
        public event EGUIListAddDelegate<I> onAdd;
        public event Func<I, bool>          onDelete;
        public event Action<R>              onItemRenderer;

        public EGUIList() { }
        public EGUIList(List<I> items) { Items = items; }

        public void Build()
        {
            EGUI.Horizontally(() =>
            {
                if (!string.IsNullOrWhiteSpace(Label))
                    EGUI.Label(Label);

                EGUI.Space();
                EGUI.Label("Count:");

                EGUI.Enabled(onAdd != null, () => { EGUI.Int(Items?.Count ?? 0, 50, CountChanged); });
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

            if (onAdd != null)
            {
                EGUI.Space(3);
                EGUI.Horizontally(() =>
                {
                    EGUI.Space();
                    EGUI.Button("+", 30, 20, TextAnchor.UpperCenter, Add);
                });
            }
        }

        /*
         * Private.
         */

        private void BuildImpl()
        {
            if (Items == null || !Items.Any())
            {
                EGUI.Space(10);
                EGUI.Label("Empty", true, TextAnchor.MiddleCenter);
                EGUI.Space(10);
                return;
            }

            for (var i = 0; i < Items.Count; i++)
            {
                var itemRenderer = (R)Activator.CreateInstance(typeof(R));
                itemRenderer.Init(i, Items[i], Items);
                itemRenderer.OnEGUI();

                OnItemRendererDefault(itemRenderer);
                onItemRenderer?.Invoke(itemRenderer);
            }
        }

        private void OnItemRendererDefault(EGUIListItemRenderer<I> renderer)
        {
            I item;
            int index;

            switch (renderer.Action)
            {
                case EGUIListItemRendererAction.Up:
                    item = renderer.Item;
                    index = Math.Max(0, Items.IndexOf(item) - 1);
                    Items.Remove(item);
                    Items.Insert(index, item);
                    break;

                case EGUIListItemRendererAction.Down:
                    item = renderer.Item;
                    index = Items.IndexOf(item);
                    if (index < Items.Count - 1)
                    {
                        index = Math.Min(Items.Count, Items.IndexOf(item) + 1);
                        Items.Remove(item);
                        Items.Insert(index, item);
                    }

                    break;

                case EGUIListItemRendererAction.Delete:
                    if (onDelete == null)
                    {
                        EGUI.LogError("EGUIList: Delete handler not specified.");
                        break;
                    }

                    if (onDelete.Invoke(renderer.Item))
                        Items.Remove(renderer.Item);
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
            else if (count > Items.Count)
            {
                if (count > Items.Capacity)
                    Items.Capacity = count;

                var countToAdd = count - Items.Count;
                for (var i = 0; i < countToAdd; i++)
                {
                    if (onAdd != null && onAdd.Invoke(out var item))
                        Items.Add(item);
                }
            }
        }

        private void Add()
        {
            TryCreate();
            if (onAdd != null && onAdd.Invoke(out var item))
                Items.Add(item);
        }

        private void TryCreate()
        {
            if (Items != null)
                return;
            Items = new List<I>();
            onCreated?.Invoke(Items);
        }
    }
}

#endif