namespace LBSoft.IndustrialCtrls.Buttons
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class LBButton : LBIndustrialCtrlBase
    {
        private Color buttonColor = Color.Red;
        private ButtonState buttonState;
        private ButtonStyle buttonStyle;
        private IContainer components;
        private bool enableRepeatState;
        private string label = string.Empty;
        private int repeatInterval = 100;
        private int startRepeatInterval = 500;
        private Timer tmrRepeat;

        public event LBSoft.IndustrialCtrls.Buttons.ButtonChangeState ButtonChangeState;

        public event LBSoft.IndustrialCtrls.Buttons.ButtonRepeatState ButtonRepeatState;

        public LBButton()
        {
            this.InitializeComponent();
            this.buttonColor = Color.Red;
            base.Size = new Size(50, 50);
            this.tmrRepeat = new Timer();
            this.tmrRepeat.Enabled = false;
            this.tmrRepeat.Interval = this.startRepeatInterval;
            this.tmrRepeat.Tick += new EventHandler(this.Timer_Tick);
        }

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBButtonRenderer();
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
            base.Name = "LBButton";
            base.MouseDown += new MouseEventHandler(this.OnMouseDown);
            base.MouseUp += new MouseEventHandler(this.OnMuoseUp);
            base.ResumeLayout(false);
        }

        protected virtual void OnButtonChangeState(LBButtonEventArgs e)
        {
            if (this.ButtonChangeState != null)
            {
                this.ButtonChangeState(this, e);
            }
        }

        protected virtual void OnButtonRepeatState(LBButtonEventArgs e)
        {
            if (this.ButtonRepeatState != null)
            {
                this.ButtonRepeatState(this, e);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.State = ButtonState.Pressed;
            base.Invalidate();
            LBButtonEventArgs args = new LBButtonEventArgs {
                State = this.State
            };
            this.OnButtonChangeState(args);
            if (this.RepeatState)
            {
                this.tmrRepeat.Interval = this.StartRepeatInterval;
                this.tmrRepeat.Enabled = true;
            }
        }

        private void OnMuoseUp(object sender, MouseEventArgs e)
        {
            this.State = ButtonState.Normal;
            base.Invalidate();
            LBButtonEventArgs args = new LBButtonEventArgs {
                State = this.State
            };
            this.OnButtonChangeState(args);
            this.tmrRepeat.Enabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.tmrRepeat.Enabled = false;
            if (this.tmrRepeat.Interval == this.startRepeatInterval)
            {
                this.tmrRepeat.Interval = this.repeatInterval;
            }
            LBButtonEventArgs args = new LBButtonEventArgs {
                State = this.State
            };
            this.OnButtonRepeatState(args);
            this.tmrRepeat.Enabled = true;
        }

        [Description("Color of the body of the button"), Category("Button")]
        public Color ButtonColor
        {
            get
            {
                return this.buttonColor;
            }
            set
            {
                this.buttonColor = value;
                base.Invalidate();
            }
        }

        [Description("Label of the button"), Category("Button")]
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

        [Description("Interva in ms for the repetition"), Category("Button")]
        public int RepeatInterval
        {
            get
            {
                return this.repeatInterval;
            }
            set
            {
                this.repeatInterval = value;
            }
        }

        [Category("Button"), Description("Enable/Disable the repetition of the event if the button is pressed")]
        public bool RepeatState
        {
            get
            {
                return this.enableRepeatState;
            }
            set
            {
                this.enableRepeatState = value;
            }
        }

        [Description("Interval to wait in ms for start the repetition"), Category("Button")]
        public int StartRepeatInterval
        {
            get
            {
                return this.startRepeatInterval;
            }
            set
            {
                this.startRepeatInterval = value;
            }
        }

        [Description("State of the button"), Category("Button")]
        public ButtonState State
        {
            get
            {
                return this.buttonState;
            }
            set
            {
                this.buttonState = value;
                base.Invalidate();
            }
        }

        [Category("Button"), Description("Style of the button")]
        public ButtonStyle Style
        {
            get
            {
                return this.buttonStyle;
            }
            set
            {
                this.buttonStyle = value;
                this.CalculateDimensions();
            }
        }

        public enum ButtonState
        {
            Normal,
            Pressed
        }

        public enum ButtonStyle
        {
            Circular,
            Rectangular,
            Elliptical
        }
    }
}

