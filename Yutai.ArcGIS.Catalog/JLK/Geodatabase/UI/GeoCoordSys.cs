namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility.BaseClass;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GeoCoordSys : UserControl
    {
        private bool bool_0 = false;
        private ComboBoxEdit cboAngleName;
        private ComboBoxEdit cboDatumName;
        private ComboBoxEdit cboPrimeMeridians;
        private ComboBoxEdit cboSpheres;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IDatum idatum_0;
        private IGeographicCoordinateSystem igeographicCoordinateSystem_0;
        private int int_0 = 0;
        private int[] int_1 = new int[] { 0x1846, 0x1788, 0x18b6, 0x19d2 };
        private int[] int_2 = new int[] { 0x1b70, 0x1b76, 0x1b89 };
        private int[] int_3 = new int[] { 0x238e, 0x2392, 0x238f, 0x2390, 0x238d };
        private int[] int_4 = new int[] { 0x22c5, 0x22d0, 0x22cb };
        private IPrimeMeridian iprimeMeridian_0;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private ISpheroid ispheroid_0;
        private IUnit iunit_0;
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
        private RadioButton rdoFlattening;
        private RadioButton rdoMiniorAxis;
        private TextEdit textEditName;
        private TextEdit txtDegree;
        private TextEdit txtFlattening;
        private TextEdit txtMajorAxis;
        private TextEdit txtMiniorAxis;
        private TextEdit txtMinute;
        private TextEdit txtSecond;
        private TextEdit txtValue;

        public GeoCoordSys()
        {
            this.InitializeComponent();
        }

        private void cboAngleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                this.method_5(this.txtValue, false);
            }
            else
            {
                this.method_5(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 = this.ispatialReferenceFactory_0.CreateUnit(this.int_3[this.cboAngleName.SelectedIndex - 1]);
                }
                if (this.iunit_0 != null)
                {
                    this.txtValue.Text = ((IAngularUnit) this.iunit_0).RadiansPerUnit.ToString();
                }
            }
        }

        private void cboDatumName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDatumName.SelectedIndex < 0)
            {
                this.cboSpheres.Enabled = true;
            }
            else
            {
                int index;
                if (this.cboDatumName.SelectedIndex == 0)
                {
                    this.cboSpheres.Enabled = true;
                    index = -1;
                    if ((this.idatum_0 != null) && (this.idatum_0.Spheroid != null))
                    {
                        index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    }
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                }
                else
                {
                    this.cboSpheres.Enabled = false;
                    if (this.bool_0)
                    {
                        this.idatum_0 = this.ispatialReferenceFactory_0.CreateDatum(this.int_1[this.cboDatumName.SelectedIndex - 1]);
                        this.ispheroid_0 = this.idatum_0.Spheroid;
                        this.bool_0 = false;
                    }
                    index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void cboPrimeMeridians_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboPrimeMeridians.SelectedIndex <= 0)
            {
                this.method_5(this.txtDegree, false);
                this.method_5(this.txtMinute, false);
                this.method_5(this.txtSecond, false);
            }
            else
            {
                this.method_5(this.txtDegree, true);
                this.method_5(this.txtMinute, true);
                this.method_5(this.txtSecond, true);
                if (this.bool_0)
                {
                    this.iprimeMeridian_0 = this.ispatialReferenceFactory_0.CreatePrimeMeridian(this.int_4[this.cboPrimeMeridians.SelectedIndex - 1]);
                }
                if (this.iprimeMeridian_0 != null)
                {
                    int longitude = (int) this.iprimeMeridian_0.Longitude;
                    double num2 = (this.iprimeMeridian_0.Longitude - longitude) * 60.0;
                    int num3 = (int) num2;
                    num2 = (num2 - num3) * 60.0;
                    this.txtDegree.Text = longitude.ToString();
                    this.txtMinute.Text = num3.ToString();
                    this.txtSecond.Text = num2.ToString();
                }
            }
        }

        private void cboSpheres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                this.method_5(this.txtMajorAxis, false);
                this.method_5(this.txtMiniorAxis, false);
                this.method_5(this.txtFlattening, false);
            }
            else
            {
                this.method_5(this.txtMajorAxis, true);
                this.method_5(this.txtMiniorAxis, true);
                this.method_5(this.txtFlattening, true);
                if (this.bool_0)
                {
                    this.ispheroid_0 = this.ispatialReferenceFactory_0.CreateSpheroid(this.int_2[this.cboSpheres.SelectedIndex - 1]);
                }
                if (this.ispheroid_0 != null)
                {
                    this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                    this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                    this.txtFlattening.Text = (1.0 / this.ispheroid_0.Flattening).ToString();
                }
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void GeoCoordSys_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.method_2();
            this.method_3();
            this.rdoMiniorAxis.Checked = true;
            this.txtFlattening.Enabled = false;
            this.method_4();
            this.bool_0 = true;
        }

        public IGeographicCoordinateSystem GetSpatialRefrence()
        {
            double num;
            if (this.int_0 > 0)
            {
                MessageBox.Show("请检查输入数据是否正确！");
                return null;
            }
            if (this.textEditName.Text.Length == 0)
            {
                MessageBox.Show("未指定名字，必须为坐标系统指定一个名字！");
                return null;
            }
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                double num2;
                num = Convert.ToDouble(this.txtMajorAxis.Text);
                if (this.rdoFlattening.Checked)
                {
                    num2 = 1.0 / Convert.ToDouble(this.txtFlattening.Text);
                }
                else
                {
                    double num3 = Convert.ToDouble(this.txtMiniorAxis.Text);
                    num2 = (num - num3) / num;
                }
                ((ISpheroidEdit) this.ispheroid_0).DefineEx(this.cboSpheres.Text, null, null, null, ref num, ref num2);
            }
            if (this.cboDatumName.SelectedIndex <= 0)
            {
                ((IDatumEdit) this.idatum_0).DefineEx(this.cboDatumName.Text, null, null, null, this.ispheroid_0);
            }
            if (this.cboAngleName.SelectedIndex <= 0)
            {
                num = Convert.ToDouble(this.txtValue.Text);
                ((IAngularUnitEdit) this.iunit_0).DefineEx(this.cboAngleName.Text, null, null, null, ref num);
            }
            if (this.cboPrimeMeridians.SelectedIndex <= 0)
            {
                double num4 = Convert.ToDouble(this.txtDegree.Text);
                if (num4 < 0.0)
                {
                    num = (num4 - (Convert.ToDouble(this.txtMinute.Text) / 60.0)) - (Convert.ToDouble(this.txtSecond.Text) / 3600.0);
                }
                else
                {
                    num = (num4 + (Convert.ToDouble(this.txtMinute.Text) / 60.0)) + (Convert.ToDouble(this.txtSecond.Text) / 3600.0);
                }
                ((IPrimeMeridianEdit) this.iprimeMeridian_0).DefineEx(this.cboPrimeMeridians.Text, null, null, null, ref num);
            }
            this.igeographicCoordinateSystem_0 = new GeographicCoordinateSystemClass();
            IGeographicCoordinateSystemEdit edit = this.igeographicCoordinateSystem_0 as IGeographicCoordinateSystemEdit;
            IAngularUnit geographicUnit = this.iunit_0 as IAngularUnit;
            string alias = "";
            try
            {
                edit.DefineEx(this.textEditName.Text, alias, alias, alias, alias, this.idatum_0, this.iprimeMeridian_0, geographicUnit);
            }
            catch
            {
                MessageBox.Show("无法定义地理坐标!");
                return null;
            }
            return this.igeographicCoordinateSystem_0;
        }

        private void InitializeComponent()
        {
            this.textEditName = new TextEdit();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboDatumName = new ComboBoxEdit();
            this.groupBox2 = new GroupBox();
            this.cboSpheres = new ComboBoxEdit();
            this.txtFlattening = new TextEdit();
            this.txtMiniorAxis = new TextEdit();
            this.txtMajorAxis = new TextEdit();
            this.rdoFlattening = new RadioButton();
            this.rdoMiniorAxis = new RadioButton();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.groupBox4 = new GroupBox();
            this.cboAngleName = new ComboBoxEdit();
            this.txtValue = new TextEdit();
            this.label6 = new Label();
            this.label5 = new Label();
            this.groupBox3 = new GroupBox();
            this.cboPrimeMeridians = new ComboBoxEdit();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.txtSecond = new TextEdit();
            this.txtMinute = new TextEdit();
            this.txtDegree = new TextEdit();
            this.label7 = new Label();
            this.label8 = new Label();
            this.textEditName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboDatumName.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboSpheres.Properties.BeginInit();
            this.txtFlattening.Properties.BeginInit();
            this.txtMiniorAxis.Properties.BeginInit();
            this.txtMajorAxis.Properties.BeginInit();
            this.groupBox4.SuspendLayout();
            this.cboAngleName.Properties.BeginInit();
            this.txtValue.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.cboPrimeMeridians.Properties.BeginInit();
            this.txtSecond.Properties.BeginInit();
            this.txtMinute.Properties.BeginInit();
            this.txtDegree.Properties.BeginInit();
            base.SuspendLayout();
            this.textEditName.EditValue = "";
            this.textEditName.Location = new System.Drawing.Point(0x30, 8);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new Size(0xf8, 0x15);
            this.textEditName.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称";
            this.groupBox1.Controls.Add(this.cboDatumName);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 0x20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 0xd8);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据";
            this.cboDatumName.EditValue = "";
            this.cboDatumName.Location = new System.Drawing.Point(0x40, 0x18);
            this.cboDatumName.Name = "cboDatumName";
            this.cboDatumName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDatumName.Size = new Size(0xd8, 0x15);
            this.cboDatumName.TabIndex = 6;
            this.cboDatumName.SelectedIndexChanged += new EventHandler(this.cboDatumName_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.cboSpheres);
            this.groupBox2.Controls.Add(this.txtFlattening);
            this.groupBox2.Controls.Add(this.txtMiniorAxis);
            this.groupBox2.Controls.Add(this.txtMajorAxis);
            this.groupBox2.Controls.Add(this.rdoFlattening);
            this.groupBox2.Controls.Add(this.rdoMiniorAxis);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(0x10, 0x38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 0x98);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "椭球参数";
            this.cboSpheres.EditValue = "";
            this.cboSpheres.Location = new System.Drawing.Point(80, 0x18);
            this.cboSpheres.Name = "cboSpheres";
            this.cboSpheres.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSpheres.Size = new Size(160, 0x15);
            this.cboSpheres.TabIndex = 9;
            this.cboSpheres.SelectedIndexChanged += new EventHandler(this.cboSpheres_SelectedIndexChanged);
            this.txtFlattening.EditValue = "1";
            this.txtFlattening.Location = new System.Drawing.Point(80, 120);
            this.txtFlattening.Name = "txtFlattening";
            this.txtFlattening.Size = new Size(160, 0x15);
            this.txtFlattening.TabIndex = 8;
            this.txtFlattening.EditValueChanged += new EventHandler(this.txtFlattening_EditValueChanged);
            this.txtMiniorAxis.EditValue = "1";
            this.txtMiniorAxis.Location = new System.Drawing.Point(80, 0x58);
            this.txtMiniorAxis.Name = "txtMiniorAxis";
            this.txtMiniorAxis.Size = new Size(160, 0x15);
            this.txtMiniorAxis.TabIndex = 7;
            this.txtMiniorAxis.EditValueChanged += new EventHandler(this.txtMiniorAxis_EditValueChanged);
            this.txtMajorAxis.EditValue = "1";
            this.txtMajorAxis.Location = new System.Drawing.Point(80, 0x38);
            this.txtMajorAxis.Name = "txtMajorAxis";
            this.txtMajorAxis.Size = new Size(160, 0x15);
            this.txtMajorAxis.TabIndex = 6;
            this.txtMajorAxis.EditValueChanged += new EventHandler(this.txtMajorAxis_EditValueChanged);
            this.rdoFlattening.Location = new System.Drawing.Point(8, 0x76);
            this.rdoFlattening.Name = "rdoFlattening";
            this.rdoFlattening.Size = new Size(0x48, 0x18);
            this.rdoFlattening.TabIndex = 4;
            this.rdoFlattening.Text = "扁率倒数";
            this.rdoFlattening.Click += new EventHandler(this.rdoFlattening_Click);
            this.rdoMiniorAxis.Location = new System.Drawing.Point(8, 0x53);
            this.rdoMiniorAxis.Name = "rdoMiniorAxis";
            this.rdoMiniorAxis.Size = new Size(0x40, 0x18);
            this.rdoMiniorAxis.TabIndex = 3;
            this.rdoMiniorAxis.Text = "短半轴";
            this.rdoMiniorAxis.Click += new EventHandler(this.rdoMiniorAxis_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 0x39);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x30, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "长半轴:";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x18);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "名称:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "名称";
            this.groupBox4.Controls.Add(this.cboAngleName);
            this.groupBox4.Controls.Add(this.txtValue);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(8, 0x100);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x128, 0x58);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "角度单位";
            this.cboAngleName.EditValue = "";
            this.cboAngleName.Location = new System.Drawing.Point(0x58, 0x18);
            this.cboAngleName.Name = "cboAngleName";
            this.cboAngleName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboAngleName.Size = new Size(0xb8, 0x15);
            this.cboAngleName.TabIndex = 10;
            this.cboAngleName.SelectedIndexChanged += new EventHandler(this.cboAngleName_SelectedIndexChanged);
            this.txtValue.EditValue = "1";
            this.txtValue.Location = new System.Drawing.Point(0x58, 0x38);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new Size(0xb8, 0x15);
            this.txtValue.TabIndex = 9;
            this.txtValue.EditValueChanged += new EventHandler(this.txtValue_EditValueChanged);
            this.label6.Location = new System.Drawing.Point(0x10, 60);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x48, 0x10);
            this.label6.TabIndex = 4;
            this.label6.Text = "单位(弧度):";
            this.label5.Location = new System.Drawing.Point(0x10, 0x18);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x38, 0x10);
            this.label5.TabIndex = 2;
            this.label5.Text = "名称:";
            this.groupBox3.Controls.Add(this.cboPrimeMeridians);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtSecond);
            this.groupBox3.Controls.Add(this.txtMinute);
            this.groupBox3.Controls.Add(this.txtDegree);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(8, 0x160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x128, 0x58);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "本初子午线";
            this.cboPrimeMeridians.EditValue = "";
            this.cboPrimeMeridians.Location = new System.Drawing.Point(0x40, 0x18);
            this.cboPrimeMeridians.Name = "cboPrimeMeridians";
            this.cboPrimeMeridians.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboPrimeMeridians.Size = new Size(0xd8, 0x15);
            this.cboPrimeMeridians.TabIndex = 15;
            this.cboPrimeMeridians.SelectedIndexChanged += new EventHandler(this.cboPrimeMeridians_SelectedIndexChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0x110, 0x38);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x11, 0x11);
            this.label11.TabIndex = 11;
            this.label11.Text = "″";
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0xb8, 0x38);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x11, 0x11);
            this.label10.TabIndex = 10;
            this.label10.Text = "′";
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0x70, 0x38);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 0x11);
            this.label9.TabIndex = 9;
            this.label9.Text = "\x00b0";
            this.txtSecond.EditValue = "0";
            this.txtSecond.Location = new System.Drawing.Point(0xd0, 0x38);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new Size(0x30, 0x15);
            this.txtSecond.TabIndex = 14;
            this.txtSecond.EditValueChanged += new EventHandler(this.txtSecond_EditValueChanged);
            this.txtMinute.EditValue = "0";
            this.txtMinute.Location = new System.Drawing.Point(0x88, 0x38);
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new Size(0x30, 0x15);
            this.txtMinute.TabIndex = 13;
            this.txtMinute.EditValueChanged += new EventHandler(this.txtMinute_EditValueChanged);
            this.txtDegree.EditValue = "0";
            this.txtDegree.Location = new System.Drawing.Point(0x40, 0x38);
            this.txtDegree.Name = "txtDegree";
            this.txtDegree.Size = new Size(40, 0x15);
            this.txtDegree.TabIndex = 12;
            this.txtDegree.EditValueChanged += new EventHandler(this.txtDegree_EditValueChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0x10, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 0x11);
            this.label7.TabIndex = 4;
            this.label7.Text = "经度:";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0x10, 0x19);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 0x11);
            this.label8.TabIndex = 2;
            this.label8.Text = "名称:";
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textEditName);
            base.Name = "GeoCoordSys";
            base.Size = new Size(320, 0x1c8);
            base.Load += new EventHandler(this.GeoCoordSys_Load);
            this.textEditName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboDatumName.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboSpheres.Properties.EndInit();
            this.txtFlattening.Properties.EndInit();
            this.txtMiniorAxis.Properties.EndInit();
            this.txtMajorAxis.Properties.EndInit();
            this.groupBox4.ResumeLayout(false);
            this.cboAngleName.Properties.EndInit();
            this.txtValue.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.cboPrimeMeridians.Properties.EndInit();
            this.txtSecond.Properties.EndInit();
            this.txtMinute.Properties.EndInit();
            this.txtDegree.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.cboDatumName.Properties.Items.Add("自定义");
            this.cboDatumName.Properties.Items.Add("D_Beijing_1954");
            this.cboDatumName.Properties.Items.Add("D_Krasovsky_1940");
            this.cboDatumName.Properties.Items.Add("D_WGS_1984");
            this.cboDatumName.Properties.Items.Add("D_Xian_1980");
        }

        private void method_1()
        {
            this.cboSpheres.Properties.Items.Add("自定义");
            this.cboSpheres.Properties.Items.Add("Krasovsky_1940");
            this.cboSpheres.Properties.Items.Add("WGS_1984");
            this.cboSpheres.Properties.Items.Add("Xian_1980");
        }

        private void method_2()
        {
            this.cboAngleName.Properties.Items.Add("自定义");
            this.cboAngleName.Properties.Items.Add("Degree");
            this.cboAngleName.Properties.Items.Add("Gon");
            this.cboAngleName.Properties.Items.Add("Minute");
            this.cboAngleName.Properties.Items.Add("Second");
            this.cboAngleName.Properties.Items.Add("Radian");
        }

        private void method_3()
        {
            this.cboPrimeMeridians.Properties.Items.Add("自定义");
            this.cboPrimeMeridians.Properties.Items.Add("Greenwich");
            this.cboPrimeMeridians.Properties.Items.Add("Athens");
            this.cboPrimeMeridians.Properties.Items.Add("Bern");
        }

        private void method_4()
        {
            if (this.igeographicCoordinateSystem_0 != null)
            {
                this.textEditName.Text = this.igeographicCoordinateSystem_0.Name;
                this.idatum_0 = this.igeographicCoordinateSystem_0.Datum;
                int index = this.cboDatumName.Properties.Items.IndexOf(this.idatum_0.Name);
                this.ispheroid_0 = this.idatum_0.Spheroid;
                if (index != -1)
                {
                    this.cboDatumName.SelectedIndex = index;
                }
                else
                {
                    this.cboDatumName.Text = this.idatum_0.Name;
                    index = this.cboSpheres.Properties.Items.IndexOf(this.idatum_0.Spheroid.Name);
                    if (index != -1)
                    {
                        this.cboSpheres.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboSpheres.Text = this.idatum_0.Spheroid.Name;
                        this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                        this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                        this.txtFlattening.Text = (1.0 / this.ispheroid_0.Flattening).ToString();
                    }
                }
                this.iunit_0 = this.igeographicCoordinateSystem_0.CoordinateUnit;
                index = this.cboAngleName.Properties.Items.IndexOf(this.iunit_0.Name);
                if (index != -1)
                {
                    this.cboAngleName.SelectedIndex = index;
                }
                else
                {
                    this.cboAngleName.Text = this.iunit_0.Name;
                    this.txtValue.Text = ((IAngularUnit) this.iunit_0).RadiansPerUnit.ToString();
                }
                this.iprimeMeridian_0 = this.igeographicCoordinateSystem_0.PrimeMeridian;
                index = this.cboPrimeMeridians.Properties.Items.IndexOf(this.iprimeMeridian_0.Name);
                if (index != -1)
                {
                    this.cboPrimeMeridians.SelectedIndex = index;
                }
                else
                {
                    this.cboPrimeMeridians.Text = this.iprimeMeridian_0.Name;
                    int longitude = (int) this.iprimeMeridian_0.Longitude;
                    double num4 = (this.iprimeMeridian_0.Longitude - longitude) * 60.0;
                    int num5 = (int) num4;
                    num4 = (num4 - num5) * 60.0;
                    this.txtDegree.Text = longitude.ToString();
                    this.txtMinute.Text = num5.ToString();
                    this.txtSecond.Text = num4.ToString();
                }
            }
            else
            {
                this.igeographicCoordinateSystem_0 = new GeographicCoordinateSystemClass();
                this.idatum_0 = this.igeographicCoordinateSystem_0.Datum;
                this.ispheroid_0 = this.idatum_0.Spheroid;
                this.iunit_0 = this.igeographicCoordinateSystem_0.CoordinateUnit;
                this.iprimeMeridian_0 = this.igeographicCoordinateSystem_0.PrimeMeridian;
                this.cboDatumName.SelectedIndex = 0;
                this.cboSpheres.SelectedIndex = 0;
                this.cboAngleName.SelectedIndex = 0;
                this.cboPrimeMeridians.SelectedIndex = 0;
            }
        }

        private void method_5(TextEdit textEdit_0, bool bool_1)
        {
            textEdit_0.Properties.ReadOnly = bool_1;
            if (textEdit_0.Properties.ReadOnly)
            {
                textEdit_0.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                textEdit_0.BackColor = SystemColors.Window;
            }
        }

        private void rdoFlattening_Click(object sender, EventArgs e)
        {
            if (this.rdoFlattening.Checked)
            {
                this.txtMiniorAxis.Enabled = false;
                this.txtFlattening.Enabled = true;
            }
        }

        private void rdoMiniorAxis_Click(object sender, EventArgs e)
        {
            if (this.rdoMiniorAxis.Checked)
            {
                this.txtMiniorAxis.Enabled = true;
                this.txtFlattening.Enabled = false;
            }
        }

        private void txtDegree_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtDegree.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtDegree.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtDegree.Text))
                {
                    double num = Convert.ToDouble(this.txtDegree.Text);
                    if ((num >= -180.0) && (num <= 180.0))
                    {
                        if (this.txtDegree.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtDegree.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtDegree.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtDegree.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtDegree.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtDegree.ForeColor = Color.Red;
                }
            }
        }

        private void txtFlattening_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtFlattening.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtFlattening.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtFlattening.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtFlattening.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtFlattening.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtFlattening.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtFlattening.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtFlattening.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtFlattening.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtFlattening.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtFlattening.ForeColor = Color.Red;
                }
            }
        }

        private void txtMajorAxis_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMajorAxis.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMajorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMajorAxis.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtMajorAxis.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtMajorAxis.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtMajorAxis.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtMajorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMajorAxis.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMajorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMajorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMajorAxis.ForeColor = Color.Red;
                }
            }
        }

        private void txtMiniorAxis_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMiniorAxis.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMiniorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMiniorAxis.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtMiniorAxis.Text))
                {
                    double num = 0.0;
                    try
                    {
                        num = Convert.ToDouble(this.txtMiniorAxis.Text);
                    }
                    catch
                    {
                    }
                    if (num > 0.0)
                    {
                        if (this.txtMiniorAxis.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtMiniorAxis.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMiniorAxis.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMiniorAxis.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMiniorAxis.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMiniorAxis.ForeColor = Color.Red;
                }
            }
        }

        private void txtMinute_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtMinute.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtMinute.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMinute.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtMinute.Text))
                {
                    double num = Convert.ToDouble(str);
                    if ((num >= 0.0) && (num <= 60.0))
                    {
                        if (this.txtMinute.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtMinute.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtMinute.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtMinute.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtMinute.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtMinute.ForeColor = Color.Red;
                }
            }
        }

        private void txtSecond_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtSecond.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtSecond.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtSecond.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtSecond.Text))
                {
                    double num = Convert.ToDouble(str);
                    if ((num >= 0.0) && (num <= 60.0))
                    {
                        if (this.txtSecond.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtSecond.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtSecond.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtSecond.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtSecond.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtSecond.ForeColor = Color.Red;
                }
            }
        }

        private void txtValue_EditValueChanged(object sender, EventArgs e)
        {
            string str = this.txtValue.Text.Trim();
            if (str.Length != 0)
            {
                if ((str == "-") || (str == "+"))
                {
                    if (this.txtValue.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
                else if (Common.IsNmuber(this.txtValue.Text))
                {
                    if (Convert.ToDouble(this.txtValue.Text) > 0.0)
                    {
                        if (this.txtValue.ForeColor != SystemColors.WindowText)
                        {
                            this.int_0--;
                        }
                        this.txtValue.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        if (this.txtValue.ForeColor != Color.Red)
                        {
                            this.int_0++;
                        }
                        this.txtValue.ForeColor = Color.Red;
                    }
                }
                else
                {
                    if (this.txtValue.ForeColor != Color.Red)
                    {
                        this.int_0++;
                    }
                    this.txtValue.ForeColor = Color.Red;
                }
            }
        }

        public IGeographicCoordinateSystem GeographicCoordinateSystem
        {
            set
            {
                this.igeographicCoordinateSystem_0 = value;
            }
        }
    }
}

