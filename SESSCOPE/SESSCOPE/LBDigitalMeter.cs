namespace LBSoft.IndustrialCtrls.Meters
{
    using LBSoft.IndustrialCtrls.Base;
    using LBSoft.IndustrialCtrls.Leds;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LBDigitalMeter : LBIndustrialCtrlBase
    {
        protected int _dpPos;
        private string _format = string.Empty;
        protected int _numDigits;
        private bool _signed;
        private IContainer components;
        private double val;

        public LBDigitalMeter()
        {
            this.InitializeComponent();
            this.BackColor = Color.Black;
            this.Format = "000";
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBDigitalMeterRenderer();
        }

        private void DisplayClicked(object sender, EventArgs e)
        {
            base.InvokeOnClick(this, e);
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "LBDigitalMeter";
            base.Size = new Size(0x181, 150);
            base.ResumeLayout(false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.RepositionControls();
        }

        protected void RepositionControls()
        {
            Rectangle clientRectangle = base.ClientRectangle;
            if (base.Controls.Count > 0)
            {
                int num = clientRectangle.Width / base.Controls.Count;
                bool flag = false;
                foreach (Control control in base.Controls)
                {
                    if (control.GetType() == typeof(LB7SegmentDisplay))
                    {
                        LB7SegmentDisplay display = control as LB7SegmentDisplay;
                        int num2 = 0;
                        if (display.Name.Contains("digit_sign"))
                        {
                            flag = true;
                        }
                        else if (display.Name.Contains("digit_"))
                        {
                            num2 = Convert.ToInt32(display.Name.Remove(0, 6));
                            if (flag)
                            {
                                num2++;
                            }
                        }
                        Point point = new Point {
                            X = num2 * num,
                            Y = 0
                        };
                        display.Location = point;
                        Size size = new Size {
                            Width = num,
                            Height = clientRectangle.Height
                        };
                        display.Size = size;
                    }
                }
            }
        }

        protected virtual void UpdateControls()
        {
            int length = this.Format.Length;
            this._dpPos = -1;
            char[] anyOf = new char[] { '.', ',' };
            int num2 = this.Format.IndexOfAny(anyOf);
            if (num2 > 0)
            {
                length--;
                this._dpPos = num2 - 1;
                this._numDigits = length;
            }
            this._numDigits = length;
            base.Controls.Clear();
            if (this.Signed)
            {
                LB7SegmentDisplay display = new LB7SegmentDisplay {
                    Name = "digit_sign",
                    Value = -1
                };
                base.Controls.Add(display);
            }
            for (int i = 0; i < length; i++)
            {
                LB7SegmentDisplay display2 = new LB7SegmentDisplay {
                    Name = "digit_" + i.ToString()
                };
                display2.Click += new EventHandler(this.DisplayClicked);
                if ((num2 - 1) == i)
                {
                    display2.ShowDP = true;
                }
                base.Controls.Add(display2);
            }
            this.RepositionControls();
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                foreach (Control control in base.Controls)
                {
                    if (control.GetType() == typeof(LB7SegmentDisplay))
                    {
                        LB7SegmentDisplay display = control as LB7SegmentDisplay;
                        display.BackColor = value;
                    }
                }
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreach (Control control in base.Controls)
                {
                    if (control.GetType() == typeof(LB7SegmentDisplay))
                    {
                        LB7SegmentDisplay display = control as LB7SegmentDisplay;
                        display.ForeColor = value;
                    }
                }
            }
        }

        [Category("Digital meter"), Description("Format of the display value")]
        public string Format
        {
            get
            {
                return this._format;
            }
            set
            {
                if (this._format != value)
                {
                    this._format = value;
                    this.UpdateControls();
                    this.Value = this.Value;
                }
            }
        }

        [Category("Digital meter"), Description("Set the signed value of the meter")]
        public bool Signed
        {
            get
            {
                return this._signed;
            }
            set
            {
                if (this._signed != value)
                {
                    this._signed = value;
                    this.UpdateControls();
                }
            }
        }

        [Category("Digital meter"), Description("Value to display")]
        public double Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
                string str = this.val.ToString(this.Format).Replace(".", string.Empty).Replace(",", string.Empty);
                bool flag = false;
                if (str[0] == '-')
                {
                    flag = true;
                    str = str.TrimStart(new char[] { '-' });
                }
                if (str.Length > this._numDigits)
                {
                    foreach (LB7SegmentDisplay display in base.Controls)
                    {
                        display.Value = 0x45;
                    }
                }
                else
                {
                    int num = 0;
                    for (num = str.Length - 1; num >= 0; num--)
                    {
                        int num2 = num;
                        if (this.Signed)
                        {
                            num2++;
                        }
                        LB7SegmentDisplay display2 = base.Controls[num2] as LB7SegmentDisplay;
                        display2.Value = Convert.ToInt32(str[num].ToString());
                    }
                    LB7SegmentDisplay display3 = base.Controls["digit_sign"] as LB7SegmentDisplay;
                    if (display3 != null)
                    {
                        if (flag)
                        {
                            display3.Value = 0x2d;
                        }
                        else
                        {
                            display3.Value = -1;
                        }
                    }
                }
            }
        }
    }
}

