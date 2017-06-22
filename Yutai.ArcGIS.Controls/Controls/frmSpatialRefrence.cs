using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmSpatialRefrence : Form
    {
        private GeoCoordSys m_GeoCoordSys = new GeoCoordSys();
        private bool m_IsNew = true;
        private ProjCoordSys m_ProjCoordSys = new ProjCoordSys();
        private enumSpatialRefrenceType m_SRType = enumSpatialRefrenceType.enumProjectCoord;

        public frmSpatialRefrence()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.m_SRType)
                {
                    case enumSpatialRefrenceType.enumProjectCoord:
                        this.m_pSpatialRefrence = this.m_ProjCoordSys.GetSpatialRefrence();
                        if (this.m_pSpatialRefrence != null)
                        {
                            goto Label_0072;
                        }
                        return;

                    case enumSpatialRefrenceType.enumGeographicCoord:
                        this.m_pSpatialRefrence = this.m_GeoCoordSys.GetSpatialRefrence();
                        if (this.m_pSpatialRefrence != null)
                        {
                            goto Label_0072;
                        }
                        return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return;
        Label_0072:
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

 private void frmSpatialRefrence_Load(object sender, EventArgs e)
        {
            switch (this.m_SRType)
            {
                case enumSpatialRefrenceType.enumProjectCoord:
                    if (this.m_pSpatialRefrence == null)
                    {
                        this.Text = "新建投影坐标系统";
                        break;
                    }
                    this.Text = "投影坐标系统属性";
                    this.m_ProjCoordSys.ProjectedCoordinateSystem = (IProjectedCoordinateSystem) this.m_pSpatialRefrence;
                    break;

                case enumSpatialRefrenceType.enumGeographicCoord:
                    if (this.m_pSpatialRefrence == null)
                    {
                        this.Text = "新建地理坐标系统";
                    }
                    else
                    {
                        this.Text = "地理坐标系统属性";
                        this.m_GeoCoordSys.GeographicCoordinateSystem = (IGeographicCoordinateSystem) this.m_pSpatialRefrence;
                    }
                    base.Controls.Add(this.m_GeoCoordSys);
                    return;

                default:
                    return;
            }
            base.Controls.Add(this.m_ProjCoordSys);
        }

 public ISpatialReference SpatialRefrence
        {
            get
            {
                return this.m_pSpatialRefrence;
            }
            set
            {
                this.m_pSpatialRefrence = value;
                if (this.m_pSpatialRefrence is IGeographicCoordinateSystem)
                {
                    this.m_SRType = enumSpatialRefrenceType.enumGeographicCoord;
                }
                else if (this.m_pSpatialRefrence is IProjectedCoordinateSystem)
                {
                    this.m_SRType = enumSpatialRefrenceType.enumProjectCoord;
                }
            }
        }

        public enumSpatialRefrenceType SpatialRefrenceType
        {
            get
            {
                return this.m_SRType;
            }
            set
            {
                this.m_SRType = value;
            }
        }

        public enum enumSpatialRefrenceType
        {
            enumProjectCoord,
            enumGeographicCoord
        }
    }
}

