namespace LBSoft.IndustrialCtrls.Leds
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class LB7SegmentDisplay : LBIndustrialCtrlBase
    {
        private IContainer components;
        private bool showDp;
        public int val;

        public LB7SegmentDisplay()
        {
            this.InitializeComponent();
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LB7SegmentDisplayRenderer();
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
            base.Name = "LB7SegmentDisplay";
            base.Size = new Size(0x2c, 0x41);
            base.ResumeLayout(false);
        }

        [Description("Show the point of the display"), Category("Display")]
        public bool ShowDP
        {
            get
            {
                return this.showDp;
            }
            set
            {
                this.showDp = value;
                base.Invalidate();
            }
        }

        [Category("Display"), Description("Value of the display")]
        public int Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value;
                base.Invalidate();
            }
        }
    }
}

