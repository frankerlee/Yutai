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
    internal partial class FieldTypeIntegerCtrl : UserControl, IControlBaseInterface
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IWorkspace iworkspace_0 = null;

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

