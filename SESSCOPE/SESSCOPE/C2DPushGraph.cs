namespace CustomUIControls.Graphing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class C2DPushGraph : Control
    {
        private IContainer components;
        private bool m_bAutoScale;
        private bool m_bHighQuality;
        private bool m_bMaxLabelSet;
        private bool m_bMinLabelSet;
        private bool m_bShowGrid;
        private bool m_bShowMinMax;
        private Color m_GridColor;
        private int m_GridSize;
        private int m_LineInterval;
        private List<Line> m_Lines;
        private int m_MaxCoords;
        private string m_MaxLabel;
        private int m_MaxPeek;
        private string m_MinLabel;
        private int m_MinPeek;
        private int m_MoveOffset;
        private int m_OffsetX;
        private Color m_TextColor;

        public C2DPushGraph()
        {
            this.m_TextColor = Color.Yellow;
            this.m_GridColor = Color.Green;
            this.m_MaxLabel = "Max";
            this.m_MinLabel = "Minimum";
            this.m_bHighQuality = true;
            this.m_bShowMinMax = true;
            this.m_bShowGrid = true;
            this.m_MaxCoords = -1;
            this.m_LineInterval = 5;
            this.m_MaxPeek = 100;
            this.m_GridSize = 15;
            this.m_Lines = new List<Line>();
            this.InitializeComponent();
            this.InitializeStyles();
        }

        public C2DPushGraph(Form Parent)
        {
            this.m_TextColor = Color.Yellow;
            this.m_GridColor = Color.Green;
            this.m_MaxLabel = "Max";
            this.m_MinLabel = "Minimum";
            this.m_bHighQuality = true;
            this.m_bShowMinMax = true;
            this.m_bShowGrid = true;
            this.m_MaxCoords = -1;
            this.m_LineInterval = 5;
            this.m_MaxPeek = 100;
            this.m_GridSize = 15;
            this.m_Lines = new List<Line>();
            Parent.Controls.Add(this);
            this.InitializeComponent();
            this.InitializeStyles();
        }

        public C2DPushGraph(Form parent, Rectangle rectPos)
        {
            this.m_TextColor = Color.Yellow;
            this.m_GridColor = Color.Green;
            this.m_MaxLabel = "Max";
            this.m_MinLabel = "Minimum";
            this.m_bHighQuality = true;
            this.m_bShowMinMax = true;
            this.m_bShowGrid = true;
            this.m_MaxCoords = -1;
            this.m_LineInterval = 5;
            this.m_MaxPeek = 100;
            this.m_GridSize = 15;
            this.m_Lines = new List<Line>();
            parent.Controls.Add(this);
            base.Location = rectPos.Location;
            base.Height = rectPos.Height;
            base.Width = rectPos.Width;
            this.InitializeComponent();
            this.InitializeStyles();
        }

        public LineHandle AddLine(int numID, Color clr)
        {
            if (this.LineExists(numID))
            {
                return null;
            }
            Line item = new Line(numID) {
                m_Color = clr
            };
            this.m_Lines.Add(item);
            object line = item;
            return new LineHandle(ref line, this);
        }

        public LineHandle AddLine(string nameID, Color clr)
        {
            if (this.LineExists(nameID))
            {
                return null;
            }
            Line item = new Line(nameID) {
                m_Color = clr
            };
            this.m_Lines.Add(item);
            object line = item;
            return new LineHandle(ref line, this);
        }

        protected void CalculateMaxPushPoints()
        {
            this.m_MaxCoords = (((base.Width - this.m_OffsetX) / this.m_LineInterval) + 2) + ((((base.Width - this.m_OffsetX) % this.m_LineInterval) != 0) ? 1 : 0);
            if (this.m_MaxCoords <= 0)
            {
                this.m_MaxCoords = 1;
            }
        }

        private void CullAndEqualizeMagnitudeCounts()
        {
            if (this.m_MaxCoords == -1)
            {
                this.CalculateMaxPushPoints();
            }
            int count = 0;
            foreach (Line line in this.m_Lines)
            {
                if (count < line.m_MagnitudeList.Count)
                {
                    count = line.m_MagnitudeList.Count;
                }
            }
            if (count != 0)
            {
                foreach (Line line2 in this.m_Lines)
                {
                    if (line2.m_MagnitudeList.Count == 0)
                    {
                        line2.m_MagnitudeList.Add(this.m_MinPeek);
                    }
                    while (line2.m_MagnitudeList.Count < count)
                    {
                        line2.m_MagnitudeList.Add(line2.m_MagnitudeList[line2.m_MagnitudeList.Count - 1]);
                    }
                    int num2 = (line2.m_MagnitudeList.Count - this.m_MaxCoords) + 1;
                    if (num2 > 0)
                    {
                        line2.m_MagnitudeList.RemoveRange(0, num2);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DrawBar(Rectangle rect, Line line, ref Graphics g)
        {
            SolidBrush brush = new SolidBrush(line.m_Color);
            g.FillRectangle(brush, rect);
            brush.Dispose();
        }

        protected void DrawGrid(ref Graphics g)
        {
            Pen pen = new Pen(this.m_GridColor, 1f);
            for (int i = base.Height - 1; i >= 0; i -= this.m_GridSize)
            {
                g.DrawLine(pen, this.m_OffsetX, i, base.Width, i);
            }
            for (int j = this.m_OffsetX + this.m_MoveOffset; j < base.Width; j += this.m_GridSize)
            {
                if (j >= this.m_OffsetX)
                {
                    g.DrawLine(pen, j, 0, j, base.Height);
                }
            }
            pen.Dispose();
        }

        protected void DrawLabels(ref Graphics g)
        {
            SizeF ef = g.MeasureString(this.m_MaxLabel, this.Font);
            SizeF ef2 = g.MeasureString(this.m_MinLabel, this.Font);
            int num = ((ef.Width > ef2.Width) ? ((int) ef.Width) : ((int) ef2.Width)) + 6;
            SolidBrush brush = new SolidBrush(this.m_TextColor);
            g.DrawString(this.m_MaxLabel, this.Font, brush, (float) ((num / 2) - (ef.Width / 2f)), (float) 2f);
            g.DrawString(this.m_MinLabel, this.Font, brush, (float) ((num / 2) - (ef2.Width / 2f)), (float) ((base.Height - ef2.Height) - 2f));
            brush.Dispose();
            Pen pen = new Pen(this.m_GridColor, 1f);
            g.DrawLine(pen, num + 6, 0, num + 6, base.Height);
            pen.Dispose();
            this.m_OffsetX = num + 6;
        }

        protected void DrawLines(ref Graphics g)
        {
            foreach (Line line in this.m_Lines)
            {
                if (line.m_MagnitudeList.Count == 0)
                {
                    break;
                }
                if (line.m_bVisible)
                {
                    Pen pen = new Pen(line.m_Color, (float) line.m_Thickness);
                    Point point = new Point {
                        X = this.m_OffsetX,
                        Y = base.Height - ((line.m_MagnitudeList[0] * base.Height) / (this.m_MaxPeek - this.m_MinPeek))
                    };
                    for (int i = 0; i < line.m_MagnitudeList.Count; i++)
                    {
                        if (line.m_bShowAsBar)
                        {
                            Rectangle rect = new Rectangle();
                            Point location = rect.Location;
                            location.X = (this.m_OffsetX + (i * this.m_LineInterval)) + 1;
                            location.Y = base.Height - ((line.m_MagnitudeList[i] * base.Height) / (this.m_MaxPeek - this.m_MinPeek));
                            rect.Location = location;
                            rect.Width = this.m_LineInterval - 1;
                            rect.Height = base.Height;
                            this.DrawBar(rect, line, ref g);
                        }
                        else
                        {
                            int num2 = this.m_OffsetX + (i * this.m_LineInterval);
                            int num3 = base.Height - ((line.m_MagnitudeList[i] * base.Height) / (this.m_MaxPeek - this.m_MinPeek));
                            g.DrawLine(pen, point.X, point.Y, num2, num3);
                            point.X = num2;
                            point.Y = num3;
                        }
                    }
                    pen.Dispose();
                }
            }
        }

        private Line GetLine(int numID)
        {
            foreach (Line line in this.m_Lines)
            {
                if (numID == line.m_NumID)
                {
                    return line;
                }
            }
            return null;
        }

        private Line GetLine(string nameID)
        {
            foreach (Line line in this.m_Lines)
            {
                if (string.Compare(nameID, line.m_NameID, true) == 0)
                {
                    return line;
                }
            }
            return null;
        }

        public LineHandle GetLineHandle(int numID)
        {
            object line = this.GetLine(numID);
            if (line == null)
            {
                return null;
            }
            return new LineHandle(ref line, this);
        }

        public LineHandle GetLineHandle(string nameID)
        {
            object line = this.GetLine(nameID);
            if (line == null)
            {
                return null;
            }
            return new LineHandle(ref line, this);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        private void InitializeStyles()
        {
            this.BackColor = Color.Black;
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public bool LineExists(int numID)
        {
            return (this.GetLine(numID) != null);
        }

        public bool LineExists(string nameID)
        {
            return (this.GetLine(nameID) != null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = this.m_bHighQuality ? SmoothingMode.HighQuality : SmoothingMode.Default;
            this.m_OffsetX = 0;
            if (this.m_bShowMinMax)
            {
                this.DrawLabels(ref g);
            }
            if (this.m_bShowGrid)
            {
                this.DrawGrid(ref g);
            }
            if (this.m_OffsetX != 0)
            {
                g.Clip = new Region(new Rectangle(this.m_OffsetX, 0, base.Width - this.m_OffsetX, base.Height));
            }
            this.DrawLines(ref g);
            g.ResetClip();
            g.SmoothingMode = smoothingMode;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.m_MaxCoords = -1;
            this.Refresh();
            base.OnSizeChanged(e);
        }

        public bool Push(int magnitude, int numID)
        {
            Line line = this.GetLine(numID);
            if (line == null)
            {
                return false;
            }
            return this.PushDirect(magnitude, line);
        }

        public bool Push(int magnitude, string nameID)
        {
            Line line = this.GetLine(nameID);
            if (line == null)
            {
                return false;
            }
            return this.PushDirect(magnitude, line);
        }

        private bool PushDirect(int magnitude, Line line)
        {
            if (!this.m_bAutoScale && (magnitude > this.m_MaxPeek))
            {
                magnitude = this.m_MaxPeek;
            }
            else if (this.m_bAutoScale && (magnitude > this.m_MaxPeek))
            {
                this.m_MaxPeek = magnitude;
                this.RefreshLabels();
            }
            else if (!this.m_bAutoScale && (magnitude < this.m_MinPeek))
            {
                magnitude = this.m_MinPeek;
            }
            else if (this.m_bAutoScale && (magnitude < this.m_MinPeek))
            {
                this.m_MinPeek = magnitude;
                this.RefreshLabels();
            }
            magnitude -= this.m_MinPeek;
            line.m_MagnitudeList.Add(magnitude);
            return true;
        }

        protected void RefreshLabels()
        {
            if (!this.m_bMinLabelSet)
            {
                this.m_MinLabel = this.m_MinPeek.ToString();
            }
            if (!this.m_bMaxLabelSet)
            {
                this.m_MaxLabel = this.m_MaxPeek.ToString();
            }
        }

        public bool RemoveLine(int numID)
        {
            Line item = this.GetLine(numID);
            if (item == null)
            {
                return false;
            }
            return this.m_Lines.Remove(item);
        }

        public bool RemoveLine(string nameID)
        {
            Line item = this.GetLine(nameID);
            if (item == null)
            {
                return false;
            }
            return this.m_Lines.Remove(item);
        }

        public void UpdateGraph()
        {
            int count = 0;
            foreach (Line line in this.m_Lines)
            {
                if (count < line.m_MagnitudeList.Count)
                {
                    count = line.m_MagnitudeList.Count;
                }
            }
            if (count >= this.m_MaxCoords)
            {
                this.m_MoveOffset = (this.m_MoveOffset - (((count - this.m_MaxCoords) + 1) * this.m_LineInterval)) % this.m_GridSize;
            }
            this.CullAndEqualizeMagnitudeCounts();
            this.Refresh();
        }

        public bool AutoAdjustPeek
        {
            get
            {
                return this.m_bAutoScale;
            }
            set
            {
                if (this.m_bAutoScale != value)
                {
                    this.m_bAutoScale = value;
                    this.Refresh();
                }
            }
        }

        public Color GridColor
        {
            get
            {
                return this.m_GridColor;
            }
            set
            {
                if (this.m_GridColor != value)
                {
                    this.m_GridColor = value;
                    this.Refresh();
                }
            }
        }

        public ushort GridSize
        {
            get
            {
                return (ushort) this.m_GridSize;
            }
            set
            {
                if (this.m_GridSize != value)
                {
                    this.m_GridSize = value;
                    this.Refresh();
                }
            }
        }

        public bool HighQuality
        {
            get
            {
                return this.m_bHighQuality;
            }
            set
            {
                if (value != this.m_bHighQuality)
                {
                    this.m_bHighQuality = value;
                    this.Refresh();
                }
            }
        }

        public ushort LineInterval
        {
            get
            {
                return (ushort) this.m_LineInterval;
            }
            set
            {
                if (((ushort) this.m_LineInterval) != value)
                {
                    this.m_LineInterval = value;
                    this.m_MaxCoords = -1;
                    this.Refresh();
                }
            }
        }

        public string MaxLabel
        {
            get
            {
                return this.m_MaxLabel;
            }
            set
            {
                this.m_bMaxLabelSet = true;
                if (string.Compare(this.m_MaxLabel, value) != 0)
                {
                    this.m_MaxLabel = value;
                    this.m_MaxCoords = -1;
                    this.Refresh();
                }
            }
        }

        public int MaxPeekMagnitude
        {
            get
            {
                return this.m_MaxPeek;
            }
            set
            {
                this.m_MaxPeek = value;
                this.RefreshLabels();
            }
        }

        public string MinLabel
        {
            get
            {
                return this.m_MinLabel;
            }
            set
            {
                this.m_bMinLabelSet = true;
                if (string.Compare(this.m_MinLabel, value) != 0)
                {
                    this.m_MinLabel = value;
                    this.m_MaxCoords = -1;
                    this.Refresh();
                }
            }
        }

        public int MinPeekMagnitude
        {
            get
            {
                return this.m_MinPeek;
            }
            set
            {
                this.m_MinPeek = value;
                this.RefreshLabels();
            }
        }

        public bool ShowGrid
        {
            get
            {
                return this.m_bShowGrid;
            }
            set
            {
                if (this.m_bShowGrid != value)
                {
                    this.m_bShowGrid = value;
                    this.Refresh();
                }
            }
        }

        public bool ShowLabels
        {
            get
            {
                return this.m_bShowMinMax;
            }
            set
            {
                if (this.m_bShowMinMax != value)
                {
                    this.m_bShowMinMax = value;
                    this.m_MaxCoords = -1;
                    this.Refresh();
                }
            }
        }

        public Color TextColor
        {
            get
            {
                return this.m_TextColor;
            }
            set
            {
                if (this.m_TextColor != value)
                {
                    this.m_TextColor = value;
                    this.Refresh();
                }
            }
        }

        private class Line
        {
            public bool m_bShowAsBar;
            public bool m_bVisible;
            public Color m_Color;
            public List<int> m_MagnitudeList;
            public string m_NameID;
            public int m_NumID;
            public uint m_Thickness;

            public Line(int num)
            {
                this.m_MagnitudeList = new List<int>();
                this.m_Color = Color.Green;
                this.m_NameID = "";
                this.m_NumID = -1;
                this.m_Thickness = 1;
                this.m_bVisible = true;
                this.m_NumID = num;
            }

            public Line(string name)
            {
                this.m_MagnitudeList = new List<int>();
                this.m_Color = Color.Green;
                this.m_NameID = "";
                this.m_NumID = -1;
                this.m_Thickness = 1;
                this.m_bVisible = true;
                this.m_NameID = name;
            }
        }

        public class LineHandle
        {
            private C2DPushGraph.Line m_Line;
            private C2DPushGraph m_Owner;

            public LineHandle(ref object line, C2DPushGraph owner)
            {
                if (string.Compare(line.GetType().Name, "Line") != 0)
                {
                    throw new ArithmeticException("LineHandle: First Parameter must be type of 'Line' cast to base 'Object'");
                }
                this.m_Line = (C2DPushGraph.Line) line;
                this.m_Owner = owner;
            }

            public void Clear()
            {
                this.m_Line.m_MagnitudeList.Clear();
                this.m_Owner.UpdateGraph();
            }

            public System.Drawing.Color Color
            {
                get
                {
                    return this.m_Line.m_Color;
                }
                set
                {
                    if (this.m_Line.m_Color != value)
                    {
                        this.m_Line.m_Color = value;
                        this.m_Owner.Refresh();
                    }
                }
            }

            public bool ShowAsBar
            {
                get
                {
                    return this.m_Line.m_bShowAsBar;
                }
                set
                {
                    if (this.m_Line.m_bShowAsBar != value)
                    {
                        this.m_Line.m_bShowAsBar = value;
                        this.m_Owner.Refresh();
                    }
                }
            }

            public uint Thickness
            {
                get
                {
                    return this.m_Line.m_Thickness;
                }
                set
                {
                    if (this.m_Line.m_Thickness != value)
                    {
                        this.m_Line.m_Thickness = value;
                        this.m_Owner.Refresh();
                    }
                }
            }

            public bool Visible
            {
                get
                {
                    return this.m_Line.m_bVisible;
                }
                set
                {
                    if (this.m_Line.m_bVisible != value)
                    {
                        this.m_Line.m_bVisible = value;
                        this.m_Owner.Refresh();
                    }
                }
            }
        }
    }
}

