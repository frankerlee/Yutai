using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Raster;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCreateRasterFolder : Form
    {
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReference ispatialReference_1 = null;

        public frmCreateRasterFolder()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtRasterDatastName.Text.Trim().Length == 0)
            {
                MessageBox.Show("新建栅格目录名字不能为空!");
            }
            else
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                if (
                    RasterUtility.createCatalog(ex, this.txtRasterDatastName.Text, "Raster", "Shape",
                        this.ispatialReference_0, this.ispatialReference_1, this.cboPixelType.SelectedIndex == 0, null,
                        this.cboConfigKey.Text) != null)
                {
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.cboConfigKey.Properties.Items.Clear();
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_1(ex as IGeodatabaseRelease, this.ispatialReference_1);
                    this.method_1(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    this.txtLoaction.Name = this.igxObject_0.FullName;
                    if ((this.igxObject_0 is IGxDatabase) && (this.igxObject_0 as IGxDatabase).IsRemoteDatabase)
                    {
                        this.method_2((this.igxObject_0 as IGxDatabase).Workspace as IWorkspaceConfiguration);
                    }
                }
            }
        }

        private void btnSelectRasterSpatialRef_Click(object sender, EventArgs e)
        {
            frmSpatialReference reference = new frmSpatialReference
            {
                SpatialRefrence = this.ispatialReference_1
            };
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_1 = reference.SpatialRefrence;
                this.txtRasterSpatialRefName.Text = this.ispatialReference_1.Name;
            }
        }

        private void btnSelectSpatialRef_Click(object sender, EventArgs e)
        {
            ISpatialReference reference2;
            frmSpatialReference reference = new frmSpatialReference();
            if (this.ispatialReference_0 == null)
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                reference2 = this.method_0(ex as IGeodatabaseRelease);
            }
            else
            {
                reference2 = this.ispatialReference_0;
            }
            reference.SpatialRefrence = reference2;
            if (reference.ShowDialog() == DialogResult.OK)
            {
                this.ispatialReference_0 = reference.SpatialRefrence;
                this.txtSpatialRefName.Text = this.ispatialReference_0.Name;
            }
        }

        private void cboPixelType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void frmCreateRasterFolder_Load(object sender, EventArgs e)
        {
            this.cboConfigKey.Properties.Items.Clear();
            if (this.igxObject_0 != null)
            {
                this.txtLoaction.Text = this.igxObject_0.FullName;
                if ((this.igxObject_0 is IGxDatabase) && (this.igxObject_0 as IGxDatabase).IsRemoteDatabase)
                {
                    this.method_2((this.igxObject_0 as IGxDatabase).Workspace as IWorkspaceConfiguration);
                }
            }
        }

        private ISpatialReference method_0(IGeodatabaseRelease igeodatabaseRelease_0)
        {
            return SpatialReferenctOperator.ConstructCoordinateSystem(igeodatabaseRelease_0);
        }

        private void method_1(IGeodatabaseRelease igeodatabaseRelease_0, ISpatialReference ispatialReference_2)
        {
            SpatialReferenctOperator.ChangeCoordinateSystem(igeodatabaseRelease_0, ispatialReference_2, true);
        }

        private void method_2(IWorkspaceConfiguration iworkspaceConfiguration_0)
        {
            IEnumConfigurationKeyword configurationKeywords = iworkspaceConfiguration_0.ConfigurationKeywords;
            configurationKeywords.Reset();
            for (IConfigurationKeyword keyword2 = configurationKeywords.Next();
                keyword2 != null;
                keyword2 = configurationKeywords.Next())
            {
                this.cboConfigKey.Properties.Items.Add(keyword2.Name);
            }
        }

        public IGxObject OutLocation
        {
            set { this.igxObject_0 = value; }
        }
    }
}