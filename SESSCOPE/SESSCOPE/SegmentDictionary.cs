namespace LBSoft.IndustrialCtrls.Leds
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class SegmentDictionary : DictionaryBase
    {
        public void Add(char ch, Segment seg)
        {
            if (!this.Contains(ch))
            {
                base.Dictionary.Add(ch, seg);
            }
            else
            {
                this[ch] = seg;
            }
        }

        public bool Contains(char ch)
        {
            return base.Dictionary.Contains(ch);
        }

        public Segment this[char ch]
        {
            get
            {
                if (!base.Dictionary.Contains(ch))
                {
                    return null;
                }
                return (Segment) base.Dictionary[ch];
            }
            set
            {
                if (!base.Dictionary.Contains(ch))
                {
                    this.Add(ch, value);
                }
                else
                {
                    base.Dictionary[ch] = value;
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

