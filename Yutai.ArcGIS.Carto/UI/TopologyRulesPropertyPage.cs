using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public class TopologyRulesPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private SimpleButton btnDescription;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Container container_0 = null;
        private ITopology itopology_0 = null;
        private ITopologyLayer itopologyLayer_0 = null;
        private ListView listView1;

        public TopologyRulesPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            return true;
        }

        private void btnDescription_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnDescription = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(8, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x128, 0xd0);
            this.listView1.TabIndex = 0;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 0x4c;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 0x87;
            this.columnHeader_2.Text = "要素类";
            this.columnHeader_2.Width = 0x4b;
            this.btnDescription.Enabled = false;
            this.btnDescription.Location = new Point(0x138, 8);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new Size(0x40, 0x20);
            this.btnDescription.TabIndex = 1;
            this.btnDescription.Text = "描述";
            this.btnDescription.Visible = false;
            this.btnDescription.Click += new EventHandler(this.btnDescription_Click);
            base.Controls.Add(this.btnDescription);
            base.Controls.Add(this.listView1);
            base.Name = "TopologyRulesPropertyPage";
            base.Size = new Size(0x188, 0x110);
            base.Load += new EventHandler(this.TopologyRulesPropertyPage_Load);
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDescription.Enabled = true;
            }
            else
            {
                this.btnDescription.Enabled = false;
            }
        }

        private string method_0(esriTopologyRuleType esriTopologyRuleType_0)
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

        private void method_1()
        {
            try
            {
                this.listView1.Items.Clear();
                string[] items = new string[3];
                ITopologyRuleContainer container = this.itopology_0 as ITopologyRuleContainer;
                IEnumRule rules = container.Rules;
                rules.Reset();
                for (IRule rule2 = rules.Next(); rule2 != null; rule2 = rules.Next())
                {
                    items[0] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID((rule2 as ITopologyRule).OriginClassID).AliasName;
                    items[1] = this.method_0((rule2 as ITopologyRule).TopologyRuleType);
                    if (((rule2 as ITopologyRule).OriginClassID != (rule2 as ITopologyRule).DestinationClassID) && ((rule2 as ITopologyRule).DestinationClassID != 0))
                    {
                        items[2] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID((rule2 as ITopologyRule).DestinationClassID).AliasName;
                    }
                    ListViewItem item = new ListViewItem(items) {
                        Tag = new TopologyRuleWrap(rule2 as ITopologyRule, false)
                    };
                    this.listView1.Items.Add(item);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void TopologyRulesPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.itopologyLayer_0 = value as ITopologyLayer;
                this.itopology_0 = this.itopologyLayer_0.Topology;
            }
        }

        internal class TopologyRuleWrap
        {
            private bool bool_0 = false;
            private ITopologyRule itopologyRule_0 = null;

            public TopologyRuleWrap(ITopologyRule itopologyRule_1, bool bool_1)
            {
                this.itopologyRule_0 = itopologyRule_1;
                this.bool_0 = bool_1;
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public ITopologyRule TopoRule
            {
                get
                {
                    return this.itopologyRule_0;
                }
            }
        }
    }
}

