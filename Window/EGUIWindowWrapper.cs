using System;
using UnityEditor;
using UnityEngine;

namespace Build1.UnityEGUI.Window
{
    public abstract class EGUIWindowWrapperBase
    {
        protected readonly string           _title;
        protected readonly bool             _utility;
        protected          int              _width;
        protected          int              _height;
        protected          EGUIWindowAnchor _anchor = EGUIWindowAnchor.Center;

        protected EGUIWindowWrapperBase(string title, bool utility)
        {
            _title = title;
            _utility = utility;
        }

        protected Rect GetWindowRect(int width, int height, EGUIWindowAnchor anchor)
        {
            switch (anchor)
            {
                case EGUIWindowAnchor.Center:
                    var main = EditorGUIUtility.GetMainWindowPosition();
                    var centerWidth = (main.width - width) * 0.5f;
                    var centerHeight = (main.height - height) * 0.5f;
                    return new Rect(main.x + centerWidth, main.y + centerHeight, width, height);

                default:
                    throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
            }
        }
    }

    public sealed class EGUIWindowWrapper<W> : EGUIWindowWrapperBase where W : EGUIWindow
    {
        public EGUIWindowWrapper(string title, bool utility) : base(title, utility)
        {
        }

        public EGUIWindowWrapper<W> Anchor(EGUIWindowAnchor anchor)
        {
            _anchor = anchor;
            return this;
        }

        public EGUIWindowWrapper<W> Size(int width, int height)
        {
            _width = width;
            _height = height;
            return this;
        }

        public EGUIWindowWrapper<W> Size(Vector2Int size)
        {
            _width = size.x;
            _height = size.y;
            return this;
        }

        public W Get(bool focus = true)
        {
            var window = EditorWindow.GetWindow<W>(_utility, _title, focus);
            window.position = GetWindowRect(_width, _height, _anchor);
            return window;
        }

        public W Create()
        {
            var window = EditorWindow.CreateWindow<W>(_title);
            window.position = GetWindowRect(_width, _height, _anchor);
            return window;
        }
    }

    public sealed class EGUIWindowWrapper<W, D> : EGUIWindowWrapperBase where W : EGUIWindow<D>
    {
        private readonly D _data;

        public EGUIWindowWrapper(string title, bool utility, D data) : base(title, utility)
        {
            _data = data;
        }

        public EGUIWindowWrapper<W, D> Anchor(EGUIWindowAnchor anchor)
        {
            _anchor = anchor;
            return this;
        }

        public EGUIWindowWrapper<W, D> Size(int width, int height)
        {
            _width = width;
            _height = height;
            return this;
        }

        public EGUIWindowWrapper<W, D> Size(Vector2Int size)
        {
            _width = size.x;
            _height = size.y;
            return this;
        }

        public W Get(bool focus = true)
        {
            var window = EditorWindow.GetWindow<W>(_utility, _title, focus);
            window.position = GetWindowRect(_width, _height, _anchor);
            window.SetData(_data);
            return window;
        }

        public W Create()
        {
            var window = EditorWindow.CreateWindow<W>(_title);
            window.position = GetWindowRect(_width, _height, _anchor);
            window.SetData(_data);
            return window;
        }
    }
}