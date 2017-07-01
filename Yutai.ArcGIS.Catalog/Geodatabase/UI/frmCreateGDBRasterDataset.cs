using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Raster;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCreateGDBRasterDataset : Form
    {
        private Container container_0 = null;
        private IGxObject igxObject_0 = null;
        private ISpatialReference ispatialReference_0 = null;

        public frmCreateGDBRasterDataset()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtRasterDatastName.Text.Trim().Length == 0)
            {
                MessageBox.Show("新建栅格数据集名字不能为空!");
            }
            else
            {
                IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                try
                {
                    if (
                        RasterUtility.CreateSDERasterDs(ex, this.txtRasterDatastName.Text,
                            int.Parse(this.txtRasterBand.Text), (rstPixelType) this.cboPixelType.SelectedIndex,
                            this.ispatialReference_0, null, null, "") != null)
                    {
                        base.DialogResult = DialogResult.OK;
                    }
                }
                catch (COMException exception)
                {
                    int errorCode = exception.ErrorCode;
                    MessageBox.Show(exception.Message);
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
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    IRasterWorkspaceEx ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_0(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    this.txtLoaction.Text = this.igxObject_0.FullName;
                }
            }
        }

        private void btnSelectSpatialRef_Click(object sender, EventArgs e)
        {
            IRasterWorkspaceEx ex;
            ISpatialReference reference2;
            frmSpatialReference reference = new frmSpatialReference();
            if (this.ispatialReference_0 == null)
            {
                ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                reference2 = this.method_1(ex as IGeodatabaseRelease);
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
                if (this.igxObject_0 != null)
                {
                    ex = this.igxObject_0.InternalObjectName.Open() as IRasterWorkspaceEx;
                    this.method_0(ex as IGeodatabaseRelease, this.ispatialReference_0);
                    ComReleaser.ReleaseCOMObject(ex);
                }
            }
        }

        private void frmCreateGDBRasterDataset_Load(object sender, EventArgs e)
        {
            if (this.igxObject_0 != null)
            {
                this.txtLoaction.Text = this.igxObject_0.FullName;
            }
        }

        private void method_0(IGeodatabaseRelease igeodatabaseRelease_0, ISpatialReference ispatialReference_1)
        {
            SpatialReferenctOperator.ChangeCoordinateSystem(igeodatabaseRelease_0, ispatialReference_1, true);
        }

        private ISpatialReference method_1(IGeodatabaseRelease igeodatabaseRelease_0)
        {
            return SpatialReferenctOperator.ConstructCoordinateSystem(igeodatabaseRelease_0);
        }

        public IGxObject OutLocation
        {
            set { this.igxObject_0 = value; }
        }
    }
}