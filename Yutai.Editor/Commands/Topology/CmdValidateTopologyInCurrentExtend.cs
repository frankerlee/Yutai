using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class CmdValidateTopologyInCurrentExtend : YutaiCommand
    {
        public override bool Enabled
        {
            get
            {
                bool flag;
                if (this._context.FocusMap == null)
                {
                    flag = false;
                }
                else if (this._context.FocusMap.LayerCount == 0)
                {
                    CmdSelectTopology.m_TopologyGraph = null;
                    flag = false;
                }
                else if (!(this._context.Hook is IApplication) || (this._context.Hook as IApplication).CanEdited)
                {
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    {
                        CmdSelectTopology.m_TopologyGraph = null;
                    }
                    else
                    {
                        if (CmdSelectTopology.m_TopologyGraph == null)
                        {
                            goto Label1;
                        }
                        flag = true;
                        return flag;
                    }
                    Label1:
                    flag = false;
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }
        public CmdValidateTopologyInCurrentExtend(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

       
        public override void OnCreate(object object_0)
        {
            _context = object_0 as IAppContext;
            this.m_caption = "在当前视图范围内校验拓扑";
            this.m_category = "拓扑";
            this.m_name = "Editor_Topology_ValidateTopologyInCurrentExtend";
            _key = "Editor_Topology_ValidateTopologyInCurrentExtend";
            _itemType= RibbonItemType.Button;
            this.m_message = "在当前视图范围内校验拓扑";
            this.m_toolTip = "在当前视图范围内校验拓扑";
            this.m_bitmap = Properties.Resources.ValidateTopologyInCurrentExtendCommand;
        }

        public override void OnClick()
        {
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            try
            {
                ITopology mTopology = CmdSelectTopology.m_Topology;
                ISegmentCollection polygonClass = new Polygon() as ISegmentCollection;
                polygonClass.SetRectangle((this._context.FocusMap as IActiveView).Extent);
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