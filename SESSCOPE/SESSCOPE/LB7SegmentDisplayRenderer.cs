namespace LBSoft.IndustrialCtrls.Leds
{
    using LBSoft.IndustrialCtrls.Base;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class LB7SegmentDisplayRenderer : LBRendererBase
    {
        protected PointsList defPoints = new PointsList();
        public const int HEIGHT_PIXELS = 0x12;
        protected PointsList points = new PointsList();
        protected RectangleF rectDP = new RectangleF();
        protected SegmentDictionary segments = new SegmentDictionary();
        protected SegmentsValueDictionary valuesSegments = new SegmentsValueDictionary();
        public const int WIDTH_PIXEL = 11;

        public LB7SegmentDisplayRenderer()
        {
            this.CreateSegmetsData();
            this.CreateDefPointsCoordinates();
            this.CreateSegmentsValuesList();
            this.UpdatePointsCoordinates();
        }

        protected virtual void CreateDefPointsCoordinates()
        {
            PointF item = new PointF(3f, 1f);
            this.defPoints.Add(item);
            item = new PointF(8f, 1f);
            this.defPoints.Add(item);
            item = new PointF(9f, 2f);
            this.defPoints.Add(item);
            item = new PointF(10f, 3f);
            this.defPoints.Add(item);
            item = new PointF(10f, 8f);
            this.defPoints.Add(item);
            item = new PointF(9f, 9f);
            this.defPoints.Add(item);
            item = new PointF(10f, 10f);
            this.defPoints.Add(item);
            item = new PointF(10f, 15f);
            this.defPoints.Add(item);
            item = new PointF(9f, 16f);
            this.defPoints.Add(item);
            item = new PointF(8f, 17f);
            this.defPoints.Add(item);
            item = new PointF(3f, 17f);
            this.defPoints.Add(item);
            item = new PointF(2f, 16f);
            this.defPoints.Add(item);
            item = new PointF(1f, 15f);
            this.defPoints.Add(item);
            item = new PointF(1f, 10f);
            this.defPoints.Add(item);
            item = new PointF(2f, 9f);
            this.defPoints.Add(item);
            item = new PointF(1f, 8f);
            this.defPoints.Add(item);
            item = new PointF(1f, 3f);
            this.defPoints.Add(item);
            item = new PointF(2f, 2f);
            this.defPoints.Add(item);
            item = new PointF(3f, 3f);
            this.defPoints.Add(item);
            item = new PointF(8f, 3f);
            this.defPoints.Add(item);
            item = new PointF(8f, 8f);
            this.defPoints.Add(item);
            item = new PointF(8f, 10f);
            this.defPoints.Add(item);
            item = new PointF(8f, 15f);
            this.defPoints.Add(item);
            item = new PointF(3f, 15f);
            this.defPoints.Add(item);
            item = new PointF(3f, 10f);
            this.defPoints.Add(item);
            item = new PointF(3f, 8f);
            this.defPoints.Add(item);
        }

        protected virtual void CreateSegmentsValuesList()
        {
            SegmentsList seg = new SegmentsList { 0x41, 0x42, 0x43, 0x44, 0x45, 70 };
            this.valuesSegments.Add(0, seg);
            seg = new SegmentsList { 0x42, 0x43 };
            this.valuesSegments.Add(1, seg);
            seg = new SegmentsList { 0x41, 0x42, 0x47, 0x45, 0x44 };
            this.valuesSegments.Add(2, seg);
            seg = new SegmentsList { 0x41, 0x42, 0x47, 0x43, 0x44 };
            this.valuesSegments.Add(3, seg);
            seg = new SegmentsList { 70, 0x47, 0x42, 0x43 };
            this.valuesSegments.Add(4, seg);
            seg = new SegmentsList { 0x41, 70, 0x47, 0x43, 0x44 };
            this.valuesSegments.Add(5, seg);
            seg = new SegmentsList { 0x41, 70, 0x47, 0x43, 0x44, 0x45 };
            this.valuesSegments.Add(6, seg);
            seg = new SegmentsList { 0x41, 0x42, 0x43 };
            this.valuesSegments.Add(7, seg);
            seg = new SegmentsList { 0x41, 0x42, 0x43, 0x44, 0x45, 70, 0x47 };
            this.valuesSegments.Add(8, seg);
            seg = new SegmentsList { 0x41, 0x42, 0x43, 0x44, 70, 0x47 };
            this.valuesSegments.Add(9, seg);
            seg = new SegmentsList { 0x47 };
            this.valuesSegments.Add(0x2d, seg);
            seg = new SegmentsList { 0x41, 0x44, 0x45, 70, 0x47 };
            this.valuesSegments.Add(0x45, seg);
        }

        protected virtual void CreateSegmetsData()
        {
            this.Segments.Clear();
            Segment seg = new Segment();
            seg.PointsIndexs[0] = 0;
            seg.PointsIndexs[1] = 1;
            seg.PointsIndexs[2] = 2;
            seg.PointsIndexs[3] = 0x13;
            seg.PointsIndexs[4] = 0x12;
            seg.PointsIndexs[5] = 0x11;
            this.Segments.Add('A', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 2;
            seg.PointsIndexs[1] = 3;
            seg.PointsIndexs[2] = 4;
            seg.PointsIndexs[3] = 5;
            seg.PointsIndexs[4] = 20;
            seg.PointsIndexs[5] = 0x13;
            this.Segments.Add('B', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 6;
            seg.PointsIndexs[1] = 7;
            seg.PointsIndexs[2] = 8;
            seg.PointsIndexs[3] = 0x16;
            seg.PointsIndexs[4] = 0x15;
            seg.PointsIndexs[5] = 5;
            this.Segments.Add('C', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 9;
            seg.PointsIndexs[1] = 10;
            seg.PointsIndexs[2] = 11;
            seg.PointsIndexs[3] = 0x17;
            seg.PointsIndexs[4] = 0x16;
            seg.PointsIndexs[5] = 8;
            this.Segments.Add('D', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 12;
            seg.PointsIndexs[1] = 13;
            seg.PointsIndexs[2] = 14;
            seg.PointsIndexs[3] = 0x18;
            seg.PointsIndexs[4] = 0x17;
            seg.PointsIndexs[5] = 11;
            this.Segments.Add('E', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 15;
            seg.PointsIndexs[1] = 0x10;
            seg.PointsIndexs[2] = 0x11;
            seg.PointsIndexs[3] = 0x12;
            seg.PointsIndexs[4] = 0x19;
            seg.PointsIndexs[5] = 14;
            this.Segments.Add('F', seg);
            seg = new Segment();
            seg.PointsIndexs[0] = 0x19;
            seg.PointsIndexs[1] = 20;
            seg.PointsIndexs[2] = 5;
            seg.PointsIndexs[3] = 0x15;
            seg.PointsIndexs[4] = 0x18;
            seg.PointsIndexs[5] = 14;
            this.Segments.Add('G', seg);
        }

        public override void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr");
            }
            LB7SegmentDisplay display = this.Display;
            if (display == null)
            {
                throw new NullReferenceException("Associated control is not valid");
            }
            RectangleF rc = new RectangleF(0f, 0f, (float) display.Width, (float) display.Height);
            Gr.SmoothingMode = SmoothingMode.AntiAlias;
            this.DrawBackground(Gr, rc);
            this.DrawOffSegments(Gr, rc);
            this.DrawValue(Gr, rc);
        }

        protected virtual bool DrawBackground(Graphics gr, RectangleF rc)
        {
            if (this.Display == null)
            {
                return false;
            }
            Color backColor = this.Display.BackColor;
            SolidBrush brush = new SolidBrush(backColor);
            Pen pen = new Pen(backColor);
            Rectangle rect = new Rectangle(0, 0, this.Display.Width, this.Display.Height);
            gr.DrawRectangle(pen, rect);
            gr.FillRectangle(brush, rc);
            brush.Dispose();
            pen.Dispose();
            return true;
        }

        protected virtual bool DrawOffSegments(Graphics gr, RectangleF rc)
        {
            if (this.Display == null)
            {
                return false;
            }
            SolidBrush brush = new SolidBrush(Color.FromArgb(70, this.Display.ForeColor));
            foreach (Segment segment in this.Segments.Values)
            {
                GraphicsPath path = new GraphicsPath();
                for (int i = 0; i < (segment.PointsIndexs.Length - 1); i++)
                {
                    PointF tf = this.points[segment.PointsIndexs[i]];
                    PointF tf2 = this.points[segment.PointsIndexs[i + 1]];
                    path.AddLine(tf, tf2);
                }
                path.CloseFigure();
                gr.FillPath(brush, path);
                path.Dispose();
            }
            gr.FillEllipse(brush, this.rectDP);
            brush.Dispose();
            return true;
        }

        protected virtual bool DrawValue(Graphics gr, RectangleF rc)
        {
            if (this.Display == null)
            {
                return false;
            }
            if (!this.valuesSegments.Contains(this.Display.Value))
            {
                return false;
            }
            SegmentsList list = this.valuesSegments[this.Display.Value];
            if (list == null)
            {
                return false;
            }
            SolidBrush brush = new SolidBrush(this.Display.ForeColor);
            foreach (char ch in list)
            {
                Segment segment = this.segments[ch];
                if (segment != null)
                {
                    GraphicsPath path = new GraphicsPath();
                    for (int i = 0; i < (segment.PointsIndexs.Length - 1); i++)
                    {
                        PointF tf = this.points[segment.PointsIndexs[i]];
                        PointF tf2 = this.points[segment.PointsIndexs[i + 1]];
                        path.AddLine(tf, tf2);
                    }
                    path.CloseFigure();
                    gr.FillPath(brush, path);
                    path.Dispose();
                }
            }
            if (this.Display.ShowDP)
            {
                gr.FillEllipse(brush, this.rectDP);
            }
            brush.Dispose();
            return true;
        }

        public override bool Update()
        {
            this.UpdatePointsCoordinates();
            return true;
        }

        protected virtual void UpdatePointsCoordinates()
        {
            this.points.Clear();
            double num = 1.0;
            double num2 = 1.0;
            if (this.Display != null)
            {
                num = ((double) this.Display.Width) / 11.0;
                num2 = ((double) this.Display.Height) / 18.0;
            }
            for (int i = 0; i < this.defPoints.Count; i++)
            {
                PointF tf = this.defPoints[i];
                PointF item = new PointF((float) (tf.X * num), (float) (tf.Y * num2));
                this.points.Add(item);
            }
            PointF tf3 = this.points[7];
            this.rectDP.X = tf3.X - ((float) (0.5 * num));
            PointF tf4 = this.points[8];
            this.rectDP.Y = tf4.Y;
            this.rectDP.Width = (float) num;
            this.rectDP.Height = (float) num2;
        }

        [Browsable(false)]
        public LB7SegmentDisplay Display
        {
            get
            {
                return (base.Control as LB7SegmentDisplay);
            }
            set
            {
                base.Control = value;
            }
        }

        [Browsable(false)]
        public SegmentDictionary Segments
        {
            get
            {
                return this.segments;
            }
        }
    }
}

