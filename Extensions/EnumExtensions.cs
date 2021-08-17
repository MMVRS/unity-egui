using System;
using System.Reflection;

namespace Build1.UnityEGUI.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsFlags(this Enum value)
        {
            return value.GetType().GetCustomAttribute(typeof(FlagsAttribute)) != null;
        }
    }
}