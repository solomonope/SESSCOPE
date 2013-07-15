namespace LBSoft.IndustrialCtrls.Meters
{
    using System;
    using System.Drawing;

    public class LBMeterThreshold
    {
        private System.Drawing.Color color = System.Drawing.Color.Empty;
        private double endValue = 1.0;
        private double startValue;

        public bool IsInRange(double val)
        {
            if (val > this.EndValue)
            {
                return false;
            }
            if (val < this.StartValue)
            {
                return false;
            }
            return true;
        }

        public System.Drawing.Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public double EndValue
        {
            get
            {
                return this.endValue;
            }
            set
            {
                this.endValue = value;
            }
        }

        public double StartValue
        {
            get
            {
                return this.startValue;
            }
            set
            {
                this.startValue = value;
            }
        }
    }
}

