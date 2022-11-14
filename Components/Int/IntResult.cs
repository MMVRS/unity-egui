#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI
{
    public readonly struct IntResult
    {
        private readonly int valueOld;
        private readonly int valueNew;
        
        internal IntResult(int valueOld, int valueNew)
        {
            this.valueOld = valueOld;
            this.valueNew = valueNew;
        }
        
        public int Value()
        {
            return valueNew;
        }
        
        public void Value(out int value)
        {
            value = valueNew;
        }
        
        public void OnChange(Action<int> action)
        {
            if (valueNew != valueOld)
                action(valueNew);
        }
        
        public void OnChange<T>(Action<int, T> action, T param)
        {
            if (valueNew != valueOld)
                action(valueNew, param);
        }
        
        public void OnChange<T1, T2>(Action<int, T1, T2> action, T1 param01, T2 param02)
        {
            if (valueNew != valueOld)
                action(valueNew, param01, param02);
        }
    }
}

#endif