using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.ArcGIS.Common.Framework;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using IToolContextMenu = Yutai.ArcGIS.Common.Framework.IToolContextMenu;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    public class FixTopologyErrorTool : YutaiTool, IToolContextMenu
    {
        private IDisplayFeedback idisplayFeedback_0 = null;

        private IPoint ipoint_0 = null;

        private bool bool_0 = false;

        internal static TopologyErrorSelections m_pTopoErroeSelection;

        private IPopuMenuWrap ipopuMenuWrap_0 = null;

        public object ContextMenu
        {
            get
            {
                return null;
            }
        }

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
                    FixTopologyErrorTool.m_pTopoErroeSelection.Clear();
                    result = false;
                }
                else if (this._context.Hook is IApplication && Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != this._context.FocusMap)
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
                        FixTopologyErrorTool.m_pTopoErroeSelection.Clear();
                    }
                    result = false;
                }
                return result;
            }
        }

        public IPopuMenuWrap PopuMenu
        {
            set
            {
                this.ipopuMenuWrap_0 = value;
            }
        }

        static FixTopologyErrorTool()
        {
            FixTopologyErrorTool.old_acctor_mc();
        }

        public FixTopologyErrorTool(IAppContext context)
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
            this.m_bitmap = Properties.Resources.FixTopologyErrorTool;
            this.m_cursor = new System.Windows.Forms.Cursor(base.GetType().Assembly.GetManifestResourceStream("Yutai.Plugins.Editor.Resources.Cursor.FixTopologyErrorTool.cur"));
            this.m_name = "Editor_Topology_FixTopologyErrorTool";
            _key =  "Editor_Topology_FixTopologyErrorTool";
            _itemType= RibbonItemType.Tool ;
            this.m_caption = "修复拓扑错误工具";
            this.m_category = "拓扑";
            this.m_message = "修复拓扑错误工具";
            this.m_toolTip = "修复拓扑错误工具";
            if (this._context.FocusMap != null)
            {
                FixTopologyErrorTool.m_pTopoErroeSelection.ActiveView = this._context.FocusMap as IActiveView;
            }
        }

        public void Init()
        {
        }

        private void method_0()
        {
            this.ipopuMenuWrap_0.Clear();
            this.ipopuMenuWrap_0.AddItem("TopologyError_ZoomTo", false);
            this.ipopuMenuWrap_0.AddItem("TopologyError_PanTo", false);
            this.ipopuMenuWrap_0.AddItem("TopologyError_SelectFeature", false);
            this.ipopuMenuWrap_0.AddItem("ShowTopoRuleDescript", true);
            if (FixTopologyErrorTool.m_pTopoErroeSelection.Count != 0)
            {
                ITopologyErrorFeature topologyErrorFeature = FixTopologyErrorTool.m_pTopoErroeSelection[0].TopologyErrorFeature;
                esriTopologyRuleType topologyRuleType = topologyErrorFeature.TopologyRuleType;
                bool flag = true;
                int num = 1;
                while (true)
                {
                    if (num >= FixTopologyErrorTool.m_pTopoErroeSelection.Count)
                    {
                        break;
                    }
                    else if (FixTopologyErrorTool.m_pTopoErroeSelection[num].TopologyErrorFeature.TopologyRuleType != topologyRuleType)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
                if (flag)
                {
                    esriTopologyRuleType _esriTopologyRuleType = topologyRuleType;
                    switch (_esriTopologyRuleType)
                    {
                        case esriTopologyRuleType.esriTRTAreaNoGaps:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", true);
                            break;
                        }
                        case (esriTopologyRuleType)2:
                        case (esriTopologyRuleType)6:
                        case esriTopologyRuleType.esriTRTAreaNoGaps | esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                        case esriTopologyRuleType.esriTRTLineInsideArea:
                        case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass | esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                        case esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary:
                        case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass | esriTopologyRuleType.esriTRTAreaNoOverlapArea | esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                        case esriTopologyRuleType.esriTRTAreaContainOnePoint:
                        case esriTopologyRuleType.esriTRTAreaNoGaps | esriTopologyRuleType.esriTRTAreaContainOnePoint:
                        case (esriTopologyRuleType)18:
                        {
                            break;
                        }
                        case esriTopologyRuleType.esriTRTAreaNoOverlap:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                            if (FixTopologyErrorTool.m_pTopoErroeSelection.Count == 1)
                            {
                                this.ipopuMenuWrap_0.AddItem("TopologyError_Merge", false);
                            }
                            this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", false);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                        case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                            this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", false);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                        {
                            Label1:
                            this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", true);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                            if (FixTopologyErrorTool.m_pTopoErroeSelection.Count != 1)
                            {
                                break;
                            }
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Merge", false);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTPointProperlyInsideArea:
                        {
                            Label0:
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Delete", true);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTLineNoOverlap:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTLineNoIntersection:
                        {
                            Label2:
                            if (FixTopologyErrorTool.m_pTopoErroeSelection.Count <= 0)
                            {
                                break;
                            }
                            topologyErrorFeature = FixTopologyErrorTool.m_pTopoErroeSelection[0].TopologyErrorFeature;
                            esriGeometryType shapeType = topologyErrorFeature.ShapeType;
                            bool flag1 = true;
                            num = 1;
                            while (true)
                            {
                                if (num >= FixTopologyErrorTool.m_pTopoErroeSelection.Count)
                                {
                                    break;
                                }
                                else if (FixTopologyErrorTool.m_pTopoErroeSelection[num].TopologyErrorFeature.ShapeType != shapeType)
                                {
                                    flag1 = false;
                                    break;
                                }
                                else
                                {
                                    num++;
                                }
                            }
                            if (!flag1)
                            {
                                break;
                            }
                            if (shapeType != esriGeometryType.esriGeometryPolyline)
                            {
                                this.ipopuMenuWrap_0.AddItem("TopologyError_Split", true);
                                break;
                            }
                            else
                            {
                                this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                                break;
                            }
                        }
                        case esriTopologyRuleType.esriTRTLineNoDangles:
                        {
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Extend", true);
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Trim", false);
                            break;
                        }
                        case esriTopologyRuleType.esriTRTLineNoPseudos:
                        {
                            if (FixTopologyErrorTool.m_pTopoErroeSelection.Count != 1)
                            {
                                break;
                            }
                            this.ipopuMenuWrap_0.AddItem("TopologyError_Merge", true);
                            break;
                        }
                        default:
                        {
                            switch (_esriTopologyRuleType)
                            {
                                case esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint:
                                {
                                    this.ipopuMenuWrap_0.AddItem("TopologyError_Delete", true);
                                    break;
                                }
                                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                                case esriTopologyRuleType.esriTRTAreaContainPoint:
                                {
                                    this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", true);
                                    break;
                                }
                                case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                                case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                                {
                                    this.ipopuMenuWrap_0.AddItem("TopologyError_Simply", true);
                                    break;
                                }
                                case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                                {
                                    if (FixTopologyErrorTool.m_pTopoErroeSelection.Count <= 0)
                                    {
                                        break;
                                    }
                                    topologyErrorFeature = FixTopologyErrorTool.m_pTopoErroeSelection[0].TopologyErrorFeature;
                                    esriGeometryType shapeType = topologyErrorFeature.ShapeType;
                                    bool flag1 = true;
                                    num = 1;
                                    while (true)
                                    {
                                        if (num >= FixTopologyErrorTool.m_pTopoErroeSelection.Count)
                                        {
                                            break;
                                        }
                                        else if (FixTopologyErrorTool.m_pTopoErroeSelection[num].TopologyErrorFeature.ShapeType != shapeType)
                                        {
                                            flag1 = false;
                                            break;
                                        }
                                        else
                                        {
                                            num++;
                                        }
                                    }
                                    if (!flag1)
                                    {
                                        break;
                                    }
                                    if (shapeType != esriGeometryType.esriGeometryPolyline)
                                    {
                                        this.ipopuMenuWrap_0.AddItem("TopologyError_Split", true);
                                        break;
                                    }
                                    else
                                    {
                                        this.ipopuMenuWrap_0.AddItem("TopologyError_Subtract", true);
                                        break;
                                    }
                                }
                                case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                                {
                                    this.ipopuMenuWrap_0.AddItem("TopologyError_CreateFeature", true);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                this.ipopuMenuWrap_0.AddItem("PromoteToRuleException", true);
                this.ipopuMenuWrap_0.AddItem("DemoteFromRuleException", false);
            }
        }

        private static void old_acctor_mc()
        {
            FixTopologyErrorTool.m_pTopoErroeSelection = new TopologyErrorSelections();
        }

       

        public override void OnMouseDown(int int_0, int int_1, int int_2, int int_3)
        {
            if (int_0 != 2)
            {
                if (this._context.ActiveView is IPageLayout)
                {
                    IPoint mapPoint = this._context.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                    IMap map = this._context.ActiveView.HitTestMap(mapPoint);
                    if (map == null)
                    {
                        return;
                    }
                    this._context.ActiveView.FocusMap = map;
                    this._context.ActiveView.Refresh();
                }
                IActiveView focusMap = this._context.FocusMap as IActiveView;
                this.ipoint_0 = focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3);
                this.bool_0 = true;
            }
        }

        public override void OnMouseMove(int int_0, int int_1, int int_2, int int_3)
        {
            if (this.bool_0)
            {
                IActiveView focusMap = this._context.FocusMap as IActiveView;
                if (this.idisplayFeedback_0 == null)
                {
                    this.idisplayFeedback_0 = new NewEnvelopeFeedback()
                    {
                        Display = focusMap.ScreenDisplay
                    };
                    (this.idisplayFeedback_0 as INewEnvelopeFeedback).Start(this.ipoint_0);
                }
                this.idisplayFeedback_0.MoveTo(focusMap.ScreenDisplay.DisplayTransformation.ToMapPoint(int_2, int_3));
            }
        }

        public override void OnMouseUp(int int_0, int int_1, int int_2, int int_3)
        {
            IActiveView focusMap;
            double mapUnits;
            if (this.bool_0)
            {
                IEnvelope envelope = null;
                if (this.idisplayFeedback_0 != null)
                {
                    envelope = (this.idisplayFeedback_0 as INewEnvelopeFeedback).Stop();
                    if ((envelope.IsEmpty || envelope.Width == 0 ? true : envelope.Height == 0))
                    {
                        focusMap = this._context.FocusMap as IActiveView;
                        envelope = this.ipoint_0.Envelope;
                        mapUnits = Common.ConvertPixelsToMapUnits(focusMap, 8);
                        envelope.Height = mapUnits;
                        envelope.Width = mapUnits;
                        envelope.CenterAt(this.ipoint_0);
                    }
                }
                else
                {
                    focusMap = this._context.FocusMap as IActiveView;
                    envelope = this.ipoint_0.Envelope;
                    mapUnits = Common.ConvertPixelsToMapUnits(focusMap, 8);
                    envelope.Height = mapUnits;
                    envelope.Width = mapUnits;
                    envelope.CenterAt(this.ipoint_0);
                }
                envelope.SpatialReference = this._context.FocusMap.SpatialReference;
              
                
                IActiveView activeView = this._context.FocusMap as IActiveView;
                IErrorFeatureContainer topology = CmdSelectTopology.m_TopologyLayer.Topology as IErrorFeatureContainer;
                IGeoDataset geoDataset = CmdSelectTopology.m_TopologyLayer.Topology as IGeoDataset;
                ITopologyErrorSelection mTopologyLayer = CmdSelectTopology.m_TopologyLayer as ITopologyErrorSelection;
                IEnumTopologyErrorFeature errorFeaturesByRuleType = topology.ErrorFeaturesByRuleType[geoDataset.SpatialReference, esriTopologyRuleType.esriTRTAny, envelope, mTopologyLayer.SelectErrors, mTopologyLayer.SelectExceptions];
                FixTopologyErrorTool.m_pTopoErroeSelection.Clear();
                for (ITopologyErrorFeature i = errorFeaturesByRuleType.Next(); i != null; i = errorFeaturesByRuleType.Next())
                {
                    FixTopologyErrorTool.m_pTopoErroeSelection.Add(new TopologyError(CmdSelectTopology.m_TopologyLayer, i));
                }
                this.method_0();
                activeView.Refresh();
                this.idisplayFeedback_0 = null;
                this.bool_0 = false;
            }
        }
    }
}