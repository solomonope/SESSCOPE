namespace LBSoft.IndustrialCtrls.Base
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class LBIndustrialCtrlBase : UserControl
    {
        private ILBRenderer _defaultRenderer;
        private ILBRenderer _renderer;
        private IContainer components;

        public LBIndustrialCtrlBase()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            this.BackColor = Color.Transparent;
            this._defaultRenderer = this.CreateDefaultRenderer();
            if (this._defaultRenderer != null)
            {
                this._defaultRenderer.Control = this;
            }
        }

        protected virtual void CalculateDimensions()
        {
            this.DefaultRenderer.Update();
            if (this.Renderer != null)
            {
                this.Renderer.Update();
            }
            base.Invalidate();
        }

        protected virtual ILBRenderer CreateDefaultRenderer()
        {
            return new LBRendererBase();
        }

        protected override void Dispose(bool disposing)
        {
            this.DefaultRenderer.Dispose();
            if (this.Renderer != null)
            {
                this.Renderer.Dispose();
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
        }

        [EditorBrowsable]
        protected override void OnFontChanged(EventArgs e)
        {
            this.CalculateDimensions();
        }

        [EditorBrowsable]
        protected override void OnPaint(PaintEventArgs e)
        {
            new RectangleF(0f, 0f, (float) base.Width, (float) base.Height);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.Renderer == null)
            {
                this.DefaultRenderer.Draw(e.Graphics);
            }
            else
            {
                this.Renderer.Draw(e.Graphics);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.CalculateDimensions();
            base.Invalidate();
        }

        [EditorBrowsable]
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.CalculateDimensions();
            base.Invalidate();
        }

        [Browsable(false)]
        public ILBRenderer DefaultRenderer
        {
            get
            {
                return this._defaultRenderer;
            }
        }

        [Browsable(false)]
        public ILBRenderer Renderer
        {
            get
            {
                return this._renderer;
            }
            set
            {
                this._renderer = value;
                if (this._renderer != null)
                {
                    this._renderer.Control = this;
                    this._renderer.Update();
                }
                base.Invalidate();
            }
        }
    }
}

