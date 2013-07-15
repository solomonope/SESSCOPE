namespace LBSoft.IndustrialCtrls.Meters
{
    using LBSoft.IndustrialCtrls.Base;
    using LBSoft.IndustrialCtrls.Utils;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LBAnalogMeterRenderer : LBRendererBase
    {
        protected float drawRatio;
        protected RectangleF drawRect;
        protected RectangleF glossyRect;
        protected PointF needleCenter;
        protected RectangleF needleCoverRect;

        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr");
            }
            LBAnalogMeter analogMeter = this.AnalogMeter;
            if (analogMeter == null)
            {
                throw new NullReferenceException("Associated control is not valid");
            }
            this.DrawBackground(Gr, analogMeter.Bounds);
            this.DrawBody(Gr, this.drawRect);
            this.DrawThresholds(Gr, this.drawRect);
            this.DrawDivisions(Gr, this.drawRect);
            this.DrawUM(Gr, this.drawRect);
            this.DrawValue(Gr, this.drawRect);
            this.DrawNeedle(Gr, this.drawRect);
            this.DrawNeedleCover(Gr, this.needleCoverRect);
            this.DrawGlass(Gr, this.glossyRect);
        }

        public virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            Color backColor = this.AnalogMeter.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.AnalogMeter.Width, this.AnalogMeter.Height);
            gr.DrawRectangle(pen, rect);
            gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        public virtual bool DrawBody(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            Color bodyColor = this.AnalogMeter.BodyColor;
            Color color2 = LBColorManager.StepColor(bodyColor, 20);
            LinearGradientBrush brush = new LinearGradientBrush(rc, bodyColor, color2, 45f);
            Gr.FillEllipse(brush, rc);
            RectangleF rect = rc;
            rect.X += 3f * this.drawRatio;
            rect.Y += 3f * this.drawRatio;
            rect.Width -= 6f * this.drawRatio;
            rect.Height -= 6f * this.drawRatio;
            LinearGradientBrush brush2 = new LinearGradientBrush(rect, color2, bodyColor, 45f);
            Gr.FillEllipse(brush2, rect);
            return true;
        }

        public virtual bool DrawDivisions(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            float scaleDivisions = this.AnalogMeter.ScaleDivisions;
            float scaleSubDivisions = this.AnalogMeter.ScaleSubDivisions;
            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            Color scaleColor = this.AnalogMeter.ScaleColor;
            float x = this.needleCenter.X;
            float y = this.needleCenter.Y;
            float width = rc.Width;
            float height = rc.Height;
            float radian = LBMath.GetRadian((endAngle - startAngle) / ((scaleDivisions - 1f) * (scaleSubDivisions + 1f)));
            float num11 = LBMath.GetRadian(startAngle);
            float num12 = (width / 2f) - ((float) (width * 0.08));
            float num13 = (float) minValue;
            Pen pen = new Pen(scaleColor, 1f * this.drawRatio);
            SolidBrush brush = new SolidBrush(scaleColor);
            PointF tf = new PointF(0f, 0f);
            PointF tf2 = new PointF(0f, 0f);
            for (int i = 0; i < scaleDivisions; i++)
            {
                tf.X = x + ((float) (num12 * Math.Cos((double) num11)));
                tf.Y = y + ((float) (num12 * Math.Sin((double) num11)));
                tf2.X = x + ((float) ((num12 - (width / 20f)) * Math.Cos((double) num11)));
                tf2.Y = y + ((float) ((num12 - (width / 20f)) * Math.Sin((double) num11)));
                Gr.DrawLine(pen, tf, tf2);
                Font font = new Font(this.AnalogMeter.Font.FontFamily, 6f * this.drawRatio);
                float num15 = x + ((float) ((num12 - (20f * this.drawRatio)) * Math.Cos((double) num11)));
                float num16 = y + ((float) ((num12 - (20f * this.drawRatio)) * Math.Sin((double) num11)));
                double num17 = Math.Round((double) num13);
                string text = string.Format("{0,0:D}", (int) num17);
                SizeF ef = Gr.MeasureString(text, font);
                Gr.DrawString(text, font, brush, (float) (num15 - ((float) (ef.Width * 0.5))), (float) (num16 - ((float) (ef.Height * 0.5))));
                num13 += (float) ((maxValue - minValue) / ((double) (scaleDivisions - 1f)));
                if (i == (scaleDivisions - 1f))
                {
                    font.Dispose();
                    break;
                }
                if (scaleDivisions <= 0f)
                {
                    num11 += radian;
                }
                else
                {
                    for (int j = 0; j <= scaleSubDivisions; j++)
                    {
                        num11 += radian;
                        tf.X = x + ((float) (num12 * Math.Cos((double) num11)));
                        tf.Y = y + ((float) (num12 * Math.Sin((double) num11)));
                        tf2.X = x + ((float) ((num12 - (width / 50f)) * Math.Cos((double) num11)));
                        tf2.Y = y + ((float) ((num12 - (width / 50f)) * Math.Sin((double) num11)));
                        Gr.DrawLine(pen, tf, tf2);
                    }
                }
                font.Dispose();
            }
            return true;
        }

        public virtual bool DrawGlass(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            if (this.AnalogMeter.ViewGlass)
            {
                Color color = Color.FromArgb(40, 200, 200, 200);
                Color color2 = Color.FromArgb(0, 200, 200, 200);
                LinearGradientBrush brush = new LinearGradientBrush(rc, color, color2, 45f);
                Gr.FillEllipse(brush, rc);
            }
            return true;
        }

        public virtual bool DrawNeedle(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            float width = rc.Width;
            float height = rc.Height;
            double minValue = this.AnalogMeter.MinValue;
            double maxValue = this.AnalogMeter.MaxValue;
            double num4 = this.AnalogMeter.Value;
            float startAngle = this.AnalogMeter.GetStartAngle();
            float endAngle = this.AnalogMeter.GetEndAngle();
            float num7 = (width / 2f) - ((float) (width * 0.12));
            float val = (float) (maxValue - minValue);
            val = (float) ((100.0 * (num4 - minValue)) / ((double) val));
            val = ((endAngle - startAngle) * val) / 100f;
            val += startAngle;
            float radian = LBMath.GetRadian(val);
            float x = this.needleCenter.X;
            float y = this.needleCenter.Y;
            PointF tf = new PointF(0f, 0f);
            PointF tf2 = new PointF(0f, 0f);
            GraphicsPath path = new GraphicsPath();
            tf.X = x;
            tf.Y = y;
            radian = LBMath.GetRadian(val + 10f);
            tf2.X = x + ((float) ((width * 0.09f) * Math.Cos((double) radian)));
            tf2.Y = y + ((float) ((width * 0.09f) * Math.Sin((double) radian)));
            path.AddLine(tf, tf2);
            tf = tf2;
            radian = LBMath.GetRadian(val);
            tf2.X = x + ((float) (num7 * Math.Cos((double) radian)));
            tf2.Y = y + ((float) (num7 * Math.Sin((double) radian)));
            path.AddLine(tf, tf2);
            tf = tf2;
            radian = LBMath.GetRadian(val - 10f);
            tf2.X = x + ((float) ((width * 0.09f) * Math.Cos((double) radian)));
            tf2.Y = y + ((float) ((width * 0.09f) * Math.Sin((double) radian)));
            path.AddLine(tf, tf2);
            path.CloseFigure();
            SolidBrush brush = new SolidBrush(this.AnalogMeter.NeedleColor);
            Pen pen = new Pen(this.AnalogMeter.NeedleColor);
            Gr.DrawPath(pen, path);
            Gr.FillPath(brush, path);
            return true;
        }

        public virtual bool DrawNeedleCover(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter == null)
            {
                return false;
            }
            Color needleColor = this.AnalogMeter.NeedleColor;
            RectangleF rect = rc;
            Color color = Color.FromArgb(70, needleColor);
            rect.Inflate(5f * this.drawRatio, 5f * this.drawRatio);
            SolidBrush brush = new SolidBrush(color);
            Gr.FillEllipse(brush, rect);
            color = needleColor;
            Color color3 = LBColorManager.StepColor(needleColor, 0x4b);
            LinearGradientBrush brush2 = new LinearGradientBrush(rc, color, color3, 45f);
            Gr.FillEllipse(brush2, rc);
            return true;
        }

        public virtual bool DrawThresholds(Graphics Gr, RectangleF rc)
        {
            if (this.AnalogMeter != null)
            {
                RectangleF rect = rc;
                rect.Inflate(-18f * this.drawRatio, -18f * this.drawRatio);
                double width = rect.Width;
                double num1 = width / 2.0;
                float startAngle = this.AnalogMeter.GetStartAngle();
                float num4 = this.AnalogMeter.GetEndAngle() - startAngle;
                float minValue = (float) this.AnalogMeter.MinValue;
                float maxValue = (float) this.AnalogMeter.MaxValue;
                double num7 = num4 / (maxValue - minValue);
                foreach (LBMeterThreshold threshold in this.AnalogMeter.Thresholds)
                {
                    float num8 = startAngle + ((float) (num7 * threshold.StartValue));
                    float sweepAngle = (float) (num7 * (threshold.EndValue - threshold.StartValue));
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(rect, num8, sweepAngle);
                    Pen pen = new Pen(threshold.Color, 4.5f * this.drawRatio);
                    Gr.DrawPath(pen, path);
                    pen.Dispose();
                    path.Dispose();
                }
            }
            return false;
        }

        public virtual bool DrawUM(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public virtual bool DrawValue(Graphics gr, RectangleF rc)
        {
            return false;
        }

        public override bool Update()
        {
            if (this.AnalogMeter == null)
            {
                throw new NullReferenceException("Invalid 'AnalogMeter' object");
            }
            float num = 0f;
            float num2 = 0f;
            float width = this.AnalogMeter.Size.Width;
            float height = this.AnalogMeter.Size.Height;
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
            this.needleCenter.X = this.drawRect.X + (this.drawRect.Width / 2f);
            this.needleCenter.Y = this.drawRect.Y + (this.drawRect.Height / 2f);
            this.needleCoverRect.X = this.needleCenter.X - (20f * this.drawRatio);
            this.needleCoverRect.Y = this.needleCenter.Y - (20f * this.drawRatio);
            this.needleCoverRect.Width = 40f * this.drawRatio;
            this.needleCoverRect.Height = 40f * this.drawRatio;
            this.glossyRect.X = this.drawRect.X + (20f * this.drawRatio);
            this.glossyRect.Y = this.drawRect.Y + (10f * this.drawRatio);
            this.glossyRect.Width = this.drawRect.Width - (40f * this.drawRatio);
            this.glossyRect.Height = this.needleCenter.Y + (30f * this.drawRatio);
            return false;
        }

        public LBAnalogMeter AnalogMeter
        {
            get
            {
                return (base.Control as LBAnalogMeter);
            }
        }
    }
}

