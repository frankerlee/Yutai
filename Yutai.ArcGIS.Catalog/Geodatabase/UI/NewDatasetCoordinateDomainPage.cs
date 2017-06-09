using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class NewDatasetCoordinateDomainPage : UserControl
    {
        private bool bool_0 = false;
        private double double_0 = 2147483645.0;
        private GroupBox groupM;
        private GroupBox groupXY;
        private GroupBox groupZ;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblMResolution;
        private Label lblXYResolution;
        private Label lblZResolution;
        private TextEdit textBoxMaxX;
        private TextEdit textBoxMaxY;
        private TextEdit textBoxMinX;
        private TextEdit textBoxMinY;
        private TextEdit txtMMaxValue;
        private TextEdit txtMMinValue;
        private TextEdit txtMPrecision;
        private TextEdit txtXYPrecision;
        private TextEdit txtZMaxValue;
        private TextEdit txtZMinValue;
        private TextEdit txtZPrecision;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.groupXY = new GroupBox();
            this.lblXYResolution = new Label();
            this.textBoxMaxY = new TextEdit();
            this.textBoxMinY = new TextEdit();
            this.txtXYPrecision = new TextEdit();
            this.textBoxMaxX = new TextEdit();
            this.textBoxMinX = new TextEdit();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupZ = new GroupBox();
            this.lblZResolution = new Label();
            this.txtZPrecision = new TextEdit();
            this.txtZMaxValue = new TextEdit();
            this.txtZMinValue = new TextEdit();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.groupM = new GroupBox();
            this.lblMResolution = new Label();
            this.txtMPrecision = new TextEdit();
            this.txtMMaxValue = new TextEdit();
            this.txtMMinValue = new TextEdit();
            this.label9 = new Label();
            this.label10 = new Label();
            this.label11 = new Label();
            this.groupXY.SuspendLayout();
            this.textBoxMaxY.Properties.BeginInit();
            this.textBoxMinY.Properties.BeginInit();
            this.txtXYPrecision.Properties.BeginInit();
            this.textBoxMaxX.Properties.BeginInit();
            this.textBoxMinX.Properties.BeginInit();
            this.groupZ.SuspendLayout();
            this.txtZPrecision.Properties.BeginInit();
            this.txtZMaxValue.Properties.BeginInit();
            this.txtZMinValue.Properties.BeginInit();
            this.groupM.SuspendLayout();
            this.txtMPrecision.Properties.BeginInit();
            this.txtMMaxValue.Properties.BeginInit();
            this.txtMMinValue.Properties.BeginInit();
            base.SuspendLayout();
            this.groupXY.Controls.Add(this.lblXYResolution);
            this.groupXY.Controls.Add(this.textBoxMaxY);
            this.groupXY.Controls.Add(this.textBoxMinY);
            this.groupXY.Controls.Add(this.txtXYPrecision);
            this.groupXY.Controls.Add(this.textBoxMaxX);
            this.groupXY.Controls.Add(this.textBoxMinX);
            this.groupXY.Controls.Add(this.label5);
            this.groupXY.Controls.Add(this.label4);
            this.groupXY.Controls.Add(this.label3);
            this.groupXY.Controls.Add(this.label2);
            this.groupXY.Controls.Add(this.label1);
            this.groupXY.Location = new System.Drawing.Point(13, 9);
            this.groupXY.Name = "groupXY";
            this.groupXY.Size = new Size(390, 140);
            this.groupXY.TabIndex = 0;
            this.groupXY.TabStop = false;
            this.groupXY.Text = "XY";
            this.lblXYResolution.AutoSize = true;
            this.lblXYResolution.Location = new System.Drawing.Point(0xa4, 0x13);
            this.lblXYResolution.Name = "lblXYResolution";
            this.lblXYResolution.Size = new Size(0, 12);
            this.lblXYResolution.TabIndex = 0x27;
            this.textBoxMaxY.EditValue = "450359962737.0495";
            this.textBoxMaxY.Location = new System.Drawing.Point(0x85, 0x2c);
            this.textBoxMaxY.Name = "textBoxMaxY";
            this.textBoxMaxY.Size = new Size(0x83, 0x15);
            this.textBoxMaxY.TabIndex = 0x26;
            this.textBoxMaxY.Leave += new EventHandler(this.textBoxMaxY_Leave);
            this.textBoxMaxY.EditValueChanged += new EventHandler(this.textBoxMaxY_EditValueChanged);
            this.textBoxMinY.EditValue = "-450359962737.0495";
            this.textBoxMinY.Location = new System.Drawing.Point(0x85, 110);
            this.textBoxMinY.Name = "textBoxMinY";
            this.textBoxMinY.Size = new Size(0x83, 0x15);
            this.textBoxMinY.TabIndex = 0x25;
            this.textBoxMinY.EditValueChanged += new EventHandler(this.textBoxMinY_EditValueChanged);
            this.txtXYPrecision.EditValue = "0.0001";
            this.txtXYPrecision.Location = new System.Drawing.Point(0x3f, 14);
            this.txtXYPrecision.Name = "txtXYPrecision";
            this.txtXYPrecision.Size = new Size(0x5d, 0x15);
            this.txtXYPrecision.TabIndex = 0x24;
            this.txtXYPrecision.EditValueChanged += new EventHandler(this.txtXYPrecision_EditValueChanged);
            this.textBoxMaxX.EditValue = "450359962737.0495";
            this.textBoxMaxX.Location = new System.Drawing.Point(0xf9, 0x4b);
            this.textBoxMaxX.Name = "textBoxMaxX";
            this.textBoxMaxX.Size = new Size(0x83, 0x15);
            this.textBoxMaxX.TabIndex = 0x23;
            this.textBoxMaxX.Leave += new EventHandler(this.textBoxMaxX_Leave);
            this.textBoxMaxX.EditValueChanged += new EventHandler(this.textBoxMaxX_EditValueChanged);
            this.textBoxMinX.EditValue = "-450359962737.0495";
            this.textBoxMinX.Location = new System.Drawing.Point(0x3a, 0x47);
            this.textBoxMinX.Name = "textBoxMinX";
            this.textBoxMinX.Size = new Size(0x83, 0x15);
            this.textBoxMinX.TabIndex = 0x22;
            this.textBoxMinX.EditValueChanged += new EventHandler(this.textBoxMinX_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 0x13);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x21;
            this.label5.Text = "精度";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x4f, 0x31);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 0x20;
            this.label4.Text = "最大Y";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0xcd, 80);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 0x1f;
            this.label3.Text = "最大X";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x4f, 0x73);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "最小Y";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 0x4c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0x1d;
            this.label1.Text = "最小X";
            this.groupZ.Controls.Add(this.lblZResolution);
            this.groupZ.Controls.Add(this.txtZPrecision);
            this.groupZ.Controls.Add(this.txtZMaxValue);
            this.groupZ.Controls.Add(this.txtZMinValue);
            this.groupZ.Controls.Add(this.label6);
            this.groupZ.Controls.Add(this.label7);
            this.groupZ.Controls.Add(this.label8);
            this.groupZ.Location = new System.Drawing.Point(13, 0x9f);
            this.groupZ.Name = "groupZ";
            this.groupZ.Size = new Size(390, 0x52);
            this.groupZ.TabIndex = 1;
            this.groupZ.TabStop = false;
            this.groupZ.Text = "Z";
            this.lblZResolution.AutoSize = true;
            this.lblZResolution.Location = new System.Drawing.Point(0x9e, 0x1c);
            this.lblZResolution.Name = "lblZResolution";
            this.lblZResolution.Size = new Size(0, 12);
            this.lblZResolution.TabIndex = 40;
            this.txtZPrecision.EditValue = "0.0001";
            this.txtZPrecision.Location = new System.Drawing.Point(0x3e, 0x16);
            this.txtZPrecision.Name = "txtZPrecision";
            this.txtZPrecision.Size = new Size(0x58, 0x15);
            this.txtZPrecision.TabIndex = 0x20;
            this.txtZPrecision.EditValueChanged += new EventHandler(this.txtZPrecision_EditValueChanged);
            this.txtZMaxValue.EditValue = "900719825474.099";
            this.txtZMaxValue.Location = new System.Drawing.Point(0xf9, 0x36);
            this.txtZMaxValue.Name = "txtZMaxValue";
            this.txtZMaxValue.Properties.ReadOnly = true;
            this.txtZMaxValue.Size = new Size(0x83, 0x15);
            this.txtZMaxValue.TabIndex = 0x1f;
            this.txtZMinValue.EditValue = "-100000";
            this.txtZMinValue.Location = new System.Drawing.Point(0x3e, 0x36);
            this.txtZMinValue.Name = "txtZMinValue";
            this.txtZMinValue.Size = new Size(0x83, 0x15);
            this.txtZMinValue.TabIndex = 30;
            this.txtZMinValue.EditValueChanged += new EventHandler(this.txtZMinValue_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 0x1a);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 0x1d;
            this.label6.Text = "精度";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0xcd, 0x3a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 0x1c;
            this.label7.Text = "最大值";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 0x3a);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x29, 12);
            this.label8.TabIndex = 0x1b;
            this.label8.Text = "最小值";
            this.groupM.Controls.Add(this.lblMResolution);
            this.groupM.Controls.Add(this.txtMPrecision);
            this.groupM.Controls.Add(this.txtMMaxValue);
            this.groupM.Controls.Add(this.txtMMinValue);
            this.groupM.Controls.Add(this.label9);
            this.groupM.Controls.Add(this.label10);
            this.groupM.Controls.Add(this.label11);
            this.groupM.Location = new System.Drawing.Point(13, 260);
            this.groupM.Name = "groupM";
            this.groupM.Size = new Size(390, 0x56);
            this.groupM.TabIndex = 2;
            this.groupM.TabStop = false;
            this.groupM.Text = "M";
            this.lblMResolution.AutoSize = true;
            this.lblMResolution.Location = new System.Drawing.Point(0xa3, 0x11);
            this.lblMResolution.Name = "lblMResolution";
            this.lblMResolution.Size = new Size(0, 12);
            this.lblMResolution.TabIndex = 40;
            this.txtMPrecision.EditValue = "0.0001";
            this.txtMPrecision.Location = new System.Drawing.Point(0x44, 12);
            this.txtMPrecision.Name = "txtMPrecision";
            this.txtMPrecision.Size = new Size(0x58, 0x15);
            this.txtMPrecision.TabIndex = 0x1d;
            this.txtMPrecision.EditValueChanged += new EventHandler(this.txtMPrecision_EditValueChanged);
            this.txtMMaxValue.EditValue = "900719825474.099";
            this.txtMMaxValue.Location = new System.Drawing.Point(0xf9, 0x2e);
            this.txtMMaxValue.Name = "txtMMaxValue";
            this.txtMMaxValue.Size = new Size(0x83, 0x15);
            this.txtMMaxValue.TabIndex = 0x1c;
            this.txtMMinValue.EditValue = "-100000";
            this.txtMMinValue.Location = new System.Drawing.Point(0x44, 0x2e);
            this.txtMMinValue.Name = "txtMMinValue";
            this.txtMMinValue.Size = new Size(0x83, 0x15);
            this.txtMMinValue.TabIndex = 0x1b;
            this.txtMMinValue.EditValueChanged += new EventHandler(this.txtMMinValue_EditValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 0x11);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 0x1a;
            this.label9.Text = "精度";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0xcd, 0x2e);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x29, 12);
            this.label10.TabIndex = 0x19;
            this.label10.Text = "最大值";
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 0x2e);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x29, 12);
            this.label11.TabIndex = 0x18;
            this.label11.Text = "最小值";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.groupM);
            base.Controls.Add(this.groupZ);
            base.Controls.Add(this.groupXY);
            base.Name = "NewDatasetCoordinateDomainPage";
            base.Size = new Size(0x1a1, 0x17f);
            base.VisibleChanged += new EventHandler(this.NewDatasetCoordinateDomainPage_VisibleChanged);
            base.Load += new EventHandler(this.NewDatasetCoordinateDomainPage_Load);
            this.groupXY.ResumeLayout(false);
            this.groupXY.PerformLayout();
            this.textBoxMaxY.Properties.EndInit();
            this.textBoxMinY.Properties.EndInit();
            this.txtXYPrecision.Properties.EndInit();
            this.textBoxMaxX.Properties.EndInit();
            this.textBoxMinX.Properties.EndInit();
            this.groupZ.ResumeLayout(false);
            this.groupZ.PerformLayout();
            this.txtZPrecision.Properties.EndInit();
            this.txtZMaxValue.Properties.EndInit();
            this.txtZMinValue.Properties.EndInit();
            this.groupM.ResumeLayout(false);
            this.groupM.PerformLayout();
            this.txtMPrecision.Properties.EndInit();
            this.txtMMaxValue.Properties.EndInit();
            this.txtMMinValue.Properties.EndInit();
            base.ResumeLayout(false);
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
                this.groupXY.Size = new Size(390, 0x9a);
                if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                {
                    this.groupZ.Visible = true;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.groupM.Location = new System.Drawing.Point(13, 0x112);
                    }
                    this.groupZ.Location = new System.Drawing.Point(13, 0xad);
                }
                else
                {
                    this.groupZ.Visible = false;
                    if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                    {
                        this.groupM.Location = new System.Drawing.Point(13, 0xad);
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
                if ((NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset != null) && !NewObjectClassHelper.m_pObjectClassHelper.IsEdit)
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
                    num5 = (num8 * this.double_0) - 100000.0;
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
                    num5 = (num8 * this.double_0) - 100000.0;
                    this.txtZMaxValue.Text = num5.ToString();
                    num8 = double.Parse(this.txtMPrecision.Text);
                    this.txtMMinValue.Text = "-100000";
                    num5 = (num8 * this.double_0) - 100000.0;
                    this.txtMMaxValue.Text = num5.ToString();
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        this.txtXYPrecision.Text = "0.0001";
                        num5 = (this.double_0 / 2.0) * 0.0001;
                        this.textBoxMinX.Text = "-" + num5.ToString();
                        num5 = (this.double_0 / 2.0) * 0.0001;
                        this.textBoxMaxX.Text = num5.ToString();
                        num5 = (this.double_0 / 2.0) * 0.0001;
                        this.textBoxMinY.Text = "-" + num5.ToString();
                        this.textBoxMaxY.Text = ((this.double_0 / 2.0) * 0.0001).ToString();
                        this.lblXYResolution.Text = "未知单位";
                        this.lblZResolution.Text = "未知单位";
                        this.groupXY.Size = new Size(390, 0x9a);
                        if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                        {
                            this.groupZ.Visible = true;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 0x112);
                            }
                            this.groupZ.Location = new System.Drawing.Point(13, 0xad);
                        }
                        else
                        {
                            this.groupZ.Visible = false;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 0xad);
                            }
                        }
                    }
                    else
                    {
                        if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IGeographicCoordinateSystem)
                        {
                            this.txtXYPrecision.Text = "0.000000001";
                            this.lblXYResolution.Text = "度";
                            if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem == null)
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
                            if ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem == null)
                            {
                                this.lblZResolution.Text = "未知单位";
                            }
                            else
                            {
                                this.lblZResolution.Text = "米";
                            }
                        }
                        this.groupXY.Size = new Size(390, 0x2a);
                        if (NewObjectClassHelper.m_pObjectClassHelper.HasZ)
                        {
                            this.groupZ.Visible = true;
                            this.groupZ.Location = new System.Drawing.Point(13, 0x3a);
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 0x9f);
                            }
                        }
                        else
                        {
                            this.groupZ.Visible = false;
                            if (NewObjectClassHelper.m_pObjectClassHelper.HasM)
                            {
                                this.groupM.Location = new System.Drawing.Point(13, 0x3a);
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
                this.textBoxMaxX.Text = ((num * this.double_0) + num2).ToString();
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
                        this.textBoxMinX.Text = (num - (num3 * this.double_0)).ToString();
                        this.bool_0 = true;
                    }
                    else
                    {
                        num3 = (num - num2) / this.double_0;
                        double num5 = double.Parse(this.txtXYPrecision.Text);
                        try
                        {
                            double num6 = double.Parse(this.textBoxMaxX.Text);
                            double num7 = double.Parse(this.textBoxMinX.Text);
                            num5 = (num6 - num7) / this.double_0;
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
                        this.textBoxMinY.Text = (num - (num3 * this.double_0)).ToString();
                        this.bool_0 = true;
                    }
                    else
                    {
                        num3 = (num - num2) / this.double_0;
                        double num5 = double.Parse(this.txtXYPrecision.Text);
                        try
                        {
                            double num6 = double.Parse(this.textBoxMaxX.Text);
                            double num7 = double.Parse(this.textBoxMinX.Text);
                            num5 = (num6 - num7) / this.double_0;
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
                    this.textBoxMaxX.Text = ((num * this.double_0) + num2).ToString();
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
                    this.textBoxMaxY.Text = ((num * this.double_0) + num2).ToString();
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
                    this.txtMMaxValue.Text = ((num * this.double_0) + num2).ToString();
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
                    this.txtMMaxValue.Text = ((num * this.double_0) + num2).ToString();
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
                    double num4 = (num * this.double_0) + num2;
                    this.textBoxMaxX.Text = num4.ToString();
                    this.textBoxMaxY.Text = ((num * this.double_0) + num3).ToString();
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
                    this.txtZMaxValue.Text = ((num * this.double_0) + num2).ToString();
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
                    this.txtZMaxValue.Text = ((num * this.double_0) + num2).ToString();
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

