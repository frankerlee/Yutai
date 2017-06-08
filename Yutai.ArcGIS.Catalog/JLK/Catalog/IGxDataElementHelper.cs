namespace JLK.Catalog
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

    internal interface IGxDataElementHelper
    {
        void RetrieveDEBaseProperties(IDataElement idataElement_0);
        void RetrieveDEFullProperties(IDataElement idataElement_0);
    }
}

