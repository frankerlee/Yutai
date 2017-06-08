namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.ExtendClass;
    using JLK.Utility;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    internal class frmExtendDomain : Form
    {
        private SimpleButton btnAddCode;
        private SimpleButton btnDeleteCode;
        private SimpleButton btnEditCode;
        private SimpleButton btnOK;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ColumnHeader columnHeader_3;
        private ColumnHeader columnHeader_4;
        private ListView DomainListView;
        private IContainer icontainer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IList ilist_1 = new ArrayList();
        private IList ilist_2 = new ArrayList();
        private IList ilist_3 = new ArrayList();
        private IWorkspace iworkspace_0;
        private ListView listView1;
        private SimpleButton simpleButton1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.DomainListView = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.btnDeleteCode = new SimpleButton();
            this.btnEditCode = new SimpleButton();
            this.btnAddCode = new SimpleButton();
            this.listView1 = new ListView();
            this.columnHeader_2 = new ColumnHeader();
            this.columnHeader_3 = new ColumnHeader();
            this.columnHeader_4 = new ColumnHeader();
            this.simpleButton1 = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.DomainListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.DomainListView.FullRowSelect = true;
            this.DomainListView.GridLines = true;
            this.DomainListView.HideSelection = false;
            this.DomainListView.Location = new Point(12, 12);
            this.DomainListView.MultiSelect = false;
            this.DomainListView.Name = "DomainListView";
            this.DomainListView.Size = new Size(0x135, 0x61);
            this.DomainListView.TabIndex = 1;
            this.DomainListView.UseCompatibleStateImageBehavior = false;
            this.DomainListView.View = View.Details;
            this.DomainListView.SelectedIndexChanged += new EventHandler(this.DomainListView_SelectedIndexChanged);
            this.columnHeader_0.Text = "域名";
            this.columnHeader_0.Width = 0x7d;
            this.columnHeader_1.Text = "描述";
            this.columnHeader_1.Width = 0x9d;
            this.btnDeleteCode.Location = new Point(0x147, 0x48);
            this.btnDeleteCode.Name = "btnDeleteCode";
            this.btnDeleteCode.Size = new Size(0x40, 0x18);
            this.btnDeleteCode.TabIndex = 10;
            this.btnDeleteCode.Text = "删除";
            this.btnDeleteCode.Click += new EventHandler(this.btnDeleteCode_Click);
            this.btnEditCode.Location = new Point(0x147, 0x2a);
            this.btnEditCode.Name = "btnEditCode";
            this.btnEditCode.Size = new Size(0x40, 0x18);
            this.btnEditCode.TabIndex = 9;
            this.btnEditCode.Text = "编辑...";
            this.btnEditCode.Click += new EventHandler(this.btnEditCode_Click);
            this.btnAddCode.Location = new Point(0x147, 12);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new Size(0x40, 0x18);
            this.btnAddCode.TabIndex = 8;
            this.btnAddCode.Text = "添加...";
            this.btnAddCode.Click += new EventHandler(this.btnAddCode_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_2, this.columnHeader_3, this.columnHeader_4 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(12, 0x73);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x17b, 0x8f);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_2.Text = "表名";
            this.columnHeader_2.Width = 0x73;
            this.columnHeader_3.Text = "名称字段";
            this.columnHeader_3.Width = 130;
            this.columnHeader_4.Text = "值字段";
            this.columnHeader_4.Width = 0x6b;
            this.simpleButton1.DialogResult = DialogResult.Cancel;
            this.simpleButton1.Location = new Point(0x147, 0x108);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x40, 0x18);
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x101, 0x108);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x197, 300);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnDeleteCode);
            base.Controls.Add(this.btnEditCode);
            base.Controls.Add(this.btnAddCode);
            base.Controls.Add(this.DomainListView);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExtendDomain";
            this.Text = "扩展域值管理";
            base.Load += new EventHandler(this.frmExtendDomain_Load);
            base.ResumeLayout(false);
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }

        private class Class3
        {
            private bool bool_0;
            private bool bool_1;
            private bool bool_2;
            private IDomain idomain_0;
            private IDomain idomain_1;
            private string string_0;

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
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                }
                catch (Exception exception2)
                {
                    CErrorLog.writeErrorLog(this, exception2, "");
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

