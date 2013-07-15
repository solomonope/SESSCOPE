namespace LBSoft.IndustrialCtrls.Meters
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LBAnalogMeter : LBIndustrialCtrlBase
    {
        private Color bodyColor;
        private IContainer components;
        private double currValue;
        protected float endAngle;
        private LBMeterThresholdCollection listThreshold;
        private double maxValue;
        private AnalogMeterStyle meterStyle;
        private double minValue;
        private Color needleColor;
        private Color scaleColor;
        private int scaleDivisions;
        private int scaleSubDivisions;
        protected float startAngle;
        private bool viewGlass;

        public LBAnalogMeter()
        {
            this.InitializeComponent();
            this.bodyColor = Color.Red;
            this.needleColor = Color.Yellow;
            this.scaleColor = Color.White;
            this.meterStyle = AnalogMeterStyle.Circular;
            this.viewGlass = false;
            this.startAngle = 135f;
            this.endAngle = 405f;
            this.minValue = 0.0;
            this.maxValue = 1.0;
            this.currValue = 0.0;
            this.scaleDivisions = 10;
            this.scaleSubDivisions = 10;
            this.listThreshold = new LBMeterThresholdCollection();
            this.CalculateDimensions();
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBAnalogMeterRenderer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public float GetEndAngle()
        {
            return this.endAngle;
        }

        public float GetStartAngle()
        {
            return this.startAngle;
        }

        private void InitializeComponent()
        {
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "LBAnalogMeter";
        }

        [Description("Color of the body of the control"), Category("Analog Meter")]
        public Color BodyColor
        {
            get
            {
                return this.bodyColor;
            }
            set
            {
                this.bodyColor = value;
                base.Invalidate();
            }
        }

        [Category("Analog Meter"), Description("Maximum value of the data")]
        public double MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
                base.Invalidate();
            }
        }

        [Category("Analog Meter"), Description("Style of the control")]
        public AnalogMeterStyle MeterStyle
        {
            get
            {
                return this.meterStyle;
            }
            set
            {
                this.meterStyle = value;
                base.Invalidate();
            }
        }

        [Description("Minimum value of the data"), Category("Analog Meter")]
        public double MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
                base.Invalidate();
            }
        }

        [Category("Analog Meter"), Description("Color of the needle")]
        public Color NeedleColor
        {
            get
            {
                return this.needleColor;
            }
            set
            {
                this.needleColor = value;
                base.Invalidate();
            }
        }

        [Description("Color of the scale of the control"), Category("Analog Meter")]
        public Color ScaleColor
        {
            get
            {
                return this.scaleColor;
            }
            set
            {
                this.scaleColor = value;
                base.Invalidate();
            }
        }

        [Description("Number of the scale divisions"), Category("Analog Meter")]
        public int ScaleDivisions
        {
            get
            {
                return this.scaleDivisions;
            }
            set
            {
                this.scaleDivisions = value;
                this.CalculateDimensions();
            }
        }

        [Description("Number of the scale subdivisions"), Category("Analog Meter")]
        public int ScaleSubDivisions
        {
            get
            {
                return this.scaleSubDivisions;
            }
            set
            {
                this.scaleSubDivisions = value;
                this.CalculateDimensions();
            }
        }

        [Browsable(false)]
        public LBMeterThresholdCollection Thresholds
        {
            get
            {
                return this.listThreshold;
            }
        }

        [Description("Value of the data"), Category("Analog Meter")]
        public double Value
        {
            get
            {
                return this.currValue;
            }
            set
            {
                double maxValue = value;
                if (maxValue > this.maxValue)
                {
                    maxValue = this.maxValue;
                }
                if (maxValue < this.minValue)
                {
                    maxValue = this.minValue;
                }
                this.currValue = maxValue;
                base.Invalidate();
            }
        }

        [Category("Analog Meter"), Description("Show or hide the glass effect")]
        public bool ViewGlass
        {
            get
            {
                return this.viewGlass;
            }
            set
            {
                this.viewGlass = value;
                base.Invalidate();
            }
        }

        public enum AnalogMeterStyle
        {
            Circular
        }
    }
}

