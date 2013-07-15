namespace LBSoft.IndustrialCtrls.Knobs
{
    using System;

    public class LBKnobEventArgs : EventArgs
    {
        private float val;

        public float Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
            }
        }
    }
}

