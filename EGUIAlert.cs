#if UNITY_EDITOR

using System;
using UnityEditor;

namespace Editor
{
    public static partial class EGUI
    {
        /*
         * Settings.
         */
        
        public static string AlertConfirmTextDefault { get; set; } = "OK";
        public static string AlertCancelTextDefault  { get; set; } = "Cancel";
        public static string AlertDiscardTextDefault { get; set; } = "Discard";
        
        /*
         * Methods.
         */
        
        public static bool Alert(string title, string text)                                        { return EditorUtility.DisplayDialog(title, text, AlertConfirmTextDefault); }
        public static bool Alert(string title, string text, string confirmText)                    { return EditorUtility.DisplayDialog(title, text, confirmText); }
        public static bool Alert(string title, string text, string confirmText, string cancelText) { return EditorUtility.DisplayDialog(title, text, confirmText, cancelText); }
        
        public static void Alert(string title, string text, Action onConfirm)
        {
            Alert(title, text, AlertConfirmTextDefault, onConfirm);
        }

        public static void Alert(string title, string text, string confirmText, Action onConfirm)
        {
            var res = EditorUtility.DisplayDialog(title, text, confirmText);
            if (res)
                onConfirm?.Invoke();
        }

        public static void Alert(string title, string text, string confirmText, string cancelText, Action onConfirm)
        {
            var res = EditorUtility.DisplayDialog(title, text, confirmText, cancelText);
            if (res)
                onConfirm?.Invoke();
        }
        
        public static void Alert(string title, string text, Action<EGUIAlertResult> onResult)
        {
            Alert(title, text, AlertConfirmTextDefault, AlertCancelTextDefault, AlertDiscardTextDefault, onResult);
        }

        public static void Alert(string title, string text, string confirmText, string cancelText, string discardText, Action<EGUIAlertResult> onResult)
        {
            var res = EditorUtility.DisplayDialogComplex(title, text, confirmText, cancelText, discardText);
            switch (res)
            {
                case 0:
                    onResult.Invoke(EGUIAlertResult.Confirm);
                    break;
                case 1:
                    onResult.Invoke(EGUIAlertResult.Cancel);
                    break;
                case 2:
                    onResult.Invoke(EGUIAlertResult.Discard);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(res), res, null);
            }
        }

        public static T Alert<T>(string title, string text, Func<EGUIAlertResult, T> onResult)
        {
            return Alert(title, text, AlertConfirmTextDefault, AlertCancelTextDefault, AlertDiscardTextDefault, onResult);
        }
        
        public static T Alert<T>(string title, string text, string confirmText, string cancelText, string discardText, Func<EGUIAlertResult, T> onResult)
        {
            var res = EditorUtility.DisplayDialogComplex(title, text, confirmText, cancelText, discardText);
            return res switch
            {
                0 => onResult.Invoke(EGUIAlertResult.Confirm),
                1 => onResult.Invoke(EGUIAlertResult.Cancel),
                2 => onResult.Invoke(EGUIAlertResult.Discard),
                _ => throw new ArgumentOutOfRangeException(nameof(res), res, null)
            };
        }
    }
}

#endif