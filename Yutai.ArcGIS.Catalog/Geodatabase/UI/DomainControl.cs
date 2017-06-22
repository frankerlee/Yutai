using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class DomainControl : UserControl
    {
        private int int_0;
        private int int_1;
        private bool bool_2;
        private IDomain idomain_0;
        private IDomain idomain_1;
        private string string_0;
        private bool bool_0 = true;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private IList ilist_1 = new ArrayList();
        private IList ilist_2 = new ArrayList();
        private IList ilist_3 = new ArrayList();
        private int int_2 = -1;
        private IWorkspaceDomains iworkspaceDomains_0 = null;

        public DomainControl()
        {
            this.InitializeComponent();
            this.textBox_0 = new TextBox();
            this.textBox_0.Size = new Size(0, 0);
            this.textBox_0.Location = new Point(0, 0);
            this.textBox_0.BorderStyle = BorderStyle.FixedSingle;
            this.textBox_0.Font = this.DomainListView.Font;
            this.DomainListView.Controls.Add(this.textBox_0);
            this.textBox_0.AutoSize = false;
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
            this.textBox_0.LostFocus += new EventHandler(this.textBox_0_LostFocus);
            this.textBox_1 = new TextBox();
            this.textBox_1.Size = new Size(0, 0);
            this.textBox_1.Location = new Point(0, 0);
            this.textBox_1.BorderStyle = BorderStyle.FixedSingle;
            this.textBox_1.Font = this.DomainPropertyListView.Font;
            this.DomainPropertyListView.Controls.Add(this.textBox_1);
            this.textBox_1.AutoSize = false;
            this.textBox_1.KeyPress += new KeyPressEventHandler(this.textBox_1_KeyPress);
            this.textBox_1.LostFocus += new EventHandler(this.textBox_1_LostFocus);
            this.textBox_2 = new TextBox();
            this.textBox_2.Size = new Size(0, 0);
            this.textBox_2.Location = new Point(0, 0);
            this.textBox_2.BorderStyle = BorderStyle.FixedSingle;
            this.textBox_2.Font = this.CodeValueListView.Font;
            this.CodeValueListView.Controls.Add(this.textBox_2);
            this.textBox_2.AutoSize = false;
            this.textBox_2.KeyPress += new KeyPressEventHandler(this.textBox_2_KeyPress);
            this.textBox_2.LostFocus += new EventHandler(this.textBox_2_LostFocus);
            this.comboBox_0 = new System.Windows.Forms.ComboBox();
            this.comboBox_0.Size = new Size(0, 0);
            this.comboBox_0.Location = new Point(0, 0);
            this.DomainPropertyListView.Controls.Add(this.comboBox_0);
            this.comboBox_0.SelectedIndexChanged += new EventHandler(this.comboBox_0_SelectedIndexChanged);
            this.comboBox_0.LostFocus += new EventHandler(this.comboBox_0_LostFocus);
            this.comboBox_0.KeyPress += new KeyPressEventHandler(this.comboBox_0_KeyPress);
            this.comboBox_0.Font = this.CodeValueListView.Font;
            this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_0.Hide();
            this.comboBox_1 = new System.Windows.Forms.ComboBox();
            this.comboBox_1.Size = new Size(0, 0);
            this.comboBox_1.Location = new Point(0, 0);
            this.CodeValueListView.Controls.Add(this.comboBox_1);
            this.comboBox_1.SelectedIndexChanged += new EventHandler(this.comboBox_1_SelectedIndexChanged);
            this.comboBox_1.LostFocus += new EventHandler(this.comboBox_1_LostFocus);
            this.comboBox_1.KeyPress += new KeyPressEventHandler(this.comboBox_1_KeyPress);
            this.comboBox_1.Font = this.CodeValueListView.Font;
            this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_1.Hide();
        }

        public void Apply()
        {
            if (this.iworkspaceDomains_0 != null)
            {
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    (this.ilist_0[i] as DomainWrap).Apply(this.iworkspaceDomains_0);
                }
            }
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            if (this.DomainListView.SelectedIndices.Count != 0)
            {
                ListViewItem item = this.DomainListView.SelectedItems[0];
                if (item.Tag != null)
                {
                    ICodedValueDomain domain = (item.Tag as DomainWrap).Domain as ICodedValueDomain;
                    frmCodes codes = new frmCodes {
                        CodeValueDomain = domain
                    };
                    if (codes.ShowDialog() == DialogResult.OK)
                    {
                        ListViewItem item2 = new ListViewItem(new string[] { codes.m_Code.ToString(), codes.m_CodeName });
                        this.CodeValueListView.Items.Add(item2);
                        (item.Tag as DomainWrap).Modify = true;
                    }
                }
            }
        }

        private void btnDeleteCode_Click(object sender, EventArgs e)
        {
            if (this.DomainListView.SelectedIndices.Count != 0)
            {
                ListViewItem item = this.DomainListView.SelectedItems[0];
                if ((item.Tag != null) && (this.CodeValueListView.SelectedIndices.Count > 0))
                {
                    ICodedValueDomain domain = (item.Tag as DomainWrap).Domain as ICodedValueDomain;
                    ListViewItem item2 = this.CodeValueListView.SelectedItems[0];
                    object obj2 = this.method_14(domain, item2.Text);
                    domain.DeleteCode(obj2);
                    this.CodeValueListView.Items.Remove(item2);
                    (item.Tag as DomainWrap).Modify = true;
                }
            }
        }

        private void btnEditCode_Click(object sender, EventArgs e)
        {
            if (this.DomainListView.SelectedIndices.Count != 0)
            {
                ListViewItem item = this.DomainListView.SelectedItems[0];
                if ((item.Tag != null) && (this.CodeValueListView.SelectedIndices.Count > 0))
                {
                    ListViewItem item2 = this.CodeValueListView.SelectedItems[0];
                    ICodedValueDomain domain = (item.Tag as DomainWrap).Domain as ICodedValueDomain;
                    frmCodes codes = new frmCodes {
                        CodeValueDomain = domain
                    };
                    codes.SetCode(this.method_14(domain, item2.Text), item2.SubItems[1].Text);
                    if (codes.ShowDialog() == DialogResult.OK)
                    {
                        item2.Text = codes.m_Code.ToString();
                        item2.SubItems[1].Text = codes.m_CodeName;
                        (item.Tag as DomainWrap).Modify = true;
                    }
                }
            }
        }

        private void btnExtentCodeDomainMang_Click(object sender, EventArgs e)
        {
            frmExtendDomain domain = new frmExtendDomain {
                Workspace = this.iworkspaceDomains_0 as IWorkspace
            };
            if (domain.ShowDialog() != DialogResult.OK)
            {
            }
        }

        private void CodeValueListView_DoubleClick(object sender, EventArgs e)
        {
        }

        private void CodeValueListView_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void CodeValueListView_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void CodeValueListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteCode.Enabled = this.CodeValueListView.SelectedItems.Count > 0;
            this.btnEditCode.Enabled = this.CodeValueListView.SelectedItems.Count > 0;
        }

        private void comboBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox_0_LostFocus(object sender, EventArgs e)
        {
            this.comboBox_0.Hide();
        }

        private void comboBox_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            int num2;
            if (this.bool_0)
            {
                return;
            }
            int selectedIndex = this.comboBox_0.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            IDomain domain = (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Domain;
            switch (this.DomainPropertyListView.Items.IndexOf(this.listViewItem_0))
            {
                case 0:
                    (this.DomainListView.SelectedItems[0].Tag as DomainWrap).ChangeFileType(this.method_4(selectedIndex));
                    this.method_1((this.DomainListView.SelectedItems[0].Tag as DomainWrap).Domain);
                    goto Label_03C1;

                case 1:
                    if (selectedIndex != 0)
                    {
                        (this.DomainListView.SelectedItems[0].Tag as DomainWrap).ChangeDomainType(esriDomainType.esriDTRange);
                        break;
                    }
                    (this.DomainListView.SelectedItems[0].Tag as DomainWrap).ChangeDomainType(esriDomainType.esriDTCodedValue);
                    break;

                case 2:
                    this.method_9(selectedIndex, domain);
                    switch (domain.SplitPolicy)
                    {
                        case esriSplitPolicyType.esriSPTDuplicate:
                            this.listViewItem_0.SubItems[1].Text = "复制";
                            goto Label_01C0;

                        case esriSplitPolicyType.esriSPTDefaultValue:
                            this.listViewItem_0.SubItems[1].Text = "默认值";
                            goto Label_01C0;
                    }
                    this.listViewItem_0.SubItems[1].Text = "几何比例";
                    goto Label_01C0;

                case 3:
                    this.method_11(selectedIndex, domain);
                    switch (domain.MergePolicy)
                    {
                        case esriMergePolicyType.esriMPTSumValues:
                            this.listViewItem_0.SubItems[1].Text = "总和值";
                            goto Label_0262;

                        case esriMergePolicyType.esriMPTDefaultValue:
                            this.listViewItem_0.SubItems[1].Text = "默认值";
                            goto Label_0262;
                    }
                    this.listViewItem_0.SubItems[1].Text = "加权平均";
                    goto Label_0262;

                case 4:
                    this.method_9(selectedIndex, domain);
                    switch (domain.SplitPolicy)
                    {
                        case esriSplitPolicyType.esriSPTDuplicate:
                            this.listViewItem_0.SubItems[1].Text = "复制";
                            goto Label_02FE;

                        case esriSplitPolicyType.esriSPTDefaultValue:
                            this.listViewItem_0.SubItems[1].Text = "默认值";
                            goto Label_02FE;
                    }
                    this.listViewItem_0.SubItems[1].Text = "几何比例";
                    goto Label_02FE;

                case 5:
                    this.method_11(selectedIndex, domain);
                    switch (domain.MergePolicy)
                    {
                        case esriMergePolicyType.esriMPTSumValues:
                            this.listViewItem_0.SubItems[1].Text = "总和值";
                            goto Label_03A0;

                        case esriMergePolicyType.esriMPTDefaultValue:
                            this.listViewItem_0.SubItems[1].Text = "默认值";
                            goto Label_03A0;
                    }
                    this.listViewItem_0.SubItems[1].Text = "加权平均";
                    goto Label_03A0;

                default:
                    goto Label_03C1;
            }
            this.method_1((this.DomainListView.SelectedItems[0].Tag as DomainWrap).Domain);
            goto Label_03C1;
        Label_01C0:
            (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Modify = true;
            goto Label_03C1;
        Label_0262:
            (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Modify = true;
            goto Label_03C1;
        Label_02FE:
            (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Modify = true;
            goto Label_03C1;
        Label_03A0:
            (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Modify = true;
        Label_03C1:
            num2 = this.ilist_1.IndexOf(domain);
            this.comboBox_0.Hide();
        }

        private void comboBox_1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox_1_LostFocus(object sender, EventArgs e)
        {
        }

        private void comboBox_1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

 private void DomainControl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void DomainListView_DoubleClick(object sender, EventArgs e)
        {
            if (this.DomainListView.SelectedItems.Count == 1)
            {
                int num = this.int_0;
                int x = 0;
                int num3 = 0;
                this.int_2 = -1;
                for (int i = 0; i < this.DomainListView.Columns.Count; i++)
                {
                    x = num3;
                    num3 += this.DomainListView.Columns[i].Width;
                    if ((num > x) && (num < num3))
                    {
                        this.int_2 = i;
                        break;
                    }
                }
                if (this.int_2 != -1)
                {
                    this.listViewItem_0 = this.DomainListView.SelectedItems[0];
                    this.textBox_0.Size = new Size(num3 - x, this.listViewItem_0.Bounds.Height);
                    this.textBox_0.Location = new Point(x, this.listViewItem_0.Bounds.Y);
                    this.textBox_0.Show();
                    this.textBox_0.Text = this.listViewItem_0.SubItems[this.int_2].Text;
                    this.textBox_0.SelectAll();
                    this.textBox_0.Focus();
                }
            }
        }

        private void DomainListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (((this.DomainListView.SelectedItems.Count > 0) && (this.DomainListView.SelectedItems[0].Tag != null)) && (e.KeyData == Keys.Delete))
            {
                IDomain domain = (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Domain;
                if (domain != null)
                {
                    ListViewItem item;
                    if ((this.DomainListView.SelectedItems[0].Tag as DomainWrap).IsNew)
                    {
                        (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Delete();
                        this.DomainListView.Items.Remove(this.DomainListView.SelectedItems[0]);
                        if (this.DomainListView.Items.Count == 0)
                        {
                            item = new ListViewItem(new string[] { "", "" });
                            this.DomainListView.Items.Add(item);
                        }
                    }
                    else if (this.iworkspaceDomains_0.get_CanDeleteDomain(domain.Name))
                    {
                        (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Delete();
                        this.DomainListView.Items.Remove(this.DomainListView.SelectedItems[0]);
                        if (this.DomainListView.Items.Count == 0)
                        {
                            item = new ListViewItem(new string[] { "", "" });
                            this.DomainListView.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void DomainListView_MouseDown(object sender, MouseEventArgs e)
        {
            this.int_0 = e.X;
            this.int_1 = e.Y;
        }

        private void DomainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DomainPropertyListView.Items.Clear();
                this.CodeValueListView.Items.Clear();
                if (this.DomainListView.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.DomainListView.SelectedItems[0];
                    if (item.Tag != null)
                    {
                        IDomain domain = (item.Tag as DomainWrap).Domain;
                        string[] items = new string[] { "字段类型", RowOperator.GetFieldTypeString(domain.FieldType) };
                        ListViewItem item2 = new ListViewItem(items);
                        this.DomainPropertyListView.Items.Add(item2);
                        items[0] = "域类型";
                        switch (domain.Type)
                        {
                            case esriDomainType.esriDTRange:
                                items[1] = "范围";
                                break;

                            case esriDomainType.esriDTCodedValue:
                                items[1] = "代码";
                                break;

                            default:
                                items[1] = "代码";
                                break;
                        }
                        item2 = new ListViewItem(items);
                        this.DomainPropertyListView.Items.Add(item2);
                        if (domain.Type == esriDomainType.esriDTRange)
                        {
                            items[0] = "最小值";
                            items[1] = (domain as IRangeDomain).MinValue.ToString();
                            item2 = new ListViewItem(items);
                            this.DomainPropertyListView.Items.Add(item2);
                            items[0] = "最大值";
                            items[1] = (domain as IRangeDomain).MaxValue.ToString();
                            item2 = new ListViewItem(items);
                            this.DomainPropertyListView.Items.Add(item2);
                            this.CodeValueListView.Enabled = false;
                            this.btnAddCode.Visible = false;
                            this.btnDeleteCode.Visible = false;
                            this.btnEditCode.Visible = false;
                        }
                        else
                        {
                            this.CodeValueListView.Enabled = true;
                            this.btnAddCode.Visible = true;
                            this.btnDeleteCode.Visible = true;
                            this.btnDeleteCode.Enabled = false;
                            this.btnEditCode.Enabled = false;
                            this.btnEditCode.Visible = true;
                            ICodedValueDomain domain2 = domain as ICodedValueDomain;
                            for (int i = 0; i < domain2.CodeCount; i++)
                            {
                                items[0] = domain2.get_Value(i).ToString();
                                items[1] = domain2.get_Name(i);
                                ListViewItem item3 = new ListViewItem(items);
                                this.CodeValueListView.Items.Add(item3);
                            }
                        }
                        items[0] = "拆分原则";
                        switch (domain.SplitPolicy)
                        {
                            case esriSplitPolicyType.esriSPTDuplicate:
                                items[1] = "复制";
                                break;

                            case esriSplitPolicyType.esriSPTDefaultValue:
                                items[1] = "默认值";
                                break;

                            default:
                                items[1] = "几何比例";
                                break;
                        }
                        item2 = new ListViewItem(items);
                        this.DomainPropertyListView.Items.Add(item2);
                        items[0] = "合并原则";
                        switch (domain.MergePolicy)
                        {
                            case esriMergePolicyType.esriMPTSumValues:
                                items[1] = "总和值";
                                break;

                            case esriMergePolicyType.esriMPTDefaultValue:
                                items[1] = "默认值";
                                break;

                            default:
                                items[1] = "加权平均";
                                break;
                        }
                        item2 = new ListViewItem(items);
                        this.DomainPropertyListView.Items.Add(item2);
                    }
                }
                else
                {
                    this.btnAddCode.Visible = false;
                    this.btnDeleteCode.Visible = false;
                    this.btnEditCode.Visible = false;
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        private void DomainPropertyListView_DoubleClick(object sender, EventArgs e)
        {
            if (this.DomainPropertyListView.SelectedItems.Count == 1)
            {
                int num = this.int_0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                while (num4 < this.DomainPropertyListView.Columns.Count)
                {
                    num2 = num3;
                    num3 += this.DomainPropertyListView.Columns[num4].Width;
                    if ((num > num2) && (num < num3))
                    {
                        break;
                    }
                    num4++;
                }
                if (num4 == 1)
                {
                    this.listViewItem_0 = this.DomainPropertyListView.SelectedItems[0];
                    int num5 = this.DomainPropertyListView.SelectedIndices[0];
                    ListViewItem item = this.DomainListView.SelectedItems[0];
                    IDomain domain = (item.Tag as DomainWrap).Domain;
                    switch (num5)
                    {
                        case 0:
                            this.method_2(domain.Type, num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;

                        case 1:
                            this.method_5(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;

                        case 2:
                            if (domain.Type != esriDomainType.esriDTRange)
                            {
                                this.method_8(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                                break;
                            }
                            this.method_7(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;

                        case 3:
                            if (domain.Type != esriDomainType.esriDTRange)
                            {
                                this.method_10(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                                break;
                            }
                            this.method_7(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;

                        case 4:
                            this.method_8(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;

                        case 5:
                            this.method_10(num2, this.listViewItem_0.Bounds.Y, num3 - num2, this.listViewItem_0.Bounds.Height, this.listViewItem_0.SubItems[1].Text);
                            break;
                    }
                }
            }
        }

        private void DomainPropertyListView_MouseDown(object sender, MouseEventArgs e)
        {
            this.int_0 = e.X;
            this.int_1 = e.Y;
        }

 private void method_0()
        {
            this.btnExtentCodeDomainMang.Visible = false;
            try
            {
                this.btnAddCode.Visible = false;
                this.btnDeleteCode.Visible = false;
                this.btnEditCode.Visible = false;
                if (this.iworkspaceDomains_0 != null)
                {
                    ListViewItem item;
                    IEnumDomain domains = this.iworkspaceDomains_0.Domains;
                    string[] items = new string[2];
                    if (domains != null)
                    {
                        domains.Reset();
                        for (IDomain domain2 = domains.Next(); domain2 != null; domain2 = domains.Next())
                        {
                            items[0] = domain2.Name;
                            items[1] = domain2.Description;
                            item = new ListViewItem(items) {
                                Tag = new DomainWrap(domain2)
                            };
                            this.ilist_0.Add(item.Tag);
                            this.DomainListView.Items.Add(item);
                        }
                    }
                    items[0] = "";
                    items[1] = "";
                    item = new ListViewItem(items);
                    this.DomainListView.Items.Add(item);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        private void method_1(IDomain idomain_0)
        {
            try
            {
                this.DomainPropertyListView.Items.Clear();
                this.CodeValueListView.Items.Clear();
                string[] items = new string[] { "字段类型", RowOperator.GetFieldTypeString(idomain_0.FieldType) };
                ListViewItem item = new ListViewItem(items);
                this.DomainPropertyListView.Items.Add(item);
                items[0] = "域类型";
                switch (idomain_0.Type)
                {
                    case esriDomainType.esriDTRange:
                        items[1] = "范围";
                        break;

                    case esriDomainType.esriDTCodedValue:
                        items[1] = "代码";
                        break;

                    default:
                        items[1] = "代码";
                        break;
                }
                item = new ListViewItem(items);
                this.DomainPropertyListView.Items.Add(item);
                if (idomain_0.Type == esriDomainType.esriDTRange)
                {
                    items[0] = "最小值";
                    items[1] = (idomain_0 as IRangeDomain).MinValue.ToString();
                    item = new ListViewItem(items);
                    this.DomainPropertyListView.Items.Add(item);
                    items[0] = "最大值";
                    items[1] = (idomain_0 as IRangeDomain).MaxValue.ToString();
                    item = new ListViewItem(items);
                    this.DomainPropertyListView.Items.Add(item);
                    this.CodeValueListView.Enabled = false;
                    this.btnAddCode.Visible = false;
                    this.btnDeleteCode.Visible = false;
                    this.btnEditCode.Visible = false;
                }
                else
                {
                    this.CodeValueListView.Enabled = true;
                    this.btnAddCode.Visible = true;
                    this.btnDeleteCode.Visible = true;
                    this.btnDeleteCode.Enabled = false;
                    this.btnEditCode.Enabled = false;
                    this.btnEditCode.Visible = true;
                    ICodedValueDomain domain = idomain_0 as ICodedValueDomain;
                    for (int i = 0; i < domain.CodeCount; i++)
                    {
                        items[0] = domain.get_Value(i).ToString();
                        items[1] = domain.get_Name(i);
                        ListViewItem item2 = new ListViewItem(items);
                        this.CodeValueListView.Items.Add(item2);
                    }
                }
                items[0] = "拆分原则";
                switch (idomain_0.SplitPolicy)
                {
                    case esriSplitPolicyType.esriSPTDuplicate:
                        items[1] = "复制";
                        break;

                    case esriSplitPolicyType.esriSPTDefaultValue:
                        items[1] = "默认值";
                        break;

                    default:
                        items[1] = "几何比例";
                        break;
                }
                item = new ListViewItem(items);
                this.DomainPropertyListView.Items.Add(item);
                items[0] = "合并原则";
                switch (idomain_0.MergePolicy)
                {
                    case esriMergePolicyType.esriMPTSumValues:
                        items[1] = "总和值";
                        break;

                    case esriMergePolicyType.esriMPTDefaultValue:
                        items[1] = "默认值";
                        break;

                    default:
                        items[1] = "加权平均";
                        break;
                }
                item = new ListViewItem(items);
                this.DomainPropertyListView.Items.Add(item);
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        private void method_10(int int_3, int int_4, int int_5, int int_6, string string_0)
        {
            ListViewItem item = this.DomainListView.SelectedItems[0];
            IDomain domain = (item.Tag as DomainWrap).Domain;
            this.bool_0 = true;
            this.comboBox_0.Items.Clear();
            if ((domain.Type == esriDomainType.esriDTRange) && ((((domain.FieldType == esriFieldType.esriFieldTypeDouble) || (domain.FieldType == esriFieldType.esriFieldTypeInteger)) || (domain.FieldType == esriFieldType.esriFieldTypeSingle)) || (domain.FieldType == esriFieldType.esriFieldTypeSmallInteger)))
            {
                this.comboBox_0.Items.Add("默认值");
                this.comboBox_0.Items.Add("总和值");
                this.comboBox_0.Items.Add("加权平均");
            }
            else
            {
                this.comboBox_0.Items.Add("默认值");
            }
            this.comboBox_0.Size = new Size(int_5, int_6);
            this.comboBox_0.Location = new Point(int_3, int_4);
            this.comboBox_0.Show();
            this.comboBox_0.Text = string_0;
            this.comboBox_0.Focus();
            this.bool_0 = false;
        }

        private void method_11(int int_3, IDomain idomain_0)
        {
            switch (int_3)
            {
                case 0:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTDefaultValue;
                    break;

                case 1:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTSumValues;
                    break;

                case 2:
                    idomain_0.MergePolicy = esriMergePolicyType.esriMPTAreaWeighted;
                    break;
            }
        }

        private void method_12(object sender, EventArgs e)
        {
        }

        private void method_13(object sender, EventArgs e)
        {
        }

        private object method_14(ICodedValueDomain icodedValueDomain_0, string string_0)
        {
            try
            {
                switch ((icodedValueDomain_0 as IDomain).FieldType)
                {
                    case esriFieldType.esriFieldTypeSmallInteger:
                        return short.Parse(string_0);

                    case esriFieldType.esriFieldTypeInteger:
                        return int.Parse(string_0);

                    case esriFieldType.esriFieldTypeSingle:
                        return float.Parse(string_0);

                    case esriFieldType.esriFieldTypeDouble:
                        return double.Parse(string_0);

                    case esriFieldType.esriFieldTypeString:
                        return string_0;
                }
            }
            catch
            {
            }
            return null;
        }

        private void method_2(esriDomainType esriDomainType_0, int int_3, int int_4, int int_5, int int_6, string string_0)
        {
            this.bool_0 = true;
            if (esriDomainType_0 == esriDomainType.esriDTRange)
            {
                this.comboBox_0.Tag = esriDomainType.esriDTRange;
                this.comboBox_0.Items.Clear();
                this.comboBox_0.Items.Add("短整型");
                this.comboBox_0.Items.Add("长整型");
                this.comboBox_0.Items.Add("单精度");
                this.comboBox_0.Items.Add("双精度");
                this.comboBox_0.Items.Add("日期类型");
            }
            else
            {
                this.comboBox_0.Tag = esriDomainType.esriDTCodedValue;
                this.comboBox_0.Items.Clear();
                this.comboBox_0.Items.Add("短整型");
                this.comboBox_0.Items.Add("长整型");
                this.comboBox_0.Items.Add("单精度");
                this.comboBox_0.Items.Add("双精度");
                this.comboBox_0.Items.Add("文本");
            }
            this.comboBox_0.SelectedIndex = this.comboBox_0.Items.IndexOf(string_0);
            this.comboBox_0.Size = new Size(int_5, int_6);
            this.comboBox_0.Location = new Point(int_3, int_4);
            this.comboBox_0.Show();
            this.comboBox_0.Focus();
            this.bool_0 = false;
        }

        private void method_3(int int_3, IDomain idomain_0)
        {
            esriFieldType fieldType = idomain_0.FieldType;
            switch (int_3)
            {
                case 0:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeSmallInteger;
                    if ((fieldType != esriFieldType.esriFieldTypeString) || (idomain_0.Type != esriDomainType.esriDTCodedValue))
                    {
                    }
                    break;

                case 1:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeInteger;
                    break;

                case 2:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeSingle;
                    break;

                case 3:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeDouble;
                    break;

                case 4:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeDate;
                    break;

                case 5:
                    idomain_0.FieldType = esriFieldType.esriFieldTypeString;
                    break;
            }
        }

        private esriFieldType method_4(int int_3)
        {
            esriDomainType tag = (esriDomainType) this.comboBox_0.Tag;
            switch (int_3)
            {
                case 0:
                    return esriFieldType.esriFieldTypeSmallInteger;

                case 1:
                    return esriFieldType.esriFieldTypeInteger;

                case 2:
                    return esriFieldType.esriFieldTypeSingle;

                case 3:
                    return esriFieldType.esriFieldTypeDouble;

                case 4:
                    if (tag != esriDomainType.esriDTRange)
                    {
                        if (tag == esriDomainType.esriDTCodedValue)
                        {
                            return esriFieldType.esriFieldTypeString;
                        }
                        break;
                    }
                    return esriFieldType.esriFieldTypeDate;

                case 5:
                    return esriFieldType.esriFieldTypeString;
            }
            return esriFieldType.esriFieldTypeString;
        }

        private void method_5(int int_3, int int_4, int int_5, int int_6, string string_0)
        {
            ListViewItem item = this.DomainListView.SelectedItems[0];
            IDomain domain = (item.Tag as DomainWrap).Domain;
            this.bool_0 = true;
            this.comboBox_0.Items.Clear();
            this.comboBox_0.Items.Add("代码");
            if (domain.FieldType != esriFieldType.esriFieldTypeString)
            {
                this.comboBox_0.Items.Add("范围");
            }
            this.comboBox_0.Size = new Size(int_5, int_6);
            this.comboBox_0.Location = new Point(int_3, int_4);
            this.comboBox_0.Show();
            this.comboBox_0.Text = string_0;
            this.comboBox_0.Focus();
            this.bool_0 = false;
        }

        private IDomain method_6(int int_3, IDomain idomain_0)
        {
            IDomain domain = null;
            switch (int_3)
            {
                case 0:
                    if (idomain_0.Type != esriDomainType.esriDTCodedValue)
                    {
                        domain = new CodedValueDomainClass {
                            FieldType = idomain_0.FieldType
                        };
                    }
                    break;

                case 1:
                    if (idomain_0.Type != esriDomainType.esriDTRange)
                    {
                        domain = new RangeDomainClass();
                        if (idomain_0.FieldType != esriFieldType.esriFieldTypeString)
                        {
                            domain.FieldType = idomain_0.FieldType;
                            break;
                        }
                        domain.FieldType = esriFieldType.esriFieldTypeInteger;
                        (domain as IRangeDomain).MaxValue = 0.0;
                        (domain as IRangeDomain).MinValue = 0.0;
                    }
                    break;
            }
            if (domain != null)
            {
                domain.Description = idomain_0.Description;
                domain.Name = idomain_0.Name;
                domain.SplitPolicy = idomain_0.SplitPolicy;
                domain.MergePolicy = idomain_0.MergePolicy;
            }
            return domain;
        }

        private void method_7(int int_3, int int_4, int int_5, int int_6, string string_0)
        {
            this.textBox_1.Size = new Size(int_5, int_6);
            this.textBox_1.Location = new Point(int_3, int_4);
            this.textBox_1.Show();
            this.textBox_1.Text = this.listViewItem_0.SubItems[1].Text;
            this.textBox_1.SelectAll();
            this.textBox_1.Focus();
        }

        private void method_8(int int_3, int int_4, int int_5, int int_6, string string_0)
        {
            ListViewItem item = this.DomainListView.SelectedItems[0];
            IDomain domain = (item.Tag as DomainWrap).Domain;
            this.bool_0 = true;
            this.comboBox_0.Items.Clear();
            if ((domain.Type == esriDomainType.esriDTRange) && ((((domain.FieldType == esriFieldType.esriFieldTypeDouble) || (domain.FieldType == esriFieldType.esriFieldTypeInteger)) || (domain.FieldType == esriFieldType.esriFieldTypeSingle)) || (domain.FieldType == esriFieldType.esriFieldTypeSmallInteger)))
            {
                this.comboBox_0.Items.Add("默认值");
                this.comboBox_0.Items.Add("复制");
                this.comboBox_0.Items.Add("几何比例");
            }
            else
            {
                this.comboBox_0.Items.Add("默认值");
                this.comboBox_0.Items.Add("复制");
            }
            this.comboBox_0.Size = new Size(int_5, int_6);
            this.comboBox_0.Location = new Point(int_3, int_4);
            this.comboBox_0.Show();
            this.comboBox_0.Text = string_0;
            this.comboBox_0.Focus();
            this.bool_0 = false;
        }

        private void method_9(int int_3, IDomain idomain_0)
        {
            switch (int_3)
            {
                case 0:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTDefaultValue;
                    break;

                case 1:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTDuplicate;
                    break;

                case 2:
                    idomain_0.SplitPolicy = esriSplitPolicyType.esriSPTGeometryRatio;
                    break;
            }
        }

        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_1 = false;
                this.textBox_0.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_1 = true;
                this.textBox_0.Hide();
            }
        }

        private void textBox_0_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
            }
            else
            {
                if ((this.listViewItem_0 != null) && (this.listViewItem_0.SubItems[this.int_2].Text != this.textBox_0.Text))
                {
                    try
                    {
                        if (this.listViewItem_0.Tag == null)
                        {
                            IDomain domain = new RangeDomainClass {
                                FieldType = esriFieldType.esriFieldTypeInteger,
                                MergePolicy = esriMergePolicyType.esriMPTDefaultValue,
                                SplitPolicy = esriSplitPolicyType.esriSPTDefaultValue
                            };
                            (domain as IRangeDomain).MinValue = 0.0;
                            (domain as IRangeDomain).MaxValue = 0.0;
                            this.listViewItem_0.Tag = new DomainWrap(domain, true);
                            this.ilist_0.Add(this.listViewItem_0.Tag);
                            if (this.ilist_0.Count >= (this.DomainListView.Items.Count - 1))
                            {
                                ListViewItem item = new ListViewItem(new string[] { "", "" });
                                this.DomainListView.Items.Add(item);
                            }
                            this.method_1(domain);
                        }
                        if (this.int_2 == 0)
                        {
                            (this.listViewItem_0.Tag as DomainWrap).Domain.Name = this.textBox_0.Text;
                        }
                        else if (this.int_2 == 1)
                        {
                            (this.listViewItem_0.Tag as DomainWrap).Domain.Description = this.textBox_0.Text;
                        }
                        this.listViewItem_0.SubItems[this.int_2].Text = this.textBox_0.Text;
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("",exception, "");
                    }
                }
                this.textBox_0.Hide();
            }
        }

        private void textBox_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_1 = false;
                this.textBox_1.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_1 = true;
                this.textBox_1.Hide();
            }
        }

        private void textBox_1_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
            }
            else
            {
                if ((this.listViewItem_0 != null) && (this.listViewItem_0.SubItems[1].Text != this.textBox_1.Text))
                {
                    try
                    {
                        IDomain domain = (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Domain;
                        switch (this.DomainPropertyListView.Items.IndexOf(this.listViewItem_0))
                        {
                            case 2:
                                (domain as IRangeDomain).MinValue = this.textBox_1.Text;
                                break;

                            case 3:
                                (domain as IRangeDomain).MaxValue = this.textBox_1.Text;
                                break;
                        }
                        this.listViewItem_0.SubItems[1].Text = this.textBox_1.Text;
                        (this.DomainListView.SelectedItems[0].Tag as DomainWrap).Modify = true;
                    }
                    catch (Exception exception)
                    {
                        Logger.Current.Error("",exception, "");
                    }
                }
                this.textBox_1.Hide();
            }
        }

        private void textBox_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_1 = false;
                this.textBox_2.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_1 = true;
                this.textBox_2.Hide();
            }
        }

        private void textBox_2_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
            }
            else
            {
                if ((this.listViewItem_0 != null) && (this.listViewItem_0.SubItems[this.int_2].Text != this.textBox_2.Text))
                {
                    Exception exception;
                    try
                    {
                        string str;
                        object text;
                        ListViewItem item = this.DomainListView.SelectedItems[0];
                        if (item.Tag == null)
                        {
                            goto Label_02EA;
                        }
                        ICodedValueDomain domain = (item.Tag as DomainWrap).Domain as ICodedValueDomain;
                        if (this.int_2 != 0)
                        {
                            goto Label_021E;
                        }
                        esriFieldType fieldType = (item.Tag as DomainWrap).Domain.FieldType;
                        bool flag = true;
                        int index = 0;
                        while (index < domain.CodeCount)
                        {
                            str = domain.get_Name(index);
                            if (str == this.listViewItem_0.SubItems[1].Text)
                            {
                                goto Label_00FD;
                            }
                            index++;
                        }
                        goto Label_016E;
                    Label_00FD:
                        domain.DeleteCode(domain.get_Value(index));
                        switch (fieldType)
                        {
                            case esriFieldType.esriFieldTypeString:
                                text = this.textBox_2.Text;
                                domain.AddCode(text, str);
                                break;

                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeSmallInteger:
                                try
                                {
                                    text = double.Parse(this.textBox_2.Text);
                                    domain.AddCode(text, str);
                                }
                                catch
                                {
                                }
                                break;
                        }
                        flag = false;
                    Label_016E:
                        if (flag)
                        {
                            switch (fieldType)
                            {
                                case esriFieldType.esriFieldTypeString:
                                    try
                                    {
                                        text = this.textBox_2.Text;
                                        domain.AddCode(text, "");
                                    }
                                    catch (Exception exception1)
                                    {
                                        exception = exception1;
                                        Logger.Current.Error("",exception, "");
                                    }
                                    break;

                                case esriFieldType.esriFieldTypeDouble:
                                case esriFieldType.esriFieldTypeInteger:
                                case esriFieldType.esriFieldTypeSingle:
                                case esriFieldType.esriFieldTypeSmallInteger:
                                    try
                                    {
                                        text = double.Parse(this.textBox_2.Text);
                                        domain.AddCode(text, this.listViewItem_0.SubItems[1].Text);
                                    }
                                    catch (Exception exception2)
                                    {
                                        exception = exception2;
                                        Logger.Current.Error("",exception, "");
                                    }
                                    break;
                            }
                        }
                        goto Label_02BC;
                    Label_021E:
                        if (this.int_2 != 1)
                        {
                            goto Label_02BC;
                        }
                        flag = true;
                        try
                        {
                            index = 0;
                            while (index < domain.CodeCount)
                            {
                                if (domain.get_Name(index) == this.listViewItem_0.SubItems[1].Text)
                                {
                                    goto Label_0276;
                                }
                                index++;
                            }
                            goto Label_02B6;
                        Label_0276:
                            text = domain.get_Value(index);
                            domain.DeleteCode(domain.get_Value(index));
                            domain.AddCode(text, this.textBox_2.Text);
                            flag = false;
                        }
                        catch (Exception exception3)
                        {
                            exception = exception3;
                            Logger.Current.Error("",exception, "");
                        }
                    Label_02B6:
                        if (!flag)
                        {
                        }
                    Label_02BC:
                        (item.Tag as DomainWrap).Modify = true;
                        if (this.CodeValueListView.Items.Count != domain.CodeCount)
                        {
                        }
                    Label_02EA:
                        this.listViewItem_0.SubItems[this.int_2].Text = this.textBox_2.Text;
                    }
                    catch (Exception exception4)
                    {
                        exception = exception4;
                        Logger.Current.Error("",exception, "");
                    }
                }
                this.textBox_2.Hide();
            }
        }

        public IWorkspaceDomains WorkspaceDomains
        {
            set
            {
                this.iworkspaceDomains_0 = value;
            }
        }

        protected partial class DomainWrap
        {
            private int int_0;
            private int int_1;
            private bool bool_2;
            private IDomain idomain_0;
            private IDomain idomain_1;
            private string string_0;
            private bool bool_0 = true;
            private bool bool_1 = false;
            private int int_2 = -1;

            public DomainWrap(IDomain idomain_2)
            {
                this.string_0 = "";
                this.idomain_0 = null;
                this.idomain_1 = null;
                this.bool_0 = false;
                this.bool_1 = false;
                this.bool_2 = false;
                this.idomain_1 = idomain_2;
                this.string_0 = idomain_2.Name;
            }

            public DomainWrap(IDomain idomain_2, bool bool_3)
            {
                this.string_0 = "";
                this.idomain_0 = null;
                this.idomain_1 = null;
                this.bool_0 = false;
                this.bool_1 = false;
                this.bool_2 = false;
                this.idomain_1 = idomain_2;
                this.bool_0 = bool_3;
                this.string_0 = idomain_2.Name;
            }

            public void Apply(IWorkspaceDomains iworkspaceDomains_0)
            {
                try
                {
                    if (this.bool_1)
                    {
                        if (!this.bool_0)
                        {
                            iworkspaceDomains_0.DeleteDomain(this.string_0);
                        }
                    }
                    else if (this.bool_0)
                    {
                        iworkspaceDomains_0.AddDomain(this.idomain_1);
                    }
                    else if (this.bool_2)
                    {
                        if (this.idomain_0 != null)
                        {
                            iworkspaceDomains_0.DeleteDomain(this.idomain_0.Name);
                            (iworkspaceDomains_0 as IWorkspaceDomains2).AddDomain(this.idomain_1);
                        }
                        else
                        {
                            (iworkspaceDomains_0 as IWorkspaceDomains2).AlterDomain(this.idomain_1);
                        }
                    }
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147220969)
                    {
                        MessageBox.Show("非数据库所有者，无法修改域值!");
                    }
                    else
                    {
                        Logger.Current.Error("",exception, "");
                    }
                }
                catch (Exception exception2)
                {
                    Logger.Current.Error("",exception2, "");
                }
                this.bool_0 = false;
                this.bool_1 = false;
                this.bool_2 = false;
            }

            public void ChangeDomainType(esriDomainType esriDomainType_0)
            {
                IDomain domain;
                if (!this.bool_0)
                {
                    if (this.idomain_1.Type != esriDomainType_0)
                    {
                        if (this.idomain_0 == null)
                        {
                            this.idomain_0 = this.idomain_1;
                        }
                        domain = null;
                        if (esriDomainType_0 == esriDomainType.esriDTCodedValue)
                        {
                            domain = new CodedValueDomainClass {
                                FieldType = this.idomain_1.FieldType
                            };
                        }
                        else if (esriDomainType_0 == esriDomainType.esriDTRange)
                        {
                            domain = new RangeDomainClass();
                            if (this.idomain_1.FieldType == esriFieldType.esriFieldTypeString)
                            {
                                domain.FieldType = esriFieldType.esriFieldTypeInteger;
                            }
                            else
                            {
                                domain.FieldType = this.idomain_1.FieldType;
                            }
                        }
                        if (domain != null)
                        {
                            domain.Description = this.idomain_1.Description;
                            domain.Name = this.idomain_1.Name;
                            domain.SplitPolicy = this.idomain_1.SplitPolicy;
                            domain.MergePolicy = this.idomain_1.MergePolicy;
                        }
                        this.idomain_1 = domain;
                        this.bool_2 = true;
                    }
                }
                else if (this.idomain_1.Type != esriDomainType_0)
                {
                    domain = null;
                    if (esriDomainType_0 == esriDomainType.esriDTCodedValue)
                    {
                        domain = new CodedValueDomainClass {
                            FieldType = this.idomain_1.FieldType
                        };
                    }
                    else if (esriDomainType_0 == esriDomainType.esriDTRange)
                    {
                        domain = new RangeDomainClass();
                        if (this.idomain_1.FieldType == esriFieldType.esriFieldTypeString)
                        {
                            domain.FieldType = esriFieldType.esriFieldTypeInteger;
                        }
                        else
                        {
                            domain.FieldType = this.idomain_1.FieldType;
                        }
                    }
                    if (domain != null)
                    {
                        domain.Description = this.idomain_1.Description;
                        domain.Name = this.idomain_1.Name;
                        domain.SplitPolicy = this.idomain_1.SplitPolicy;
                        domain.MergePolicy = this.idomain_1.MergePolicy;
                    }
                    this.idomain_1 = domain;
                }
            }

            public void ChangeFileType(esriFieldType esriFieldType_0)
            {
                esriFieldType fieldType = this.idomain_1.FieldType;
                if (esriFieldType_0 != fieldType)
                {
                    int num;
                    object obj2;
                    IDomain domain = (this.idomain_1 as IClone).Clone() as IDomain;
                    if (fieldType == esriFieldType.esriFieldTypeString)
                    {
                        if ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            if (domain is ICodedValueDomain)
                            {
                                if (this.idomain_0 == null)
                                {
                                    this.idomain_0 = this.idomain_1;
                                }
                                this.idomain_1 = new CodedValueDomainClass();
                                this.idomain_1.FieldType = esriFieldType_0;
                                this.idomain_1.Name = this.idomain_0.Name;
                                this.idomain_1.Description = this.idomain_0.Description;
                                this.idomain_1.MergePolicy = this.idomain_0.MergePolicy;
                                this.idomain_1.SplitPolicy = this.idomain_0.SplitPolicy;
                                num = 0;
                                while (num < (domain as ICodedValueDomain).CodeCount)
                                {
                                    obj2 = (domain as ICodedValueDomain).get_Value(num);
                                    try
                                    {
                                        double num2 = double.Parse(obj2.ToString());
                                        (this.idomain_1 as ICodedValueDomain).AddCode(num2, (domain as ICodedValueDomain).get_Name(num));
                                    }
                                    catch
                                    {
                                    }
                                    num++;
                                }
                            }
                        }
                        else
                        {
                            num = 0;
                            while (num < (domain as ICodedValueDomain).CodeCount)
                            {
                                obj2 = (domain as ICodedValueDomain).get_Value(num);
                                (this.idomain_1 as ICodedValueDomain).DeleteCode(obj2);
                                num++;
                            }
                        }
                    }
                    else if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                    {
                        if (domain is ICodedValueDomain)
                        {
                            if (this.idomain_0 == null)
                            {
                                this.idomain_0 = this.idomain_1;
                            }
                            this.idomain_1 = new CodedValueDomainClass();
                            this.idomain_1.FieldType = esriFieldType_0;
                            this.idomain_1.Name = this.idomain_0.Name;
                            this.idomain_1.Description = this.idomain_0.Description;
                            this.idomain_1.MergePolicy = this.idomain_0.MergePolicy;
                            this.idomain_1.SplitPolicy = this.idomain_0.SplitPolicy;
                            for (num = 0; num < (domain as ICodedValueDomain).CodeCount; num++)
                            {
                                obj2 = (domain as ICodedValueDomain).get_Value(num);
                                (this.idomain_1 as ICodedValueDomain).AddCode(obj2.ToString(), (domain as ICodedValueDomain).get_Name(num));
                            }
                        }
                    }
                    else if (domain is ICodedValueDomain)
                    {
                        if (this.idomain_0 == null)
                        {
                            this.idomain_0 = this.idomain_1;
                        }
                        this.idomain_1 = new CodedValueDomainClass();
                        this.idomain_1.FieldType = esriFieldType_0;
                        this.idomain_1.Name = this.idomain_0.Name;
                        this.idomain_1.Description = this.idomain_0.Description;
                        this.idomain_1.MergePolicy = this.idomain_0.MergePolicy;
                        this.idomain_1.SplitPolicy = this.idomain_0.SplitPolicy;
                        for (num = 0; num < (domain as ICodedValueDomain).CodeCount; num++)
                        {
                            obj2 = this.method_0((domain as ICodedValueDomain).get_Value(num), fieldType, esriFieldType_0);
                            if (obj2 != null)
                            {
                                (this.idomain_1 as ICodedValueDomain).AddCode(obj2, (domain as ICodedValueDomain).get_Name(num));
                            }
                        }
                    }
                    else if (domain is IRangeDomain)
                    {
                        if (this.idomain_0 == null)
                        {
                            this.idomain_0 = this.idomain_1;
                        }
                        this.idomain_1 = new RangeDomainClass();
                        this.idomain_1.FieldType = esriFieldType_0;
                        this.idomain_1.Name = this.idomain_0.Name;
                        this.idomain_1.Description = this.idomain_0.Description;
                        this.idomain_1.MergePolicy = this.idomain_0.MergePolicy;
                        this.idomain_1.SplitPolicy = this.idomain_0.SplitPolicy;
                        object obj3 = this.method_0((domain as IRangeDomain).MaxValue, fieldType, esriFieldType_0);
                        object obj4 = this.method_0((domain as IRangeDomain).MinValue, fieldType, esriFieldType_0);
                        if ((obj3 != null) && (obj4 != null))
                        {
                            (this.idomain_1 as IRangeDomain).MinValue = obj4;
                            (this.idomain_1 as IRangeDomain).MaxValue = obj3;
                        }
                    }
                    this.bool_2 = true;
                }
            }

            public void Delete()
            {
                if (!this.bool_0)
                {
                    this.bool_1 = true;
                }
            }

            private object method_0(object object_0, esriFieldType esriFieldType_0, esriFieldType esriFieldType_1)
            {
                DateTime time;
                switch (esriFieldType_1)
                {
                    case esriFieldType.esriFieldTypeSmallInteger:
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                        {
                            try
                            {
                                return short.Parse(object_0.ToString());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if ((esriFieldType_0 != esriFieldType.esriFieldTypeDate) && ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger)))
                        {
                            try
                            {
                                return (short) object_0;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        return null;

                    case esriFieldType.esriFieldTypeInteger:
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                        {
                            try
                            {
                                return int.Parse(object_0.ToString());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeDate)
                        {
                            try
                            {
                                time = (DateTime) object_0;
                                return (int) time.Ticks;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            try
                            {
                                return (int) object_0;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        return null;

                    case esriFieldType.esriFieldTypeSingle:
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                        {
                            try
                            {
                                return float.Parse(object_0.ToString());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeDate)
                        {
                            try
                            {
                                time = (DateTime) object_0;
                                return (float) time.Ticks;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            try
                            {
                                return (float) object_0;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        return null;

                    case esriFieldType.esriFieldTypeDouble:
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                        {
                            try
                            {
                                return double.Parse(object_0.ToString());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeDate)
                        {
                            try
                            {
                                time = (DateTime) object_0;
                                return (double) time.Ticks;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            try
                            {
                                return (double) object_0;
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        return null;

                    case esriFieldType.esriFieldTypeString:
                        return object_0.ToString();

                    case esriFieldType.esriFieldTypeDate:
                        if (esriFieldType_0 == esriFieldType.esriFieldTypeString)
                        {
                            try
                            {
                                return DateTime.Parse(object_0.ToString());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        if ((((esriFieldType_0 == esriFieldType.esriFieldTypeDouble) || (esriFieldType_0 == esriFieldType.esriFieldTypeInteger)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSingle)) || (esriFieldType_0 == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            try
                            {
                                return new DateTime((long) object_0);
                            }
                            catch
                            {
                                return null;
                            }
                        }
                        return null;
                }
                return null;
            }

            public IDomain Domain
            {
                get
                {
                    return this.idomain_1;
                }
                set
                {
                    this.idomain_1 = value;
                }
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
            }

            public bool Modify
            {
                set
                {
                    this.bool_2 = value;
                }
            }
        }
    }
}

