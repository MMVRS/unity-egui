#if UNITY_EDITOR

using System;

namespace Build1.UnityEGUI.Other
{
    public sealed class FoldInfo
    {
        public bool fold01;
        public bool fold02;
        public bool fold03;
        public bool fold04;
        public bool fold05;
        public bool fold06;
        public bool fold07;
        public bool fold08;
        
        public bool this[int i]
        {
            get
            {
                return i switch
                {
                    0 => fold01,
                    1 => fold02,
                    2 => fold03,
                    3 => fold04,
                    4 => fold05,
                    5 => fold06,
                    6 => fold07,
                    7 => fold08,
                    _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
                };
            }
            set
            {
                switch (i)
                {
                    case 0:
                        fold01 = value;
                        break;
                    case 1:
                        fold02 = value;
                        break;
                    case 2:
                        fold03 = value;
                        break;
                    case 3:
                        fold04 = value;
                        break;
                    case 4:
                        fold05 = value;
                        break;
                    case 5:
                        fold06 = value;
                        break;
                    case 6:
                        fold07 = value;
                        break;
                    case 7:
                        fold08 = value;
                        break;
                }
            }
        }

        public FoldInfo(bool fold01 = false,
                        bool fold02 = false,
                        bool fold03 = false,
                        bool fold04 = false,
                        bool fold05 = false,
                        bool fold06 = false,
                        bool fold07 = false,
                        bool fold08 = false)
        {
            this.fold01 = fold01;
            this.fold02 = fold02;
            this.fold03 = fold03;
            this.fold04 = fold04;
            this.fold05 = fold05;
            this.fold06 = fold06;
            this.fold07 = fold07;
            this.fold08 = fold08;
        }
    }
}

#endif