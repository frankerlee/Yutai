namespace Yutai.Catalog.VCT
{
    using System;

    public interface ICoConvert : IDisposable
    {
        void Close();
        void ConvertFlush();
        void ConvertFlush(CoLayerMapper coLayerMapper_0);
        void Flush();
        void Flush(CoLayerMapper coLayerMapper_0);
        int NextFeature();
        void Reset();

        ICoConvert DestConvert { set; }

        int FeatureCount { get; }

        ICoLayer XpgisLayer { get; }
    }
}

