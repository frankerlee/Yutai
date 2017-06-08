namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class frmCreateTopologySet : Form
    {
        private Button btnAddRule;
        private Button btnAddRule1;
        private Button btnCancel;
        private Button btnClearAllFeatureClass;
        private Button btnClearAllRule;
        private Button btnClearRule;
        private Button btnNext;
        private Button btnPrevious;
        private Button btnSelectAllFeatureClass;
        private Button btnZ;
        private CheckedListBox chkListBoxFeatureClass;
        private ComboBox comboBox_0 = new ComboBox();
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IArray iarray_2 = new ArrayClass();
        private int int_0 = 5;
        private int int_1;
        private Label lblLimit;
        private Label lblPri;
        private Label lblPriRang;
        private Label lblRule;
        private Label lblSelFeatureClass;
        private Label lblTopoName;
        private Label lblUnit;
        private ListView listRule;
        private ListViewItem listViewItem_0 = null;
        private ListView listViewPri;
        public IFeatureDataset m_pFeatDataset;
        private TextBox txtLimiteValue;
        private TextBox txtMaxPri;
        private TextBox txtTopologyName;

        public frmCreateTopologySet()
        {
            this.InitializeComponent();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            frmRule rule = new frmRule {
                OriginClassArray = this.iarray_1
            };
            if (rule.ShowDialog() == DialogResult.OK)
            {
                if (!rule.bVaildRule)
                {
                    MessageBox.Show("无效规则！");
                }
                else
                {
                    ITopologyRule topologyRule = rule.TopologyRule;
                    if (this.method_2(topologyRule))
                    {
                        MessageBox.Show("该规则已应用到该要素类！");
                    }
                    else
                    {
                        int num;
                        IFeatureClass class2;
                        for (num = 0; num < this.iarray_0.Count; num++)
                        {
                            class2 = (IFeatureClass) this.iarray_0.get_Element(num);
                            if (class2.ObjectClassID == topologyRule.OriginClassID)
                            {
                                this.listRule.Items.Add(class2.AliasName);
                                this.listRule.Items[this.listRule.Items.Count - 1].SubItems.Add(topologyRule.Name);
                                this.listRule.Items[this.listRule.Items.Count - 1].Tag = topologyRule;
                                break;
                            }
                        }
                        if (topologyRule.OriginClassID != topologyRule.DestinationClassID)
                        {
                            for (num = 0; num < this.iarray_0.Count; num++)
                            {
                                class2 = (IFeatureClass) this.iarray_0.get_Element(num);
                                if (class2.ObjectClassID == topologyRule.DestinationClassID)
                                {
                                    this.listRule.Items[this.listRule.Items.Count - 1].SubItems.Add(class2.AliasName);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnAddRule1_Click(object sender, EventArgs e)
        {
            frmAddRuleByClass class2 = new frmAddRuleByClass {
                OriginClassArray = this.iarray_1
            };
            if (class2.ShowDialog() == DialogResult.OK)
            {
                ITopologyRule[] topologyRules = class2.TopologyRules;
                if (topologyRules != null)
                {
                    for (int i = 0; i < topologyRules.Length; i++)
                    {
                        IFeatureClass class3;
                        ITopologyRule rule = topologyRules[i];
                        if (this.method_2(rule))
                        {
                            continue;
                        }
                        int index = 0;
                        while (index < this.iarray_0.Count)
                        {
                            class3 = (IFeatureClass) this.iarray_0.get_Element(index);
                            if (class3.ObjectClassID == rule.OriginClassID)
                            {
                                goto Label_0093;
                            }
                            index++;
                        }
                        goto Label_0106;
                    Label_0093:
                        this.listRule.Items.Add(class3.AliasName);
                        this.listRule.Items[this.listRule.Items.Count - 1].SubItems.Add(rule.Name);
                        this.listRule.Items[this.listRule.Items.Count - 1].Tag = rule;
                    Label_0106:
                        if (rule.OriginClassID != rule.DestinationClassID)
                        {
                            for (index = 0; index < this.iarray_0.Count; index++)
                            {
                                class3 = (IFeatureClass) this.iarray_0.get_Element(index);
                                if (class3.ObjectClassID == rule.DestinationClassID)
                                {
                                    goto Label_015C;
                                }
                            }
                        }
                        continue;
                    Label_015C:
                        this.listRule.Items[this.listRule.Items.Count - 1].SubItems.Add(class3.AliasName);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnClearAllFeatureClass_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkListBoxFeatureClass.Items.Count; i++)
            {
                this.chkListBoxFeatureClass.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void btnClearRule_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listRule.SelectedItems.Count; i++)
            {
                ITopologyRule tag = (ITopologyRule) this.listRule.SelectedItems[i].Tag;
                this.listRule.Items.Remove(this.listRule.SelectedItems[i]);
            }
            this.listRule.SelectedItems.Clear();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (this.int_1 != 4)
            {
                this.int_1++;
                this.method_0();
            }
            else
            {
                ITopology topology;
                Exception exception2;
                ITopologyContainer pFeatDataset = (ITopologyContainer) this.m_pFeatDataset;
                try
                {
                    topology = pFeatDataset.CreateTopology(this.txtTopologyName.Text, double.Parse(this.txtLimiteValue.Text), -1, "");
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147220969)
                    {
                        MessageBox.Show("非数据所有者，无法创建拓扑!");
                    }
                    else
                    {
                        MessageBox.Show(exception.Message);
                    }
                    return;
                }
                catch (Exception exception4)
                {
                    exception2 = exception4;
                    MessageBox.Show(exception2.Message);
                    return;
                }
                int count = this.iarray_1.Count;
                int index = 0;
                while (true)
                {
                    if (index >= count)
                    {
                        break;
                    }
                    int tag = (int) this.listViewPri.Items[index].Tag;
                    try
                    {
                        topology.AddClass((IClass) this.iarray_1.get_Element(index), 5.0, tag, 1, false);
                    }
                    catch (COMException exception3)
                    {
                        if (exception3.ErrorCode != -2147215019)
                        {
                        }
                        CErrorLog.writeErrorLog(this, exception3, "");
                    }
                    catch (Exception exception6)
                    {
                        exception2 = exception6;
                        CErrorLog.writeErrorLog(this, exception2, "");
                    }
                    index++;
                }
                ITopologyRuleContainer container2 = (ITopologyRuleContainer) topology;
                index = 0;
                while (true)
                {
                    if (index >= this.listRule.Items.Count)
                    {
                        break;
                    }
                    try
                    {
                        container2.AddRule((ITopologyRule) this.listRule.Items[index].Tag);
                    }
                    catch (Exception exception7)
                    {
                        exception2 = exception7;
                        CErrorLog.writeErrorLog(this, exception2, "");
                    }
                    index++;
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.int_1--;
            this.method_0();
        }

        private void btnSelectAllFeatureClass_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkListBoxFeatureClass.Items.Count; i++)
            {
                this.chkListBoxFeatureClass.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void chkListBoxFeatureClass_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                this.iarray_1.Add(this.iarray_0.get_Element(e.Index));
                IFeatureClass class2 = (IFeatureClass) this.iarray_0.get_Element(e.Index);
                this.listViewPri.Items.Add(class2.AliasName);
                this.listViewPri.Items[this.listViewPri.Items.Count - 1].SubItems.Add("1");
                this.listViewPri.Items[this.listViewPri.Items.Count - 1].Tag = 1;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                for (int i = 0; i < this.iarray_1.Count; i++)
                {
                    if (this.iarray_1.get_Element(i) == this.iarray_0.get_Element(e.Index))
                    {
                        this.iarray_1.Remove(i);
                        this.listViewPri.Items.RemoveAt(i);
                        break;
                    }
                }
            }
            if (this.iarray_1.Count > 0)
            {
                this.btnNext.Enabled = true;
            }
            else
            {
                this.btnNext.Enabled = false;
            }
        }

        private void chkListBoxFeatureClass_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkListBoxFeatureClass_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void comboBox_0_LostFocus(object sender, EventArgs e)
        {
            if (this.listViewItem_0 != null)
            {
                this.listViewItem_0.Tag = this.comboBox_0.SelectedIndex + 1;
                this.listViewItem_0.SubItems[1].Text = this.listViewItem_0.Tag.ToString();
            }
            this.listViewItem_0 = null;
            this.comboBox_0.Visible = false;
        }

        private void comboBox_0_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCreateTopologySet_Load(object sender, EventArgs e)
        {
            this.comboBox_0.Visible = false;
            this.listViewPri.Controls.Add(this.comboBox_0);
            this.comboBox_0.SelectedValueChanged += new EventHandler(this.comboBox_0_SelectedValueChanged);
            this.comboBox_0.LostFocus += new EventHandler(this.comboBox_0_LostFocus);
            this.int_1 = 1;
            ISpatialReference spatialReference = (this.m_pFeatDataset as IGeoDataset).SpatialReference;
            double xMin = -10000.0;
            double xMax = 11474.83645;
            double yMin = -10000.0;
            double yMax = 11474.83645;
            ((ISpatialReference2GEN) spatialReference).GetDomain(ref xMin, ref xMax, ref yMin, ref yMax);
            double num5 = xMax - xMin;
            double num6 = yMax - yMin;
            num5 = (num5 > num6) ? num5 : num6;
            this.txtLimiteValue.Text = ((num5 / 2147483645.0) * 2.0).ToString("0.#############");
            int num8 = this.m_pFeatDataset.Name.LastIndexOf(".");
            this.txtTopologyName.Text = "topo_" + this.m_pFeatDataset.Name.Substring(num8 + 1);
            IEnumDataset subsets = this.m_pFeatDataset.Subsets;
            subsets.Reset();
            IDataset unk = subsets.Next();
            this.iarray_0.RemoveAll();
            while (unk != null)
            {
                if ((unk.Type == esriDatasetType.esriDTFeatureClass) && ((unk as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple))
                {
                    string browseName;
                    if (unk is ITopologyClass)
                    {
                        if (!(unk as ITopologyClass).IsInTopology)
                        {
                            browseName = unk.BrowseName;
                            this.chkListBoxFeatureClass.Items.Add(browseName);
                            this.iarray_0.Add(unk);
                        }
                    }
                    else
                    {
                        browseName = unk.BrowseName;
                        this.chkListBoxFeatureClass.Items.Add(browseName);
                        this.iarray_0.Add(unk);
                    }
                }
                unk = subsets.Next();
            }
            this.listRule.Columns.Add("要素类", 120, HorizontalAlignment.Left);
            this.listRule.Columns.Add("规则", 120, HorizontalAlignment.Left);
            this.listRule.Columns.Add("要素类", 120, HorizontalAlignment.Left);
            this.listViewPri.Columns.Add("要素类", 180, HorizontalAlignment.Left);
            this.listViewPri.Columns.Add("优先级", 180, HorizontalAlignment.Left);
            this.method_0();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmCreateTopologySet));
            this.chkListBoxFeatureClass = new CheckedListBox();
            this.lblTopoName = new Label();
            this.txtTopologyName = new TextBox();
            this.lblLimit = new Label();
            this.txtLimiteValue = new TextBox();
            this.lblSelFeatureClass = new Label();
            this.listRule = new ListView();
            this.groupBox1 = new GroupBox();
            this.btnPrevious = new Button();
            this.btnNext = new Button();
            this.btnCancel = new Button();
            this.lblRule = new Label();
            this.btnSelectAllFeatureClass = new Button();
            this.btnClearAllFeatureClass = new Button();
            this.btnAddRule = new Button();
            this.btnClearRule = new Button();
            this.btnClearAllRule = new Button();
            this.lblUnit = new Label();
            this.lblPriRang = new Label();
            this.txtMaxPri = new TextBox();
            this.lblPri = new Label();
            this.btnZ = new Button();
            this.listViewPri = new ListView();
            this.btnAddRule1 = new Button();
            base.SuspendLayout();
            this.chkListBoxFeatureClass.Location = new System.Drawing.Point(8, 40);
            this.chkListBoxFeatureClass.Name = "chkListBoxFeatureClass";
            this.chkListBoxFeatureClass.Size = new Size(0x120, 0xe4);
            this.chkListBoxFeatureClass.TabIndex = 0;
            this.chkListBoxFeatureClass.ThreeDCheckBoxes = true;
            this.chkListBoxFeatureClass.Visible = false;
            this.chkListBoxFeatureClass.SelectedIndexChanged += new EventHandler(this.chkListBoxFeatureClass_SelectedIndexChanged);
            this.chkListBoxFeatureClass.ItemCheck += new ItemCheckEventHandler(this.chkListBoxFeatureClass_ItemCheck);
            this.chkListBoxFeatureClass.SelectedValueChanged += new EventHandler(this.chkListBoxFeatureClass_SelectedValueChanged);
            this.lblTopoName.AutoSize = true;
            this.lblTopoName.Location = new System.Drawing.Point(8, 0x10);
            this.lblTopoName.Name = "lblTopoName";
            this.lblTopoName.Size = new Size(0x4d, 12);
            this.lblTopoName.TabIndex = 1;
            this.lblTopoName.Text = "拓扑关系名称";
            this.txtTopologyName.Location = new System.Drawing.Point(0x60, 8);
            this.txtTopologyName.Name = "txtTopologyName";
            this.txtTopologyName.Size = new Size(0xb8, 0x15);
            this.txtTopologyName.TabIndex = 2;
            this.txtTopologyName.KeyPress += new KeyPressEventHandler(this.txtTopologyName_KeyPress);
            this.lblLimit.AutoSize = true;
            this.lblLimit.Location = new System.Drawing.Point(0x10, 0x30);
            this.lblLimit.Name = "lblLimit";
            this.lblLimit.Size = new Size(0x29, 12);
            this.lblLimit.TabIndex = 3;
            this.lblLimit.Text = "容限值";
            this.txtLimiteValue.Location = new System.Drawing.Point(0x60, 40);
            this.txtLimiteValue.Name = "txtLimiteValue";
            this.txtLimiteValue.Size = new Size(0x68, 0x15);
            this.txtLimiteValue.TabIndex = 4;
            this.txtLimiteValue.KeyPress += new KeyPressEventHandler(this.txtLimiteValue_KeyPress);
            this.txtLimiteValue.TextChanged += new EventHandler(this.txtLimiteValue_TextChanged);
            this.lblSelFeatureClass.AutoSize = true;
            this.lblSelFeatureClass.Location = new System.Drawing.Point(8, 8);
            this.lblSelFeatureClass.Name = "lblSelFeatureClass";
            this.lblSelFeatureClass.Size = new Size(0xa1, 12);
            this.lblSelFeatureClass.TabIndex = 5;
            this.lblSelFeatureClass.Text = "选择要参与拓扑关系的要素类";
            this.lblSelFeatureClass.Visible = false;
            this.listRule.Location = new System.Drawing.Point(8, 40);
            this.listRule.Name = "listRule";
            this.listRule.Size = new Size(320, 240);
            this.listRule.TabIndex = 6;
            this.listRule.UseCompatibleStateImageBehavior = false;
            this.listRule.View = View.Details;
            this.listRule.Visible = false;
            this.listRule.SelectedIndexChanged += new EventHandler(this.listRule_SelectedIndexChanged);
            this.groupBox1.Location = new System.Drawing.Point(8, 0x120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x180, 8);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.btnPrevious.Location = new System.Drawing.Point(160, 0x138);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new Size(0x48, 0x18);
            this.btnPrevious.TabIndex = 8;
            this.btnPrevious.Text = "<上一步";
            this.btnPrevious.Click += new EventHandler(this.btnPrevious_Click);
            this.btnNext.Location = new System.Drawing.Point(0xf8, 0x138);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x18);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.Location = new System.Drawing.Point(0x148, 0x138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(0x10, 8);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new Size(0x4d, 12);
            this.lblRule.TabIndex = 11;
            this.lblRule.Text = "定义拓扑规则";
            this.lblRule.Visible = false;
            this.btnSelectAllFeatureClass.Location = new System.Drawing.Point(320, 0x48);
            this.btnSelectAllFeatureClass.Name = "btnSelectAllFeatureClass";
            this.btnSelectAllFeatureClass.Size = new Size(0x40, 0x18);
            this.btnSelectAllFeatureClass.TabIndex = 12;
            this.btnSelectAllFeatureClass.Text = "全选";
            this.btnSelectAllFeatureClass.Visible = false;
            this.btnSelectAllFeatureClass.Click += new EventHandler(this.btnSelectAllFeatureClass_Click);
            this.btnClearAllFeatureClass.Location = new System.Drawing.Point(320, 0x68);
            this.btnClearAllFeatureClass.Name = "btnClearAllFeatureClass";
            this.btnClearAllFeatureClass.Size = new Size(0x40, 0x18);
            this.btnClearAllFeatureClass.TabIndex = 13;
            this.btnClearAllFeatureClass.Text = "全部清除";
            this.btnClearAllFeatureClass.Visible = false;
            this.btnClearAllFeatureClass.Click += new EventHandler(this.btnClearAllFeatureClass_Click);
            this.btnAddRule.Location = new System.Drawing.Point(0x150, 40);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new Size(0x76, 0x18);
            this.btnAddRule.TabIndex = 14;
            this.btnAddRule.Text = "由要素类添加规则";
            this.btnAddRule.Visible = false;
            this.btnAddRule.Click += new EventHandler(this.btnAddRule_Click);
            this.btnClearRule.Location = new System.Drawing.Point(0x150, 0x48);
            this.btnClearRule.Name = "btnClearRule";
            this.btnClearRule.Size = new Size(0x76, 0x18);
            this.btnClearRule.TabIndex = 15;
            this.btnClearRule.Text = "删除";
            this.btnClearRule.Visible = false;
            this.btnClearRule.Click += new EventHandler(this.btnClearRule_Click);
            this.btnClearAllRule.Location = new System.Drawing.Point(0x150, 0x68);
            this.btnClearAllRule.Name = "btnClearAllRule";
            this.btnClearAllRule.Size = new Size(0x76, 0x18);
            this.btnClearAllRule.TabIndex = 0x10;
            this.btnClearAllRule.Text = "删除全部";
            this.btnClearAllRule.Visible = false;
            this.lblUnit.Location = new System.Drawing.Point(0xd8, 40);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new Size(0x58, 0x10);
            this.lblUnit.TabIndex = 0x11;
            this.lblPriRang.AutoSize = true;
            this.lblPriRang.Location = new System.Drawing.Point(8, 8);
            this.lblPriRang.Name = "lblPriRang";
            this.lblPriRang.Size = new Size(0x89, 12);
            this.lblPriRang.TabIndex = 0x12;
            this.lblPriRang.Text = "输入优先级的数字(1-50)";
            this.txtMaxPri.Location = new System.Drawing.Point(0xa8, 8);
            this.txtMaxPri.Name = "txtMaxPri";
            this.txtMaxPri.Size = new Size(80, 0x15);
            this.txtMaxPri.TabIndex = 0x13;
            this.txtMaxPri.Text = "5";
            this.txtMaxPri.TextChanged += new EventHandler(this.txtMaxPri_TextChanged);
            this.lblPri.AutoSize = true;
            this.lblPri.Location = new System.Drawing.Point(0x10, 40);
            this.lblPri.Name = "lblPri";
            this.lblPri.Size = new Size(0xc5, 12);
            this.lblPri.TabIndex = 20;
            this.lblPri.Text = "点击优先级列，输入要素类的优先级";
            this.btnZ.Location = new System.Drawing.Point(0x100, 8);
            this.btnZ.Name = "btnZ";
            this.btnZ.Size = new Size(0x48, 0x18);
            this.btnZ.TabIndex = 0x15;
            this.btnZ.Text = "Z属性";
            this.btnZ.Visible = false;
            this.listViewPri.Location = new System.Drawing.Point(8, 40);
            this.listViewPri.Name = "listViewPri";
            this.listViewPri.Size = new Size(320, 240);
            this.listViewPri.TabIndex = 0x16;
            this.listViewPri.UseCompatibleStateImageBehavior = false;
            this.listViewPri.View = View.Details;
            this.listViewPri.MouseDown += new MouseEventHandler(this.listViewPri_MouseDown);
            this.listViewPri.Click += new EventHandler(this.listViewPri_Click);
            this.btnAddRule1.Location = new System.Drawing.Point(0x14e, 0x92);
            this.btnAddRule1.Name = "btnAddRule1";
            this.btnAddRule1.Size = new Size(120, 0x18);
            this.btnAddRule1.TabIndex = 0x17;
            this.btnAddRule1.Text = "添加规则";
            this.btnAddRule1.Visible = false;
            this.btnAddRule1.Click += new EventHandler(this.btnAddRule1_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1c7, 0x15d);
            base.Controls.Add(this.btnAddRule1);
            base.Controls.Add(this.listViewPri);
            base.Controls.Add(this.btnZ);
            base.Controls.Add(this.lblPri);
            base.Controls.Add(this.txtMaxPri);
            base.Controls.Add(this.lblPriRang);
            base.Controls.Add(this.lblUnit);
            base.Controls.Add(this.btnClearAllRule);
            base.Controls.Add(this.btnClearRule);
            base.Controls.Add(this.btnAddRule);
            base.Controls.Add(this.btnClearAllFeatureClass);
            base.Controls.Add(this.btnSelectAllFeatureClass);
            base.Controls.Add(this.lblRule);
            base.Controls.Add(this.lblSelFeatureClass);
            base.Controls.Add(this.txtLimiteValue);
            base.Controls.Add(this.lblLimit);
            base.Controls.Add(this.txtTopologyName);
            base.Controls.Add(this.lblTopoName);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnPrevious);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listRule);
            base.Controls.Add(this.chkListBoxFeatureClass);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCreateTopologySet";
            this.Text = "新建拓扑设置";
            base.Load += new EventHandler(this.frmCreateTopologySet_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listRule.SelectedItems.Count > 0)
            {
                this.btnClearRule.Enabled = true;
            }
            else
            {
                this.btnClearRule.Enabled = false;
            }
        }

        private void listViewPri_Click(object sender, EventArgs e)
        {
        }

        private void listViewPri_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int num = int.Parse(this.txtMaxPri.Text);
                this.int_0 = num;
            }
            catch (Exception)
            {
                MessageBox.Show("请输入数字!");
                return;
            }
            int width = this.listViewPri.Columns[0].Width;
            if (width <= e.X)
            {
                int num3 = Math.Min(this.listViewPri.Columns[1].Width, this.listViewPri.Width - width) - 4;
                for (int i = 0; i < this.listViewPri.Items.Count; i++)
                {
                    if (this.listViewPri.Items[i].Bounds.Contains(e.X, e.Y))
                    {
                        this.listViewItem_0 = this.listViewPri.Items[i];
                        int tag = (int) this.listViewPri.Items[i].Tag;
                        this.method_3(new System.Drawing.Point((this.listViewPri.Items[i].Bounds.Left + width) + 1, this.listViewPri.Items[i].Bounds.Top), new Size(num3, this.listViewPri.Items[i].Bounds.Height - 3), ref tag);
                        break;
                    }
                }
            }
        }

        private void method_0()
        {
            switch (this.int_1)
            {
                case 1:
                    this.lblTopoName.Visible = true;
                    this.lblLimit.Visible = true;
                    this.txtTopologyName.Visible = true;
                    this.txtLimiteValue.Visible = true;
                    this.lblUnit.Visible = true;
                    this.btnPrevious.Enabled = false;
                    if ((this.txtLimiteValue.Text.Length != 0) && (this.txtTopologyName.Text.Length != 0))
                    {
                        this.btnNext.Enabled = true;
                        break;
                    }
                    this.btnNext.Enabled = false;
                    break;

                case 2:
                    this.lblTopoName.Visible = false;
                    this.lblLimit.Visible = false;
                    this.txtTopologyName.Visible = false;
                    this.txtLimiteValue.Visible = false;
                    this.lblUnit.Visible = false;
                    this.btnPrevious.Enabled = true;
                    this.lblSelFeatureClass.Visible = true;
                    this.chkListBoxFeatureClass.Visible = true;
                    this.btnSelectAllFeatureClass.Visible = true;
                    this.btnClearAllFeatureClass.Visible = true;
                    if (this.iarray_1.Count <= 0)
                    {
                        this.btnNext.Enabled = false;
                    }
                    else
                    {
                        this.btnNext.Enabled = true;
                    }
                    this.lblPri.Visible = false;
                    this.lblPriRang.Visible = false;
                    this.txtMaxPri.Visible = false;
                    this.btnZ.Visible = false;
                    this.listViewPri.Visible = false;
                    this.lblRule.Visible = false;
                    this.listRule.Visible = false;
                    this.btnAddRule.Visible = false;
                    this.btnAddRule1.Visible = false;
                    this.btnClearRule.Visible = false;
                    this.btnClearAllRule.Visible = false;
                    return;

                case 3:
                    this.lblSelFeatureClass.Visible = false;
                    this.chkListBoxFeatureClass.Visible = false;
                    this.btnSelectAllFeatureClass.Visible = false;
                    this.btnClearAllFeatureClass.Visible = false;
                    this.btnNext.Text = "下一步>";
                    this.lblPri.Visible = true;
                    this.lblPriRang.Visible = true;
                    this.txtMaxPri.Visible = true;
                    this.btnZ.Visible = true;
                    this.listViewPri.Visible = true;
                    this.txtMaxPri_TextChanged(this, new EventArgs());
                    this.lblRule.Visible = false;
                    this.listRule.Visible = false;
                    this.btnAddRule.Visible = false;
                    this.btnAddRule1.Visible = false;
                    this.btnClearRule.Visible = false;
                    this.btnClearAllRule.Visible = false;
                    return;

                case 4:
                    this.lblSelFeatureClass.Visible = false;
                    this.chkListBoxFeatureClass.Visible = false;
                    this.btnSelectAllFeatureClass.Visible = false;
                    this.btnClearAllFeatureClass.Visible = false;
                    this.btnNext.Text = "下一步>";
                    this.lblPri.Visible = false;
                    this.lblPriRang.Visible = false;
                    this.txtMaxPri.Visible = false;
                    this.btnZ.Visible = false;
                    this.listViewPri.Visible = false;
                    this.btnNext.Text = "完成";
                    this.lblRule.Visible = true;
                    this.listRule.Visible = true;
                    this.btnAddRule.Visible = true;
                    this.btnAddRule1.Visible = true;
                    this.btnClearRule.Visible = true;
                    this.btnClearAllRule.Visible = true;
                    return;

                default:
                    return;
            }
            this.lblSelFeatureClass.Visible = false;
            this.chkListBoxFeatureClass.Visible = false;
            this.btnSelectAllFeatureClass.Visible = false;
            this.btnClearAllFeatureClass.Visible = false;
            this.lblPri.Visible = false;
            this.lblPriRang.Visible = false;
            this.txtMaxPri.Visible = false;
            this.btnZ.Visible = false;
            this.listViewPri.Visible = false;
            this.lblRule.Visible = false;
            this.listRule.Visible = false;
            this.btnAddRule.Visible = false;
            this.btnAddRule1.Visible = false;
            this.btnClearRule.Visible = false;
            this.btnClearAllRule.Visible = false;
        }

        private bool method_1(ITopologyRule itopologyRule_0, IArray iarray_3)
        {
            for (int i = 0; i < iarray_3.Count; i++)
            {
                ITopologyRule rule = (ITopologyRule) iarray_3.get_Element(i);
                if (((rule.Name == itopologyRule_0.Name) && (rule.OriginClassID == itopologyRule_0.OriginClassID)) && (rule.DestinationClassID == itopologyRule_0.DestinationClassID))
                {
                    return true;
                }
            }
            return false;
        }

        private bool method_2(ITopologyRule itopologyRule_0)
        {
            for (int i = 0; i < this.listRule.Items.Count; i++)
            {
                ITopologyRule tag = (ITopologyRule) this.listRule.Items[i].Tag;
                if (((tag.Name == itopologyRule_0.Name) && (tag.OriginClassID == itopologyRule_0.OriginClassID)) && (tag.DestinationClassID == itopologyRule_0.DestinationClassID))
                {
                    return true;
                }
            }
            return false;
        }

        private void method_3(System.Drawing.Point point_0, Size size_0, ref int int_2)
        {
            int width = this.listViewPri.Columns[0].Width;
            Math.Min(this.listViewPri.Columns[1].Width, this.listViewPri.Width - width);
            this.comboBox_0.Location = point_0;
            this.comboBox_0.Size = size_0;
            this.comboBox_0.Items.Clear();
            for (int i = 1; i < (this.int_0 + 1); i++)
            {
                this.comboBox_0.Items.Add(i);
            }
            this.comboBox_0.Visible = true;
            this.comboBox_0.Text = ((int) int_2).ToString();
            this.comboBox_0.DroppedDown = true;
        }

        private void method_4()
        {
            int count = this.chkListBoxFeatureClass.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.chkListBoxFeatureClass.GetItemChecked(i))
                {
                    this.iarray_1.Add(this.iarray_0.get_Element(i));
                    IFeatureClass class2 = (IFeatureClass) this.iarray_0.get_Element(i);
                    this.listViewPri.Items.Add(class2.AliasName);
                    this.listViewPri.Items[this.listViewPri.Items.Count - 1].SubItems.Add("1");
                    this.listViewPri.Items[this.listViewPri.Items.Count - 1].Tag = 1;
                }
            }
        }

        private void method_5(object sender, EventArgs e)
        {
            if (this.listViewItem_0 != null)
            {
                this.listViewItem_0.Tag = this.comboBox_0.SelectedIndex + 1;
                this.listViewItem_0.SubItems[1].Text = this.listViewItem_0.Tag.ToString();
            }
        }

        private void txtLimiteValue_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtLimiteValue_TextChanged(object sender, EventArgs e)
        {
            this.btnNext.Enabled = false;
            if ((this.txtTopologyName.Text.Length > 0) && (this.txtLimiteValue.Text.Length > 0))
            {
                try
                {
                    double.Parse(this.txtLimiteValue.Text);
                    this.btnNext.Enabled = true;
                }
                catch (Exception)
                {
                    this.btnNext.Enabled = false;
                }
            }
        }

        private void txtMaxPri_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtMaxPri.Text);
                this.btnNext.Enabled = true;
            }
            catch
            {
                this.btnNext.Enabled = false;
            }
        }

        private void txtTopologyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.btnNext.Enabled = false;
            if ((this.txtTopologyName.Text.Length > 0) && (this.txtLimiteValue.Text.Length > 0))
            {
                try
                {
                    double.Parse(this.txtLimiteValue.Text);
                    this.btnNext.Enabled = true;
                }
                catch (Exception)
                {
                    this.btnNext.Enabled = false;
                }
            }
        }
    }
}

