#if UNITY_EDITOR

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Build1.UnityEGUI.Json
{
    public sealed class JsonTreeView : TreeView
    {
        private static int _id;

        private readonly string _rootName;
        private readonly JToken _token;

        public JsonTreeView(string json, TreeViewState treeViewState) : this(null, json, treeViewState)
        {
        }

        public JsonTreeView(string rootName, string json, TreeViewState treeViewState) : base(treeViewState)
        {
            _rootName = rootName;
            _token = JToken.Parse(json);
            
            showBorder = true;

            Reload();
        }

        public void OnEGUI(float x, float y, float width, float height) { OnEGUI(new Rect(x, y, width, height)); }
        public void OnEGUI(Rect rect)                                   { OnGUI(rect); }

        protected override TreeViewItem BuildRoot()
        {
            var depth = -1;

            var root = new TreeViewItem { id = _id++, depth = depth++ };
            var items = new List<TreeViewItem>();

            if (_rootName != null)
                items.Add(new TreeViewItem { id = _id++, depth = depth++, displayName = _rootName });

            WalkNode(_token, _rootName, depth, items);
            SetupParentsAndChildrenFromDepths(root, items);
            return root;
        }

        private static void WalkNode(JToken node, string name, int depth, List<TreeViewItem> items)
        {
            switch (node.Type)
            {
                case JTokenType.Object:
                {
                    if (name != null)
                        items.Add(new TreeViewItem { id = _id++, depth = depth, displayName = $"{name}:" });

                    foreach (var child in node.Children<JProperty>())
                        WalkNode(child.Value, child.Name, depth + 1, items);
                    break;
                }

                case JTokenType.Array:
                {
                    if (name != null)
                        items.Add(new TreeViewItem { id = _id++, depth = depth, displayName = $"{name}:" });

                    var index = 0;
                    foreach (var child in node.Children())
                    {
                        WalkNode(child, index.ToString(), depth + 1, items);
                        index++;
                    }

                    break;
                }

                case JTokenType.String:
                {
                    var value = node.ToString().Replace("\n", "\\n");
                    items.Add(new TreeViewItem { id = _id++, depth = depth, displayName = $"{name}: \"{value}\"" });
                    break;
                }

                case JTokenType.Boolean:
                {
                    var value = node.ToString().ToLower();
                    items.Add(new TreeViewItem { id = _id++, depth = depth, displayName = $"{name}: {value}" });
                    break;
                }
                    
                case JTokenType.Integer:
                case JTokenType.Float:
                {
                    items.Add(new TreeViewItem { id = _id++, depth = depth, displayName = $"{name}: {node}" });
                    break;
                }
            }
        }
    }
}

#endif