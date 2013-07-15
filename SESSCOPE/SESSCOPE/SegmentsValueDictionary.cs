namespace LBSoft.IndustrialCtrls.Leds
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class SegmentsValueDictionary : DictionaryBase
    {
        public void Add(int num, SegmentsList seg)
        {
            if (!this.Contains(num))
            {
                base.Dictionary.Add(num, seg);
            }
            else
            {
                this[num] = seg;
            }
        }

        public bool Contains(int ch)
        {
            return base.Dictionary.Contains(ch);
        }

        public SegmentsList this[int num]
        {
            get
            {
                if (!base.Dictionary.Contains(num))
                {
                    return null;
                }
                return (SegmentsList) base.Dictionary[num];
            }
            set
            {
                if (!base.Dictionary.Contains(num))
                {
                    this.Add(num, value);
                }
                else
                {
                    base.Dictionary[num] = value;
                }
            }
        }

        public ICollection Keys
        {
            get
            {
                return base.Dictionary.Keys;
            }
        }

        public ICollection Values
        {
            get
            {
                return base.Dictionary.Values;
            }
        }
    }
}

