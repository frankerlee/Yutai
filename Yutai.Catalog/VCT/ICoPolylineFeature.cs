﻿namespace Yutai.Catalog.VCT
{
    using System.Collections.Generic;

    public interface ICoPolylineFeature
    {
        List<CoPointCollection> Points { get; }
    }
}
