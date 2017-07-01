using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCreateTopologySet : Form
    {
        private ComboBox comboBox_0 = new ComboBox();
        private Container container_0 = null;
        private IArray iarray_0 = new ArrayClass();
        private IArray iarray_1 = new ArrayClass();
        private IArray iarray_2 = new ArrayClass();
        private int int_0 = 5;
        private ListViewItem listViewItem_0 = null;
        public IFeatureDataset m_pFeatDataset;

        public frmCreateTopologySet()
        {
            this.InitializeComponent();
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            frmRule rule = new frmRule
            {
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
            frmAddRuleByClass class2 = new frmAddRuleByClass
            {
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
                    topology = pFeatDataset.CreateTopology(this.txtTopologyName.Text,
                        double.Parse(this.txtLimiteValue.Text), -1, "");
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
                        Logger.Current.Error("", exception3, "");
                    }
                    catch (Exception exception6)
                    {
                        exception2 = exception6;
                        Logger.Current.Error("", exception2, "");
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
                        Logger.Current.Error("", exception2, "");
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
            this.txtLimiteValue.Text = ((num5/2147483645.0)*2.0).ToString("0.#############");
            int num8 = this.m_pFeatDataset.Name.LastIndexOf(".");
            this.txtTopologyName.Text = "topo_" + this.m_pFeatDataset.Name.Substring(num8 + 1);
            IEnumDataset subsets = this.m_pFeatDataset.Subsets;
            subsets.Reset();
            IDataset unk = subsets.Next();
            this.iarray_0.RemoveAll();
            while (unk != null)
            {
                if ((unk.Type == esriDatasetType.esriDTFeatureClass) &&
                    ((unk as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple))
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
                        this.method_3(
                            new System.Drawing.Point((this.listViewPri.Items[i].Bounds.Left + width) + 1,
                                this.listViewPri.Items[i].Bounds.Top),
                            new Size(num3, this.listViewPri.Items[i].Bounds.Height - 3), ref tag);
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
                if (((rule.Name == itopologyRule_0.Name) && (rule.OriginClassID == itopologyRule_0.OriginClassID)) &&
                    (rule.DestinationClassID == itopologyRule_0.DestinationClassID))
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
                if (((tag.Name == itopologyRule_0.Name) && (tag.OriginClassID == itopologyRule_0.OriginClassID)) &&
                    (tag.DestinationClassID == itopologyRule_0.DestinationClassID))
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