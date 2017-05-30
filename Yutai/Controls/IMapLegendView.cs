using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Yutai.Controls
{
    public interface IMapLegendView
    {
        ITOCControl2 TocControl { get; }
        ITOCBuddy2 TocBuddyControl { get; }

        IBasicMap SelectedMap { get; }

        ILayer SelectedLayer
        {
            get;

        }

        esriTOCControlItem SelectedItemType { get; }
    }
}
