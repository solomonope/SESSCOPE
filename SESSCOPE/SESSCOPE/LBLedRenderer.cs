namespace LBSoft.IndustrialCtrls.Leds
{
    using LBSoft.IndustrialCtrls.Base;
    using LBSoft.IndustrialCtrls.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LBLedRenderer : LBRendererBase
    {
        private RectangleF drawRect;
        private RectangleF rectLabel;
        private RectangleF rectLed;

        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr");
            }
            LBLed led = this.Led;
            if (led == null)
            {
                throw new NullReferenceException("Associated control is not valid");
            }
            Rectangle bounds = led.Bounds;
            this.DrawBackground(Gr, bounds);
            if (this.rectLed.Width <= 0f)
            {
                this.rectLed.Width = this.rectLabel.Width;
            }
            if (this.rectLed.Height <= 0f)
            {
                this.rectLed.Height = led.LedSize.Height;
            }
            this.DrawLed(Gr, this.rectLed);
            this.DrawLabel(Gr, this.rectLabel);
        }

        public virtual bool DrawBackground(Graphics Gr, RectangleF rc)
        {
            if (this.Led == null)
            {
                return false;
            }
            Color backColor = this.Led.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.Led.Width, this.Led.Height);
            Gr.DrawRectangle(pen, rect);
            Gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        public virtual bool DrawLabel(Graphics Gr, RectangleF rc)
        {
            if (this.Led == null)
            {
                return false;
            }
            if (this.Led.Label == string.Empty)
            {
                return false;
            }
            SizeF ef = Gr.MeasureString(this.Led.Label, this.Led.Font);
            SolidBrush brush = new SolidBrush(this.Led.ForeColor);
            float num = 0f;
            float num2 = 0f;
            switch (this.Led.LabelPosition)
            {
                case LBLed.LedLabelPosition.Left:
                    num = rc.Width - ef.Width;
                    num2 = (rc.Height * 0.5f) - (ef.Height * 0.5f);
                    break;

                case LBLed.LedLabelPosition.Top:
                    num = (rc.Width * 0.5f) - (ef.Width * 0.5f);
                    num2 = rc.Bottom - ef.Height;
                    break;

                case LBLed.LedLabelPosition.Right:
                    num2 = (rc.Height * 0.5f) - (ef.Height * 0.5f);
                    break;

                case LBLed.LedLabelPosition.Bottom:
                    num = (rc.Width * 0.5f) - (ef.Width * 0.5f);
                    break;
            }
            Gr.DrawString(this.Led.Label, this.Led.Font, brush, (float) (rc.Left + num), (float) (rc.Top + num2));
            return true;
        }

        public virtual bool DrawLed(Graphics Gr, RectangleF rc)
        {
            if (this.Led == null)
            {
                return false;
            }
            Color color = LBColorManager.StepColor(Color.LightGray, 20);
            Color color2 = LBColorManager.StepColor(this.Led.LedColor, 60);
            LinearGradientBrush brush = new LinearGradientBrush(rc, Color.Gray, color, 45f);
            LinearGradientBrush brush2 = new LinearGradientBrush(rc, this.Led.LedColor, color2, 45f);
            if (this.Led.State == LBLed.LedState.Blink)
            {
                if (!this.Led.BlinkIsOn)
                {
                    if (this.Led.Style == LBLed.LedStyle.Circular)
                    {
                        Gr.FillEllipse(brush, rc);
                    }
                    else if (this.Led.Style == LBLed.LedStyle.Rectangular)
                    {
                        Gr.FillRectangle(brush, rc);
                    }
                }
                else if (this.Led.Style == LBLed.LedStyle.Circular)
                {
                    Gr.FillEllipse(brush2, rc);
                }
                else if (this.Led.Style == LBLed.LedStyle.Rectangular)
                {
                    Gr.FillRectangle(brush2, rc);
                }
            }
            else if (this.Led.State == LBLed.LedState.Off)
            {
                if (this.Led.Style == LBLed.LedStyle.Circular)
                {
                    Gr.FillEllipse(brush, rc);
                }
                else if (this.Led.Style == LBLed.LedStyle.Rectangular)
                {
                    Gr.FillRectangle(brush, rc);
                }
            }
            else if (this.Led.Style == LBLed.LedStyle.Circular)
            {
                Gr.FillEllipse(brush2, rc);
            }
            else if (this.Led.Style == LBLed.LedStyle.Rectangular)
            {
                Gr.FillRectangle(brush2, rc);
            }
            brush.Dispose();
            brush2.Dispose();
            return true;
        }

        public override bool Update()
        {
            if (this.Led == null)
            {
                throw new NullReferenceException("Invalid 'Led' object");
            }
            float num = 0f;
            float num2 = 0f;
            float width = this.Led.Size.Width;
            float height = this.Led.Size.Height;
            this.drawRect.X = num;
            this.drawRect.Y = num2;
            this.drawRect.Width = width - 2f;
            this.drawRect.Height = height - 2f;
            if (this.drawRect.Width <= 0f)
            {
                this.drawRect.Width = 20f;
            }
            if (this.drawRect.Height <= 0f)
            {
                this.drawRect.Height = 20f;
            }
            this.rectLed = this.drawRect;
            this.rectLabel = this.drawRect;
            if (this.Led.LabelPosition == LBLed.LedLabelPosition.Bottom)
            {
                this.rectLed.X = (this.rectLed.Width * 0.5f) - (this.Led.LedSize.Width * 0.5f);
                this.rectLed.Width = this.Led.LedSize.Width;
                this.rectLed.Height = this.Led.LedSize.Height;
                this.rectLabel.Y = this.rectLed.Bottom;
            }
            else if (this.Led.LabelPosition == LBLed.LedLabelPosition.Top)
            {
                this.rectLed.X = (this.rectLed.Width * 0.5f) - (this.Led.LedSize.Width * 0.5f);
                this.rectLed.Y = this.rectLed.Height - this.Led.LedSize.Height;
                this.rectLed.Width = this.Led.LedSize.Width;
                this.rectLed.Height = this.Led.LedSize.Height;
                this.rectLabel.Height = this.rectLed.Top;
            }
            else if (this.Led.LabelPosition == LBLed.LedLabelPosition.Left)
            {
                this.rectLed.X = this.rectLed.Width - this.Led.LedSize.Width;
                this.rectLed.Width = this.Led.LedSize.Width;
                this.rectLed.Height = this.Led.LedSize.Height;
                this.rectLabel.Width -= this.rectLed.Width;
            }
            else if (this.Led.LabelPosition == LBLed.LedLabelPosition.Right)
            {
                this.rectLed.Width = this.Led.LedSize.Width;
                this.rectLed.Height = this.Led.LedSize.Height;
                this.rectLabel.X = this.rectLed.Right;
            }
            return true;
        }

        public LBLed Led
        {
            get
            {
                return (base.Control as LBLed);
            }
        }
    }
}

