using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class NewDatasetCoordinateDomainPage : UserControl
    {
        private bool bool_0 = false;
        private double double_0 = 2147483645.0;
        private IContainer icontainer_0 = null;

        public NewDatasetCoordinateDomainPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                double num = double.Parse(this.txtXYPrecision.Text);
                NewObjectClassHelper.m_pObjectClassHelper.XYResolution = num;
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
                return true;
            }
            catch
            {
            }
            return true;
        }

        private void method_0()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
            {
                double num;
                double num2;
                double num3;
                double num4;
                double num6;
                double num7;
                this.groupXY.Size = new Size(390, 154);
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.groupZ.Visible = true;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.groupM.Location = new System.Drawing.Point(13, 274);
                    }
                    this.groupZ.Location = new System.Drawing.Point(13, 173);
                }
                else
                {
                    this.groupZ.Visible = false;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.groupM.Location = new System.Drawing.Point(13, 173);
                    }
                }
                NewObjectClassHelper.m_pObjectClassHelper.GetDomain(out num, out num2, out num3, out num4);
                this.textBoxMinX.Text = num.ToString();
                this.textBoxMaxX.Text = num2.ToString();
                this.textBoxMinY.Text = num3.ToString();
                this.textBoxMaxY.Text = num4.ToString();
                this.txtXYPrecision.Properties.ReadOnly = true;
                this.txtXYPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.XYResolution.ToString();
                this.textBoxMinX.Properties.ReadOnly = true;
                this.textBoxMaxX.Properties.ReadOnly = true;
                this.textBoxMinY.Properties.ReadOnly = true;
                this.textBoxMaxY.Properties.ReadOnly = true;
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    NewObjectClassHelper.m_pObjectClassHelper.GetZDomain(out num6, out num7);
                    this.txtZMinValue.Text = num6.ToString();
                    this.txtZMaxValue.Text = num7.ToString();
                    this.txtZMinValue.Properties.ReadOnly = true;
                    this.txtZMaxValue.Properties.ReadOnly = true;
                    this.txtZPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.ZResolution.ToString();
                    this.txtZPrecision.Properties.ReadOnly = true;
                }
                if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                {
                    NewObjectClassHelper.m_pObjectClassHelper.GetZDomain(out num6, out num7);
                    this.txtMMinValue.Text = num6.ToString();
                    this.txtMMaxValue.Text = num7.ToString();
                    this.txtMPrecision.Properties.ReadOnly = true;
                    this.txtMPrecision.Text = NewObjectClassHelper.m_pObjectClassHelper.MResolution.ToString();
                    this.txtMMinValue.Properties.ReadOnly = true;
                    this.txtMMinValue.Properties.ReadOnly = true;
                }
                this.groupM.Visible = NewObjectClassHelper.m_pObjectClassHelper.HasM;
            }
            else
            {
                double num5;
                double num8;
                if ((NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset != null) &&
                    !NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
                {
                    this.groupXY.Visible = false;
                    this.groupZ.Visible = false;
                    this.groupM.Location = this.groupXY.Location;
                    this.txtZMinValue.Text = "-100000";
                    this.lblMResolution.Text = "未知单位";
                    if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        this.double_0 = 9.00719925474099E+15;
                    }
                    else
                    {
                        this.double_0 = 2147483645.0;
                    }
                    num8 = double.Parse(this.txtMPrecision.Text);
                    num5 = (num8*this.double_0) - 100000.0;
                    this.txtMMaxValue.Text = num5.ToString();
                }
                else
                {
                    if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        this.double_0 = 9.00719925474099E+15;
                    }
                    else
                    {
                        this.double_0 = 2147483645.0;
                    }
                    num8 = double.Parse(this.txtZPrecision.Text);
                    this.txtZMinValue.Text = "-100000";
                    this.lblMResolution.Text = "未知单位";
                    num5 = (num8*this.double_0) - 100000.0;
                    this.txtZMaxValue.Text = num5.ToString();
                    num8 = double.Parse(this.txtMPrecision.Text);
                    this.txtMMinValue.Text = "-100000";
                    num5 = (num8*this.double_0) - 100000.0;
                    this.txtMMaxValue.Text = num5.ToString();
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        this.txtXYPrecision.Text = "0.0001";
                        num5 = (this.double_0/2.0)*0.0001;
                        this.textBoxMinX.Text = "-" + num5.ToString();
                        num5 = (this.double_0/2.0)*0.0001;
                        this.textBoxMaxX.Text = num5.ToString();
                        num5 = (this.double_0/2.0)*0.0001;
                        this.textBoxMinY.Text = "-" + num5.ToString();
                        this.textBoxMaxY.Text = ((this.double_0/2.0)*0.0001).ToString();
                        this.lblXYResolution.Text = "未知单位";
                        this.lblZResolution.Text = "未知单位";
                        this.groupXY.Size = new Size(390, 154);
                        if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                        {
                            this.groupZ.Visible = true;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 274);
                            }
                            this.groupZ.Location = new System.Drawing.Point(13, 173);
                        }
                        else
                        {
                            this.groupZ.Visible = false;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 173);
                            }
                        }
                    }
                    else
                    {
                        if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IGeographicCoordinateSystem)
                        {
                            this.txtXYPrecision.Text = "0.000000001";
                            this.lblXYResolution.Text = "度";
                            if (
                                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3)
                                    .VerticalCoordinateSystem == null)
                            {
                                this.lblZResolution.Text = "未知单位";
                            }
                            else
                            {
                                this.lblZResolution.Text = "米";
                            }
                        }
                        else
                        {
                            this.txtXYPrecision.Text = "0.0001";
                            this.lblXYResolution.Text = "米";
                            if (
                                (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3)
                                    .VerticalCoordinateSystem == null)
                            {
                                this.lblZResolution.Text = "未知单位";
                            }
                            else
                            {
                                this.lblZResolution.Text = "米";
                            }
                        }
                        this.groupXY.Size = new Size(390, 42);
                        if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                        {
                            this.groupZ.Visible = true;
                            this.groupZ.Location = new System.Drawing.Point(13, 58);
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 159);
                            }
                        }
                        else
                        {
                            this.groupZ.Visible = false;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 58);
                            }
                        }
                    }
                    this.groupM.Visible = NewObjectClassHelper.m_pObjectClassHelper.HasM;
                }
            }
        }

        private void method_1()
        {
            try
            {
                double num = double.Parse(this.txtXYPrecision.Text);
                double num2 = double.Parse(this.textBoxMinX.Text);
                this.bool_0 = false;
                this.textBoxMaxX.Text = ((num*this.double_0) + num2).ToString();
                this.bool_0 = true;
            }
            catch
            {
            }
        }

        private void NewDatasetCoordinateDomainPage_Load(object sender, EventArgs e)
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        private void NewDatasetCoordinateDomainPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
            }
        }

        private void textBoxMaxX_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void textBoxMaxX_Leave(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                double num;
                try
                {
                    num = double.Parse(this.textBoxMaxX.Text);
                    this.textBoxMaxX.ForeColor = Color.Black;
                }
                catch
                {
                    this.textBoxMaxX.ForeColor = Color.Red;
                    return;
                }
                try
                {
                    double num3;
                    double num2 = double.Parse(this.textBoxMinX.Text);
                    if (num <= num2)
                    {
                        MessageBox.Show("最大的X值应比最小X值大!");
                        num3 = double.Parse(this.txtXYPrecision.Text);
                        this.bool_0 = false;
                        this.textBoxMinX.Text = (num - (num3*this.double_0)).ToString();
                        this.bool_0 = true;
                    }
                    else
                    {
                        num3 = (num - num2)/this.double_0;
                        double num5 = double.Parse(this.txtXYPrecision.Text);
                        try
                        {
                            double num6 = double.Parse(this.textBoxMaxX.Text);
                            double num7 = double.Parse(this.textBoxMinX.Text);
                            num5 = (num6 - num7)/this.double_0;
                        }
                        catch
                        {
                        }
                        if (num3 > num5)
                        {
                            this.bool_0 = false;
                            this.txtXYPrecision.Text = num3.ToString();
                            this.bool_0 = true;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void textBoxMaxY_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void textBoxMaxY_Leave(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                double num;
                try
                {
                    num = double.Parse(this.textBoxMaxY.Text);
                    this.textBoxMaxY.ForeColor = Color.Black;
                }
                catch
                {
                    this.textBoxMaxY.ForeColor = Color.Red;
                    return;
                }
                try
                {
                    double num3;
                    double num2 = double.Parse(this.textBoxMinY.Text);
                    if (num <= num2)
                    {
                        MessageBox.Show("最大的Y值应比最小Y值大!");
                        num3 = double.Parse(this.txtXYPrecision.Text);
                        this.bool_0 = false;
                        this.textBoxMinY.Text = (num - (num3*this.double_0)).ToString();
                        this.bool_0 = true;
                    }
                    else
                    {
                        num3 = (num - num2)/this.double_0;
                        double num5 = double.Parse(this.txtXYPrecision.Text);
                        try
                        {
                            double num6 = double.Parse(this.textBoxMaxX.Text);
                            double num7 = double.Parse(this.textBoxMinX.Text);
                            num5 = (num6 - num7)/this.double_0;
                        }
                        catch
                        {
                        }
                        if (num3 > num5)
                        {
                            this.bool_0 = false;
                            this.txtXYPrecision.Text = num3.ToString();
                            this.bool_0 = true;
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void textBoxMinX_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtXYPrecision.Text);
                    double num2 = double.Parse(this.textBoxMinX.Text);
                    this.bool_0 = false;
                    this.textBoxMaxX.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.textBoxMinX.ForeColor = Color.Black;
                }
                catch
                {
                    this.textBoxMinX.ForeColor = Color.Red;
                }
            }
        }

        private void textBoxMinY_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtXYPrecision.Text);
                    double num2 = double.Parse(this.textBoxMinY.Text);
                    this.bool_0 = false;
                    this.textBoxMaxY.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.textBoxMinY.ForeColor = Color.Black;
                }
                catch
                {
                    this.textBoxMinY.ForeColor = Color.Red;
                }
            }
        }

        private void txtMMinValue_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtMPrecision.Text);
                    double num2 = double.Parse(this.txtMMinValue.Text);
                    this.bool_0 = false;
                    this.txtMMaxValue.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.txtMMinValue.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtMMinValue.ForeColor = Color.Red;
                }
            }
        }

        private void txtMPrecision_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtMPrecision.Text);
                    double num2 = double.Parse(this.txtMMinValue.Text);
                    this.bool_0 = false;
                    this.txtMMaxValue.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.txtMPrecision.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtMPrecision.ForeColor = Color.Red;
                }
            }
        }

        private void txtXYPrecision_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtXYPrecision.Text);
                    double num2 = double.Parse(this.textBoxMinX.Text);
                    double num3 = double.Parse(this.textBoxMinY.Text);
                    this.bool_0 = false;
                    double num4 = (num*this.double_0) + num2;
                    this.textBoxMaxX.Text = num4.ToString();
                    this.textBoxMaxY.Text = ((num*this.double_0) + num3).ToString();
                    this.bool_0 = true;
                    this.txtXYPrecision.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtXYPrecision.ForeColor = Color.Red;
                }
            }
        }

        private void txtZMinValue_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtZPrecision.Text);
                    double num2 = double.Parse(this.txtZMinValue.Text);
                    this.bool_0 = false;
                    this.txtZMaxValue.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.txtZMinValue.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtZMinValue.ForeColor = Color.Red;
                }
            }
        }

        private void txtZPrecision_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    double num = double.Parse(this.txtZPrecision.Text);
                    double num2 = double.Parse(this.txtZMinValue.Text);
                    this.bool_0 = false;
                    this.txtZMaxValue.Text = ((num*this.double_0) + num2).ToString();
                    this.bool_0 = true;
                    this.txtZPrecision.ForeColor = Color.Black;
                }
                catch
                {
                    this.txtZPrecision.ForeColor = Color.Red;
                }
            }
        }
    }
}