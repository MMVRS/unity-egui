#if UNITY_EDITOR

using System;
using Build1.UnityEGUI.Results;
using UnityEditor;

namespace Build1.UnityEGUI
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
        
        public static void Alert(string title, string text, Action<AlertResult> onResult)
        {
            Alert(title, text, AlertConfirmTextDefault, AlertCancelTextDefault, AlertDiscardTextDefault, onResult);
        }

        public static void Alert(string title, string text, string confirmText, string cancelText, string discardText, Action<AlertResult> onResult)
        {
            onResult.Invoke(Alert(title, text, confirmText, cancelText, discardText));
        }
        
        public static AlertResult Alert(string title, string text, string confirmText, string cancelText, string discardText)
        {
            var res = EditorUtility.DisplayDialogComplex(title, text, confirmText, cancelText, discardText);
            return res switch
            {
                0 => AlertResult.Confirm,
                1 => AlertResult.Cancel,
                2 => AlertResult.Discard,
                _ => throw new ArgumentOutOfRangeException(nameof(res), res, null)
            };
        }

        public static T Alert<T>(string title, string text, Func<AlertResult, T> onResult)
        {
            return Alert(title, text, AlertConfirmTextDefault, AlertCancelTextDefault, AlertDiscardTextDefault, onResult);
        }
        
        public static T Alert<T>(string title, string text, string confirmText, string cancelText, string discardText, Func<AlertResult, T> onResult)
        {
            var res = EditorUtility.DisplayDialogComplex(title, text, confirmText, cancelText, discardText);
            return res switch
            {
                0 => onResult.Invoke(AlertResult.Confirm),
                1 => onResult.Invoke(AlertResult.Cancel),
                2 => onResult.Invoke(AlertResult.Discard),
                _ => throw new ArgumentOutOfRangeException(nameof(res), res, null)
            };
        }
    }
}

#endif