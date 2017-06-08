namespace JLK.Catalog.VCT
{
    using System;

    public abstract class AbsCoConvert : IDisposable, ICoConvert
    {
        private ICoConvert icoConvert_0 = null;
        private ICoLayer icoLayer_0 = new CoLayerClass();

        protected AbsCoConvert()
        {
        }

        public abstract void Close();
        public void ConvertFlush()
        {
            this.ConvertFlush(null);
        }

        public void ConvertFlush(CoLayerMapper coLayerMapper_0)
        {
            if (this.icoConvert_0 == null)
            {
                throw new Exception("mDestConvert 为空，未将对象引用到实例。");
            }
            ICoLayer xpgisLayer = this.icoConvert_0.XpgisLayer;
            for (int i = 0; i < this.icoLayer_0.FeatureCount; i++)
            {
                xpgisLayer.AppendFeature(this.icoLayer_0.GetFeatureByIndex(i));
            }
            if (coLayerMapper_0 == null)
            {
                this.icoConvert_0.Flush();
            }
            else
            {
                this.icoConvert_0.Flush(coLayerMapper_0);
            }
            this.icoConvert_0.XpgisLayer.RemoveAllFeature();
            this.icoLayer_0.RemoveAllFeature();
        }

        public virtual void Dispose()
        {
            if (this.icoLayer_0 != null)
            {
                this.icoLayer_0.RemoveAllFeature();
                this.icoLayer_0 = null;
            }
            if (this.icoConvert_0 != null)
            {
                this.icoConvert_0.Dispose();
                this.icoConvert_0 = null;
            }
            this.Close();
        }

        public abstract void Flush();
        public abstract void Flush(CoLayerMapper coLayerMapper_0);
        public abstract int NextFeature();
        public abstract void Reset();
        protected abstract void UpdateLayerStruct();

        public ICoConvert DestConvert
        {
            set
            {
                this.icoConvert_0 = value;
            }
        }

        public abstract int FeatureCount { get; }

        public ICoLayer XpgisLayer
        {
            get
            {
                return this.icoLayer_0;
            }
            set
            {
                this.icoLayer_0 = value;
            }
        }
    }
}

