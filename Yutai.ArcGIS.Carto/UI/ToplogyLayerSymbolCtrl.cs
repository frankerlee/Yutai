using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using ItemCheckEventArgs = DevExpress.XtraEditors.Controls.ItemCheckEventArgs;
using ItemCheckEventHandler = DevExpress.XtraEditors.Controls.ItemCheckEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class ToplogyLayerSymbolCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IList ilist_0 = new ArrayList();
        private IList ilist_1 = new ArrayList();
        private IList ilist_2 = new ArrayList();
        private ITopologyLayer itopologyLayer_0 = null;
        public IStyleGallery m_pSG = null;

        public ToplogyLayerSymbolCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                for (int i = 0; i < this.chkListRender.Items.Count; i++)
                {
                    CheckedListBoxItem item = this.chkListRender.Items[i];
                    if (item.CheckState == CheckState.Checked)
                    {
                        this.itopologyLayer_0.set_Renderer((esriTopologyRenderer) i, this.ilist_0[i] as IFeatureRenderer);
                    }
                    else
                    {
                        this.itopologyLayer_0.set_Renderer((esriTopologyRenderer) i, null);
                    }
                }
            }
            return true;
        }

        private void btnSymbol_Click(object sender, EventArgs e)
        {
            if (this.chkListRender.SelectedIndex > -1)
            {
                try
                {
                    frmSymbolSelector selector = new frmSymbolSelector();
                    if (selector != null)
                    {
                        selector.SetStyleGallery(this.m_pSG);
                        selector.SetSymbol(this.btnSymbol.Style);
                        if (selector.ShowDialog() == DialogResult.OK)
                        {
                            this.btnSymbol.Style = selector.GetSymbol();
                            IFeatureRenderer renderer =
                                this.ilist_1[this.chkListRender.SelectedIndex] as IFeatureRenderer;
                            (renderer as ISimpleRenderer).Symbol = this.btnSymbol.Style as ISymbol;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void chkListRender_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.bool_1 = true;
        }

        private void chkListRender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.listView1.Items.Clear();
                this.btnSymbol.Style = null;
                if (this.chkListRender.SelectedIndex > -1)
                {
                    if (this.ilist_0.Count == 0)
                    {
                        return;
                    }
                    IFeatureRenderer renderer = this.ilist_0[this.chkListRender.SelectedIndex] as IFeatureRenderer;
                    this.bool_0 = false;
                    this.rdoRenderType.SelectedIndex = (renderer is ISimpleRenderer) ? 0 : 1;
                    renderer = this.ilist_1[this.chkListRender.SelectedIndex] as IFeatureRenderer;
                    this.btnSymbol.Style = (renderer as ISimpleRenderer).Symbol;
                    renderer = this.ilist_2[this.chkListRender.SelectedIndex] as IFeatureRenderer;
                    if (renderer != null)
                    {
                        this.listView1.Enabled = true;
                        object[] objArray = new object[2];
                        this.rdoRenderType.SelectedIndex = 1;
                        for (int i = 0; i < (renderer as IUniqueValueRenderer).ValueCount; i++)
                        {
                            string str = (renderer as IUniqueValueRenderer).get_Value(i);
                            ISymbol symbol = (renderer as IUniqueValueRenderer).get_Symbol(str);
                            objArray[0] = symbol;
                            objArray[1] = (renderer as IUniqueValueRenderer).get_Label(str);
                            this.listView1.Add(objArray);
                        }
                    }
                    else
                    {
                        this.listView1.Enabled = false;
                    }
                    this.bool_0 = true;
                }
                this.btnSymbol.Invalidate();
            }
        }

        private void method_0()
        {
            IFeatureRenderer pInObject = null;
            IFillSymbol symbol;
            ILineSymbol symbol2;
            IObjectCopy copy;
            ISimpleMarkerSymbol symbol3;
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRAreaErrors);
            this.chkListRender.Items.Add("面错误", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol = new SimpleFillSymbolClass
                {
                    Color = ColorManage.CreatColor(255, 128, 128)
                };
                symbol2 = new SimpleLineSymbolClass
                {
                    Width = 2.5,
                    Color = ColorManage.CreatColor(255, 0, 0)
                };
                symbol.Outline = symbol2;
                (pInObject as ISimpleRenderer).Symbol = symbol as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRAreaErrors));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRAreaErrors));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRLineErrors);
            this.chkListRender.Items.Add("线错误", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol2 = new SimpleLineSymbolClass
                {
                    Width = 2.5,
                    Color = ColorManage.CreatColor(255, 128, 128)
                };
                (pInObject as ISimpleRenderer).Symbol = symbol2 as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRLineErrors));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRLineErrors));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRPointErrors);
            this.chkListRender.Items.Add("点错误", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol3 = new SimpleMarkerSymbolClass
                {
                    Size = 6.0,
                    Style = esriSimpleMarkerStyle.esriSMSSquare,
                    Color = ColorManage.CreatColor(255, 128, 128)
                };
                (pInObject as ISimpleRenderer).Symbol = symbol3 as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRPointErrors));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRPointErrors));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRAreaExceptions);
            this.chkListRender.Items.Add("面例外", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol = new SimpleFillSymbolClass
                {
                    Color = ColorManage.CreatColor(255, 255, 255)
                };
                symbol2 = new SimpleLineSymbolClass
                {
                    Width = 1.0,
                    Color = ColorManage.CreatColor(128, 255, 128)
                };
                symbol.Outline = symbol2;
                (pInObject as ISimpleRenderer).Symbol = symbol as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRAreaExceptions));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRAreaExceptions));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRLineExceptions);
            this.chkListRender.Items.Add("线例外", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol2 = new SimpleLineSymbolClass
                {
                    Width = 1.0,
                    Color = ColorManage.CreatColor(128, 255, 128)
                };
                (pInObject as ISimpleRenderer).Symbol = symbol2 as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRLineExceptions));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRLineExceptions));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRPointExceptions);
            this.chkListRender.Items.Add("点例外", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol3 = new SimpleMarkerSymbolClass
                {
                    Size = 5.0,
                    Style = esriSimpleMarkerStyle.esriSMSSquare,
                    Color = ColorManage.CreatColor(128, 255, 128)
                };
                (pInObject as ISimpleRenderer).Symbol = symbol3 as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology,
                    esriTopologyRenderer.esriTRPointExceptions));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRPointExceptions));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
            pInObject = this.itopologyLayer_0.get_Renderer(esriTopologyRenderer.esriTRDirtyAreas);
            this.chkListRender.Items.Add("脏区", pInObject != null);
            if (pInObject == null)
            {
                pInObject = new SimpleRendererClass();
                symbol = new SimpleFillSymbolClass();
                (symbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                symbol.Color = ColorManage.CreatColor(128, 128, 255);
                symbol2 = new SimpleLineSymbolClass
                {
                    Width = 1.0,
                    Color = ColorManage.CreatColor(128, 128, 255)
                };
                symbol.Outline = symbol2;
                (pInObject as ISimpleRenderer).Symbol = symbol as ISymbol;
            }
            else
            {
                copy = new ObjectCopyClass();
                pInObject = copy.Copy(pInObject) as IFeatureRenderer;
            }
            if (pInObject is ISimpleRenderer)
            {
                this.ilist_1.Add(pInObject);
                this.ilist_2.Add(this.method_6(this.itopologyLayer_0.Topology, esriTopologyRenderer.esriTRDirtyAreas));
            }
            else
            {
                this.ilist_1.Add(this.method_1(esriTopologyRenderer.esriTRDirtyAreas));
                this.ilist_2.Add(pInObject);
            }
            this.ilist_0.Add(pInObject);
        }

        private IFeatureRenderer method_1(esriTopologyRenderer esriTopologyRenderer_0)
        {
            IFeatureRenderer renderer = null;
            IFillSymbol symbol;
            ILineSymbol symbol2;
            ISimpleMarkerSymbol symbol3;
            switch (esriTopologyRenderer_0)
            {
                case esriTopologyRenderer.esriTRAreaErrors:
                    renderer = new SimpleRendererClass();
                    symbol = new SimpleFillSymbolClass
                    {
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 2.5,
                        Color = ColorManage.CreatColor(255, 0, 0)
                    };
                    symbol.Outline = symbol2;
                    (renderer as ISimpleRenderer).Symbol = symbol as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRLineErrors:
                    renderer = new SimpleRendererClass();
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 2.5,
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    (renderer as ISimpleRenderer).Symbol = symbol2 as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRPointErrors:
                    renderer = new SimpleRendererClass();
                    symbol3 = new SimpleMarkerSymbolClass
                    {
                        Size = 6.0,
                        Style = esriSimpleMarkerStyle.esriSMSSquare,
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    (renderer as ISimpleRenderer).Symbol = symbol3 as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRAreaExceptions:
                    renderer = new SimpleRendererClass();
                    symbol = new SimpleFillSymbolClass
                    {
                        Color = ColorManage.CreatColor(255, 255, 255)
                    };
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 1.0,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    symbol.Outline = symbol2;
                    (renderer as ISimpleRenderer).Symbol = symbol as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRLineExceptions:
                    renderer = new SimpleRendererClass();
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 1.0,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    (renderer as ISimpleRenderer).Symbol = symbol2 as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRPointExceptions:
                    renderer = new SimpleRendererClass();
                    symbol3 = new SimpleMarkerSymbolClass
                    {
                        Size = 5.0,
                        Style = esriSimpleMarkerStyle.esriSMSSquare,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    (renderer as ISimpleRenderer).Symbol = symbol3 as ISymbol;
                    return renderer;

                case esriTopologyRenderer.esriTRDirtyAreas:
                    renderer = new SimpleRendererClass();
                    symbol = new SimpleFillSymbolClass();
                    (symbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    symbol.Color = ColorManage.CreatColor(128, 128, 255);
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 1.0,
                        Color = ColorManage.CreatColor(128, 128, 255)
                    };
                    symbol.Outline = symbol2;
                    (renderer as ISimpleRenderer).Symbol = symbol as ISymbol;
                    return renderer;
            }
            return renderer;
        }

        private IList method_2(ITopology itopology_0)
        {
            IList list = new ArrayList();
            ITopologyRuleContainer container = itopology_0 as ITopologyRuleContainer;
            IEnumRule rules = container.Rules;
            rules.Reset();
            for (ITopologyRule rule2 = rules.Next() as ITopologyRule;
                rule2 != null;
                rule2 = rules.Next() as ITopologyRule)
            {
                bool flag;
                bool flag2;
                bool flag3;
                rule2.ErrorShapeTypes(out flag, out flag2, out flag3);
                if (flag)
                {
                    list.Add(rule2);
                }
            }
            return list;
        }

        private IList method_3(ITopology itopology_0)
        {
            IList list = new ArrayList();
            ITopologyRuleContainer container = itopology_0 as ITopologyRuleContainer;
            IEnumRule rules = container.Rules;
            rules.Reset();
            for (ITopologyRule rule2 = rules.Next() as ITopologyRule;
                rule2 != null;
                rule2 = rules.Next() as ITopologyRule)
            {
                bool flag;
                bool flag2;
                bool flag3;
                rule2.ErrorShapeTypes(out flag, out flag2, out flag3);
                if (flag2)
                {
                    list.Add(rule2);
                }
            }
            return list;
        }

        private IList method_4(ITopology itopology_0)
        {
            IList list = new ArrayList();
            ITopologyRuleContainer container = itopology_0 as ITopologyRuleContainer;
            IEnumRule rules = container.Rules;
            rules.Reset();
            for (ITopologyRule rule2 = rules.Next() as ITopologyRule;
                rule2 != null;
                rule2 = rules.Next() as ITopologyRule)
            {
                bool flag;
                bool flag2;
                bool flag3;
                rule2.ErrorShapeTypes(out flag, out flag2, out flag3);
                if (flag3)
                {
                    list.Add(rule2);
                }
            }
            return list;
        }

        private string method_5(esriTopologyRuleType esriTopologyRuleType_0)
        {
            switch (esriTopologyRuleType_0)
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

        private IFeatureRenderer method_6(ITopology itopology_0, esriTopologyRenderer esriTopologyRenderer_0)
        {
            IUniqueValueRenderer renderer = null;
            IList list;
            IFillSymbol symbol;
            ILineSymbol symbol2;
            bool flag;
            IEnumColors colors;
            IFillSymbol symbol3;
            int num;
            ITopologyRule rule;
            int topologyRuleType;
            ILineSymbol symbol4;
            ISimpleMarkerSymbol symbol5;
            ISimpleMarkerSymbol symbol6;
            IRandomColorRamp ramp = new RandomColorRampClass
            {
                StartHue = 40,
                EndHue = 120,
                MinValue = 65,
                MaxValue = 90,
                MinSaturation = 25,
                MaxSaturation = 45,
                Size = 5,
                Seed = 23
            };
            switch (esriTopologyRenderer_0)
            {
                case esriTopologyRenderer.esriTRAreaErrors:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_4(itopology_0);
                    symbol = new SimpleFillSymbolClass
                    {
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 2.5,
                        Color = ColorManage.CreatColor(255, 0, 0)
                    };
                    symbol.Outline = symbol2;
                    renderer.DefaultSymbol = (symbol as IClone).Clone() as ISymbol;
                    ramp.Size = list.Count + 2;
                    ramp.CreateRamp(out flag);
                    colors = ramp.Colors;
                    colors.Reset();
                    symbol3 = (symbol as IClone).Clone() as IFillSymbol;
                    symbol3.Color = colors.Next();
                    renderer.AddValue("0", "必须大于集束容限值", symbol3 as ISymbol);
                    renderer.set_Label("0", "必须大于集束容限值");
                    for (num = 0; num < list.Count; num++)
                    {
                        rule = list[num] as ITopologyRule;
                        symbol3 = (symbol as IClone).Clone() as IFillSymbol;
                        symbol3.Color = colors.Next();
                        topologyRuleType = (int) rule.TopologyRuleType;
                        renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                            symbol3 as ISymbol);
                        renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                    }
                    break;

                case esriTopologyRenderer.esriTRLineErrors:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_3(itopology_0);
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 2.5,
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    renderer.DefaultSymbol = (symbol2 as IClone).Clone() as ISymbol;
                    ramp.Size = list.Count + 2;
                    ramp.CreateRamp(out flag);
                    colors = ramp.Colors;
                    colors.Reset();
                    symbol4 = (symbol2 as IClone).Clone() as ILineSymbol;
                    symbol4.Color = colors.Next();
                    renderer.AddValue("0", "必须大于集束容限值", symbol4 as ISymbol);
                    renderer.set_Label("0", "必须大于集束容限值");
                    for (num = 0; num < list.Count; num++)
                    {
                        rule = list[num] as ITopologyRule;
                        symbol4 = (symbol2 as IClone).Clone() as ILineSymbol;
                        symbol4.Color = colors.Next();
                        topologyRuleType = (int) rule.TopologyRuleType;
                        renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                            symbol4 as ISymbol);
                        renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                    }
                    break;

                case esriTopologyRenderer.esriTRPointErrors:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_2(itopology_0);
                    symbol5 = new SimpleMarkerSymbolClass
                    {
                        Size = 6.0,
                        Style = esriSimpleMarkerStyle.esriSMSSquare,
                        Color = ColorManage.CreatColor(255, 128, 128)
                    };
                    renderer.DefaultSymbol = (symbol5 as IClone).Clone() as ISymbol;
                    if (list.Count > 0)
                    {
                        ramp.Size = list.Count + 1;
                        ramp.CreateRamp(out flag);
                        colors = ramp.Colors;
                        colors.Reset();
                        symbol6 = null;
                        for (num = 0; num < list.Count; num++)
                        {
                            rule = list[num] as ITopologyRule;
                            symbol6 = (symbol5 as IClone).Clone() as ISimpleMarkerSymbol;
                            symbol6.Color = colors.Next();
                            topologyRuleType = (int) rule.TopologyRuleType;
                            renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                                symbol6 as ISymbol);
                            renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                        }
                    }
                    break;

                case esriTopologyRenderer.esriTRAreaExceptions:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_4(itopology_0);
                    symbol = new SimpleFillSymbolClass
                    {
                        Color = ColorManage.CreatColor(255, 255, 255)
                    };
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 1.0,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    symbol.Outline = symbol2;
                    renderer.DefaultSymbol = (symbol as IClone).Clone() as ISymbol;
                    ramp.Size = list.Count + 2;
                    ramp.CreateRamp(out flag);
                    colors = ramp.Colors;
                    colors.Reset();
                    symbol3 = (symbol as IClone).Clone() as IFillSymbol;
                    symbol3.Color = colors.Next();
                    renderer.AddValue("0", "必须大于集束容限值", symbol3 as ISymbol);
                    renderer.set_Label("0", "必须大于集束容限值");
                    for (num = 0; num < list.Count; num++)
                    {
                        rule = list[num] as ITopologyRule;
                        symbol3 = (symbol as IClone).Clone() as IFillSymbol;
                        symbol3.Color = colors.Next();
                        topologyRuleType = (int) rule.TopologyRuleType;
                        renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                            symbol3 as ISymbol);
                        renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                    }
                    break;

                case esriTopologyRenderer.esriTRLineExceptions:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_3(itopology_0);
                    symbol2 = new SimpleLineSymbolClass
                    {
                        Width = 1.0,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    renderer.DefaultSymbol = (symbol2 as IClone).Clone() as ISymbol;
                    ramp.Size = list.Count + 2;
                    ramp.CreateRamp(out flag);
                    colors = ramp.Colors;
                    colors.Reset();
                    symbol4 = (symbol2 as IClone).Clone() as ILineSymbol;
                    symbol4.Color = colors.Next();
                    renderer.AddValue("0", "必须大于集束容限值", symbol4 as ISymbol);
                    renderer.set_Label("0", "必须大于集束容限值");
                    for (num = 0; num < list.Count; num++)
                    {
                        rule = list[num] as ITopologyRule;
                        symbol4 = (symbol2 as IClone).Clone() as ILineSymbol;
                        symbol4.Color = colors.Next();
                        topologyRuleType = (int) rule.TopologyRuleType;
                        renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                            symbol4 as ISymbol);
                        renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                    }
                    break;

                case esriTopologyRenderer.esriTRPointExceptions:
                    renderer = new UniqueValueRendererClass();
                    list = this.method_2(itopology_0);
                    symbol5 = new SimpleMarkerSymbolClass
                    {
                        Size = 5.0,
                        Style = esriSimpleMarkerStyle.esriSMSSquare,
                        Color = ColorManage.CreatColor(128, 255, 128)
                    };
                    renderer.DefaultSymbol = (symbol5 as IClone).Clone() as ISymbol;
                    if (list.Count > 0)
                    {
                        ramp.Size = list.Count + 1;
                        ramp.CreateRamp(out flag);
                        colors = ramp.Colors;
                        colors.Reset();
                        symbol6 = null;
                        for (num = 0; num < list.Count; num++)
                        {
                            rule = list[num] as ITopologyRule;
                            symbol6 = (symbol5 as IClone).Clone() as ISimpleMarkerSymbol;
                            symbol6.Color = colors.Next();
                            topologyRuleType = (int) rule.TopologyRuleType;
                            renderer.AddValue(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType),
                                symbol6 as ISymbol);
                            renderer.set_Label(topologyRuleType.ToString(), this.method_5(rule.TopologyRuleType));
                        }
                    }
                    break;
            }
            return (renderer as IFeatureRenderer);
        }

        private void rdoRenderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.chkListRender.SelectedIndex > -1)
                {
                    if (this.chkListRender.SelectedIndex != 6)
                    {
                        if (this.rdoRenderType.SelectedIndex == 0)
                        {
                            this.ilist_0[this.chkListRender.SelectedIndex] =
                                this.ilist_1[this.chkListRender.SelectedIndex];
                        }
                        else
                        {
                            this.ilist_0[this.chkListRender.SelectedIndex] =
                                this.ilist_2[this.chkListRender.SelectedIndex];
                        }
                    }
                    else if (this.rdoRenderType.SelectedIndex == 1)
                    {
                        this.bool_0 = false;
                        MessageBox.Show("脏区只能使用单一符号显示!");
                        this.rdoRenderType.SelectedIndex = 0;
                        this.bool_0 = true;
                    }
                }
            }
        }

        private void ToplogyLayerSymbolCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public object SelectItem
        {
            set { this.itopologyLayer_0 = value as ITopologyLayer; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
                this.listView1.StyleGallery = this.m_pSG;
            }
        }
    }
}