namespace LBSoft.IndustrialCtrls.Base
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class LBRendererBase : ILBRenderer, IDisposable
    {
        protected object _control;

        public void Dispose()
        {
            this.OnDispose();
        }

        public virtual void Draw(Graphics Gr)
        {
            if (Gr == null)
            {
                throw new ArgumentNullException("Gr");
            }
            System.Windows.Forms.Control control = this.Control as System.Windows.Forms.Control;
            if (control == null)
            {
                throw new NullReferenceException("Associated control is not valid");
            }
            Rectangle bounds = control.Bounds;
            Gr.FillRectangle(Brushes.White, control.Bounds);
            Gr.DrawRectangle(Pens.Black, control.Bounds);
            Gr.DrawLine(Pens.Red, control.Left, control.Top, control.Right, control.Bottom);
            Gr.DrawLine(Pens.Red, control.Right, control.Top, control.Left, control.Bottom);
        }

        public virtual void OnDispose()
        {
        }

        public virtual bool Update()
        {
            return false;
        }

        public object Control
        {
            get
            {
                return this._control;
            }
            set
            {
                this._control = value;
            }
        }
    }
}

