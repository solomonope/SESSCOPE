namespace LBSoft.IndustrialCtrls.Base
{
    using System;
    using System.Drawing;

    public interface ILBRenderer : IDisposable
    {
        void Draw(Graphics Gr);
        bool Update();

        object Control { get; set; }
    }
}

