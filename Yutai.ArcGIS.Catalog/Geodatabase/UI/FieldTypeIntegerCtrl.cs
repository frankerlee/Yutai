using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.CodeDomainEx;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class FieldTypeIntegerCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ComboBoxEdit cboAllowNull;
        private ComboBoxEdit cboDomain;
        private Container container_0 = null;
        private IFieldEdit ifieldEdit_0;
        private IWorkspace iworkspace_0 = null;
        private TextEdit textEdit1;
        private TextEdit textEdit3;
        private TextEdit textEdit4;
        private TextEdit textEdit6;
        private TextEdit textEdit8;
        private TextEdit txtAlias;
        private TextEdit txtDefault;
        private TextEdit txtPrecision;

        public event FieldChangedHandler FieldChanged;

        public event ValueChangedHandler ValueChanged;

        public FieldTypeIntegerCtrl()
        {
            this.InitializeComponent();
        }

        private void cboAllowNull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.IsNullable_2 = this.cboAllowNull.SelectedIndex == 1;
            }
        }

        private void cboDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.cboDomain.SelectedItem is DomainWrap1)
                {
                    if ((this.cboDomain.SelectedItem as DomainWrap1).Domain != null)
                    {
                        this.ifieldEdit_0.Domain_2 = (this.cboDomain.SelectedItem as DomainWrap1).Domain;
                        if (NewObjectClassHelper.m_pObjectClassHelper != null)
                        {
                            if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomanIsExit(this.ifieldEdit_0))
                            {
                                NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0] = null;
                            }
                            else
                            {
                                NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.Add(this.ifieldEdit_0, null);
                            }
                        }
                    }
                    else if (((this.cboDomain.SelectedItem as DomainWrap1).DomainEx != null) && (NewObjectClassHelper.m_pObjectClassHelper != null))
                    {
                        if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomanIsExit(this.ifieldEdit_0))
                        {
                            NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0] = (this.cboDomain.SelectedItem as DomainWrap1).DomainEx;
                        }
                        else
                        {
                            NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.Add(this.ifieldEdit_0, (this.cboDomain.SelectedItem as DomainWrap1).DomainEx);
                        }
                    }
                }
                else
                {
                    this.ifieldEdit_0.Domain_2 = null;
                    if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomanIsExit(this.ifieldEdit_0))
                    {
                        NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0] = null;
                    }
                }
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
                this.method_0(this.ifieldEdit_0, FieldChangeType.FCTDomain);
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void FieldTypeIntegerCtrl_Load(object sender, EventArgs e)
        {
            this.cboDomain.Properties.Items.Clear();
            try
            {
                this.cboDomain.Properties.Items.Add(new DomainWrap1());
                int num = -1;
                if (this.iworkspace_0 is IWorkspaceDomains)
                {
                    IEnumDomain domains = (this.iworkspace_0 as IWorkspaceDomains).Domains;
                    if (domains != null)
                    {
                        domains.Reset();
                        for (IDomain domain2 = domains.Next(); domain2 != null; domain2 = domains.Next())
                        {
                            if ((domain2.FieldType == esriFieldType.esriFieldTypeInteger) || (domain2.FieldType == esriFieldType.esriFieldTypeSmallInteger))
                            {
                                this.cboDomain.Properties.Items.Add(new DomainWrap1(domain2));
                                if ((this.ifieldEdit_0.Domain != null) && (this.ifieldEdit_0.Domain.Name == domain2.Name))
                                {
                                    num = this.cboDomain.Properties.Items.Count - 1;
                                }
                            }
                        }
                    }
                    this.cboDomain.SelectedIndex = num;
                }
                string domainID = "";
                if (NewObjectClassHelper.m_pObjectClassHelper != null)
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.ContainsKey(this.ifieldEdit_0))
                    {
                        if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0] != null)
                        {
                            domainID = NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0].DomainID;
                        }
                    }
                    else if (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass != null)
                    {
                        string name = (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IDataset).Name;
                        domainID = CodeDomainManage.GetDamainName(this.ifieldEdit_0.Name, name);
                    }
                }
                using (List<CodeDomainEx>.Enumerator enumerator = CodeDomainManage.CodeDomainExs.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                       CodeDomainEx current = enumerator.Current;
                        if ((current.FieldType == esriFieldType.esriFieldTypeInteger) || (current.FieldType == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            this.cboDomain.Properties.Items.Add(new DomainWrap1(current));
                            if (domainID == current.DomainID)
                            {
                                goto Label_021F;
                            }
                        }
                    }
                    goto Label_0247;
                Label_021F:
                    num = this.cboDomain.Properties.Items.Count - 1;
                }
            Label_0247:
                this.cboDomain.SelectedIndex = num;
            }
            catch
            {
            }
            this.method_1();
        }

        private void FieldTypeIntegerCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_1();
            }
        }

        public void Init()
        {
            this.method_1();
        }

        private void InitializeComponent()
        {
            this.textEdit1 = new TextEdit();
            this.txtAlias = new TextEdit();
            this.textEdit4 = new TextEdit();
            this.cboAllowNull = new ComboBoxEdit();
            this.cboDomain = new ComboBoxEdit();
            this.textEdit3 = new TextEdit();
            this.txtDefault = new TextEdit();
            this.textEdit6 = new TextEdit();
            this.txtPrecision = new TextEdit();
            this.textEdit8 = new TextEdit();
            this.textEdit1.Properties.BeginInit();
            this.txtAlias.Properties.BeginInit();
            this.textEdit4.Properties.BeginInit();
            this.cboAllowNull.Properties.BeginInit();
            this.cboDomain.Properties.BeginInit();
            this.textEdit3.Properties.BeginInit();
            this.txtDefault.Properties.BeginInit();
            this.textEdit6.Properties.BeginInit();
            this.txtPrecision.Properties.BeginInit();
            this.textEdit8.Properties.BeginInit();
            base.SuspendLayout();
            this.textEdit1.EditValue = "别名";
            this.textEdit1.Location = new Point(8, 8);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.AllowFocused = false;
            this.textEdit1.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(0x58, 0x15);
            this.textEdit1.TabIndex = 0;
            this.txtAlias.Location = new Point(0x60, 8);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Properties.BorderStyle = BorderStyles.Simple;
            this.txtAlias.Size = new Size(0x70, 0x15);
            this.txtAlias.TabIndex = 1;
            this.txtAlias.EditValueChanged += new EventHandler(this.txtAlias_EditValueChanged);
            this.textEdit4.EditValue = "允许空值";
            this.textEdit4.Location = new Point(8, 0x1b);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit4.Properties.ReadOnly = true;
            this.textEdit4.Size = new Size(0x58, 0x15);
            this.textEdit4.TabIndex = 2;
            this.cboAllowNull.EditValue = "是";
            this.cboAllowNull.Location = new Point(0x60, 0x1b);
            this.cboAllowNull.Name = "cboAllowNull";
            this.cboAllowNull.Properties.BorderStyle = BorderStyles.Simple;
            this.cboAllowNull.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboAllowNull.Properties.Items.AddRange(new object[] { "否", "是" });
            this.cboAllowNull.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboAllowNull.Size = new Size(0x70, 0x15);
            this.cboAllowNull.TabIndex = 4;
            this.cboAllowNull.SelectedIndexChanged += new EventHandler(this.cboAllowNull_SelectedIndexChanged);
            this.cboDomain.EditValue = "";
            this.cboDomain.Location = new Point(0x60, 0x2e);
            this.cboDomain.Name = "cboDomain";
            this.cboDomain.Properties.BorderStyle = BorderStyles.Simple;
            this.cboDomain.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDomain.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            this.cboDomain.Size = new Size(0x70, 0x15);
            this.cboDomain.TabIndex = 6;
            this.cboDomain.SelectedIndexChanged += new EventHandler(this.cboDomain_SelectedIndexChanged);
            this.textEdit3.EditValue = "域";
            this.textEdit3.Location = new Point(8, 0x2e);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit3.Properties.ReadOnly = true;
            this.textEdit3.Size = new Size(0x58, 0x15);
            this.textEdit3.TabIndex = 5;
            this.txtDefault.EditValue = "";
            this.txtDefault.Location = new Point(0x60, 0x41);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Properties.BorderStyle = BorderStyles.Simple;
            this.txtDefault.Size = new Size(0x70, 0x15);
            this.txtDefault.TabIndex = 8;
            this.txtDefault.EditValueChanged += new EventHandler(this.txtDefault_EditValueChanged);
            this.textEdit6.EditValue = "默认值";
            this.textEdit6.Location = new Point(8, 0x41);
            this.textEdit6.Name = "textEdit6";
            this.textEdit6.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit6.Properties.ReadOnly = true;
            this.textEdit6.Size = new Size(0x58, 0x15);
            this.textEdit6.TabIndex = 7;
            this.txtPrecision.EditValue = "0";
            this.txtPrecision.Location = new Point(0x60, 0x54);
            this.txtPrecision.Name = "txtPrecision";
            this.txtPrecision.Properties.BorderStyle = BorderStyles.Simple;
            this.txtPrecision.Size = new Size(0x70, 0x15);
            this.txtPrecision.TabIndex = 10;
            this.txtPrecision.EditValueChanged += new EventHandler(this.txtPrecision_EditValueChanged);
            this.textEdit8.EditValue = "精度";
            this.textEdit8.Location = new Point(8, 0x54);
            this.textEdit8.Name = "textEdit8";
            this.textEdit8.Properties.BorderStyle = BorderStyles.Simple;
            this.textEdit8.Properties.ReadOnly = true;
            this.textEdit8.Size = new Size(0x58, 0x15);
            this.textEdit8.TabIndex = 9;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.txtPrecision);
            base.Controls.Add(this.textEdit8);
            base.Controls.Add(this.txtDefault);
            base.Controls.Add(this.textEdit6);
            base.Controls.Add(this.cboDomain);
            base.Controls.Add(this.textEdit3);
            base.Controls.Add(this.cboAllowNull);
            base.Controls.Add(this.textEdit4);
            base.Controls.Add(this.txtAlias);
            base.Controls.Add(this.textEdit1);
            base.Name = "FieldTypeIntegerCtrl";
            base.Size = new Size(240, 0xd0);
            base.VisibleChanged += new EventHandler(this.FieldTypeIntegerCtrl_VisibleChanged);
            base.Load += new EventHandler(this.FieldTypeIntegerCtrl_Load);
            this.textEdit1.Properties.EndInit();
            this.txtAlias.Properties.EndInit();
            this.textEdit4.Properties.EndInit();
            this.cboAllowNull.Properties.EndInit();
            this.cboDomain.Properties.EndInit();
            this.textEdit3.Properties.EndInit();
            this.txtDefault.Properties.EndInit();
            this.textEdit6.Properties.EndInit();
            this.txtPrecision.Properties.EndInit();
            this.textEdit8.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0(IField ifield_0, FieldChangeType fieldChangeType_0)
        {
            if (this.FieldChanged != null)
            {
                this.FieldChanged(ifield_0, fieldChangeType_0);
            }
        }

        private void method_1()
        {
            this.bool_0 = false;
            this.txtAlias.Text = this.ifieldEdit_0.AliasName;
            this.cboAllowNull.SelectedIndex = Convert.ToInt32(this.ifieldEdit_0.IsNullable);
            this.txtDefault.Text = this.ifieldEdit_0.DefaultValue.ToString();
            this.txtPrecision.Text = this.ifieldEdit_0.Precision.ToString();
            this.txtDefault.Properties.ReadOnly = this.bool_1;
            this.txtPrecision.Properties.ReadOnly = this.bool_1;
            this.cboAllowNull.Enabled = !this.bool_1;
            if (ObjectClassShareData.m_IsShapeFile)
            {
                this.txtAlias.Enabled = false;
            }
            string name = "";
            int num2 = -1;
            if (this.ifieldEdit_0.Domain != null)
            {
                name = this.ifieldEdit_0.Domain.Name;
            }
            else if (NewObjectClassHelper.m_pObjectClassHelper != null)
            {
                if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomanIsExit(this.ifieldEdit_0))
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0] != null)
                    {
                        name = NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[this.ifieldEdit_0].DomainID;
                    }
                }
                else if (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass != null)
                {
                    string str2 = (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IDataset).Name;
                    name = CodeDomainManage.GetDamainName(this.ifieldEdit_0.Name, str2);
                }
            }
            if (name.Length > 0)
            {
                for (int i = 0; i < this.cboDomain.Properties.Items.Count; i++)
                {
                    DomainWrap1 wrap = this.cboDomain.Properties.Items[i] as DomainWrap1;
                    if (this.ifieldEdit_0.Domain != null)
                    {
                        if ((wrap.Domain == null) || !(wrap.Domain.Name == name))
                        {
                            continue;
                        }
                        num2 = i;
                        break;
                    }
                    if ((wrap.DomainEx != null) && (wrap.DomainEx.DomainID == name))
                    {
                        num2 = i;
                        break;
                    }
                }
            }
            this.cboDomain.SelectedIndex = num2;
            this.bool_0 = true;
        }

        private void txtAlias_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.ifieldEdit_0.AliasName_2 = this.txtAlias.Text;
                if (this.ValueChanged != null)
                {
                    this.ValueChanged(this, e);
                }
                this.method_0(this.ifieldEdit_0, FieldChangeType.FCTAlias);
            }
        }

        private void txtDefault_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double.Parse(this.txtDefault.Text);
                    this.ifieldEdit_0.DefaultValue_2 = this.txtDefault.Text;
                    if (this.ValueChanged != null)
                    {
                        this.ValueChanged(this, e);
                    }
                }
                catch
                {
                }
            }
        }

        private void txtPrecision_EditValueChanged(object sender, EventArgs e)
        {
        }

        public IField Filed
        {
            set
            {
                this.ifieldEdit_0 = value as IFieldEdit;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

