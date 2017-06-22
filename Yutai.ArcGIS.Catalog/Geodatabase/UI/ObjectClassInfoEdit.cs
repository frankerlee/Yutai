using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class ObjectClassInfoEdit : Form
    {
        private bool bool_0 = true;
        private Container container_0 = null;
        private ObjectClassGeneral objectClassGeneral_0 = new ObjectClassGeneral();
        private ObjectClassKeyConfig objectClassKeyConfig_0 = new ObjectClassKeyConfig();
        private ObjectFieldsPage objectFieldsPage_0 = new ObjectFieldsPage();

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

 private void method_0()
        {
        }

        private void method_1(int int_0, IFieldsEdit ifieldsEdit_1)
        {
            IFieldEdit field = new FieldClass();
            field.Name_2 = "OBJECTID";
            field.AliasName_2 = "OBJECTID";
            field.IsNullable_2 = false;
            field.Type_2 = esriFieldType.esriFieldTypeOID;
            ifieldsEdit_1.AddField(field);
            if (int_0 == 1)
            {
                field = new FieldClass();
                field.Name_2 = "SHAPE";
                field.AliasName_2 = "SHAPE";
                field.IsNullable_2 = true;
                field.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDefEdit edit2 = new GeometryDefClass();
                edit2.SpatialReference_2 = new UnknownCoordinateSystemClass();
                edit2.GridCount_2 = 1;
                edit2.set_GridSize(0, 1000.0);
                edit2.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                edit2.HasZ_2 = false;
                edit2.HasM_2 = false;
                field.GeometryDef_2 = edit2;
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

