#if UNITY_EDITOR

namespace Build1.UnityEGUI.PropertyList
{
    public enum ButtonType
    {
        None    = 0,
        Up      = 1 << 0,
        Down    = 1 << 1,
        Delete  = 1 << 2,
        Details = 1 << 3,
        Copy    = 1 << 4,
    }
}

#endif