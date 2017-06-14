using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    public interface IYTHookHelper : IHookHelper
    {
        ILayer CurrentLayer { get; set; }
        double Tolerance { get; set; }
        IEngineSnapEnvironment SnapEnvironment { get; }
        IStyleGallery StyleGallery { get; }
        string MapDocName { get; set; }
        double SnapTolerance { get; set; }
        IMapDocument MapDocument { get; set; }
        YutaiTool CurrentTool { get; set; }
        void ResetCurrentTool();
        void UpdateUI();
        void SetStatus(string string_0);
        void SetStatus(int int_0, string string_0);
        void DockWindows(object object_0, Bitmap bitmap_0);
        void HideDockWindow(object object_0);
        bool ShowCommandString(string string_0, CommandTipsType commandTipsType_0);
        void MapDocumentChanged();
    }





}
