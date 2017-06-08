namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using System;

    internal interface IProjectForm
    {
        IFeatureDataConverter FeatureProgress { set; }
    }
}

