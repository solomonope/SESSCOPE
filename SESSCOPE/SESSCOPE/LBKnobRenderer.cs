namespace LBSoft.IndustrialCtrls.Knobs
{
    using LBSoft.IndustrialCtrls.Base;
    using LBSoft.IndustrialCtrls.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LBKnobRenderer : LBRendererBase
    {
        private float drawRatio;
        private RectangleF drawRect;
        private PointF knobCenter;
        private PointF knobIndicatorPos;
        private RectangleF rectKnob;
        private RectangleF rectScale;

        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr");
            }
            LBKnob knob = this.Knob;
            if (knob == null)
            {
                throw new NullReferenceException("Associated control is not valid");
            }
            this.DrawBackground(Gr, knob.Bounds);
            this.DrawScale(Gr, this.rectScale);
            this.DrawKnob(Gr, this.rectKnob);
            this.DrawKnobIndicator(Gr, this.rectKnob, this.knobIndicatorPos);
        }

        public virtual bool DrawBackground(Graphics Gr, RectangleF rc)
        {
            if (this.Knob == null)
            {
                return false;
            }
            Color backColor = this.Knob.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.Knob.Width, this.Knob.Height);
            Gr.DrawRectangle(pen, rect);
            Gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        public virtual bool DrawKnob(Graphics Gr, RectangleF rc)
        {
            if (this.Knob == null)
            {
                return false;
            }
            Color knobColor = this.Knob.KnobColor;
            Color color2 = LBColorManager.StepColor(knobColor, 60);
            LinearGradientBrush brush = new LinearGradientBrush(rc, knobColor, color2, 45f);
            Gr.FillEllipse(brush, rc);
            brush.Dispose();
            return true;
        }

        public virtual bool DrawKnobIndicator(Graphics Gr, RectangleF rc, PointF pos)
        {
            if (this.Knob == null)
            {
                return false;
            }
            RectangleF rect = rc;
            rect.X = pos.X - 4f;
            rect.Y = pos.Y - 4f;
            rect.Width = 8f;
            rect.Height = 8f;
            Color indicatorColor = this.Knob.IndicatorColor;
            Color color2 = LBColorManager.StepColor(indicatorColor, 60);
            LinearGradientBrush brush = new LinearGradientBrush(rect, color2, indicatorColor, 45f);
            Gr.FillEllipse(brush, rect);
            brush.Dispose();
            return true;
        }

        public virtual bool DrawScale(Graphics Gr, RectangleF rc)
        {
            if (this.Knob == null)
            {
                return false;
            }
            Color scaleColor = this.Knob.ScaleColor;
            Color color2 = LBColorManager.StepColor(scaleColor, 60);
            LinearGradientBrush brush = new LinearGradientBrush(rc, color2, scaleColor, 45f);
            Gr.FillEllipse(brush, rc);
            brush.Dispose();
            return true;
        }

        public override bool Update()
        {
            if (this.Knob == null)
            {
                throw new NullReferenceException("Invalid 'Knob' object");
            }
            float num = 0f;
            float num2 = 0f;
            float width = this.Knob.Size.Width;
            float height = this.Knob.Size.Height;
            this.drawRatio = Math.Min(width, height) / 200f;
            if (this.drawRatio == 0.0)
            {
                this.drawRatio = 1f;
            }
            this.drawRect.X = num;
            this.drawRect.Y = num2;
            this.drawRect.Width = width - 2f;
            this.drawRect.Height = height - 2f;
            if (width < height)
            {
                this.drawRect.Height = width;
            }
            else if (width > height)
            {
                this.drawRect.Width = height;
            }
            if (this.drawRect.Width < 10f)
            {
                this.drawRect.Width = 10f;
            }
            if (this.drawRect.Height < 10f)
            {
                this.drawRect.Height = 10f;
            }
            this.rectScale = this.drawRect;
            this.rectKnob = this.drawRect;
            this.rectKnob.Inflate(-20f * this.drawRatio, -20f * this.drawRatio);
            this.knobCenter.X = this.rectKnob.Left + (this.rectKnob.Width * 0.5f);
            this.knobCenter.Y = this.rectKnob.Top + (this.rectKnob.Height * 0.5f);
            this.knobIndicatorPos = this.Knob.GetPositionFromValue(this.Knob.Value);
            this.Knob.KnobRect = this.rectKnob;
            this.Knob.KnobCenter = this.knobCenter;
            this.Knob.DrawRatio = this.drawRatio;
            return true;
        }

        public LBKnob Knob
        {
            get
            {
                return (base.Control as LBKnob);
            }
        }
    }
}

