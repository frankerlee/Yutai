using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmSpatialRefrence : Form
    {
        private bool bool_0 = true;
        private Container container_0 = null;
        private enumSpatialRefrenceType enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumProjectCoord;
        private GeoCoordSys geoCoordSys_0 = new GeoCoordSys();
        private ProjCoordSys projCoordSys_0 = new ProjCoordSys();

        public frmSpatialRefrence()
        {
            if (CatalogLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.enumSpatialRefrenceType_0)
                {
                    case enumSpatialRefrenceType.enumProjectCoord:
                        this.ispatialReference_0 = this.projCoordSys_0.GetSpatialRefrence();
                        if (this.ispatialReference_0 != null)
                        {
                            goto Label_0065;
                        }
                        return;

                    case enumSpatialRefrenceType.enumGeographicCoord:
                        this.ispatialReference_0 = this.geoCoordSys_0.GetSpatialRefrence();
                        if (this.ispatialReference_0 != null)
                        {
                            goto Label_0065;
                        }
                        return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return;
            Label_0065:
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void frmSpatialRefrence_Load(object sender, EventArgs e)
        {
            switch (this.enumSpatialRefrenceType_0)
            {
                case enumSpatialRefrenceType.enumProjectCoord:
                    if (this.ispatialReference_0 == null)
                    {
                        this.Text = "新建投影坐标系统";
                        break;
                    }
                    this.Text = "投影坐标系统属性";
                    this.projCoordSys_0.ProjectedCoordinateSystem =
                        (IProjectedCoordinateSystem) this.ispatialReference_0;
                    break;

                case enumSpatialRefrenceType.enumGeographicCoord:
                    if (this.ispatialReference_0 == null)
                    {
                        this.Text = "新建地理坐标系统";
                    }
                    else
                    {
                        this.Text = "地理坐标系统属性";
                        this.geoCoordSys_0.GeographicCoordinateSystem =
                            (IGeographicCoordinateSystem) this.ispatialReference_0;
                    }
                    base.Controls.Add(this.geoCoordSys_0);
                    return;

                default:
                    return;
            }
            base.Controls.Add(this.projCoordSys_0);
        }

        public ISpatialReference SpatialRefrence
        {
            get { return this.ispatialReference_0; }
            set
            {
                this.ispatialReference_0 = value;
                if (this.ispatialReference_0 is IGeographicCoordinateSystem)
                {
                    this.enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumGeographicCoord;
                }
                else if (this.ispatialReference_0 is IProjectedCoordinateSystem)
                {
                    this.enumSpatialRefrenceType_0 = enumSpatialRefrenceType.enumProjectCoord;
                }
            }
        }

        public enumSpatialRefrenceType SpatialRefrenceType
        {
            get { return this.enumSpatialRefrenceType_0; }
            set { this.enumSpatialRefrenceType_0 = value; }
        }

        public enum enumSpatialRefrenceType
        {
            enumProjectCoord,
            enumGeographicCoord
        }
    }
}