using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class NewDatasetTolerancePage : UserControl
    {
        public CheckEdit chkUseDefault;
        private IContainer icontainer_0 = null;

        public NewDatasetTolerancePage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                double num = double.Parse(this.txtXYPrecision.Text);
                NewObjectClassHelper.m_pObjectClassHelper.XYResolution = num;
                NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain = this.chkUseDefault.Checked;
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    num = double.Parse(this.txtZPrecision.Text);
                    NewObjectClassHelper.m_pObjectClassHelper.ZResolution = num;
                }
                if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                {
                    num = double.Parse(this.txtMPrecision.Text);
                    NewObjectClassHelper.m_pObjectClassHelper.MResolution = num;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void chkUseDefault_CheckedChanged(object sender, EventArgs e)
        {
        }

 private void method_0()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
            {
                this.txtXYPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.XYTolerance.ToString();
                this.txtXYPrecision.Properties.ReadOnly = true;
                if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                {
                    this.txtMPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.MTolerance.ToString();
                    this.txtMPrecision.Properties.ReadOnly = true;
                }
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.txtZPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.ZTolerance.ToString();
                    this.txtZPrecision.Properties.ReadOnly = true;
                }
                this.chkUseDefault.Visible = false;
            }
            else
            {
                this.txtZPrecision.Text = "0.001";
                this.txtMPrecision.Text = "0.001";
                if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                {
                    this.txtXYPrecision.Text = "0.0001";
                    this.lblXYResolution.Text = "未知单位";
                    this.txtZPrecision.Text = "0.001";
                    this.txtMPrecision.Text = "0.001";
                    this.lblZResolution.Text = "未知单位";
                    this.lblMResolution.Text = "未知单位";
                }
                else if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IGeographicCoordinateSystem)
                {
                    this.txtXYPrecision.Text = "0.000000008982923";
                    this.lblXYResolution.Text = "度";
                    if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem == null)
                    {
                        this.lblZResolution.Text = "未知单位";
                    }
                    else
                    {
                        this.lblZResolution.Text = "米";
                    }
                    this.lblMResolution.Text = "未知单位";
                }
                else
                {
                    this.txtXYPrecision.Text = "0.001";
                    this.lblXYResolution.Text = "米";
                    if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem == null)
                    {
                        this.lblZResolution.Text = "未知单位";
                    }
                    else
                    {
                        this.lblZResolution.Text = "米";
                    }
                    this.lblMResolution.Text = "未知单位";
                }
            }
            if (!((NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset == null) || NewObjectClassHelper.m_pObjectClassHelper.IsEdit))
            {
                this.groupBoxXY.Visible = false;
                this.groupZ.Visible = false;
                this.gropuM.Location = this.groupBoxXY.Location;
                this.chkUseDefault.Location = new System.Drawing.Point(16, this.gropuM.Top + 74);
            }
            else
            {
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.groupZ.Visible = true;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.gropuM.Location = new System.Drawing.Point(16, 168);
                    }
                }
                else
                {
                    this.groupZ.Visible = false;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.gropuM.Location = new System.Drawing.Point(16, 92);
                    }
                }
                this.gropuM.Visible = NewObjectClassHelper.m_pObjectClassHelper.HasM;
                if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(16, this.gropuM.Top + 74);
                }
                else if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(16, this.groupZ.Top + 74);
                }
                else
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(16, this.groupBoxXY.Top + 74);
                }
            }
        }

        private void NewDatasetTolerancePage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void NewDatasetTolerancePage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.method_0();
            }
        }
    }
}

