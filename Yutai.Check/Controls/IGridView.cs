using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using ESRI.ArcGIS.Carto;

namespace Yutai.Check.Controls
{
    interface IGridView
    {
        IFeatureLayer FeatureLayer { set; }
        IMap Map { set; }
        DockStyle Dock { get; set; }
        GridControl Grid { get; }
        void ExportToXls(string exportFilePath);
        void ExportToXlsx(string exportFilePath);
        void ExportToRtf(string exportFilePath);
        void ExportToPdf(string exportFilePath);
        void ExportToHtml(string exportFilePath);
        void ExportToMht(string exportFilePath);
    }
}
