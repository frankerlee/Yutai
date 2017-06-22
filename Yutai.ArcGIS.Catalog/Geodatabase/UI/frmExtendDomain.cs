using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ExtendClass;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class frmExtendDomain : Form
    {
        private IContainer icontainer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IList ilist_1 = new ArrayList();
        private IList ilist_2 = new ArrayList();
        private IList ilist_3 = new ArrayList();

        public frmExtendDomain()
        {
            this.InitializeComponent();
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            frmNewJLKCodeDomain domain = new frmNewJLKCodeDomain {
                Workspace = this.iworkspace_0
            };
            string[] items = new string[2];
            if (domain.ShowDialog() == DialogResult.OK)
            {
                IJLKCodeValueDomain domain2 = new JLKCodeValueDomain {
                    Workspace = this.iworkspace_0,
                    TableName = domain.TableName
                };
                (domain2 as IDomain).Name = domain.DomainName;
                (domain2 as IDomain).Description = domain.DomainDescription;
                domain2.NameFieldName = domain.NameField;
                domain2.ValueFieldName = domain.ValueField;
                items[0] = (domain2 as IDomain).Name;
                items[1] = (domain2 as IDomain).Description;
                ListViewItem item = new ListViewItem(items) {
                    Tag = new Class3(domain2 as IDomain, true)
                };
                this.ilist_0.Add(item.Tag);
                this.DomainListView.Items.Add(item);
                this.ilist_0.Add(item.Tag);
            }
        }

        private void btnDeleteCode_Click(object sender, EventArgs e)
        {
        }

        private void btnEditCode_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IWorkspaceDomains domains = this.iworkspace_0 as IWorkspaceDomains;
            if (domains != null)
            {
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    (this.ilist_0[i] as Class3).Apply(domains);
                }
            }
        }

 private void DomainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDeleteCode.Enabled = this.DomainListView.SelectedItems.Count > 0;
            this.btnEditCode.Enabled = this.DomainListView.SelectedItems.Count > 0;
            this.listView1.Items.Clear();
            if (this.DomainListView.SelectedItems.Count != 1)
            {
            }
        }

        private void frmExtendDomain_Load(object sender, EventArgs e)
        {
            IWorkspaceDomains domains = this.iworkspace_0 as IWorkspaceDomains;
            if (this.iworkspace_0 != null)
            {
                IEnumDomain domain = domains.Domains;
                string[] items = new string[2];
                if (domain != null)
                {
                    domain.Reset();
                    for (IDomain domain2 = domain.Next(); domain2 != null; domain2 = domain.Next())
                    {
                        if (domain2 is IJLKCodeValueDomain)
                        {
                            items[0] = domain2.Name;
                            items[1] = domain2.Description;
                            ListViewItem item = new ListViewItem(items) {
                                Tag = new Class3(domain2)
                            };
                            this.ilist_0.Add(item.Tag);
                            this.DomainListView.Items.Add(item);
                        }
                    }
                }
            }
        }

 public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }

        private partial class Class3
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
            public Class3(IDomain idomain_2)
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

            public Class3(IDomain idomain_2, bool bool_3)
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

