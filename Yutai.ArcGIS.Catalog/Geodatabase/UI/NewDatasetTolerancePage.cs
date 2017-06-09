using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class NewDatasetTolerancePage : UserControl
    {
        public CheckEdit chkUseDefault;
        private GroupBox gropuM;
        private GroupBox groupBoxXY;
        private GroupBox groupZ;
        private IContainer icontainer_0 = null;
        private Label lblMResolution;
        private Label lblXYResolution;
        private Label lblZResolution;
        private TextEdit txtMPrecision;
        private TextEdit txtXYPrecision;
        private TextEdit txtZPrecision;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.gropuM = new GroupBox();
            this.lblMResolution = new Label();
            this.txtMPrecision = new TextEdit();
            this.groupZ = new GroupBox();
            this.lblZResolution = new Label();
            this.txtZPrecision = new TextEdit();
            this.groupBoxXY = new GroupBox();
            this.lblXYResolution = new Label();
            this.txtXYPrecision = new TextEdit();
            this.chkUseDefault = new CheckEdit();
            this.gropuM.SuspendLayout();
            this.txtMPrecision.Properties.BeginInit();
            this.groupZ.SuspendLayout();
            this.txtZPrecision.Properties.BeginInit();
            this.groupBoxXY.SuspendLayout();
            this.txtXYPrecision.Properties.BeginInit();
            this.chkUseDefault.Properties.BeginInit();
            base.SuspendLayout();
            this.gropuM.Controls.Add(this.lblMResolution);
            this.gropuM.Controls.Add(this.txtMPrecision);
            this.gropuM.Location = new System.Drawing.Point(0x10, 0xa8);
            this.gropuM.Name = "gropuM";
            this.gropuM.Size = new Size(0x13c, 0x3a);
            this.gropuM.TabIndex = 5;
            this.gropuM.TabStop = false;
            this.gropuM.Text = "M容差";
            this.lblMResolution.AutoSize = true;
            this.lblMResolution.Location = new System.Drawing.Point(0xa3, 0x11);
            this.lblMResolution.Name = "lblMResolution";
            this.lblMResolution.Size = new Size(0, 12);
            this.lblMResolution.TabIndex = 40;
            this.txtMPrecision.EditValue = "";
            this.txtMPrecision.Location = new System.Drawing.Point(0x10, 0x12);
            this.txtMPrecision.Name = "txtMPrecision";
            this.txtMPrecision.Size = new Size(140, 0x15);
            this.txtMPrecision.TabIndex = 0x1d;
            this.groupZ.Controls.Add(this.lblZResolution);
            this.groupZ.Controls.Add(this.txtZPrecision);
            this.groupZ.Location = new System.Drawing.Point(0x10, 0x5c);
            this.groupZ.Name = "groupZ";
            this.groupZ.Size = new Size(0x13c, 0x3d);
            this.groupZ.TabIndex = 4;
            this.groupZ.TabStop = false;
            this.groupZ.Text = "Z容差";
            this.lblZResolution.AutoSize = true;
            this.lblZResolution.Location = new System.Drawing.Point(0x9e, 0x1c);
            this.lblZResolution.Name = "lblZResolution";
            this.lblZResolution.Size = new Size(0, 12);
            this.lblZResolution.TabIndex = 40;
            this.txtZPrecision.EditValue = "";
            this.txtZPrecision.Location = new System.Drawing.Point(0x10, 0x16);
            this.txtZPrecision.Name = "txtZPrecision";
            this.txtZPrecision.Size = new Size(0x86, 0x15);
            this.txtZPrecision.TabIndex = 0x20;
            this.groupBoxXY.Controls.Add(this.lblXYResolution);
            this.groupBoxXY.Controls.Add(this.txtXYPrecision);
            this.groupBoxXY.Location = new System.Drawing.Point(0x10, 13);
            this.groupBoxXY.Name = "groupBoxXY";
            this.groupBoxXY.Size = new Size(0x13c, 0x3d);
            this.groupBoxXY.TabIndex = 3;
            this.groupBoxXY.TabStop = false;
            this.groupBoxXY.Text = "XY容差";
            this.lblXYResolution.AutoSize = true;
            this.lblXYResolution.Location = new System.Drawing.Point(0xa4, 0x1a);
            this.lblXYResolution.Name = "lblXYResolution";
            this.lblXYResolution.Size = new Size(0, 12);
            this.lblXYResolution.TabIndex = 0x27;
            this.txtXYPrecision.EditValue = "";
            this.txtXYPrecision.Location = new System.Drawing.Point(0x10, 0x15);
            this.txtXYPrecision.Name = "txtXYPrecision";
            this.txtXYPrecision.Size = new Size(0x8e, 0x15);
            this.txtXYPrecision.TabIndex = 0x24;
            this.chkUseDefault.EditValue = true;
            this.chkUseDefault.Location = new System.Drawing.Point(0x10, 0xf2);
            this.chkUseDefault.Name = "chkUseDefault";
            this.chkUseDefault.Properties.Caption = "使用默认精度和坐标范围(推荐)";
            this.chkUseDefault.Size = new Size(210, 0x13);
            this.chkUseDefault.TabIndex = 6;
            this.chkUseDefault.CheckedChanged += new EventHandler(this.chkUseDefault_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkUseDefault);
            base.Controls.Add(this.gropuM);
            base.Controls.Add(this.groupZ);
            base.Controls.Add(this.groupBoxXY);
            base.Name = "NewDatasetTolerancePage";
            base.Size = new Size(0x163, 0x145);
            base.VisibleChanged += new EventHandler(this.NewDatasetTolerancePage_VisibleChanged);
            base.Load += new EventHandler(this.NewDatasetTolerancePage_Load);
            this.gropuM.ResumeLayout(false);
            this.gropuM.PerformLayout();
            this.txtMPrecision.Properties.EndInit();
            this.groupZ.ResumeLayout(false);
            this.groupZ.PerformLayout();
            this.txtZPrecision.Properties.EndInit();
            this.groupBoxXY.ResumeLayout(false);
            this.groupBoxXY.PerformLayout();
            this.txtXYPrecision.Properties.EndInit();
            this.chkUseDefault.Properties.EndInit();
            base.ResumeLayout(false);
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
                this.chkUseDefault.Location = new System.Drawing.Point(0x10, this.gropuM.Top + 0x4a);
            }
            else
            {
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.groupZ.Visible = true;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.gropuM.Location = new System.Drawing.Point(0x10, 0xa8);
                    }
                }
                else
                {
                    this.groupZ.Visible = false;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.gropuM.Location = new System.Drawing.Point(0x10, 0x5c);
                    }
                }
                this.gropuM.Visible = NewObjectClassHelper.m_pObjectClassHelper.HasM;
                if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(0x10, this.gropuM.Top + 0x4a);
                }
                else if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(0x10, this.groupZ.Top + 0x4a);
                }
                else
                {
                    this.chkUseDefault.Location = new System.Drawing.Point(0x10, this.groupBoxXY.Top + 0x4a);
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

