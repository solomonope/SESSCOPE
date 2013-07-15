namespace LBSoft.IndustrialCtrls.Buttons
{
    using LBSoft.IndustrialCtrls.Base;
    using LBSoft.IndustrialCtrls.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LBButtonRenderer : LBRendererBase
    {
        protected float drawRatio = 1f;
        protected RectangleF rectBody;
        protected RectangleF rectCtrl = new RectangleF(0f, 0f, 0f, 0f);
        protected RectangleF rectText;

        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr", "Invalid Graphics object");
            }
            if (this.Button == null)
            {
                throw new NullReferenceException("Invalid 'Button' object");
            }
            this.DrawBackground(Gr, this.rectCtrl);
            this.DrawBody(Gr, this.rectBody);
            this.DrawText(Gr, this.rectText);
        }

        public virtual bool DrawBackground(Graphics Gr, RectangleF rc)
        {
            if (this.Button == null)
            {
                return false;
            }
            Color backColor = this.Button.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.Button.Width, this.Button.Height);
            Gr.DrawRectangle(pen, rect);
            Gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        public virtual bool DrawBody(Graphics Gr, RectangleF rc)
        {
            if (this.Button == null)
            {
                return false;
            }
            Color buttonColor = this.Button.ButtonColor;
            Color color2 = LBColorManager.StepColor(buttonColor, 20);
            LinearGradientBrush brush = new LinearGradientBrush(rc, buttonColor, color2, 45f);
            if ((this.Button.Style == LBButton.ButtonStyle.Circular) || (this.Button.Style == LBButton.ButtonStyle.Elliptical))
            {
                Gr.FillEllipse(brush, rc);
            }
            else
            {
                GraphicsPath path = this.RoundedRect(rc, 15f);
                Gr.FillPath(brush, path);
                path.Dispose();
            }
            if (this.Button.State == LBButton.ButtonState.Pressed)
            {
                RectangleF rect = rc;
                rect.Inflate(-15f * this.drawRatio, -15f * this.drawRatio);
                LinearGradientBrush brush2 = new LinearGradientBrush(rect, color2, buttonColor, 45f);
                if ((this.Button.Style == LBButton.ButtonStyle.Circular) || (this.Button.Style == LBButton.ButtonStyle.Elliptical))
                {
                    Gr.FillEllipse(brush2, rect);
                }
                else
                {
                    GraphicsPath path2 = this.RoundedRect(rect, 10f);
                    Gr.FillPath(brush2, path2);
                    path2.Dispose();
                }
                brush2.Dispose();
            }
            brush.Dispose();
            return true;
        }

        public virtual bool DrawText(Graphics Gr, RectangleF rc)
        {
            if (this.Button != null)
            {
                Font font = new Font(this.Button.Font.FontFamily, this.Button.Font.Size * this.drawRatio, this.Button.Font.Style);
                string label = this.Button.Label;
                Color buttonColor = this.Button.ButtonColor;
                Color color = LBColorManager.StepColor(buttonColor, 20);
                SizeF ef = Gr.MeasureString(label, font);
                SolidBrush brush = new SolidBrush(buttonColor);
                SolidBrush brush2 = new SolidBrush(color);
                Gr.DrawString(label, font, brush, (float) ((rc.Left + ((rc.Width * 0.5f) - (ef.Width * 0.5f))) + (1f * this.drawRatio)), (float) ((rc.Top + ((rc.Height * 0.5f) - ((float) (ef.Height * 0.5)))) + (1f * this.drawRatio)));
                Gr.DrawString(label, font, brush2, (float) (rc.Left + ((rc.Width * 0.5f) - (ef.Width * 0.5f))), (float) (rc.Top + ((rc.Height * 0.5f) - ((float) (ef.Height * 0.5)))));
                brush.Dispose();
                brush2.Dispose();
                font.Dispose();
            }
            return false;
        }

        protected GraphicsPath RoundedRect(RectangleF rect, float radius)
        {
            RectangleF ef = rect;
            float width = (radius * this.drawRatio) * 2f;
            SizeF size = new SizeF(width, width);
            RectangleF ef3 = new RectangleF(ef.Location, size);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(ef3, 180f, 90f);
            ef3.X = ef.Right - width;
            path.AddArc(ef3, 270f, 90f);
            ef3.Y = ef.Bottom - width;
            path.AddArc(ef3, 0f, 90f);
            ef3.X = ef.Left;
            path.AddArc(ef3, 90f, 90f);
            path.CloseFigure();
            return path;
        }

        public override bool Update()
        {
            if (this.Button == null)
            {
                throw new NullReferenceException("Invalid 'Button' object");
            }
            this.rectCtrl.X = 0f;
            this.rectCtrl.Y = 0f;
            this.rectCtrl.Width = this.Button.Width;
            this.rectCtrl.Height = this.Button.Height;
            if (this.Button.Style == LBButton.ButtonStyle.Circular)
            {
                if (this.rectCtrl.Width < this.rectCtrl.Height)
                {
                    this.rectCtrl.Height = this.rectCtrl.Width;
                }
                else if (this.rectCtrl.Width > this.rectCtrl.Height)
                {
                    this.rectCtrl.Width = this.rectCtrl.Height;
                }
                if (this.rectCtrl.Width < 10f)
                {
                    this.rectCtrl.Width = 10f;
                }
                if (this.rectCtrl.Height < 10f)
                {
                    this.rectCtrl.Height = 10f;
                }
            }
            this.rectBody = this.rectCtrl;
            this.rectBody.Width--;
            this.rectBody.Height--;
            this.rectText = this.rectCtrl;
            this.rectText.Width -= 2f;
            this.rectText.Height -= 2f;
            this.drawRatio = Math.Min(this.rectCtrl.Width, this.rectCtrl.Height) / 200f;
            if (this.drawRatio == 0.0)
            {
                this.drawRatio = 1f;
            }
            return true;
        }

        public LBButton Button
        {
            get
            {
                return (base.Control as LBButton);
            }
        }
    }
}

