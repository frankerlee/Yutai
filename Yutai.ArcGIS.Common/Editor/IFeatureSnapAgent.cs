using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
    public interface IFeatureSnapAgent : IEngineSnapAgent
    {
        IFeatureClass FeatureClass
        {
            get;
            set;
        }

        IFeatureCache FeatureCache
        {
            get;
            set;
        }

        int GeometryHitType
        {
            get;
            set;
        }
    }
}
