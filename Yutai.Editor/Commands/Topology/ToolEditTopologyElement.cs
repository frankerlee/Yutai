using System;
using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class ToolEditTopologyElement : YutaiTool
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

        private IEnvelope ienvelope_0 = null;

        private INewEnvelopeFeedback inewEnvelopeFeedback_0 = null;

        private IPoint ipoint_0 = null;

        private bool bool_0 = false;

        private ILineSymbol ilineSymbol_0;

        private IMarkerSymbol imarkerSymbol_0;

        private Map map_0;

        private bool bool_1;

        public override bool Enabled
        {
            get
            {
                bool result;
                if (this._context.FocusMap == null)
                {
                    result = false;
                }
                else if (this._context.FocusMap.LayerCount == 0)
                {
                    CmdSelectTopology.m_TopologyGraph = null;
                    result = false;
                }
                else if (this._context.Hook is IApplication && !(this._context.Hook as IApplication).CanEdited)
                {
                    result = false;
                }
                else
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                    {
                        if (CmdSelectTopology.m_TopologyGraph != null)
                        {
                            result = true;
                            return result;
                        }
                    }
                    else
                    {
                        CmdSelectTopology.m_TopologyGraph = null;
                    }
                    result = false;
                }
                return result;
            }
        }

        public ToolEditTopologyElement(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            IRgbColor rgbColorClass = new RgbColor()
            {
                Red = 255,
                Green = 0,
                Blue = 0
            };
            IRgbColor rgbColor = rgbColorClass;
            this.imarkerSymbol_0 = new SimpleMarkerSymbol();
            ((ISimpleMarkerSymbol)this.imarkerSymbol_0).Style = esriSimpleMarkerStyle.esriSMSCircle;
            this.imarkerSymbol_0.Color = rgbColor;
            this.imarkerSymbol_0.Size = 3;
            IRgbColor rgbColorClass1 = new RgbColor()
            {
                Red = 255,
                Green = 0,
                Blue = 0
            };
            IRgbColor rgbColor1 = rgbColorClass1;
            this.ilineSymbol_0 = new SimpleLineSymbol()
            {
                Color = rgbColor1,
                Width = 1.5
            };
            this.bool_1 = false;
            this.m_message = "编辑拓扑元素";
            this.m_caption = "拓扑编辑工具";
            this.m_toolTip = "拓扑编辑工具";
            this.m_name = "Editor_Topology_EditTopologyElement";
            this._key = "Editor_Topology_EditTopologyElement";
            _itemType= RibbonItemType.Tool;
            this.m_category = "拓扑";
            this.m_bitmap = Properties.Resources.TopologyEdit;
            this.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.TopologyEdit.cur"));

            if (this._context.FocusMap != null)
            {
                if (this._context.Hook is IApplicationEvents)
                {
                    (this._context.Hook as IApplicationEvents).OnActiveHookChanged += new OnActiveHookChangedHandler(this.method_0);
                }
                this.map_0 = (Map)this._context.FocusMap;
                this.map_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_3);
            }
        }

        private void method_0(object object_0)
        {
            if (this.map_0 != null)
            {
                try
                {
                    this.map_0.AfterDraw -= new IActiveViewEvents_AfterDrawEventHandler(this.method_3);
                }
                catch
                {
                }
            }
            this.map_0 = (Map)this._context.FocusMap;
            try
            {
                this.map_0.AfterDraw += new IActiveViewEvents_AfterDrawEventHandler(this.method_3);
            }
            catch
            {
            }
        }

        private void method_1(IDisplay idisplay_0)
        {
            if (CmdSelectTopology.m_TopologyGraph != null)
            {
                IEnumTopologyEdge edgeSelection = CmdSelectTopology.m_TopologyGraph.EdgeSelection;
                edgeSelection.Reset();
                ITopologyEdge topologyEdge = edgeSelection.Next();
                if (topologyEdge != null)
                {
                    IPolyline polylineClass = new Polyline() as IPolyline;
                    idisplay_0.SetSymbol((ISymbol)this.ilineSymbol_0);
                    while (topologyEdge != null)
                    {
                        idisplay_0.DrawPolyline(topologyEdge.Geometry);
                        topologyEdge = edgeSelection.Next();
                    }
                }
                IEnumTopologyNode nodeSelection = CmdSelectTopology.m_TopologyGraph.NodeSelection;
                nodeSelection.Reset();
                ITopologyNode topologyNode = nodeSelection.Next();
                if (topologyNode != null)
                {
                    IMultipoint multipointClass = new Multipoint() as IMultipoint;
                    idisplay_0.SetSymbol((ISymbol)this.imarkerSymbol_0);
                    while (topologyNode != null)
                    {
                        idisplay_0.DrawPoint(topologyNode.Geometry);
                        topologyNode = nodeSelection.Next();
                    }
                }
            }
        }

        private void method_2()
        {
            object ilineSymbol0;
            if (CmdSelectTopology.m_TopologyGraph != null && this._context.Hook is IMapControl2)
            {
                IMapControl2 hook = (IMapControl2)this._context.Hook;
                IEnumTopologyEdge edgeSelection = CmdSelectTopology.m_TopologyGraph.EdgeSelection;
                edgeSelection.Reset();
                ITopologyEdge topologyEdge = edgeSelection.Next();
                if (topologyEdge != null)
                {
                    ilineSymbol0 = this.ilineSymbol_0;
                    while (topologyEdge != null)
                    {
                        hook.DrawShape(topologyEdge.Geometry, ref ilineSymbol0);
                        topologyEdge = edgeSelection.Next();
                    }
                }
                IEnumTopologyNode nodeSelection = CmdSelectTopology.m_TopologyGraph.NodeSelection;
                nodeSelection.Reset();
                ITopologyNode topologyNode = nodeSelection.Next();
                if (topologyNode != null)
                {
                    ilineSymbol0 = this.imarkerSymbol_0;
                    while (topologyNode != null)
                    {
                        hook.DrawShape(topologyNode.Geometry, ref ilineSymbol0);
                        topologyNode = nodeSelection.Next();
                    }
                }
            }
        }

        private void method_3(IDisplay idisplay_0, esriViewDrawPhase esriViewDrawPhase_0)
        {
            if ((CmdSelectTopology.m_TopologyGraph == null ? false : this.Enabled))
            {
                IEnumTopologyEdge edgeSelection = CmdSelectTopology.m_TopologyGraph.EdgeSelection;
                edgeSelection.Reset();
                ITopologyEdge topologyEdge = edgeSelection.Next();
                object value = Missing.Value;
                if (topologyEdge != null)
                {
                    IGeometryCollection polylineClass = new Polyline() as IGeometryCollection;
                    while (topologyEdge != null)
                    {
                        polylineClass.AddGeometryCollection((IGeometryCollection)topologyEdge.Geometry);
                        topologyEdge = edgeSelection.Next();
                    }
                    idisplay_0.SetSymbol((ISymbol)this.ilineSymbol_0);
                    idisplay_0.DrawPolyline((IGeometry)polylineClass);
                }
                IEnumTopologyNode nodeSelection = CmdSelectTopology.m_TopologyGraph.NodeSelection;
                nodeSelection.Reset();
                ITopologyNode topologyNode = nodeSelection.Next();
                if (topologyNode != null)
                {
                    IPointCollection multipointClass = new Multipoint();
                    while (topologyNode != null)
                    {
                        multipointClass.AddPoint((IPoint)topologyNode.Geometry, ref value, ref value);
                        topologyNode = nodeSelection.Next();
                    }
                    idisplay_0.SetSymbol((ISymbol)this.imarkerSymbol_0);
                    idisplay_0.DrawMultipoint((IGeometry)multipointClass);
                }
            }
        }

     

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (CmdSelectTopology.m_TopologyGraph != null)
            {
                if (this.ienvelope_0 != null)
                {
                    IEnvelope extent = this._context.ActiveView.Extent;
                    if ((this.ienvelope_0.XMax != extent.XMax || this.ienvelope_0.YMax != extent.YMax || this.ienvelope_0.XMin != extent.XMin ? true : this.ienvelope_0.YMin != extent.YMin))
                    {
                        this.ienvelope_0 = this._context.ActiveView.Extent;
                        CmdSelectTopology.m_TopologyGraph.Build(this.ienvelope_0, true);
                    }
                }
                else
                {
                    this.ienvelope_0 = this._context.ActiveView.Extent;
                    CmdSelectTopology.m_TopologyGraph.Build(this.ienvelope_0, true);
                }
                this.ipoint_0 = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.bool_1)
                {
                    this.idisplayFeedback_0 = CmdSelectTopology.m_TopologyGraph.GetSelectionFeedback(3, this.ipoint_0, true);
                    this.idisplayFeedback_0.Display = this._context.ActiveView.ScreenDisplay;
                }
                this.bool_0 = true;
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            IEnumTGHitInfo enumTGHitInfo;
            ITopologyElement topologyElement;
            IPoint point;
            if (CmdSelectTopology.m_TopologyGraph != null)
            {
                IPoint mapPoint = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                if (this.idisplayFeedback_0 != null)
                {
                    this.idisplayFeedback_0.MoveTo(mapPoint);
                }
                else if (!this.bool_0)
                {
                    double searchTolerance = 3.5;
                    if (this._context.Hook is IApplication)
                    {
                        searchTolerance = (double)(this._context.Hook as IApplication).SelectionEnvironment.SearchTolerance;
                    }
                    searchTolerance = Common.ConvertPixelsToMapUnits((IActiveView)this._context.FocusMap, searchTolerance);
                    CmdSelectTopology.m_TopologyGraph.EnumHitTest(3, mapPoint, searchTolerance, out enumTGHitInfo);
                    enumTGHitInfo.Reset();
                    enumTGHitInfo.Next(out topologyElement, out point, ref searchTolerance);
                    while (topologyElement != null)
                    {
                        if (topologyElement.IsSelected)
                        {
                            this.bool_1 = true;
                            return;
                        }
                        else
                        {
                            enumTGHitInfo.Next(out topologyElement, out point, ref searchTolerance);
                        }
                    }
                    this.bool_1 = false;
                }
                else
                {
                    if (this.inewEnvelopeFeedback_0 == null)
                    {
                        this.inewEnvelopeFeedback_0 = new NewEnvelopeFeedback()
                        {
                            Display = this._context.ActiveView.ScreenDisplay
                        };
                        this.inewEnvelopeFeedback_0.Start(this.ipoint_0);
                    }
                    this.inewEnvelopeFeedback_0.MoveTo(mapPoint);
                }
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            IEnvelope envelope;
            IGeometry geometry;
            if (CmdSelectTopology.m_TopologyGraph != null)
            {
                this.bool_0 = false;
                if (this.idisplayFeedback_0 != null)
                {
                    IPoint mapPoint = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                    IWorkspaceEdit editWorkspace = Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace;
                    editWorkspace.StartEditOperation();
                    try
                    {
                        IAffineTransformation2D affineTransformation2DClass = new AffineTransformation2D() as IAffineTransformation2D;
                        affineTransformation2DClass.Move(mapPoint.X - this.ipoint_0.X, mapPoint.Y - this.ipoint_0.Y);
                        CmdSelectTopology.m_TopologyGraph.TransformSelection(esriTransformDirection.esriTransformForward, affineTransformation2DClass, false);
                        CmdSelectTopology.m_TopologyGraph.Post(out envelope);
                        this._context.ActiveView.Refresh();
                    }
                    catch (Exception exception)
                    {
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                    editWorkspace.StopEditOperation();
                    this.idisplayFeedback_0 = null;
                }
                else if (this.inewEnvelopeFeedback_0 != null)
                {
                    geometry = this.inewEnvelopeFeedback_0.Stop();
                    CmdSelectTopology.m_TopologyGraph.SelectByGeometry(3, esriTopologySelectionResultEnum.esriTopologySelectionResultNew, geometry);
                    this.inewEnvelopeFeedback_0 = null;
                }
                else
                {
                    double searchTolerance = 3.5;
                    if (this._context.Hook is IApplication)
                    {
                        searchTolerance = (double)(this._context.Hook as IApplication).SelectionEnvironment.SearchTolerance;
                    }
                    searchTolerance = Common.ConvertPixelsToMapUnits((IActiveView)this._context.FocusMap, searchTolerance);
                    geometry = ((ITopologicalOperator)this.ipoint_0).Buffer(searchTolerance);
                    CmdSelectTopology.m_TopologyGraph.SelectByGeometry(1, esriTopologySelectionResultEnum.esriTopologySelectionResultNew, geometry);
                    if (CmdSelectTopology.m_TopologyGraph.NodeSelection.Count == 0)
                    {
                        CmdSelectTopology.m_TopologyGraph.SelectByGeometry(2, esriTopologySelectionResultEnum.esriTopologySelectionResultNew, geometry);
                    }
                }
                this._context.ActiveView.Refresh();
            }
        }
    }
}