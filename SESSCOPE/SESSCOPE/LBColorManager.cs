namespace LBSoft.IndustrialCtrls.Utils
{
    using System;
    using System.Drawing;

    public class LBColorManager
    {
        public static double BlendColour(double fg, double bg, double alpha)
        {
            double num = bg + (alpha * (fg - bg));
            if (num < 0.0)
            {
                num = 0.0;
            }
            if (num > 255.0)
            {
                num = 255.0;
            }
            return num;
        }

        public static Color StepColor(Color clr, int alpha)
        {
            if (alpha == 100)
            {
                return clr;
            }
            byte a = clr.A;
            byte r = clr.R;
            byte g = clr.G;
            byte b = clr.B;
            float num5 = 0f;
            int num6 = Math.Min(alpha, 200);
            double num7 = (Math.Max(alpha, 0) - 100.0) / 100.0;
            if (num7 > 100.0)
            {
                num5 = 255f;
                num7 = 1.0 - num7;
            }
            else
            {
                num5 = 0f;
                num7 = 1.0 + num7;
            }
            r = (byte) BlendColour((double) r, (double) num5, num7);
            g = (byte) BlendColour((double) g, (double) num5, num7);
            b = (byte) BlendColour((double) b, (double) num5, num7);
            return Color.FromArgb(a, r, g, b);
        }
    }
}

