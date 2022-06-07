#if UNITY_EDITOR

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

        public FoldInfo(bool fold01 = false,
                        bool fold02 = false,
                        bool fold03 = false,
                        bool fold04 = false,
                        bool fold05 = false,
                        bool fold06 = false,
                        bool fold07 = false)
        {
            this.fold01 = fold01;
            this.fold02 = fold02;
            this.fold03 = fold03;
            this.fold04 = fold04;
            this.fold05 = fold05;
            this.fold06 = fold06;
            this.fold07 = fold07;
        }
    }
}

#endif