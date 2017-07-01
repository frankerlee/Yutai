using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;

namespace Yutai.Plugins.Identifer
{
    public class QuerySettings
    {
        public QuerySettings()
        {
            SelectionEnvironment = new SelectionEnvironmentClass();
        }

        private double QueryTorelance { get; set; }
        public ISelectionEnvironment SelectionEnvironment { get; set; }
        public IFeatureLayer CurrentLayer { get; set; }
    }
}