#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Build1.UnityEGUI.PropertyList;
using Build1.UnityEGUI.PropertyList.ItemRenderers;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static PropertyList<int> PropertyList(object instance, IList<int> items, string propertyName)
        {
            return PropertyList<int>(instance, items, propertyName).ItemRenderer<IntItemRenderer>();
        }
        
        public static PropertyList<string> PropertyList(object instance, IList<string> items, string propertyName)
        {
            return PropertyList<string>(instance, items, propertyName).ItemRenderer<StringItemRenderer>();
        }
        
        public static PropertyList<I> PropertyList<I>(object instance, IList<I> items, string propertyName)
        {
            var type = instance.GetType();
            var property = type.GetProperty(propertyName);
            if (property == null)
                throw new Exception($"Property [{propertyName}] not found for [{type.FullName}].");

            var itemsGot = (List<I>)property.GetValue(instance);
            if (!ReferenceEquals(itemsGot, items))
                throw new Exception("Items collections not the same.");

            var list = new PropertyList<I>(FormatCameCase(propertyName), itemsGot);
            list.OnCreate(i => { property.SetValue(instance, i); });

            return list;
        }
    }
}

#endif