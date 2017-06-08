namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ObjectClassInfoEdit : Form
    {
        private bool bool_0 = true;
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private enumUseType enumUseType_0;
        private IFeatureDataset ifeatureDataset_0;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private IFieldsEdit ifieldsEdit_0;
        private IObjectClass iobjectClass_0;
        private ObjectClassGeneral objectClassGeneral_0 = new ObjectClassGeneral();
        private ObjectClassKeyConfig objectClassKeyConfig_0 = new ObjectClassKeyConfig();
        private ObjectFieldsPage objectFieldsPage_0 = new ObjectFieldsPage();
        private TabControl tabControl1;

        public ObjectClassInfoEdit()
        {
            this.InitializeComponent();
            ObjectClassHelper.CreateOCHelper();
            NewObjectClassHelper.m_pObjectClassHelper = new NewObjectClassHelper();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!ObjectClassShareData.m_IsShapeFile && (this.objectClassGeneral_0.AliasName != this.iobjectClass_0.AliasName))
            {
                (this.iobjectClass_0 as IClassSchemaEdit).AlterAliasName(this.objectClassGeneral_0.AliasName);
            }
            this.objectFieldsPage_0.Apply();
            this.btnApply.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.btnApply.Enabled)
            {
                if (this.objectClassGeneral_0.AliasName != this.iobjectClass_0.AliasName)
                {
                    (this.iobjectClass_0 as IClassSchemaEdit).AlterAliasName(this.objectClassGeneral_0.AliasName);
                }
                if (!this.objectFieldsPage_0.Apply())
                {
                    return;
                }
            }
            base.DialogResult = DialogResult.OK;
            base.Close();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ObjectClassInfoEdit));
            this.btnOK = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.tabControl1 = new TabControl();
            base.SuspendLayout();
            this.btnOK.Location = new System.Drawing.Point(0xa8, 0x1f0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnApply.Location = new System.Drawing.Point(0x128, 0x1f0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xe8, 0x1f0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x170, 480);
            this.tabControl1.TabIndex = 3;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x180, 0x215);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnOK);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "ObjectClassInfoEdit";
            this.Text = "新建";
            base.Load += new EventHandler(this.ObjectClassInfoEdit_Load);
            base.ResumeLayout(false);
        }

        private void method_0()
        {
        }

        private void method_1(int int_0, IFieldsEdit ifieldsEdit_1)
        {
            IFieldEdit field = new FieldClass {
                Name = "OBJECTID",
                AliasName = "OBJECTID",
                IsNullable = false,
                Type = esriFieldType.esriFieldTypeOID
            };
            ifieldsEdit_1.AddField(field);
            if (int_0 == 1)
            {
                field = new FieldClass {
                    Name = "SHAPE",
                    AliasName = "SHAPE",
                    IsNullable = true,
                    Type = esriFieldType.esriFieldTypeGeometry
                };
                IGeometryDefEdit edit2 = new GeometryDefClass {
                    SpatialReference = new UnknownCoordinateSystemClass(),
                    GridCount = 1
                };
                edit2.set_GridSize(0, 1000.0);
                edit2.GeometryType = esriGeometryType.esriGeometryPolygon;
                edit2.HasZ = false;
                edit2.HasM = false;
                field.GeometryDef = edit2;
                ifieldsEdit_1.AddField(field);
            }
        }

        private bool method_2(IObjectClass iobjectClass_1)
        {
            bool flag;
            try
            {
                IEnumSchemaLockInfo info;
                (iobjectClass_1 as ISchemaLock).GetCurrentSchemaLocks(out info);
                info.Reset();
                for (ISchemaLockInfo info2 = info.Next(); info2 != null; info2 = info.Next())
                {
                    if (info2.SchemaLockType == esriSchemaLock.esriExclusiveSchemaLock)
                    {
                        goto Label_0041;
                    }
                }
                return true;
            Label_0041:
                flag = false;
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private void method_3(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private void method_4(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private void ObjectClassInfoEdit_Load(object sender, EventArgs e)
        {
            this.objectFieldsPage_0.Fields = this.iobjectClass_0.Fields;
            this.objectFieldsPage_0.IsEdit = true;
            if (this.iobjectClass_0 is IFeatureClass)
            {
                this.objectFieldsPage_0.ShapeFieldName = (this.iobjectClass_0 as IFeatureClass).ShapeFieldName;
            }
            this.objectClassGeneral_0.IsEdit = true;
            this.objectClassGeneral_0.ClassName = (this.iobjectClass_0 as IDataset).Name;
            this.objectClassGeneral_0.AliasName = this.iobjectClass_0.AliasName;
            TabPage page = new TabPage("常规");
            page.Controls.Add(this.objectClassGeneral_0);
            this.objectClassGeneral_0.ValueChanged += new ValueChangedHandler(this.method_3);
            this.tabControl1.TabPages.Add(page);
            page = new TabPage("字段");
            page.Controls.Add(this.objectFieldsPage_0);
            this.objectFieldsPage_0.Table = this.iobjectClass_0 as ITable;
            this.objectFieldsPage_0.Workspace = this.ifeatureWorkspace_0 as IWorkspace;
            this.objectFieldsPage_0.ValueChanged += new ValueChangedHandler(this.method_4);
            this.tabControl1.TabPages.Add(page);
            this.btnApply.Enabled = false;
        }

        public object Dataset
        {
            set
            {
                this.ifeatureWorkspace_0 = value as IFeatureWorkspace;
                if (this.ifeatureWorkspace_0 != null)
                {
                    if ((this.ifeatureWorkspace_0 as IWorkspace).Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        ObjectClassShareData.m_IsShapeFile = true;
                    }
                    else
                    {
                        ObjectClassShareData.m_IsShapeFile = false;
                    }
                }
                this.ifeatureDataset_0 = value as IFeatureDataset;
            }
        }

        public IFieldsEdit FiedsEdit
        {
            get
            {
                return this.ifieldsEdit_0;
            }
            set
            {
                this.ifieldsEdit_0 = value;
            }
        }

        public IObjectClass ObjectClass
        {
            set
            {
                this.iobjectClass_0 = value;
                this.ifeatureWorkspace_0 = (this.iobjectClass_0 as IDataset).Workspace as IFeatureWorkspace;
                if (this.ifeatureWorkspace_0 != null)
                {
                    if ((this.ifeatureWorkspace_0 as IWorkspace).Type == esriWorkspaceType.esriFileSystemWorkspace)
                    {
                        ObjectClassShareData.m_IsShapeFile = true;
                    }
                    else
                    {
                        ObjectClassShareData.m_IsShapeFile = false;
                    }
                }
                ObjectClassHelper.m_pObjectClassHelper.m_IsFeatureClass = value is IFeatureClass;
                NewObjectClassHelper.m_pObjectClassHelper.ObjectClass = value;
                this.bool_0 = this.method_2(this.iobjectClass_0);
            }
        }

        public enumUseType UseType
        {
            set
            {
                this.enumUseType_0 = value;
                this.objectFieldsPage_0.UseType = value;
            }
        }
    }
}

