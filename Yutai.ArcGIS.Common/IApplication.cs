using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Common
{
    public interface IApplication
    {
        void AcvtiveHookChanged(object object_0);
        void AddAfterDrawCallBack(AfterDraw afterDraw_0);
        void AddCommands(ICommand icommand_0);
        void Close();
        void DockWindows(object object_0, Bitmap bitmap_0);
        void HideDockWindow(object object_0);
        void HideToolTip();
        void LayerDeleted(ILayer ilayer_0);
        void MapClipChanged(object object_0);
        void MapDocumentChanged();
        void MapDocumentSave(string string_0);
        void ResetCurrentTool();
        void SetStatus(string string_0);
        void SetStatus(int int_0, string string_0);
        void SetToolTip(string string_0);
        bool ShowCommandString(string string_0, CommandTipsType commandTipsType_0);
        void UpdateUI();

        Control ActiveControl { get; set; }

        AxMapControl ActiveMapView { get; set; }

        IActiveView ActiveView { get; }

        Form AppMainForm { get; set; }

        IGeometry BufferGeometry { get; set; }

        bool CanEdited { get; set; }

        object ContainerHook { get; set; }

        ILayer CurrentLayer { get; set; }

        ITool CurrentTool { get; set; }

        bool DrawBuffer { get; set; }

        IEngineSnapEnvironment EngineSnapEnvironment { get; }

        IMap FocusMap { get; }

        object Hook { get; set; }

        bool IsClose { get; set; }

        bool IsInEdit { get; set; }

        bool IsSnapBoundary { get; set; }

        bool IsSnapEndPoint { get; set; }

        bool IsSnapIntersectionPoint { get; set; }

        bool IsSnapMiddlePoint { get; set; }

        bool IsSnapPoint { get; set; }

        bool IsSnapSketch { get; set; }

        bool IsSnapVertexPoint { get; set; }

        bool IsSupportZD { get; set; }

        object MainHook { get; set; }

        List<object> MapCommands { get; set; }

        string MapDocName { get; set; }

        IMapDocument MapDocument { get; set; }

        AxMapControl NavitorMapView { get; set; }

        IOperationStack OperationStack { get; }

        IPageLayout PageLayout { get; }

        string PaintStyleName { get; set; }

        PyramidPromptType PyramidPromptType { get; set; }

        object SecondaryHook { get; set; }

        IWorkspace SelectedWorkspace { get; set; }

        ISelectionEnvironment SelectionEnvironment { get; }

        ISnapEnvironment SnapEnvironment { get; }

        double SnapTolerance { get; set; }

        double Tolerance { get; set; }

        bool UpdateClickTool { get; set; }

        bool UseSnap { get; set; }
    }
}

