namespace LBSoft.IndustrialCtrls.Leds
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LBLed : LBIndustrialCtrlBase
    {
        private int blinkInterval = 500;
        private bool blinkIsOn;
        private IContainer components;
        private string label = "Led";
        private LedLabelPosition labelPosition;
        private Color ledColor;
        private SizeF ledSize;
        private LedState state;
        private LedStyle style;
        private Timer tmrBlink;

        public LBLed()
        {
            this.InitializeComponent();
            base.Size = new Size(20, 20);
            this.ledColor = Color.Red;
            this.state = LedState.Off;
            this.style = LedStyle.Circular;
            this.blinkIsOn = false;
            this.ledSize = new SizeF(10f, 10f);
            this.labelPosition = LedLabelPosition.Top;
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBLedRenderer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.tmrBlink = new Timer(this.components);
            base.SuspendLayout();
            this.tmrBlink.Interval = 500;
            this.tmrBlink.Tick += new EventHandler(this.OnBlink);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "LBLed";
            base.ResumeLayout(false);
        }

        private void OnBlink(object sender, EventArgs e)
        {
            if (this.State == LedState.Blink)
            {
                if (!this.blinkIsOn)
                {
                    this.blinkIsOn = true;
                }
                else
                {
                    this.blinkIsOn = false;
                }
                base.Invalidate();
            }
        }

        [Description("Interval for the blink state of the led"), Category("Led")]
        public int BlinkInterval
        {
            get
            {
                return this.blinkInterval;
            }
            set
            {
                this.blinkInterval = value;
            }
        }

        [Browsable(false)]
        public bool BlinkIsOn
        {
            get
            {
                return this.blinkIsOn;
            }
        }

        [Category("Led"), Description("Label of the led")]
        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
                base.Invalidate();
            }
        }

        [Description("Position of the label of the led"), Category("Led")]
        public LedLabelPosition LabelPosition
        {
            get
            {
                return this.labelPosition;
            }
            set
            {
                this.labelPosition = value;
                this.CalculateDimensions();
                base.Invalidate();
            }
        }

        [Description("Color of the led"), Category("Led")]
        public Color LedColor
        {
            get
            {
                return this.ledColor;
            }
            set
            {
                this.ledColor = value;
                base.Invalidate();
            }
        }

        [Category("Led"), Description("Size of the led")]
        public SizeF LedSize
        {
            get
            {
                return this.ledSize;
            }
            set
            {
                this.ledSize = value;
                this.CalculateDimensions();
                base.Invalidate();
            }
        }

        [Description("State of the led"), Category("Led")]
        public LedState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
                if (this.state == LedState.Blink)
                {
                    this.blinkIsOn = true;
                    this.tmrBlink.Interval = this.BlinkInterval;
                    this.tmrBlink.Start();
                }
                else
                {
                    this.blinkIsOn = true;
                    this.tmrBlink.Stop();
                }
                base.Invalidate();
            }
        }

        [Category("Led"), Description("Style of the led")]
        public LedStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
                this.CalculateDimensions();
            }
        }

        public enum LedLabelPosition
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public enum LedState
        {
            Off,
            On,
            Blink
        }

        public enum LedStyle
        {
            Circular,
            Rectangular
        }
    }
}

