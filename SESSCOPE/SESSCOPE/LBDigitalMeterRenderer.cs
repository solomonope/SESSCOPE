namespace LBSoft.IndustrialCtrls.Meters
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LBDigitalMeterRenderer : LBRendererBase
    {
        public override void Draw(Graphics Gr)
        {
            if (this.Meter != null)
            {
                RectangleF rc = new RectangleF(0f, 0f, (float) this.Meter.Width, (float) this.Meter.Height);
                Gr.SmoothingMode = SmoothingMode.AntiAlias;
                this.DrawBackground(Gr, rc);
                this.DrawBorder(Gr, rc);
            }
        }

        public virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            if (this.Meter == null)
            {
                return false;
            }
            Color backColor = this.Meter.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.Meter.Width, this.Meter.Height);
            gr.DrawRectangle(pen, rect);
            gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        public virtual bool DrawBorder(Graphics gr, RectangleF rc)
        {
            if (this.Meter == null)
            {
                return false;
            }
            return true;
        }

        public LBDigitalMeter Meter
        {
            get
            {
                return (base.Control as LBDigitalMeter);
            }
        }
    }
}

