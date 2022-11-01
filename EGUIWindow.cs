#if UNITY_EDITOR

using Build1.UnityEGUI.Window;

namespace Build1.UnityEGUI
{
    public static partial class EGUI
    {
        public static int WindowPaddingDefault { get; set; } = 10;
        
        public static EGUIWindowWrapper<W> Window<W>(string title, bool utility) where W : EGUIWindow
        {
            return new EGUIWindowWrapper<W>(title, utility);
        }
        
        public static EGUIWindowWrapper<W, D> Window<W, D>(string title, bool utility, D data) where W : EGUIWindow<D>
        {
            return new EGUIWindowWrapper<W, D>(title, utility, data);
        }
    }
}

#endif