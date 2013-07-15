namespace LBSoft.IndustrialCtrls.Buttons
{
    using System;

    public class LBButtonEventArgs : EventArgs
    {
        private LBButton.ButtonState state;

        public LBButton.ButtonState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
    }
}

