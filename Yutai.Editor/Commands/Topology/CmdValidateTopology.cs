using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class CmdValidateTopology : YutaiCommand
    {
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

        public CmdValidateTopology(IAppContext context)
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
            this.m_caption = "完整拓扑校验";
            this.m_category = "拓扑";
            this.m_name = "Editor_Topology_ValidateTopology";
            _key = "Editor_Topology_ValidateTopology";
            _itemType= RibbonItemType.Button;
            this.m_message = "完整拓扑校验";
            this.m_toolTip = "完整拓扑校验";
            this.m_bitmap = Properties.Resources.ValidateEntireTopology;
        }

        public override void OnClick()
        {
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.StartEditOperation();
            try
            {
                ITopology mTopology = CmdSelectTopology.m_Topology;
                ISegmentCollection polygonClass = new Polygon() as ISegmentCollection;
                polygonClass.SetRectangle((mTopology as IGeoDataset).Extent);
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