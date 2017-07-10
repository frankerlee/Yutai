using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class ToolValidateTopologyInExtend: YutaiTool
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

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
        public ToolValidateTopologyInExtend(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            this.m_caption = "在指定范围内校验拓扑";
            this.m_category = "拓扑";
            this.m_name = "Editor_Topology_ValidateTopologyInExtend";
            _key = "Editor_Topology_ValidateTopologyInExtend";
            _itemType= RibbonItemType.Tool;
            this.m_message = "在指定范围内校验拓扑";
            this.m_toolTip = "在指定范围内校验拓扑";
            this.m_bitmap = Properties.Resources.ValidateTopologyInExtendCommand;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.ValidateTopologyInExtendCommand.cur"));
        }

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap = this._context.FocusMap as IActiveView;
            this.idisplayFeedback_0 = new NewEnvelopeFeedback()
            {
                Display = focusMap.ScreenDisplay
            };
            (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3));
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.idisplayFeedback_0 != null)
            {
                IActiveView focusMap = this._context.FocusMap as IActiveView;
                this.idisplayFeedback_0.MoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3));
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.idisplayFeedback_0 != null)
            {
                IEnvelope spatialReference = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                if (!spatialReference.IsEmpty)
                {
                    if ((spatialReference.Width == 0 ? false : spatialReference.Height != 0))
                    {
                        spatialReference.SpatialReference = this._context.FocusMap.SpatialReference;
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
                        try
                        {
                            ITopology mTopology = CmdSelectTopology.m_Topology;
                            ISegmentCollection polygonClass = new Polygon() as ISegmentCollection;
                            polygonClass.SetRectangle(spatialReference);
                            IPolygon dirtyArea = mTopology.DirtyArea[polygonClass as IPolygon];
                            mTopology.ValidateTopology(dirtyArea.Envelope);
                            this._context.ActiveView.Refresh();
                        }
                        catch (COMException cOMException)
                        {
                            CErrorLog.writeErrorLog(this, cOMException, "");
                        }
                        catch (Exception exception)
                        {
                            CErrorLog.writeErrorLog(this, exception, "");
                        }
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StopEditOperation();
                    }
                }
            }
        }
    }
}