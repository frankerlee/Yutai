namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Utility;
    using JLK.Utility.BaseClass;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class TopologyRulesPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private SimpleButton btnAddRule;
        private SimpleButton btnAddRule1;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnDeleteRule;
        private SimpleButton btnDescription;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITopology itopology_0 = null;
        private ListView listView1;
        private string string_0 = "规则";

        public event OnValueChangeEventHandler OnValueChange;

        public TopologyRulesPropertyPage()
        {
            this.InitializeComponent();
            TopologyEditHelper.DeleteTopolyClass += new TopologyEditHelper.DeleteTopolyClassHandler(this.method_3);
            TopologyEditHelper.DeleteAllTopolyClass += new TopologyEditHelper.DeleteAllTopolyClassHandler(this.method_4);
        }

        public void Apply()
        {
            if (this.bool_0)
            {
                int num;
                this.bool_0 = false;
                ITopologyRuleContainer container = this.itopology_0 as ITopologyRuleContainer;
                for (num = 0; num < this.ilist_0.Count; num++)
                {
                    container.DeleteRule(this.ilist_0[num] as ITopologyRule);
                }
                this.ilist_0.Clear();
                for (num = this.listView1.Items.Count - 1; num >= 0; num--)
                {
                    ListViewItem item = this.listView1.Items[num];
                    if ((item.Tag as Class2).IsNew && container.get_CanAddRule((item.Tag as Class2).TopoRule))
                    {
                        try
                        {
                            container.AddRule((item.Tag as Class2).TopoRule);
                            (item.Tag as Class2).IsNew = false;
                        }
                        catch (Exception exception)
                        {
                            CErrorLog.writeErrorLog(this, exception, "");
                        }
                    }
                }
            }
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            frmRule rule = new frmRule {
                OriginClassArray = TopologyEditHelper.m_pList
            };
            if (rule.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!rule.bVaildRule)
                    {
                        MessageBox.Show("无效规则！");
                    }
                    else
                    {
                        ITopologyRule topologyRule = rule.TopologyRule;
                        if (!this.method_2(topologyRule))
                        {
                            string[] items = new string[3];
                            items[0] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID(topologyRule.OriginClassID).AliasName;
                            items[1] = this.method_0(topologyRule.TopologyRuleType);
                            if ((topologyRule.OriginClassID != topologyRule.DestinationClassID) && (topologyRule.DestinationClassID > 0))
                            {
                                items[2] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID(topologyRule.DestinationClassID).AliasName;
                            }
                            ListViewItem item = new ListViewItem(items) {
                                Tag = new Class2(topologyRule, true)
                            };
                            this.listView1.Items.Add(item);
                            this.bool_0 = true;
                            if (this.onValueChangeEventHandler_0 != null)
                            {
                                this.onValueChangeEventHandler_0();
                            }
                        }
                        else
                        {
                            MessageBox.Show("该规则已应用到该要素类！");
                        }
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }
        }

        private void btnAddRule1_Click(object sender, EventArgs e)
        {
            frmAddRuleByClass class2 = new frmAddRuleByClass {
                OriginClassArray = TopologyEditHelper.m_pList
            };
            if (class2.ShowDialog() == DialogResult.OK)
            {
                ITopologyRule[] topologyRules = class2.TopologyRules;
                if (topologyRules != null)
                {
                    for (int i = 0; i < topologyRules.Length; i++)
                    {
                        ITopologyRule rule = topologyRules[i];
                        if (!this.method_2(rule))
                        {
                            string[] items = new string[3];
                            items[0] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID(rule.OriginClassID).AliasName;
                            items[1] = this.method_0(rule.TopologyRuleType);
                            if ((rule.OriginClassID != rule.DestinationClassID) && (rule.DestinationClassID > 0))
                            {
                                items[2] = (this.itopology_0 as IFeatureClassContainer).get_ClassByID(rule.DestinationClassID).AliasName;
                            }
                            ListViewItem item = new ListViewItem(items) {
                                Tag = new Class2(rule, true)
                            };
                            this.listView1.Items.Add(item);
                            this.bool_0 = true;
                            if (this.onValueChangeEventHandler_0 != null)
                            {
                                this.onValueChangeEventHandler_0();
                            }
                        }
                    }
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.Items.Count > 0)
                {
                    for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = this.listView1.Items[i];
                        if (!(item.Tag as Class2).IsNew)
                        {
                            this.ilist_0.Add((item.Tag as Class2).TopoRule);
                        }
                        this.listView1.Items.Remove(item);
                    }
                    this.bool_0 = true;
                    if (this.onValueChangeEventHandler_0 != null)
                    {
                        this.onValueChangeEventHandler_0();
                    }
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void btnDeleteRule_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem item = this.listView1.SelectedItems[i];
                        if (!(item.Tag as Class2).IsNew)
                        {
                            this.ilist_0.Add((item.Tag as Class2).TopoRule);
                        }
                        this.listView1.Items.Remove(item);
                    }
                    this.bool_0 = true;
                    if (this.onValueChangeEventHandler_0 != null)
                    {
                        this.onValueChangeEventHandler_0();
                    }
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private void btnDescription_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                frmRuleInfo info = new frmRuleInfo();
                ListViewItem item = this.listView1.SelectedItems[0];
                info.TopologyRule = (item.Tag as Class2).TopoRule;
                info.ShowDialog();
            }
        }

        public void Cancel()
        {
            this.ilist_0.Clear();
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
            this.btnAddRule = new SimpleButton();
            this.btnDeleteRule = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.btnAddRule1 = new SimpleButton();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(8, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x128, 0xd0);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 0x4c;
            this.columnHeader_1.Text = "规则";
            this.columnHeader_1.Width = 0x87;
            this.columnHeader_2.Text = "要素类";
            this.columnHeader_2.Width = 0x4b;
            this.btnDescription.Location = new Point(0x138, 8);
            this.btnDescription.Name = "btnDescription";
            this.btnDescription.Size = new Size(0x60, 0x20);
            this.btnDescription.TabIndex = 1;
            this.btnDescription.Text = "描述";
            this.btnDescription.Click += new EventHandler(this.btnDescription_Click);
            this.btnAddRule.Location = new Point(0x138, 0x30);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new Size(0x60, 0x20);
            this.btnAddRule.TabIndex = 2;
            this.btnAddRule.Text = "按类添加规则";
            this.btnAddRule.Click += new EventHandler(this.btnAddRule_Click);
            this.btnDeleteRule.Enabled = false;
            this.btnDeleteRule.Location = new Point(0x138, 0x7d);
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new Size(0x60, 0x20);
            this.btnDeleteRule.TabIndex = 3;
            this.btnDeleteRule.Text = "删除";
            this.btnDeleteRule.Click += new EventHandler(this.btnDeleteRule_Click);
            this.btnDeleteAll.Location = new Point(0x138, 0xa5);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(0x60, 0x20);
            this.btnDeleteAll.TabIndex = 4;
            this.btnDeleteAll.Text = "全部删除";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            this.btnAddRule1.Location = new Point(310, 0x56);
            this.btnAddRule1.Name = "btnAddRule1";
            this.btnAddRule1.Size = new Size(0x62, 0x20);
            this.btnAddRule1.TabIndex = 5;
            this.btnAddRule1.Text = "添加规则";
            this.btnAddRule1.Click += new EventHandler(this.btnAddRule1_Click);
            base.Controls.Add(this.btnAddRule1);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDeleteRule);
            base.Controls.Add(this.btnAddRule);
            base.Controls.Add(this.btnDescription);
            base.Controls.Add(this.listView1);
            base.Name = "TopologyRulesPropertyPage";
            base.Size = new Size(0x19b, 0x110);
            base.Load += new EventHandler(this.TopologyRulesPropertyPage_Load);
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.btnDeleteRule.Enabled = true;
            }
            else
            {
                this.btnDeleteRule.Enabled = false;
            }
            if (this.listView1.SelectedItems.Count == 1)
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
                        Tag = new Class2(rule2 as ITopologyRule, false)
                    };
                    this.listView1.Items.Add(item);
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
        }

        private bool method_2(ITopologyRule itopologyRule_0)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ITopologyRule topoRule = (this.listView1.Items[i].Tag as Class2).TopoRule;
                if (((topoRule.Name == itopologyRule_0.Name) && (topoRule.OriginClassID == itopologyRule_0.OriginClassID)) && (topoRule.DestinationClassID == itopologyRule_0.DestinationClassID))
                {
                    return true;
                }
            }
            return false;
        }

        private void method_3(int int_0)
        {
            IEnumRule rule = (this.itopology_0 as ITopologyRuleContainer).get_RulesByClass(int_0);
            for (IRule rule2 = rule.Next(); rule2 != null; rule2 = rule.Next())
            {
                int index = this.listView1.Items.Count - 1;
                while (index >= 0)
                {
                    ListViewItem item = this.listView1.Items[index];
                    Class2 tag = item.Tag as Class2;
                    if (tag.TopoRule == rule2)
                    {
                        goto Label_0071;
                    }
                    index--;
                }
                continue;
            Label_0071:
                this.listView1.Items.RemoveAt(index);
            }
        }

        private void method_4()
        {
            this.listView1.Items.Clear();
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.itopology_0 = object_0 as ITopology;
        }

        private void TopologyRulesPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        private class Class2
        {
            private bool bool_0 = false;
            private ITopologyRule itopologyRule_0 = null;

            public Class2(ITopologyRule itopologyRule_1, bool bool_1)
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

