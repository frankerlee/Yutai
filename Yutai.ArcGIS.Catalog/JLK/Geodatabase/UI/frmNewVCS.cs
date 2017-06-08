namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geometry;
    using JLK.ControlExtend;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmNewVCS : Form
    {
        private bool bool_0 = false;
        private bool bool_1;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private ComboBoxEdit cboDatumName;
        private ComboBoxEdit cboLineUnitName;
        private ComboBoxEdit cboProjectName;
        private ComboBoxEdit cboSpheres;
        private double[] double_0;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBoxSpere;
        private IContainer icontainer_0 = null;
        private IHVDatum ihvdatum_0;
        private int int_0 = 0;
        private int[] int_1 = new int[] { 0x2329, 0x234c };
        private int[] int_2 = new int[] { 0x1846, 0x1788, 0x18b6, 0x19d2 };
        private int[] int_3 = new int[] { 0x1b70, 0x1b76, 0x1b89 };
        private IParameter[] iparameter_0 = new IParameter[2];
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private ISpheroid ispheroid_0;
        private IUnit iunit_0;
        private IVerticalCoordinateSystem iverticalCoordinateSystem_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private EditListView paramlistView;
        private RadioButton rdoFlattening;
        private RadioButton rdoMiniorAxis;
        private RadioGroup rdoVCSBase;
        private TextEdit textEditName;
        private TextEdit txtFlattening;
        private TextEdit txtMajorAxis;
        private TextEdit txtMiniorAxis;
        private TextEdit txtValue;

        public frmNewVCS()
        {
            double[] numArray2 = new double[2];
            numArray2[1] = 1.0;
            this.double_0 = numArray2;
            this.bool_1 = false;
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textEditName.Text.Length == 0)
            {
                MessageBox.Show("未指定名字，必须为坐标系统指定一个名字！");
            }
            else
            {
                double num;
                if (this.cboLineUnitName.SelectedIndex <= 0)
                {
                    num = Convert.ToDouble(this.txtValue.Text);
                    ((ILinearUnitEdit) this.iunit_0).DefineEx(this.cboLineUnitName.Text, null, null, null, ref num);
                }
                if (this.rdoVCSBase.SelectedIndex == 0)
                {
                    string name = this.cboProjectName.Text;
                    if (name.Trim().Length == 0)
                    {
                        MessageBox.Show("请选择数据框架！");
                        return;
                    }
                    this.ihvdatum_0 = new VerticalDatumClass();
                    ((IVerticalDatumEdit) this.ihvdatum_0).DefineEx(name, name, name, name);
                }
                else
                {
                    if (this.cboDatumName.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("请选择数据框架！");
                        return;
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
                        this.ihvdatum_0 = new DatumClass();
                        ((IDatumEdit) this.ihvdatum_0).DefineEx(this.cboDatumName.Text, null, null, null, this.ispheroid_0);
                    }
                }
                object text = this.textEditName.Text;
                object alias = this.textEditName.Text;
                object abbreviation = this.textEditName.Text;
                object remarks = this.textEditName.Text;
                object useage = "everywhere!";
                object hvDatum = this.ihvdatum_0;
                object projectedUnit = this.iunit_0;
                object verticalShift = this.double_0[0];
                object positiveDirection = this.double_0[1];
                if (this.iverticalCoordinateSystem_0 == null)
                {
                    this.iverticalCoordinateSystem_0 = new VerticalCoordinateSystemClass();
                }
                (this.iverticalCoordinateSystem_0 as IVerticalCoordinateSystemEdit).Define(ref text, ref alias, ref abbreviation, ref remarks, ref useage, ref hvDatum, ref projectedUnit, ref verticalShift, ref positiveDirection);
                base.DialogResult = DialogResult.OK;
            }
        }

        private void cboDatumName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboDatumName.SelectedIndex <= 0)
            {
                this.cboSpheres.Enabled = true;
                if (this.cboSpheres.SelectedIndex == -1)
                {
                    this.cboSpheres.SelectedIndex = 0;
                }
            }
            else
            {
                this.cboSpheres.Enabled = false;
                if (this.bool_0)
                {
                    this.ihvdatum_0 = this.ispatialReferenceFactory_0.CreateDatum(this.int_2[this.cboDatumName.SelectedIndex - 1]) as IHVDatum;
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.bool_0 = false;
                }
                int index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                if (index != -1)
                {
                    this.cboSpheres.SelectedIndex = index;
                }
                this.bool_0 = true;
            }
        }

        private void cboLineUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                this.method_1(this.txtValue, false);
            }
            else
            {
                this.method_1(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 = this.ispatialReferenceFactory_0.CreateUnit(this.int_1[this.cboLineUnitName.SelectedIndex - 1]);
                }
                this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
            }
        }

        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboSpheres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboSpheres.SelectedIndex <= 0)
            {
                this.method_1(this.txtMajorAxis, false);
                this.method_1(this.txtMiniorAxis, false);
                this.method_1(this.txtFlattening, false);
            }
            else
            {
                this.method_1(this.txtMajorAxis, true);
                this.method_1(this.txtMiniorAxis, true);
                this.method_1(this.txtFlattening, true);
                if (this.bool_0)
                {
                    this.ispheroid_0 = this.ispatialReferenceFactory_0.CreateSpheroid(this.int_3[this.cboSpheres.SelectedIndex - 1]);
                }
                this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                this.txtFlattening.Text = (1.0 / this.ispheroid_0.Flattening).ToString();
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void frmNewVCS_Load(object sender, EventArgs e)
        {
            int index;
            if (this.bool_1)
            {
                this.Text = "编辑垂直坐标系";
            }
            else
            {
                this.Text = "新建垂直坐标系";
            }
            this.rdoMiniorAxis.Checked = true;
            this.txtFlattening.Enabled = false;
            if (this.iverticalCoordinateSystem_0 == null)
            {
                this.bool_0 = true;
                index = -1;
                this.iverticalCoordinateSystem_0 = new VerticalCoordinateSystemClass();
                this.iunit_0 = this.iverticalCoordinateSystem_0.CoordinateUnit;
                this.ihvdatum_0 = this.iverticalCoordinateSystem_0.Datum;
                if (this.ihvdatum_0 is IDatum)
                {
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.rdoVCSBase.SelectedIndex = 1;
                    index = this.cboDatumName.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Name);
                    if (index != -1)
                    {
                        this.cboDatumName.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboDatumName.Text = (this.ihvdatum_0 as IDatum).Name;
                        if (this.cboDatumName.Text == "")
                        {
                            this.cboDatumName.SelectedIndex = 0;
                        }
                        else
                        {
                            index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                            if (index != -1)
                            {
                                this.cboSpheres.SelectedIndex = index;
                            }
                            else
                            {
                                this.cboSpheres.Text = (this.ihvdatum_0 as IDatum).Spheroid.Name;
                                this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                                this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                                double num2 = 1.0 / this.ispheroid_0.Flattening;
                                this.txtFlattening.Text = num2.ToString();
                            }
                        }
                    }
                }
                else
                {
                    this.rdoVCSBase.SelectedIndex = 0;
                    this.cboProjectName.Text = (this.ihvdatum_0 as IVerticalDatum).Name;
                }
                this.cboLineUnitName.SelectedIndex = 1;
                index = this.cboLineUnitName.Properties.Items.IndexOf(this.iunit_0.Name);
                if (index != -1)
                {
                    this.cboLineUnitName.SelectedIndex = index;
                }
                else
                {
                    this.cboLineUnitName.Text = this.iunit_0.Name;
                    this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
                }
            }
            else
            {
                this.textEditName.Text = this.iverticalCoordinateSystem_0.Name;
                this.ihvdatum_0 = this.iverticalCoordinateSystem_0.Datum;
                index = -1;
                this.iunit_0 = this.iverticalCoordinateSystem_0.CoordinateUnit;
                if (this.ihvdatum_0 is IDatum)
                {
                    this.ispheroid_0 = (this.ihvdatum_0 as IDatum).Spheroid;
                    this.rdoVCSBase.SelectedIndex = 1;
                    index = this.cboDatumName.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Name);
                    if (index != -1)
                    {
                        this.cboDatumName.SelectedIndex = index;
                    }
                    else
                    {
                        this.cboDatumName.Text = (this.ihvdatum_0 as IDatum).Name;
                        index = this.cboSpheres.Properties.Items.IndexOf((this.ihvdatum_0 as IDatum).Spheroid.Name);
                        if (index != -1)
                        {
                            this.cboSpheres.SelectedIndex = index;
                        }
                        else
                        {
                            this.cboSpheres.Text = (this.ihvdatum_0 as IDatum).Spheroid.Name;
                            this.txtMajorAxis.Text = this.ispheroid_0.SemiMajorAxis.ToString();
                            this.txtMiniorAxis.Text = this.ispheroid_0.SemiMinorAxis.ToString();
                            this.txtFlattening.Text = (1.0 / this.ispheroid_0.Flattening).ToString();
                        }
                    }
                }
                else
                {
                    this.rdoVCSBase.SelectedIndex = 0;
                    this.cboProjectName.Text = (this.ihvdatum_0 as IVerticalDatum).Name;
                }
                index = this.cboLineUnitName.Properties.Items.IndexOf(this.iunit_0.Name);
                if (index != -1)
                {
                    this.cboLineUnitName.SelectedIndex = index;
                }
                else
                {
                    this.cboLineUnitName.Text = this.iunit_0.Name;
                    this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
                }
                (this.iverticalCoordinateSystem_0 as IVerticalCoordinateSystemGEN).GetParameters(ref this.iparameter_0);
                if (this.iparameter_0[0] != null)
                {
                    this.double_0[0] = this.iparameter_0[0].Value;
                    if (this.iparameter_0[1] != null)
                    {
                        this.double_0[1] = this.iparameter_0[1].Value;
                    }
                }
                this.bool_0 = true;
            }
            this.method_0(this.iparameter_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmNewVCS));
            this.groupBox4 = new GroupBox();
            this.cboLineUnitName = new ComboBoxEdit();
            this.txtValue = new TextEdit();
            this.label6 = new Label();
            this.label5 = new Label();
            this.groupBox1 = new GroupBox();
            this.groupBoxSpere = new GroupBox();
            this.cboSpheres = new ComboBoxEdit();
            this.txtFlattening = new TextEdit();
            this.txtMiniorAxis = new TextEdit();
            this.txtMajorAxis = new TextEdit();
            this.rdoFlattening = new RadioButton();
            this.rdoMiniorAxis = new RadioButton();
            this.label4 = new Label();
            this.label7 = new Label();
            this.cboDatumName = new ComboBoxEdit();
            this.label3 = new Label();
            this.cboProjectName = new ComboBoxEdit();
            this.label2 = new Label();
            this.rdoVCSBase = new RadioGroup();
            this.paramlistView = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.label1 = new Label();
            this.textEditName = new TextEdit();
            this.groupBox3 = new GroupBox();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.groupBox4.SuspendLayout();
            this.cboLineUnitName.Properties.BeginInit();
            this.txtValue.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBoxSpere.SuspendLayout();
            this.cboSpheres.Properties.BeginInit();
            this.txtFlattening.Properties.BeginInit();
            this.txtMiniorAxis.Properties.BeginInit();
            this.txtMajorAxis.Properties.BeginInit();
            this.cboDatumName.Properties.BeginInit();
            this.cboProjectName.Properties.BeginInit();
            this.rdoVCSBase.Properties.BeginInit();
            this.textEditName.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox4.Controls.Add(this.cboLineUnitName);
            this.groupBox4.Controls.Add(this.txtValue);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(0x12, 0x152);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x128, 0x58);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "线性单位";
            this.cboLineUnitName.EditValue = "";
            this.cboLineUnitName.Location = new System.Drawing.Point(80, 0x18);
            this.cboLineUnitName.Name = "cboLineUnitName";
            this.cboLineUnitName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLineUnitName.Properties.Items.AddRange(new object[] { "自定义", "Meter", "Kilometer" });
            this.cboLineUnitName.Size = new Size(0xc0, 0x15);
            this.cboLineUnitName.TabIndex = 10;
            this.cboLineUnitName.SelectedIndexChanged += new EventHandler(this.cboLineUnitName_SelectedIndexChanged);
            this.txtValue.EditValue = "1";
            this.txtValue.Location = new System.Drawing.Point(80, 0x38);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new Size(0xc0, 0x15);
            this.txtValue.TabIndex = 9;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0x10, 0x38);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3b, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "单位(米):";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x10, 0x1a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "名称:";
            this.groupBox1.Controls.Add(this.groupBoxSpere);
            this.groupBox1.Controls.Add(this.cboDatumName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboProjectName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rdoVCSBase);
            this.groupBox1.Location = new System.Drawing.Point(0x12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 0x124);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据框架";
            this.groupBoxSpere.Controls.Add(this.cboSpheres);
            this.groupBoxSpere.Controls.Add(this.txtFlattening);
            this.groupBoxSpere.Controls.Add(this.txtMiniorAxis);
            this.groupBoxSpere.Controls.Add(this.txtMajorAxis);
            this.groupBoxSpere.Controls.Add(this.rdoFlattening);
            this.groupBoxSpere.Controls.Add(this.rdoMiniorAxis);
            this.groupBoxSpere.Controls.Add(this.label4);
            this.groupBoxSpere.Controls.Add(this.label7);
            this.groupBoxSpere.Enabled = false;
            this.groupBoxSpere.Location = new System.Drawing.Point(0x10, 0x80);
            this.groupBoxSpere.Name = "groupBoxSpere";
            this.groupBoxSpere.Size = new Size(0x108, 0x98);
            this.groupBoxSpere.TabIndex = 9;
            this.groupBoxSpere.TabStop = false;
            this.groupBoxSpere.Text = "椭球参数";
            this.cboSpheres.EditValue = "";
            this.cboSpheres.Location = new System.Drawing.Point(80, 0x18);
            this.cboSpheres.Name = "cboSpheres";
            this.cboSpheres.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboSpheres.Properties.Items.AddRange(new object[] { "自定义", "Krasovsky_1940", "WGS_1984", "Xian_1980" });
            this.cboSpheres.Size = new Size(160, 0x15);
            this.cboSpheres.TabIndex = 9;
            this.cboSpheres.SelectedIndexChanged += new EventHandler(this.cboSpheres_SelectedIndexChanged);
            this.txtFlattening.EditValue = "1";
            this.txtFlattening.Location = new System.Drawing.Point(80, 120);
            this.txtFlattening.Name = "txtFlattening";
            this.txtFlattening.Size = new Size(160, 0x15);
            this.txtFlattening.TabIndex = 8;
            this.txtMiniorAxis.EditValue = "1";
            this.txtMiniorAxis.Location = new System.Drawing.Point(80, 0x58);
            this.txtMiniorAxis.Name = "txtMiniorAxis";
            this.txtMiniorAxis.Size = new Size(160, 0x15);
            this.txtMiniorAxis.TabIndex = 7;
            this.txtMajorAxis.EditValue = "1";
            this.txtMajorAxis.Location = new System.Drawing.Point(80, 0x38);
            this.txtMajorAxis.Name = "txtMajorAxis";
            this.txtMajorAxis.Size = new Size(160, 0x15);
            this.txtMajorAxis.TabIndex = 6;
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
            this.label4.Size = new Size(0x2f, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "长半轴:";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 0x18);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "名称:";
            this.cboDatumName.EditValue = "";
            this.cboDatumName.Enabled = false;
            this.cboDatumName.Location = new System.Drawing.Point(0x48, 0x62);
            this.cboDatumName.Name = "cboDatumName";
            this.cboDatumName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboDatumName.Properties.Items.AddRange(new object[] { "自定义", "D_Beijing_1954", "D_Krasovsky_1940", "D_WGS_1984", "D_Xian_1980" });
            this.cboDatumName.Size = new Size(200, 0x15);
            this.cboDatumName.TabIndex = 8;
            this.cboDatumName.SelectedIndexChanged += new EventHandler(this.cboDatumName_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x18, 0x67);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "名称:";
            this.cboProjectName.EditValue = "";
            this.cboProjectName.Location = new System.Drawing.Point(0x48, 0x2d);
            this.cboProjectName.Name = "cboProjectName";
            this.cboProjectName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboProjectName.Properties.Items.AddRange(new object[] { "Bandar_Abbas", "Caspian", "Fahud_Height_Datum_1993", "Fao", "Ha_Tien_1960", "Hon_Dau_1992", "Hong_Kong_Chart_Datum", "Hong_Kong_Principal_Datum", "Japanese_Standard_Levelling_Datum_1949", "KOC_Construction_Datum", "KOC_Well_Datum", "Kuwait_PWD", "PDO_Height_Datum_1993", "Yellow_Sea_1956", "Yellow_Sea_1985" });
            this.cboProjectName.Size = new Size(200, 0x15);
            this.cboProjectName.TabIndex = 6;
            this.cboProjectName.SelectedIndexChanged += new EventHandler(this.cboProjectName_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x18, 50);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "名称:";
            this.rdoVCSBase.Location = new System.Drawing.Point(6, -3);
            this.rdoVCSBase.Name = "rdoVCSBase";
            this.rdoVCSBase.Properties.Appearance.BackColor = Color.Transparent;
            this.rdoVCSBase.Properties.Appearance.Options.UseBackColor = true;
            this.rdoVCSBase.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "基于大地水准面"), new RadioGroupItem(null, "基于椭球表面") });
            this.rdoVCSBase.Size = new Size(0xce, 0x76);
            this.rdoVCSBase.TabIndex = 4;
            this.rdoVCSBase.SelectedIndexChanged += new EventHandler(this.rdoVCSBase_SelectedIndexChanged);
            this.paramlistView.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.paramlistView.ComboBoxBgColor = Color.LightBlue;
            this.paramlistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.EditBgColor = Color.LightBlue;
            this.paramlistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.FullRowSelect = true;
            this.paramlistView.GridLines = true;
            this.paramlistView.Location = new System.Drawing.Point(0x12, 0x15);
            this.paramlistView.LockRowCount = 0;
            this.paramlistView.Name = "paramlistView";
            this.paramlistView.Size = new Size(0x110, 0x55);
            this.paramlistView.TabIndex = 2;
            this.paramlistView.UseCompatibleStateImageBehavior = false;
            this.paramlistView.View = View.Details;
            this.paramlistView.SelectedIndexChanged += new EventHandler(this.paramlistView_SelectedIndexChanged);
            this.paramlistView.ValueChanged += new JLK.ControlExtend.ValueChangedHandler(this.method_2);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "参数";
            this.lvcolumnHeader_0.Width = 0x90;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 0x7a;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "名称:";
            this.textEditName.EditValue = "";
            this.textEditName.Location = new System.Drawing.Point(0x3a, 8);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new Size(240, 0x15);
            this.textEditName.TabIndex = 9;
            this.groupBox3.Controls.Add(this.paramlistView);
            this.groupBox3.Location = new System.Drawing.Point(0x12, 0x1b2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x128, 0x74);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x119, 0x235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new System.Drawing.Point(0xd9, 0x235);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x15d, 0x259);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textEditName);
            base.Controls.Add(this.groupBox3);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "frmNewVCS";
            this.Text = "New VCS";
            base.Load += new EventHandler(this.frmNewVCS_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.cboLineUnitName.Properties.EndInit();
            this.txtValue.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSpere.ResumeLayout(false);
            this.groupBoxSpere.PerformLayout();
            this.cboSpheres.Properties.EndInit();
            this.txtFlattening.Properties.EndInit();
            this.txtMiniorAxis.Properties.EndInit();
            this.txtMajorAxis.Properties.EndInit();
            this.cboDatumName.Properties.EndInit();
            this.cboProjectName.Properties.EndInit();
            this.rdoVCSBase.Properties.EndInit();
            this.textEditName.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(IParameter[] iparameter_1)
        {
            this.paramlistView.Items.Clear();
            string[] items = new string[] { "垂直偏移", this.double_0[0].ToString() };
            this.paramlistView.Items.Add(new ListViewItem(items));
            items[0] = "方向";
            items[1] = this.double_0[1].ToString();
            this.paramlistView.Items.Add(new ListViewItem(items));
        }

        private void method_1(TextEdit textEdit_0, bool bool_2)
        {
            textEdit_0.Properties.ReadOnly = bool_2;
            if (textEdit_0.Properties.ReadOnly)
            {
                textEdit_0.BackColor = SystemColors.InactiveBorder;
            }
            else
            {
                textEdit_0.BackColor = SystemColors.Window;
            }
        }

        private void method_2(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(e.NewValue);
                this.double_0[e.Row] = num;
            }
            catch
            {
                MessageBox.Show("数据格式错!");
            }
        }

        private void paramlistView_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void rdoVCSBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.rdoVCSBase.SelectedIndex == 0;
            this.cboProjectName.Enabled = flag;
            this.cboDatumName.Enabled = !flag;
            this.groupBoxSpere.Enabled = !flag;
        }

        public IVerticalCoordinateSystem VerticalCoordinateSystem
        {
            get
            {
                return this.iverticalCoordinateSystem_0;
            }
            set
            {
                this.bool_1 = true;
                this.iverticalCoordinateSystem_0 = value;
            }
        }
    }
}

