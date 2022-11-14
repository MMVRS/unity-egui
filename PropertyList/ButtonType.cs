#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.PropertyList
{
    [Flags]
    public enum ButtonType
    {
        Up      = 1 << 0,
        Down    = 1 << 1,
        Delete  = 1 << 2,
        Details = 1 << 3,

        None = 0,
        All  = Up | Down | Delete | Details
    }
}

#endif