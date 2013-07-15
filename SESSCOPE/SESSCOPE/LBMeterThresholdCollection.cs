namespace LBSoft.IndustrialCtrls.Meters
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class LBMeterThresholdCollection : CollectionBase
    {
        private bool _IsReadOnly;

        public virtual void Add(LBMeterThreshold sector)
        {
            base.InnerList.Add(sector);
        }

        public bool Contains(LBMeterThreshold sector)
        {
            foreach (LBMeterThreshold threshold in base.InnerList)
            {
                if ((threshold.StartValue == sector.StartValue) && (threshold.EndValue == sector.EndValue))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void CopyTo(LBMeterThreshold[] MeterThresholdArray, int index)
        {
            throw new Exception("This Method is not valid for this implementation.");
        }

        public virtual bool Remove(LBMeterThreshold sector)
        {
            for (int i = 0; i < base.InnerList.Count; i++)
            {
                LBMeterThreshold threshold = (LBMeterThreshold) base.InnerList[i];
                if ((threshold.StartValue == sector.StartValue) && (threshold.EndValue == sector.EndValue))
                {
                    base.InnerList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return this._IsReadOnly;
            }
        }

        public virtual LBMeterThreshold this[int index]
        {
            get
            {
                return (LBMeterThreshold) base.InnerList[index];
            }
            set
            {
                base.InnerList[index] = value;
            }
        }
    }
}

