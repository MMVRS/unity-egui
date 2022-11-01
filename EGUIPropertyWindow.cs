#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.PropertyWindow;
using UnityEngine;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static Vector2Int PropertyWindowSizeDefault { get; set; } = new(600, 400);

        private static readonly List<PropertyWindowImpl> _windowsInstances = new();

        public static void PropertyWindow<W, I>(I item) where W : PropertyWindow<I>
        {
            var window = Activator.CreateInstance<W>();
            window.Initialize(0, item, null);

            var windowInstance = Window<PropertyWindowImpl>(window.Title, false)
                                .Size(window.Size)
                                .Create()
                                .Initialize(window)
                                .Show();

            _windowsInstances.Add(windowInstance);
        }

        public static void PropertyWindow<W, I>(I item, string title) where W : PropertyWindow<I>
        {
            var window = Activator.CreateInstance<W>();
            window.Initialize(0, item, null);

            var windowInstance = Window<PropertyWindowImpl>(title, false)
                                .Size(window.Size)
                                .Create()
                                .Initialize(window)
                                .Show();

            _windowsInstances.Add(windowInstance);
        }

        public static void PropertyWindow<I>(Type windowType, I item, int index, List<I> items)
        {
            var window = (PropertyWindow<I>)Activator.CreateInstance(windowType);
            window.Initialize(index, item, items);

            var windowInstance = Window<PropertyWindowImpl>(window.Title, false)
                                .Size(window.Size)
                                .Create()
                                .Initialize(window)
                                .Show();

            _windowsInstances.Add(windowInstance);
        }

        public static void PropertyWindowCloseAll()
        {
            foreach (var window in _windowsInstances)
                window.Close();

            _windowsInstances.Clear();
        }
    }
}

#endif