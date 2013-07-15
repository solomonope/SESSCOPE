namespace LBSoft.IndustrialCtrls.Knobs
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class LBKnob : LBIndustrialCtrlBase
    {
        private IContainer components;
        private float currValue;
        private float drawRatio = 1f;
        private Color indicatorColor = Color.Red;
        private float indicatorOffset = 10f;
        private bool isKnobRotating;
        private PointF knobCenter = PointF.Empty;
        private Color knobColor = Color.Black;
        private RectangleF knobRect = RectangleF.Empty;
        private float maxValue = 1f;
        private float minValue;
        private Color scaleColor = Color.Green;
        private float stepValue = 0.1f;
        private KnobStyle style;

        public event LBSoft.IndustrialCtrls.Knobs.KnobChangeValue KnobChangeValue;

        public LBKnob()
        {
            this.InitializeComponent();
            this.CalculateDimensions();
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBKnobRenderer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public virtual PointF GetPositionFromValue(float val)
        {
            PointF tf = new PointF(0f, 0f);
            if ((this.MaxValue - this.MinValue) != 0f)
            {
                float indicatorOffset = this.IndicatorOffset;
                float num = (270f * val) / (this.MaxValue - this.MinValue);
                num = ((num + 135f) * 3.141593f) / 180f;
                tf.X = (int) (((Math.Cos((double) num) * ((this.knobRect.Width * 0.5f) - this.indicatorOffset)) + this.knobRect.X) + (this.knobRect.Width * 0.5f));
                tf.Y = (int) (((Math.Sin((double) num) * ((this.knobRect.Width * 0.5f) - this.indicatorOffset)) + this.knobRect.Y) + (this.knobRect.Height * 0.5f));
            }
            return tf;
        }

        public virtual float GetValueFromPosition(PointF position)
        {
            float num = 0f;
            float maxValue = 0f;
            PointF knobCenter = this.KnobCenter;
            if (position.X <= knobCenter.X)
            {
                num = (knobCenter.Y - position.Y) / (knobCenter.X - position.X);
                num = (float) Math.Atan((double) num);
                num = (float) ((num * 57.295779513082323) + 45.0);
                maxValue = (num * (this.MaxValue - this.MinValue)) / 270f;
            }
            else if (position.X > knobCenter.X)
            {
                num = (position.Y - knobCenter.Y) / (position.X - knobCenter.X);
                num = (float) Math.Atan((double) num);
                num = (float) (225.0 + (num * 57.295779513082323));
                maxValue = (num * (this.MaxValue - this.MinValue)) / 270f;
            }
            if (maxValue > this.MaxValue)
            {
                maxValue = this.MaxValue;
            }
            if (maxValue < this.MinValue)
            {
                maxValue = this.MinValue;
            }
            return maxValue;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "LBKnob";
            base.MouseDown += new MouseEventHandler(this.OnMouseDown);
            base.MouseMove += new MouseEventHandler(this.OnMouseMove);
            base.MouseUp += new MouseEventHandler(this.OnMouseUp);
            base.KeyDown += new KeyEventHandler(this.OnKeyDown);
            base.ResumeLayout(false);
        }

        [EditorBrowsable]
        protected override void OnClick(EventArgs e)
        {
            base.Focus();
            base.Invalidate();
            base.OnClick(e);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            float minValue = this.Value;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    minValue = this.Value + this.StepValue;
                    break;

                case Keys.Down:
                    minValue = this.Value - this.StepValue;
                    break;
            }
            if (minValue < this.MinValue)
            {
                minValue = this.MinValue;
            }
            if (minValue > this.MaxValue)
            {
                minValue = this.MaxValue;
            }
            this.Value = minValue;
        }

        protected virtual void OnKnobChangeValue(LBKnobEventArgs e)
        {
            if (this.KnobChangeValue != null)
            {
                this.KnobChangeValue(this, e);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (this.knobRect.Contains((PointF) e.Location))
            {
                this.isKnobRotating = true;
                base.Focus();
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.isKnobRotating)
            {
                float valueFromPosition = this.GetValueFromPosition((PointF) e.Location);
                if (valueFromPosition != this.Value)
                {
                    this.Value = valueFromPosition;
                    base.Invalidate();
                }
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.isKnobRotating = false;
            if (this.knobRect.Contains((PointF) e.Location))
            {
                float valueFromPosition = this.GetValueFromPosition((PointF) e.Location);
                if (valueFromPosition != this.Value)
                {
                    this.Value = valueFromPosition;
                    base.Invalidate();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool flag = true;
            float num = this.Value;
            if ((msg.Msg != 0x100) && (msg.Msg != 260))
            {
                return flag;
            }
            switch (keyData)
            {
                case Keys.PageUp:
                    if (num < this.MaxValue)
                    {
                        num += this.StepValue * 10f;
                        this.Value = num;
                    }
                    return flag;

                case Keys.Next:
                    if (num > this.MinValue)
                    {
                        num -= this.StepValue * 10f;
                        this.Value = num;
                    }
                    return flag;

                case Keys.End:
                    this.Value = this.MaxValue;
                    return flag;

                case Keys.Home:
                    this.Value = this.MinValue;
                    return flag;

                case Keys.Up:
                    num += this.StepValue;
                    if (num <= this.MaxValue)
                    {
                        this.Value = num;
                    }
                    return flag;

                case Keys.Down:
                    num -= this.StepValue;
                    if (num >= this.MinValue)
                    {
                        this.Value = num;
                    }
                    return flag;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        [Browsable(false)]
        public float DrawRatio
        {
            get
            {
                return this.drawRatio;
            }
            set
            {
                this.drawRatio = value;
            }
        }

        [Category("Knob"), Description("Color of the indicator")]
        public Color IndicatorColor
        {
            get
            {
                return this.indicatorColor;
            }
            set
            {
                this.indicatorColor = value;
                base.Invalidate();
            }
        }

        [Description("Offset of the indicator from the kob border"), Category("Knob")]
        public float IndicatorOffset
        {
            get
            {
                return this.indicatorOffset;
            }
            set
            {
                this.indicatorOffset = value;
                this.CalculateDimensions();
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public PointF KnobCenter
        {
            get
            {
                return this.knobCenter;
            }
            set
            {
                this.knobCenter = value;
            }
        }

        [Category("Knob"), Description("Color of the knob")]
        public Color KnobColor
        {
            get
            {
                return this.knobColor;
            }
            set
            {
                this.knobColor = value;
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public RectangleF KnobRect
        {
            get
            {
                return this.knobRect;
            }
            set
            {
                this.knobRect = value;
            }
        }

        [Description("Maximum value of the knob"), Category("Knob")]
        public float MaxValue
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

        [Category("Knob"), Description("Minimum value of the knob")]
        public float MinValue
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

        [Category("Knob"), Description("Color of the scale")]
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

        [Category("Knob"), Description("Step value of the knob")]
        public float StepValue
        {
            get
            {
                return this.stepValue;
            }
            set
            {
                this.stepValue = value;
                base.Invalidate();
            }
        }

        [Description("Style of the knob"), Category("Knob")]
        public KnobStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
                base.Invalidate();
            }
        }

        [Description("Current value of the knob"), Category("Knob")]
        public float Value
        {
            get
            {
                return this.currValue;
            }
            set
            {
                if (value != this.currValue)
                {
                    this.currValue = value;
                    this.CalculateDimensions();
                    base.Invalidate();
                    LBKnobEventArgs e = new LBKnobEventArgs {
                        Value = this.currValue
                    };
                    this.OnKnobChangeValue(e);
                }
            }
        }

        public enum KnobStyle
        {
            Circular
        }
    }
}

