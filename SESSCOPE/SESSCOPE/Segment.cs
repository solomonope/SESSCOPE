namespace LBSoft.IndustrialCtrls.Leds
{
    using System;

    public class Segment
    {
        private int[] points = new int[6];

        public int[] PointsIndexs
        {
            get
            {
                return this.points;
            }
        }
    }
}

