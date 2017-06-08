using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class FindTopologyErrorCtrl : UserControl, IDockContent
    {
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private SimpleButton btnFind;
        private ComboBoxEdit cboFindType;
        private CheckEdit chkError;
        private CheckEdit chkException;
        private CheckEdit chkVisibleRegion;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private IContainer components;
        private BarButtonItem Delete;
        private BarButtonItem DemoteFromRuleException;
        private BarButtonItem Explode;
        private BarButtonItem ExtendLine;
        private Label label1;
        private Label lblErrorNum;
        private ListView listViewError;
        private bool m_CanDo;
        private IWorkspace m_pEditWorkspace;
        private IMap m_pFocusMap;
        private TopologyErrorSelections m_pTopoErroeSelection = new TopologyErrorSelections();
        private ITopologyLayer m_TopologyLayer;
        private BarButtonItem MergeErrorToFeature;
        private BarButtonItem NewFeature;
        private Panel panel1;
        private BarButtonItem PanTo;
        private PopupMenu popupMenu1;
        private BarButtonItem PromoteToRuleException;
        private string[] RuleDes;
        private esriTopologyRuleType[] RuleType;
        private BarButtonItem SelectFeature;
        private BarButtonItem ShowTopoRuleDescript;
        private BarButtonItem Simplify;
        private BarButtonItem Split;
        private BarButtonItem SubtractError;
        private BarButtonItem TrimLine;
        private BarButtonItem ZoomTo;

        public FindTopologyErrorCtrl()
        {
            esriTopologyRuleType[] typeArray = new esriTopologyRuleType[0x1b];
            typeArray[0] = esriTopologyRuleType.esriTRTAny;
            typeArray[2] = esriTopologyRuleType.esriTRTAreaNoGaps;
            typeArray[3] = esriTopologyRuleType.esriTRTAreaNoOverlap;
            typeArray[4] = esriTopologyRuleType.esriTRTAreaCoveredByAreaClass;
            typeArray[5] = esriTopologyRuleType.esriTRTAreaAreaCoverEachOther;
            typeArray[6] = esriTopologyRuleType.esriTRTAreaCoveredByArea;
            typeArray[7] = esriTopologyRuleType.esriTRTAreaNoOverlapArea;
            typeArray[8] = esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary;
            typeArray[9] = esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary;
            typeArray[10] = esriTopologyRuleType.esriTRTPointProperlyInsideArea;
            typeArray[11] = esriTopologyRuleType.esriTRTLineNoOverlap;
            typeArray[12] = esriTopologyRuleType.esriTRTLineNoIntersection;
            typeArray[13] = esriTopologyRuleType.esriTRTLineNoDangles;
            typeArray[14] = esriTopologyRuleType.esriTRTLineNoPseudos;
            typeArray[15] = esriTopologyRuleType.esriTRTLineCoveredByLineClass;
            typeArray[0x10] = esriTopologyRuleType.esriTRTLineNoOverlapLine;
            typeArray[0x11] = esriTopologyRuleType.esriTRTPointCoveredByLine;
            typeArray[0x12] = esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint;
            typeArray[0x13] = esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine;
            typeArray[20] = esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary;
            typeArray[0x15] = esriTopologyRuleType.esriTRTLineNoSelfOverlap;
            typeArray[0x16] = esriTopologyRuleType.esriTRTLineNoSelfIntersect;
            typeArray[0x17] = esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch;
            typeArray[0x18] = esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint;
            typeArray[0x19] = esriTopologyRuleType.esriTRTAreaContainPoint;
            typeArray[0x1a] = esriTopologyRuleType.esriTRTLineNoMultipart;
            this.RuleType = typeArray;
            this.RuleDes = new string[] { 
                "所有错误", "必需大于集束容限值", "面不能有缝隙", "面不能重叠", "面必须被面要素类覆盖", "面必须和其它面要素层相互覆盖", "面必须被面覆盖", "面不能与其他面层重叠", "线必须被面要素边界线覆盖", "点必须被面要素边界线覆盖", "点落在面要素内", "线不能重叠", "线不能相交", "线不能有悬挂点", "线不能有伪节点", "线必须被线要素覆盖", 
                "线与线不能重叠", "点必须被线要素覆盖", "点必须被线要素终点覆盖", "面边界线必须被线要素覆盖", "面边界线必须被其它面层边界线覆盖", "线不能自重叠", "线不能自相交", "线不能相交或内部相接", "线终点必须被点要素覆盖", "面包含点", "线必须为单部分"
             };
            this.m_pFocusMap = null;
            this.m_TopologyLayer = null;
            this.m_CanDo = false;
            this.m_pEditWorkspace = null;
            this.InitializeComponent();
            this.Text = "拓扑错误信息";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                this.listViewError.Items.Clear();
                IGeoDataset topology = this.m_TopologyLayer.Topology as IGeoDataset;
                IFeatureClassContainer container = topology as IFeatureClassContainer;
                IEnvelope extent = topology.Extent;
                if (this.chkVisibleRegion.Checked)
                {
                    extent = (this.m_pFocusMap as IActiveView).Extent;
                }
                esriTopologyRuleType esriTRTAny = esriTopologyRuleType.esriTRTAny;
                if (this.cboFindType.SelectedIndex == 0)
                {
                    esriTRTAny = esriTopologyRuleType.esriTRTAny;
                }
                else if (this.cboFindType.SelectedIndex == 1)
                {
                    esriTRTAny = esriTopologyRuleType.esriTRTFeatureLargerThanClusterTolerance;
                }
                else if (this.cboFindType.SelectedItem is TopologyRuleWrap)
                {
                    esriTRTAny = (this.cboFindType.SelectedItem as TopologyRuleWrap).TopologyRule.TopologyRuleType;
                }
                IEnumTopologyErrorFeature feature = (this.m_TopologyLayer.Topology as IErrorFeatureContainer).get_ErrorFeaturesByRuleType(topology.SpatialReference, esriTRTAny, extent, this.chkError.Checked, this.chkException.Checked);
                ITopologyErrorFeature feature2 = feature.Next();
                string[] items = new string[7];
                while (feature2 != null)
                {
                    items[0] = this.GetRuleDescription(feature2.TopologyRuleType);
                    items[1] = container.get_ClassByID(feature2.OriginClassID).AliasName;
                    if ((feature2.DestinationClassID != 0) && (feature2.DestinationClassID != feature2.OriginClassID))
                    {
                        items[2] = container.get_ClassByID(feature2.DestinationClassID).AliasName;
                    }
                    else
                    {
                        items[2] = "";
                    }
                    items[3] = this.GetShapeString(feature2.ShapeType);
                    items[4] = feature2.OriginOID.ToString();
                    items[5] = feature2.DestinationOID.ToString();
                    if (feature2.IsException)
                    {
                        items[6] = "正确";
                    }
                    else
                    {
                        items[6] = "错误";
                    }
                    ListViewItem item = new ListViewItem(items) {
                        Tag = feature2
                    };
                    this.listViewError.Items.Add(item);
                    feature2 = feature.Next();
                }
            }
            catch
            {
            }
        }

        private void BulidContextMenu()
        {
            this.popupMenu1.ClearLinks();
            if (this.listViewError.SelectedItems.Count != 0)
            {
                int num;
                this.popupMenu1.AddItem(this.ZoomTo);
                this.popupMenu1.AddItem(this.PanTo);
                this.popupMenu1.AddItem(this.SelectFeature);
                this.popupMenu1.AddItem(this.ShowTopoRuleDescript).BeginGroup = true;
                this.ShowTopoRuleDescript.Enabled = this.listViewError.SelectedItems.Count == 1;
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[0].Tag as ITopologyErrorFeature;
                esriTopologyRuleType topologyRuleType = tag.TopologyRuleType;
                bool flag = true;
                for (num = 1; num < this.listViewError.SelectedItems.Count; num++)
                {
                    tag = this.listViewError.SelectedItems[num].Tag as ITopologyErrorFeature;
                    if (tag.TopologyRuleType != topologyRuleType)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    switch (topologyRuleType)
                    {
                        case esriTopologyRuleType.esriTRTAreaNoGaps:
                            this.popupMenu1.AddItem(this.NewFeature).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTAreaNoOverlap:
                            this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                            if (this.listViewError.SelectedItems.Count == 1)
                            {
                                this.popupMenu1.AddItem(this.MergeErrorToFeature);
                            }
                            this.popupMenu1.AddItem(this.NewFeature);
                            break;

                        case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                        case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                            this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                            this.popupMenu1.AddItem(this.NewFeature);
                            break;

                        case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                        case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                        case esriTopologyRuleType.esriTRTAreaContainPoint:
                            this.popupMenu1.AddItem(this.NewFeature).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                            this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                            if (this.listViewError.SelectedItems.Count == 1)
                            {
                                this.popupMenu1.AddItem(this.MergeErrorToFeature);
                            }
                            break;

                        case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                            this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTPointProperlyInsideArea:
                        case esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint:
                            this.popupMenu1.AddItem(this.Delete).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTLineNoOverlap:
                            this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTLineNoIntersection:
                        case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                        {
                            tag = this.listViewError.SelectedItems[0].Tag as ITopologyErrorFeature;
                            esriGeometryType shapeType = tag.ShapeType;
                            bool flag2 = true;
                            for (num = 1; num < this.listViewError.SelectedItems.Count; num++)
                            {
                                tag = this.listViewError.SelectedItems[num].Tag as ITopologyErrorFeature;
                                if (tag.ShapeType != shapeType)
                                {
                                    flag2 = false;
                                    break;
                                }
                            }
                            if (flag2)
                            {
                                if (shapeType == esriGeometryType.esriGeometryPolyline)
                                {
                                    this.popupMenu1.AddItem(this.SubtractError).BeginGroup = true;
                                }
                                else
                                {
                                    this.popupMenu1.AddItem(this.Split).BeginGroup = true;
                                }
                            }
                            break;
                        }
                        case esriTopologyRuleType.esriTRTLineNoDangles:
                            this.popupMenu1.AddItem(this.ExtendLine).BeginGroup = true;
                            this.popupMenu1.AddItem(this.TrimLine);
                            break;

                        case esriTopologyRuleType.esriTRTLineNoPseudos:
                            if (this.listViewError.SelectedItems.Count == 1)
                            {
                                this.popupMenu1.AddItem(this.MergeErrorToFeature).BeginGroup = true;
                            }
                            break;

                        case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                        case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                            this.popupMenu1.AddItem(this.Simplify).BeginGroup = true;
                            break;

                        case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                            this.popupMenu1.AddItem(this.NewFeature).BeginGroup = true;
                            break;
                    }
                }
                this.popupMenu1.AddItem(this.PromoteToRuleException).BeginGroup = true;
                this.popupMenu1.AddItem(this.DemoteFromRuleException);
            }
        }

        private IFeature CreateFeature(ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeature feature;
            IRowSubtypes subtypes;
            IGeometry shape;
            IPolygon polygon;
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            if (pTopoErrorFeat.DestinationClassID > 0)
            {
                class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
            }
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoGaps:
                    break;

                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                {
                    IFeature feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    IFeature feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                    shape = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!shape.IsEmpty)
                    {
                        feature2.Shape = shape;
                        feature2.Store();
                    }
                    else
                    {
                        feature2.Delete();
                    }
                    shape = (feature3.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (shape.IsEmpty)
                    {
                        feature3.Delete();
                    }
                    else
                    {
                        feature3.Shape = shape;
                        feature3.Store();
                    }
                    break;
                }
                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    if (pTopoErrorFeat.OriginOID <= 0)
                    {
                        feature = class2.CreateFeature();
                        subtypes = (IRowSubtypes) feature;
                        try
                        {
                            subtypes.InitDefaultValues();
                        }
                        catch
                        {
                        }
                        shape = (pTopoErrorFeat as IFeature).Shape;
                        feature.Shape = shape;
                        feature.Store();
                        return feature;
                    }
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                    feature = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID).CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    polygon = (pTopoErrorFeat as IFeature).Shape as IPolygon;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    shape = new PolylineClass();
                    (shape as IPointCollection).AddPointCollection(polygon as IPointCollection);
                    feature.Shape = shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                    feature = class3.CreateFeature();
                    feature.Shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaContainPoint:
                    feature = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID).CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    polygon = (pTopoErrorFeat as IFeature).Shape as IPolygon;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    shape = (polygon as IArea).LabelPoint;
                    feature.Shape = shape;
                    feature.Store();
                    return feature;

                default:
                    return null;
            }
            if (pTopoErrorFeat.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                shape = new PolygonClass();
                IPolyline shapeCopy = (pTopoErrorFeat as IFeature).ShapeCopy as IPolyline;
                shape.SpatialReference = shapeCopy.SpatialReference;
                object before = Missing.Value;
                for (int i = 0; i < (shapeCopy as ISegmentCollection).SegmentCount; i++)
                {
                    (shape as ISegmentCollection).AddSegment((shapeCopy as ISegmentCollection).get_Segment(i), ref before, ref before);
                }
                if (!(shape as IPolygon).IsClosed)
                {
                    (shape as IPolygon).Close();
                }
            }
            else
            {
                shape = (pTopoErrorFeat as IFeature).ShapeCopy;
            }
            feature = class2.CreateFeature();
            subtypes = (IRowSubtypes) feature;
            try
            {
                subtypes.InitDefaultValues();
            }
            catch
            {
            }
            feature.Shape = shape;
            feature.Store();
            return feature;
        }

        private void DeComposePolyline(IFeature pFeature, IGeometryCollection pGeometryColn)
        {
            ((pFeature.Class as IDataset).Workspace as IWorkspaceEdit).StartEditOperation();
            object before = Missing.Value;
            bool zAware = false;
            bool mAware = false;
            double zLevel = 0.0;
            try
            {
                zAware = (pGeometryColn as IZAware).ZAware;
                zLevel = (pGeometryColn as IZ).ZMin;
            }
            catch
            {
            }
            try
            {
                mAware = (pGeometryColn as IMAware).MAware;
            }
            catch
            {
            }
            for (int i = 0; i < pGeometryColn.GeometryCount; i++)
            {
                IGeometry inGeometry = pGeometryColn.get_Geometry(i);
                IGeometryCollection geometrys = new PolylineClass();
                (geometrys as IZAware).ZAware = zAware;
                (geometrys as IMAware).MAware = mAware;
                geometrys.AddGeometry(inGeometry, ref before, ref before);
                if (zAware)
                {
                    (geometrys as IZ).SetConstantZ(zLevel);
                }
                (geometrys as ITopologicalOperator).Simplify();
            }
            pFeature.Delete();
        }

        private void Delete_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            IEnvelope areaToValidate = null;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).Shape.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).Shape.Envelope);
                }
                this.DeleteError(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
            }
            (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            workspace.StopEditOperation();
        }

        private bool DeleteError(ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeatureClass class2 = (this.m_TopologyLayer.Topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            esriTopologyRuleType topologyRuleType = pTopoErrorFeat.TopologyRuleType;
            if (topologyRuleType != esriTopologyRuleType.esriTRTPointProperlyInsideArea)
            {
                if (topologyRuleType != esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint)
                {
                    return false;
                }
            }
            else
            {
                class2.GetFeature(pTopoErrorFeat.OriginOID).Delete();
                return true;
            }
            class2.GetFeature(pTopoErrorFeat.OriginOID).Delete();
            return true;
        }

        private void DemoteFromRuleException_ItemClick(object sender, ItemClickEventArgs e)
        {
            ITopologyRuleContainer topology = this.m_TopologyLayer.Topology as ITopologyRuleContainer;
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                topology.DemoteFromRuleException(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            workspace.StopEditOperation();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void DoExplode(ITopologyErrorFeature pTopoErrorFeat)
        {
            ITopologicalOperator shapeCopy = (this.m_TopologyLayer.Topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID).GetFeature(pTopoErrorFeat.OriginOID).ShapeCopy as ITopologicalOperator;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoMultipart)
            {
            }
        }

        private void DoSplit(ITopologyErrorFeature pTopoErrorFeat)
        {
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            esriTopologyRuleType topologyRuleType = pTopoErrorFeat.TopologyRuleType;
            if (((topologyRuleType == esriTopologyRuleType.esriTRTLineNoIntersection) || (topologyRuleType == esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch)) && ((pTopoErrorFeat as IFeature).Shape is IPoint))
            {
                int num;
                IFeature feature2;
                IFeature feature3;
                IFeature feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPolycurve shapeCopy = feature.ShapeCopy as IPolycurve;
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IList list = this.PointSplitLine(shapeCopy, shape);
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        feature.Shape = list[num] as IGeometry;
                        feature.Store();
                    }
                    else
                    {
                        feature2 = RowOperator.CreatRowByRow(feature) as IFeature;
                        feature2.Shape = list[num] as IGeometry;
                    }
                }
                if (class3 == null)
                {
                    feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                }
                else
                {
                    feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                }
                shapeCopy = feature3.ShapeCopy as IPolycurve;
                list = this.PointSplitLine(shapeCopy, shape);
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        feature3.Shape = list[num] as IGeometry;
                        feature3.Store();
                    }
                    else
                    {
                        feature2 = RowOperator.CreatRowByRow(feature3) as IFeature;
                        feature2.Shape = list[num] as IGeometry;
                        feature2.Store();
                    }
                }
            }
        }

        private bool Extend(ITopologyErrorFeature pTopoErrorFeat)
        {
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoDangles)
            {
                feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IPolyline polyline = feature.Shape as IPolyline;
                double num = Common.distance(polyline.FromPoint, shape);
                double num2 = Common.distance(polyline.ToPoint, shape);
                ISegmentCollection segments = polyline as ISegmentCollection;
                ILine inLine = null;
                IConstructLine line2 = new LineClass();
                if (num < num2)
                {
                    inLine = segments.get_Segment(0) as ILine;
                    line2.ConstructExtended(inLine, esriSegmentExtension.esriExtendAtFrom);
                }
                else
                {
                    inLine = segments.get_Segment(segments.SegmentCount - 1) as ILine;
                    line2.ConstructExtended(inLine, esriSegmentExtension.esriExtendAtTo);
                }
                IPolyline polyline2 = new PolylineClass();
                object before = Missing.Value;
                (polyline2 as ISegmentCollection).AddSegment(line2 as ISegment, ref before, ref before);
                ISpatialFilter filter = new SpatialFilterClass {
                    Geometry = polyline2,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };
                IFeatureCursor o = class2.Search(filter, false);
                ITopologicalOperator operator2 = polyline2 as ITopologicalOperator;
                IFeature feature3 = o.NextFeature();
                bool flag = true;
                double num3 = 0.0;
                ICurve toCurve = null;
                IPoint splitPoint = null;
                IFeature feature4 = null;
                while (feature3 != null)
                {
                    if (feature3.OID != feature.OID)
                    {
                        double num4;
                        IGeometry geometry2 = operator2.Intersect(feature3.Shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry2 is IPoint)
                        {
                            num4 = Common.distance(geometry2 as IPoint, shape);
                            if (flag)
                            {
                                flag = false;
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                toCurve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                            else if (num3 > num4)
                            {
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                toCurve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                        }
                        if (geometry2 is IPointCollection)
                        {
                            IPointCollection points = geometry2 as IPointCollection;
                            for (int i = 0; i < points.PointCount; i++)
                            {
                                IPoint point3 = points.get_Point(i);
                                num4 = Common.distance(point3, shape);
                                if (flag)
                                {
                                    flag = false;
                                    num3 = num4;
                                    splitPoint = point3;
                                    toCurve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                                else if (num3 > num4)
                                {
                                    num3 = num4;
                                    splitPoint = point3;
                                    toCurve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                            }
                        }
                    }
                    feature3 = o.NextFeature();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (toCurve != null)
                {
                    bool flag3;
                    int num6;
                    int num7;
                    IConstructCurve curve2 = new PolylineClass();
                    ICurve fromCurve = feature.Shape as ICurve;
                    bool extensionsPerformed = true;
                    if (num < num2)
                    {
                        curve2.ConstructExtended(fromCurve, toCurve, 0x10, ref extensionsPerformed);
                    }
                    else
                    {
                        curve2.ConstructExtended(fromCurve, toCurve, 8, ref extensionsPerformed);
                    }
                    if (!(curve2 as IGeometry).IsEmpty)
                    {
                        feature.Shape = curve2 as IGeometry;
                        feature.Store();
                    }
                    (toCurve as IPolycurve).SplitAtPoint(splitPoint, true, false, out flag3, out num6, out num7);
                    feature4.Shape = toCurve;
                    feature4.Store();
                    return true;
                }
            }
            return false;
        }

        private void ExtendLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            IEnvelope areaToValidate = null;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).Shape.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).Shape.Envelope);
                }
                if (this.Extend(tag))
                {
                    this.m_pTopoErroeSelection.Remove(tag);
                    this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
                }
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
            }
            (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            workspace.StopEditOperation();
        }

        private IFeatureLayer FindLayer(IMap pMap, IFeatureClass pFC)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                IFeatureLayer layer = pMap.get_Layer(i) as IFeatureLayer;
                if ((layer != null) && (layer.FeatureClass.AliasName == pFC.AliasName))
                {
                    return layer;
                }
            }
            return null;
        }

        private void FindTopologyErrorCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
            this.barManager1.SetPopupContextMenu(this.listViewError, this.popupMenu1);
            this.m_CanDo = true;
        }

        private string GetRuleDescription(esriTopologyRuleType type)
        {
            switch (type)
            {
                case esriTopologyRuleType.esriTRTAny:
                    return "所有错误";

                case esriTopologyRuleType.esriTRTFeatureLargerThanClusterTolerance:
                    return "必需大于集束容限值";

                case esriTopologyRuleType.esriTRTAreaNoGaps:
                    return "面不能有缝隙";

                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    return "面不能重叠";

                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    return "面必须被面要素类覆盖";

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    return "面必须和其它面要素层相互覆盖";

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    return "面必须被面覆盖";

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    return "面不能与其他面层重叠";

                case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                    return "线必须被面要素边界线覆盖";

                case esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary:
                    return "点必须被面要素边界线覆盖";

                case esriTopologyRuleType.esriTRTPointProperlyInsideArea:
                    return "点落在面要素内";

                case esriTopologyRuleType.esriTRTLineNoOverlap:
                    return "线不能重叠";

                case esriTopologyRuleType.esriTRTLineNoIntersection:
                    return "线不能相交";

                case esriTopologyRuleType.esriTRTLineNoDangles:
                    return "线不能有悬挂点";

                case esriTopologyRuleType.esriTRTLineNoPseudos:
                    return "线不能有伪节点";

                case esriTopologyRuleType.esriTRTLineCoveredByLineClass:
                    return "线必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTLineNoOverlapLine:
                    return "线与线不能重叠";

                case esriTopologyRuleType.esriTRTPointCoveredByLine:
                    return "点必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint:
                    return "点必须被线要素终点覆盖";

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                    return "面边界线必须被线要素覆盖";

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary:
                    return "面边界线必须被其它面层边界线覆盖";

                case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                    return "线不能自重叠";

                case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                    return "线不能自相交";

                case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                    return "线不能相交或内部相接";

                case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                    return "线终点必须被点要素覆盖";

                case esriTopologyRuleType.esriTRTAreaContainPoint:
                    return "面包含点";

                case esriTopologyRuleType.esriTRTLineNoMultipart:
                    return "线必须为单部分";
            }
            return "所有错误";
        }

        private string GetShapeString(esriGeometryType type)
        {
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    return "点";

                case esriGeometryType.esriGeometryMultipoint:
                    return "多点";

                case esriGeometryType.esriGeometryPolyline:
                    return "多线";

                case esriGeometryType.esriGeometryPolygon:
                    return "多边形";
            }
            return type.ToString();
        }

        public void Init()
        {
            this.listViewError.Items.Clear();
            this.cboFindType.Properties.Items.Clear();
            ITopologyRuleContainer topology = this.m_TopologyLayer.Topology as ITopologyRuleContainer;
            IEnumRule rules = topology.Rules;
            rules.Reset();
            IRule rule2 = rules.Next();
            this.cboFindType.Properties.Items.Add("所有错误");
            this.cboFindType.Properties.Items.Add("必需大于集束容限值");
            while (rule2 != null)
            {
                this.cboFindType.Properties.Items.Add(new TopologyRuleWrap(rule2 as ITopologyRule, topology as IFeatureClassContainer));
                rule2 = rules.Next();
            }
            this.cboFindType.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.panel1 = new Panel();
            this.chkVisibleRegion = new CheckEdit();
            this.chkException = new CheckEdit();
            this.chkError = new CheckEdit();
            this.btnFind = new SimpleButton();
            this.lblErrorNum = new Label();
            this.cboFindType = new ComboBoxEdit();
            this.label1 = new Label();
            this.listViewError = new ListView();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.barManager1 = new BarManager(this.components);
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.ZoomTo = new BarButtonItem();
            this.PanTo = new BarButtonItem();
            this.SelectFeature = new BarButtonItem();
            this.PromoteToRuleException = new BarButtonItem();
            this.DemoteFromRuleException = new BarButtonItem();
            this.NewFeature = new BarButtonItem();
            this.MergeErrorToFeature = new BarButtonItem();
            this.SubtractError = new BarButtonItem();
            this.Simplify = new BarButtonItem();
            this.Explode = new BarButtonItem();
            this.Delete = new BarButtonItem();
            this.Split = new BarButtonItem();
            this.ExtendLine = new BarButtonItem();
            this.TrimLine = new BarButtonItem();
            this.ShowTopoRuleDescript = new BarButtonItem();
            this.popupMenu1 = new PopupMenu(this.components);
            this.panel1.SuspendLayout();
            this.chkVisibleRegion.Properties.BeginInit();
            this.chkException.Properties.BeginInit();
            this.chkError.Properties.BeginInit();
            this.cboFindType.Properties.BeginInit();
            this.barManager1.BeginInit();
            this.popupMenu1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.chkVisibleRegion);
            this.panel1.Controls.Add(this.chkException);
            this.panel1.Controls.Add(this.chkError);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.lblErrorNum);
            this.panel1.Controls.Add(this.cboFindType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f8, 0x48);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.chkVisibleRegion.EditValue = true;
            this.chkVisibleRegion.Location = new System.Drawing.Point(0xe8, 0x2c);
            this.chkVisibleRegion.Name = "chkVisibleRegion";
            this.chkVisibleRegion.Properties.Caption = "仅限可视区域";
            this.chkVisibleRegion.Size = new Size(0x68, 0x13);
            this.chkVisibleRegion.TabIndex = 7;
            this.chkException.Location = new System.Drawing.Point(160, 0x2c);
            this.chkException.Name = "chkException";
            this.chkException.Properties.Caption = "例外";
            this.chkException.Size = new Size(0x30, 0x13);
            this.chkException.TabIndex = 6;
            this.chkError.EditValue = true;
            this.chkError.Location = new System.Drawing.Point(0x60, 0x2c);
            this.chkError.Name = "chkError";
            this.chkError.Properties.Caption = "错误";
            this.chkError.Size = new Size(0x30, 0x13);
            this.chkError.TabIndex = 5;
            this.btnFind.Location = new System.Drawing.Point(8, 40);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new Size(0x48, 0x18);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "开始查找";
            this.btnFind.Click += new EventHandler(this.btnFind_Click);
            this.lblErrorNum.AutoSize = true;
            this.lblErrorNum.Location = new System.Drawing.Point(0x158, 10);
            this.lblErrorNum.Name = "lblErrorNum";
            this.lblErrorNum.Size = new Size(0, 12);
            this.lblErrorNum.TabIndex = 3;
            this.cboFindType.EditValue = "";
            this.cboFindType.Location = new System.Drawing.Point(0x30, 8);
            this.cboFindType.Name = "cboFindType";
            this.cboFindType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFindType.Size = new Size(0x110, 0x15);
            this.cboFindType.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "查找:";
            this.listViewError.Columns.AddRange(new ColumnHeader[] { this.columnHeader2, this.columnHeader1, this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6, this.columnHeader7 });
            this.listViewError.Dock = DockStyle.Fill;
            this.listViewError.FullRowSelect = true;
            this.listViewError.GridLines = true;
            this.listViewError.HideSelection = false;
            this.listViewError.Location = new System.Drawing.Point(0, 0x48);
            this.listViewError.Name = "listViewError";
            this.listViewError.Size = new Size(0x1f8, 0xd8);
            this.listViewError.TabIndex = 2;
            this.listViewError.UseCompatibleStateImageBehavior = false;
            this.listViewError.View = View.Details;
            this.listViewError.SelectedIndexChanged += new EventHandler(this.listViewError_SelectedIndexChanged);
            this.listViewError.Click += new EventHandler(this.listViewError_Click);
            this.listViewError.MouseDown += new MouseEventHandler(this.listViewError_MouseDown);
            this.columnHeader2.Text = "规则类型";
            this.columnHeader2.Width = 0x60;
            this.columnHeader1.Text = "类1";
            this.columnHeader1.Width = 0x43;
            this.columnHeader3.Text = "类2";
            this.columnHeader3.Width = 0x48;
            this.columnHeader4.Text = "几何类型";
            this.columnHeader4.Width = 0x45;
            this.columnHeader5.Text = "要素1";
            this.columnHeader6.Text = "要素2";
            this.columnHeader6.Width = 70;
            this.columnHeader7.Text = "例外";
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.ZoomTo, this.PanTo, this.SelectFeature, this.PromoteToRuleException, this.DemoteFromRuleException, this.NewFeature, this.MergeErrorToFeature, this.SubtractError, this.Simplify, this.Explode, this.Delete, this.Split, this.ExtendLine, this.TrimLine, this.ShowTopoRuleDescript });
            this.barManager1.MaxItemId = 15;
            this.ZoomTo.Caption = "缩放到";
            this.ZoomTo.Id = 0;
            this.ZoomTo.Name = "ZoomTo";
            this.ZoomTo.ItemClick += new ItemClickEventHandler(this.ZoomTo_ItemClick);
            this.PanTo.Caption = "平移到";
            this.PanTo.Id = 1;
            this.PanTo.Name = "PanTo";
            this.PanTo.ItemClick += new ItemClickEventHandler(this.PanTo_ItemClick);
            this.SelectFeature.Caption = "选择要素";
            this.SelectFeature.Id = 2;
            this.SelectFeature.Name = "SelectFeature";
            this.SelectFeature.ItemClick += new ItemClickEventHandler(this.SelectFeature_ItemClick);
            this.PromoteToRuleException.Caption = "标记为例外";
            this.PromoteToRuleException.Id = 3;
            this.PromoteToRuleException.Name = "PromoteToRuleException";
            this.PromoteToRuleException.ItemClick += new ItemClickEventHandler(this.PromoteToRuleException_ItemClick);
            this.DemoteFromRuleException.Caption = "标记为错误";
            this.DemoteFromRuleException.Id = 4;
            this.DemoteFromRuleException.Name = "DemoteFromRuleException";
            this.DemoteFromRuleException.ItemClick += new ItemClickEventHandler(this.DemoteFromRuleException_ItemClick);
            this.NewFeature.Caption = "新建要素";
            this.NewFeature.Id = 5;
            this.NewFeature.Name = "NewFeature";
            this.NewFeature.ItemClick += new ItemClickEventHandler(this.NewFeature_ItemClick);
            this.MergeErrorToFeature.Caption = "合并...";
            this.MergeErrorToFeature.Id = 6;
            this.MergeErrorToFeature.Name = "MergeErrorToFeature";
            this.MergeErrorToFeature.ItemClick += new ItemClickEventHandler(this.MergeErrorToFeature_ItemClick);
            this.SubtractError.Caption = "扣除";
            this.SubtractError.Id = 7;
            this.SubtractError.Name = "SubtractError";
            this.SubtractError.ItemClick += new ItemClickEventHandler(this.SubtractError_ItemClick);
            this.Simplify.Caption = "简化";
            this.Simplify.Id = 8;
            this.Simplify.Name = "Simplify";
            this.Simplify.ItemClick += new ItemClickEventHandler(this.Simplify_ItemClick);
            this.Explode.Caption = "炸开";
            this.Explode.Id = 9;
            this.Explode.Name = "Explode";
            this.Delete.Caption = "删除";
            this.Delete.Id = 10;
            this.Delete.Name = "Delete";
            this.Delete.ItemClick += new ItemClickEventHandler(this.Delete_ItemClick);
            this.Split.Caption = "断开";
            this.Split.Id = 11;
            this.Split.Name = "Split";
            this.Split.ItemClick += new ItemClickEventHandler(this.Split_ItemClick);
            this.ExtendLine.Caption = "延伸";
            this.ExtendLine.Id = 12;
            this.ExtendLine.Name = "ExtendLine";
            this.ExtendLine.ItemClick += new ItemClickEventHandler(this.ExtendLine_ItemClick);
            this.TrimLine.Caption = "裁剪";
            this.TrimLine.Id = 13;
            this.TrimLine.Name = "TrimLine";
            this.TrimLine.ItemClick += new ItemClickEventHandler(this.TrimLine_ItemClick);
            this.ShowTopoRuleDescript.Caption = "显示规则描述";
            this.ShowTopoRuleDescript.Id = 14;
            this.ShowTopoRuleDescript.Name = "ShowTopoRuleDescript";
            this.ShowTopoRuleDescript.ItemClick += new ItemClickEventHandler(this.ShowTopoRuleDescript_ItemClick);
            this.popupMenu1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.ZoomTo), new LinkPersistInfo(this.PanTo), new LinkPersistInfo(this.SelectFeature), new LinkPersistInfo(this.NewFeature, true), new LinkPersistInfo(this.PromoteToRuleException, true), new LinkPersistInfo(this.DemoteFromRuleException) });
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            base.Controls.Add(this.listViewError);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "FindTopologyErrorCtrl";
            base.Size = new Size(0x1f8, 0x120);
            base.Load += new EventHandler(this.FindTopologyErrorCtrl_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.chkVisibleRegion.Properties.EndInit();
            this.chkException.Properties.EndInit();
            this.chkError.Properties.EndInit();
            this.cboFindType.Properties.EndInit();
            this.barManager1.EndInit();
            this.popupMenu1.EndInit();
            base.ResumeLayout(false);
        }

        private void listViewError_Click(object sender, EventArgs e)
        {
        }

        private void listViewError_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ListViewItem item = this.listViewError.SelectedItems[i];
                ITopologyErrorFeature tag = item.Tag as ITopologyErrorFeature;
                Rectangle bounds = item.GetBounds(ItemBoundsPortion.Entire);
                System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
                if (bounds.Contains(pt))
                {
                    int x = bounds.X;
                    for (int j = 0; j < 4; j++)
                    {
                        x += this.listViewError.Columns[j].Width;
                    }
                    if ((tag.OriginOID != 0) && ((e.X > x) && (e.X < (x + this.listViewError.Columns[4].Width))))
                    {
                        IFeature feature = (this.m_TopologyLayer.Topology as IFeatureClassContainer).get_ClassByID(tag.OriginClassID).GetFeature(tag.OriginOID);
                        Flash.FlashFeature((this.m_pFocusMap as IActiveView).ScreenDisplay, feature);
                    }
                    else if (tag.DestinationOID != 0)
                    {
                        x += this.listViewError.Columns[4].Width;
                        if ((e.X > x) && (e.X < (x + this.listViewError.Columns[5].Width)))
                        {
                            IFeature feature3 = (this.m_TopologyLayer.Topology as IFeatureClassContainer).get_ClassByID(tag.DestinationClassID).GetFeature(tag.DestinationOID);
                            Flash.FlashFeature((this.m_pFocusMap as IActiveView).ScreenDisplay, feature3);
                        }
                    }
                    break;
                }
            }
        }

        private void listViewError_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_pTopoErroeSelection.Clear();
            if (this.listViewError.SelectedItems.Count > 0)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[0].Tag as ITopologyErrorFeature;
                this.m_pTopoErroeSelection.Add(new TopologyError(this.m_TopologyLayer, tag));
            }
            (this.m_pFocusMap as IActiveView).Refresh();
            this.BulidContextMenu();
            this.UpdateUI();
        }

        private void Merge(ITopologyErrorFeature pTopoErrorFeat)
        {
            frmSelectMergeFeature feature;
            IGeometry geometry;
            IFeature feature2;
            IFeature feature3;
            ITopologicalOperator shapeCopy;
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            string[] strArray = new string[2];
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class2.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            shapeCopy = feature3.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class3.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            shapeCopy = feature3.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;

                case esriTopologyRuleType.esriTRTLineNoPseudos:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class3.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            geometry = (feature3.ShapeCopy as ITopologicalOperator).Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;
            }
        }

        private void MergeErrorToFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            IEnvelope areaToValidate = null;
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).ShapeCopy.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).ShapeCopy.Envelope);
                }
                this.Merge(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
                this.m_pTopoErroeSelection.Remove(tag);
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
                (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            }
            workspace.StopEditOperation();
        }

        private void NewFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                workspace.StartEditOperation();
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                IFeature feature2 = this.CreateFeature(tag);
                IGeometry shapeCopy = feature2.ShapeCopy;
                if (feature2 != null)
                {
                    this.m_pTopoErroeSelection.Remove(tag);
                    try
                    {
                        this.m_TopologyLayer.Topology.ValidateTopology(feature2.Extent);
                    }
                    catch
                    {
                    }
                    this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
                    feature2.Shape = shapeCopy;
                    feature2.Store();
                    (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, feature2.Extent);
                }
                workspace.StopEditOperation();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PanTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listViewError.SelectedItems.Count == 1)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[0].Tag as ITopologyErrorFeature;
                IEnvelope extent = (tag as IFeature).Extent;
                IPoint p = new PointClass();
                p.PutCoords((extent.XMin + extent.XMax) / 2.0, (extent.YMin + extent.YMax) / 2.0);
                IActiveView pFocusMap = this.m_pFocusMap as IActiveView;
                IEnvelope envelope2 = pFocusMap.Extent;
                envelope2.CenterAt(p);
                pFocusMap.Extent = envelope2;
                pFocusMap.Refresh();
            }
        }

        private IList PointSplitLine(IPolycurve pPolycurve, IPoint pt)
        {
            int num;
            int num2;
            bool flag;
            IList list = new ArrayList();
            pPolycurve.SplitAtPoint(pt, true, true, out flag, out num2, out num);
            if (flag)
            {
                object before = Missing.Value;
                try
                {
                    int num3;
                    IGeometryCollection geometrys = new PolylineClass();
                    for (num3 = 0; num3 < num2; num3++)
                    {
                        geometrys.AddGeometry((pPolycurve as IGeometryCollection).get_Geometry(num3), ref before, ref before);
                    }
                    if ((geometrys as IPointCollection).PointCount > 1)
                    {
                        list.Add(geometrys);
                    }
                    geometrys = new PolylineClass();
                    for (num3 = num2; num3 < (pPolycurve as IGeometryCollection).GeometryCount; num3++)
                    {
                        geometrys.AddGeometry((pPolycurve as IGeometryCollection).get_Geometry(num3), ref before, ref before);
                    }
                    if ((geometrys as IPointCollection).PointCount > 1)
                    {
                        list.Add(geometrys);
                    }
                }
                catch (Exception exception)
                {
                   Logger.Current.Error("", exception, "");
                }
            }
            return list;
        }

        private void PromoteToRuleException_ItemClick(object sender, ItemClickEventArgs e)
        {
            ITopologyRuleContainer topology = this.m_TopologyLayer.Topology as ITopologyRuleContainer;
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                topology.PromoteToRuleException(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            workspace.StopEditOperation();
        }

        private void SelectFeature_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listViewError.SelectedItems.Count > 0)
            {
                this.m_pFocusMap.ClearSelection();
                IFeatureClassContainer topology = this.m_TopologyLayer.Topology as IFeatureClassContainer;
                for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
                {
                    ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                    IFeatureClass pFC = topology.get_ClassByID(tag.OriginClassID);
                    IFeatureLayer layer = this.FindLayer(this.m_pFocusMap, pFC);
                    if (layer != null)
                    {
                        if (tag.OriginOID != 0)
                        {
                            this.m_pFocusMap.SelectFeature(layer, pFC.GetFeature(tag.OriginOID));
                        }
                        if (tag.DestinationClassID != 0)
                        {
                            pFC = topology.get_ClassByID(tag.DestinationClassID);
                            layer = this.FindLayer(this.m_pFocusMap, pFC);
                            this.m_pFocusMap.SelectFeature(layer, pFC.GetFeature(tag.DestinationOID));
                        }
                    }
                }
                (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
        }

        private void ShowTopoRuleDescript_ItemClick(object sender, ItemClickEventArgs e)
        {
            ITopologyErrorFeature tag = this.listViewError.SelectedItems[0].Tag as ITopologyErrorFeature;
            new frmRuleInfo { TopologyRule = tag.TopologyRule as ITopologyRule }.ShowDialog();
        }

        private void Simplify_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            IEnvelope areaToValidate = null;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).Shape.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).Shape.Envelope);
                }
                this.Simply(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
            }
            (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            workspace.StopEditOperation();
        }

        private void Simply(ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeature feature = (this.m_TopologyLayer.Topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID).GetFeature(pTopoErrorFeat.OriginOID);
            ITopologicalOperator shapeCopy = feature.ShapeCopy as ITopologicalOperator;
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                    shapeCopy.Simplify();
                    feature.Shape = shapeCopy as IGeometry;
                    feature.Store();
                    break;
            }
        }

        private void Split_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            IEnvelope areaToValidate = null;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).Shape.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).Shape.Envelope);
                }
                this.DoSplit(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
            }
            (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            workspace.StopEditOperation();
        }

        private void Subtract(ITopologyErrorFeature pTopoErrorFeat)
        {
            IGeometry geometry;
            string[] strArray;
            frmSelectMergeFeature feature3;
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            IFeature feature2 = null;
            ITopologicalOperator shapeCopy = null;
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        break;
                    }
                    feature.Delete();
                    break;

                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                    geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class3.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray,
                        Text = "删除"
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case ((esriTopologyRuleType) 6):
                case (esriTopologyRuleType.esriTRTAreaNoOverlapArea | esriTopologyRuleType.esriTRTAreaNoGaps):
                    return;

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    if (pTopoErrorFeat.OriginOID <= 0)
                    {
                        feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                        if (geometry.IsEmpty)
                        {
                            feature2.Delete();
                        }
                        else
                        {
                            feature2.Shape = geometry;
                            feature2.Store();
                        }
                        return;
                    }
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                    }
                    else
                    {
                        feature.Delete();
                    }
                    geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (geometry.IsEmpty)
                    {
                        feature2.Delete();
                    }
                    else
                    {
                        feature2.Shape = geometry;
                        feature2.Store();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                    geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTLineNoOverlap:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineNoIntersection:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray,
                        Text = "删除"
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                        if (geometry.IsEmpty)
                        {
                            feature.Delete();
                        }
                        else
                        {
                            feature.Shape = geometry;
                            feature.Store();
                        }
                    }
                    return;

                default:
                    return;
            }
            geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
            if (geometry.IsEmpty)
            {
                feature2.Delete();
            }
            else
            {
                feature2.Shape = geometry;
                feature2.Store();
            }
        }

        private void SubtractError_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            workspace.StartEditOperation();
            IEnvelope areaToValidate = null;
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).ShapeCopy.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).ShapeCopy.Envelope);
                }
                this.Subtract(tag);
                this.m_pTopoErroeSelection.Remove(tag);
                this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
                (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            }
            workspace.StopEditOperation();
        }

        private bool Trim(ITopologyErrorFeature pTopoErrorFeat)
        {
            ITopology topology = this.m_TopologyLayer.Topology;
            IFeatureClass class2 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (topology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoDangles)
            {
                feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IPolyline polyline = feature.Shape as IPolyline;
                double num = Common.distance(polyline.FromPoint, shape);
                double num2 = Common.distance(polyline.ToPoint, shape);
                ISegmentCollection segments = polyline as ISegmentCollection;
                ILine line = null;
                IConstructLine line2 = new LineClass();
                if (num < num2)
                {
                    line = segments.get_Segment(0) as ILine;
                }
                else
                {
                    line = segments.get_Segment(segments.SegmentCount - 1) as ILine;
                }
                IPolyline polyline2 = new PolylineClass();
                object before = Missing.Value;
                (polyline2 as ISegmentCollection).AddSegment(line as ISegment, ref before, ref before);
                ISpatialFilter filter = new SpatialFilterClass {
                    Geometry = polyline2,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };
                IFeatureCursor o = class2.Search(filter, false);
                ITopologicalOperator operator2 = polyline2 as ITopologicalOperator;
                IFeature feature3 = o.NextFeature();
                bool flag = true;
                double num3 = 0.0;
                ICurve curve = null;
                IPoint splitPoint = null;
                IFeature feature4 = null;
                while (feature3 != null)
                {
                    if (feature3.OID != feature.OID)
                    {
                        double num4;
                        IGeometry geometry2 = operator2.Intersect(feature3.Shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry2 is IPoint)
                        {
                            num4 = Common.distance(geometry2 as IPoint, shape);
                            if (flag)
                            {
                                flag = false;
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                curve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                            else if (num3 > num4)
                            {
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                curve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                        }
                        if (geometry2 is IPointCollection)
                        {
                            IPointCollection points = geometry2 as IPointCollection;
                            for (int i = 0; i < points.PointCount; i++)
                            {
                                IPoint point3 = points.get_Point(i);
                                num4 = Common.distance(point3, shape);
                                if (flag)
                                {
                                    flag = false;
                                    num3 = num4;
                                    splitPoint = point3;
                                    curve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                                else if (num3 > num4)
                                {
                                    num3 = num4;
                                    splitPoint = point3;
                                    curve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                            }
                        }
                    }
                    feature3 = o.NextFeature();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (curve != null)
                {
                    bool flag2;
                    int num6;
                    int num7;
                    polyline.SplitAtPoint(splitPoint, true, false, out flag2, out num6, out num7);
                    if (num < num2)
                    {
                        if (num6 > 0)
                        {
                            (polyline as IGeometryCollection).RemoveGeometries(0, num6);
                        }
                        if (num7 > 0)
                        {
                            (polyline as ISegmentCollection).RemoveSegments(0, num7, false);
                        }
                    }
                    else
                    {
                        if (num6 > 0)
                        {
                            (polyline as IGeometryCollection).RemoveGeometries(num6 + 1, (polyline as IGeometryCollection).GeometryCount - num6);
                        }
                        if (num7 > 0)
                        {
                            (polyline as ISegmentCollection).RemoveSegments(num7 + 1, (polyline as ISegmentCollection).SegmentCount - num7, false);
                        }
                    }
                    if (polyline.IsEmpty)
                    {
                        feature.Delete();
                    }
                    else
                    {
                        feature.Shape = polyline;
                        feature.Store();
                    }
                    (curve as IPolycurve).SplitAtPoint(splitPoint, true, false, out flag2, out num6, out num7);
                    feature4.Shape = curve;
                    feature4.Store();
                    return true;
                }
            }
            return false;
        }

        private void TrimLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            IWorkspaceEdit workspace = (this.m_TopologyLayer.Topology as IDataset).Workspace as IWorkspaceEdit;
            IEnvelope areaToValidate = null;
            workspace.StartEditOperation();
            for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
            {
                ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                if (areaToValidate == null)
                {
                    areaToValidate = (tag as IFeature).Shape.Envelope;
                }
                else
                {
                    areaToValidate.Union((tag as IFeature).Shape.Envelope);
                }
                if (this.Trim(tag))
                {
                    this.m_pTopoErroeSelection.Remove(tag);
                    this.listViewError.Items.Remove(this.listViewError.SelectedItems[i]);
                }
            }
            if (areaToValidate != null)
            {
                this.m_TopologyLayer.Topology.ValidateTopology(areaToValidate);
            }
            (this.m_pFocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, areaToValidate);
            workspace.StopEditOperation();
        }

        private void UpdateUI()
        {
            this.ZoomTo.Enabled = this.listViewError.SelectedItems.Count > 0;
            this.SelectFeature.Enabled = this.listViewError.SelectedItems.Count > 0;
            this.PanTo.Enabled = this.listViewError.SelectedItems.Count == 1;
            bool flag = false;
            bool flag2 = false;
            if (this.listViewError.SelectedItems.Count > 0)
            {
                int num = 0;
                int num2 = 0;
                for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
                {
                    ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                    if (tag.IsException)
                    {
                        num++;
                    }
                    else
                    {
                        num2++;
                    }
                }
                if ((num == 0) && (num2 > 0))
                {
                    flag = true;
                }
                else if ((num > 0) && (num2 == 0))
                {
                    flag2 = true;
                }
            }
            this.PromoteToRuleException.Enabled = flag;
            this.DemoteFromRuleException.Enabled = flag2;
        }

        private void ZoomTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.listViewError.SelectedItems.Count > 0)
            {
                IArray array = new ArrayClass();
                for (int i = 0; i < this.listViewError.SelectedItems.Count; i++)
                {
                    ITopologyErrorFeature tag = this.listViewError.SelectedItems[i].Tag as ITopologyErrorFeature;
                    array.Add(tag);
                }
                Common.Zoom2Features(this.m_pFocusMap as IActiveView, array);
            }
        }

        public DockingStyle DefaultDockingStyle
        {
            get
            {
                return DockingStyle.Left;
            }
        }

        public IWorkspace EditWorkspace
        {
            set
            {
                this.m_pEditWorkspace = value;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pFocusMap = value;
                this.m_pTopoErroeSelection.ActiveView = value as IActiveView;
            }
        }

        string IDockContent.Name
        {
            get
            {
                return base.Name;
            }
        }

        int IDockContent.Width
        {
            get
            {
                return base.Width;
            }
        }

        public ITopologyLayer TopologyLayer
        {
            set
            {
                this.m_TopologyLayer = value;
                if (this.m_CanDo)
                {
                    this.Init();
                }
            }
        }

        private class TopologyRuleWrap
        {
            private IFeatureClassContainer m_pFeatClassCont = null;
            private ITopologyRule m_pRule = null;

            public TopologyRuleWrap(ITopologyRule pRule, IFeatureClassContainer pFeatClassCont)
            {
                this.m_pRule = pRule;
                this.m_pFeatClassCont = pFeatClassCont;
            }

            private string GetRuleDescription(esriTopologyRuleType type)
            {
                switch (type)
                {
                    case esriTopologyRuleType.esriTRTAny:
                        return "所有错误";

                    case esriTopologyRuleType.esriTRTFeatureLargerThanClusterTolerance:
                        return "必需大于集束容限值";

                    case esriTopologyRuleType.esriTRTAreaNoGaps:
                        return "面不能有缝隙";

                    case esriTopologyRuleType.esriTRTAreaNoOverlap:
                        return "面不能重叠";

                    case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                        return "面必须被面要素类覆盖";

                    case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                        return "面必须和其它面要素层相互覆盖";

                    case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                        return "面必须被面覆盖";

                    case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                        return "面不能与其他面层重叠";

                    case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                        return "线必须被面要素边界线覆盖";

                    case esriTopologyRuleType.esriTRTPointCoveredByAreaBoundary:
                        return "点必须被面要素边界线覆盖";

                    case esriTopologyRuleType.esriTRTPointProperlyInsideArea:
                        return "点落在面要素内";

                    case esriTopologyRuleType.esriTRTLineNoOverlap:
                        return "线不能重叠";

                    case esriTopologyRuleType.esriTRTLineNoIntersection:
                        return "线不能相交";

                    case esriTopologyRuleType.esriTRTLineNoDangles:
                        return "线不能有悬挂点";

                    case esriTopologyRuleType.esriTRTLineNoPseudos:
                        return "线不能有伪节点";

                    case esriTopologyRuleType.esriTRTLineCoveredByLineClass:
                        return "线必须被线要素覆盖";

                    case esriTopologyRuleType.esriTRTLineNoOverlapLine:
                        return "线与线不能重叠";

                    case esriTopologyRuleType.esriTRTPointCoveredByLine:
                        return "点必须被线要素覆盖";

                    case esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint:
                        return "点必须被线要素终点覆盖";

                    case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                        return "面边界线必须被线要素覆盖";

                    case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByAreaBoundary:
                        return "面边界线必须被其它面层边界线覆盖";

                    case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                        return "线不能自重叠";

                    case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                        return "线不能自相交";

                    case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                        return "线不能相交或内部相接";

                    case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                        return "线终点必须被点要素覆盖";

                    case esriTopologyRuleType.esriTRTAreaContainPoint:
                        return "面包含点";

                    case esriTopologyRuleType.esriTRTLineNoMultipart:
                        return "线必须为单部分";
                }
                return "所有错误";
            }

            public override string ToString()
            {
                string aliasName = this.m_pFeatClassCont.get_ClassByID(this.m_pRule.OriginClassID).AliasName;
                if ((this.m_pRule.DestinationClassID != 0) && (this.m_pRule.DestinationClassID != this.m_pRule.OriginClassID))
                {
                    aliasName = aliasName + "和" + this.m_pFeatClassCont.get_ClassByID(this.m_pRule.DestinationClassID).AliasName;
                }
                return (aliasName + this.GetRuleDescription(this.m_pRule.TopologyRuleType));
            }

            public ITopologyRule TopologyRule
            {
                get
                {
                    return this.m_pRule;
                }
            }
        }
    }
}

