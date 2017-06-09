using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class ProjCoordSys : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private SimpleButton btnSelect;
        private ComboBoxEdit cboLineUnitName;
        private ComboBoxEdit cboProjectName;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IGeographicCoordinateSystem igeographicCoordinateSystem_0;
        private int int_0 = 0;
        private int[] int_1 = new int[] { 0xa7fd, 0xa80c, 0xa819, 0xa7fc, 0xa7fe };
        private int[] int_2 = new int[] { 0x2329, 0x234c };
        private IParameter[] iparameter_0 = new IParameter[0x19];
        private IProjectedCoordinateSystem iprojectedCoordinateSystem_0;
        private IProjection iprojection_0;
        private ISpatialReferenceFactory ispatialReferenceFactory_0 = new SpatialReferenceEnvironmentClass();
        private IUnit iunit_0;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label6;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private EditListView paramlistView;
        private MemoEdit textBoxGeoCoodSys;
        private TextEdit textEditName;
        private TextEdit txtValue;

        public ProjCoordSys()
        {
            this.InitializeComponent();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                Text = "地理坐标系属性",
                SpatialRefrence = this.igeographicCoordinateSystem_0
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.method_3(this.igeographicCoordinateSystem_0);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmSpatialRefrence refrence = new frmSpatialRefrence {
                Text = "新建地理坐标系",
                SpatialRefrenceType = frmSpatialRefrence.enumSpatialRefrenceType.enumGeographicCoord
            };
            if (refrence.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 = (IGeographicCoordinateSystem) refrence.SpatialRefrence;
                this.method_3(this.igeographicCoordinateSystem_0);
                this.btnModify.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "空间参考文件 (*.prj)|*.prj",
                RestoreDirectory = true
            };
            while (dialog.ShowDialog() == DialogResult.OK)
            {
                this.igeographicCoordinateSystem_0 = ((ISpatialReferenceFactory2) this.ispatialReferenceFactory_0).CreateESRISpatialReferenceFromPRJFile(dialog.FileName) as IGeographicCoordinateSystem;
                if (this.igeographicCoordinateSystem_0 != null)
                {
                    this.method_3(this.igeographicCoordinateSystem_0);
                    this.btnModify.Enabled = true;
                    dialog = null;
                    return;
                }
                MessageBox.Show("请选择包含地理坐标系的空间参考文件!");
            }
        }

        private void cboLineUnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                this.method_6(this.txtValue, false);
            }
            else
            {
                this.method_6(this.txtValue, true);
                if (this.bool_0)
                {
                    this.iunit_0 = this.ispatialReferenceFactory_0.CreateUnit(this.int_2[this.cboLineUnitName.SelectedIndex - 1]);
                }
                this.txtValue.Text = (this.iunit_0 as ILinearUnit).MetersPerUnit.ToString();
            }
        }

        private void cboProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iprojection_0 = this.ispatialReferenceFactory_0.CreateProjection(this.int_1[this.cboProjectName.SelectedIndex]);
                IParameter[] defaultParameters = ((IProjectionGEN) this.iprojection_0).GetDefaultParameters();
                int index = 0;
                while (index < defaultParameters.Length)
                {
                    this.iparameter_0[index] = defaultParameters[index];
                    index++;
                }
                if (this.iparameter_0.Length > defaultParameters.Length)
                {
                    this.iparameter_0[index] = null;
                }
                this.method_4(this.iparameter_0);
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

        public IProjectedCoordinateSystem GetSpatialRefrence()
        {
            int num3;
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
            if (this.igeographicCoordinateSystem_0 == null)
            {
                MessageBox.Show("未指定地理坐标，必须创建或指定地理坐标系！");
                return null;
            }
            object alias = Missing.Value;
            if (this.cboLineUnitName.SelectedIndex <= 0)
            {
                double metersPerUnit = Convert.ToDouble(this.txtValue.Text);
                ((ILinearUnitEdit) this.iunit_0).DefineEx(this.cboLineUnitName.Text, null, null, null, ref metersPerUnit);
            }
            object text = this.textEditName.Text;
            object gcs = this.igeographicCoordinateSystem_0;
            object projectedUnit = this.iunit_0;
            object projection = this.iprojection_0;
            int num2 = 0;
            for (num3 = 0; num3 < this.iparameter_0.Length; num3++)
            {
                num2++;
                if (this.iparameter_0[num3] == null)
                {
                    break;
                }
            }
            IParameter[] parameterArray = new IParameter[num2];
            for (num3 = 0; num3 < num2; num3++)
            {
                parameterArray[num3] = this.iparameter_0[num3];
            }
            object parameters = parameterArray;
            try
            {
                ((IProjectedCoordinateSystemEdit) this.iprojectedCoordinateSystem_0).Define(ref text, ref alias, ref alias, ref alias, ref alias, ref gcs, ref projectedUnit, ref projection, ref parameters);
            }
            catch
            {
                MessageBox.Show("无法定义投影坐标!");
                return null;
            }
            return this.iprojectedCoordinateSystem_0;
        }

        private void InitializeComponent()
        {
            this.groupBox4 = new GroupBox();
            this.cboLineUnitName = new ComboBoxEdit();
            this.txtValue = new TextEdit();
            this.label6 = new Label();
            this.label5 = new Label();
            this.groupBox1 = new GroupBox();
            this.cboProjectName = new ComboBoxEdit();
            this.paramlistView = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.label2 = new Label();
            this.label1 = new Label();
            this.textEditName = new TextEdit();
            this.groupBox3 = new GroupBox();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.textBoxGeoCoodSys = new MemoEdit();
            this.groupBox4.SuspendLayout();
            this.cboLineUnitName.Properties.BeginInit();
            this.txtValue.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboProjectName.Properties.BeginInit();
            this.textEditName.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.textBoxGeoCoodSys.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox4.Controls.Add(this.cboLineUnitName);
            this.groupBox4.Controls.Add(this.txtValue);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(8, 0xd0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x128, 0x58);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "线性单位";
            this.cboLineUnitName.EditValue = "";
            this.cboLineUnitName.Location = new System.Drawing.Point(80, 0x18);
            this.cboLineUnitName.Name = "cboLineUnitName";
            this.cboLineUnitName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLineUnitName.Size = new Size(0xc0, 0x15);
            this.cboLineUnitName.TabIndex = 10;
            this.cboLineUnitName.SelectedIndexChanged += new EventHandler(this.cboLineUnitName_SelectedIndexChanged);
            this.txtValue.EditValue = "1";
            this.txtValue.Location = new System.Drawing.Point(80, 0x38);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new Size(0xc0, 0x15);
            this.txtValue.TabIndex = 9;
            this.txtValue.EditValueChanged += new EventHandler(this.txtValue_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0x10, 0x38);
            this.label6.Name = "label6";
            this.label6.Size = new Size(60, 0x11);
            this.label6.TabIndex = 4;
            this.label6.Text = "单位(米):";
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x10, 0x1a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 2;
            this.label5.Text = "名称:";
            this.groupBox1.Controls.Add(this.cboProjectName);
            this.groupBox1.Controls.Add(this.paramlistView);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x128, 160);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "投影";
            this.cboProjectName.EditValue = "";
            this.cboProjectName.Location = new System.Drawing.Point(0x40, 0x18);
            this.cboProjectName.Name = "cboProjectName";
            this.cboProjectName.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboProjectName.Size = new Size(200, 0x15);
            this.cboProjectName.TabIndex = 3;
            this.cboProjectName.SelectedIndexChanged += new EventHandler(this.cboProjectName_SelectedIndexChanged);
            this.paramlistView.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.paramlistView.ComboBoxBgColor = Color.LightBlue;
            this.paramlistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.EditBgColor = Color.LightBlue;
            this.paramlistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.FullRowSelect = true;
            this.paramlistView.GridLines = true;
            this.paramlistView.Location = new System.Drawing.Point(0x10, 0x38);
            this.paramlistView.Name = "paramlistView";
            this.paramlistView.Size = new Size(0x110, 0x60);
            this.paramlistView.TabIndex = 2;
            this.paramlistView.View = View.Details;
            this.paramlistView.ValueChanged += new Common.ControlExtend.ValueChangedHandler(this.method_5);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "参数";
            this.lvcolumnHeader_0.Width = 0x90;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 0x7a;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "名称:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 5;
            this.label1.Text = "名称:";
            this.textEditName.EditValue = "";
            this.textEditName.Location = new System.Drawing.Point(0x30, 8);
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new Size(240, 0x15);
            this.textEditName.TabIndex = 4;
            this.groupBox3.Controls.Add(this.btnModify);
            this.groupBox3.Controls.Add(this.btnNew);
            this.groupBox3.Controls.Add(this.btnSelect);
            this.groupBox3.Controls.Add(this.textBoxGeoCoodSys);
            this.groupBox3.Location = new System.Drawing.Point(8, 0x130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x128, 0x80);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "地理坐标系";
            this.btnModify.Location = new System.Drawing.Point(0xe8, 0x60);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(0x30, 0x18);
            this.btnModify.TabIndex = 14;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(0xe8, 0x40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x30, 0x18);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnSelect.Location = new System.Drawing.Point(0xe8, 0x20);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x30, 0x18);
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.textBoxGeoCoodSys.EditValue = "";
            this.textBoxGeoCoodSys.Location = new System.Drawing.Point(0x10, 0x18);
            this.textBoxGeoCoodSys.Name = "textBoxGeoCoodSys";
            this.textBoxGeoCoodSys.Properties.ReadOnly = true;
            this.textBoxGeoCoodSys.Size = new Size(200, 0x60);
            this.textBoxGeoCoodSys.TabIndex = 4;
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textEditName);
            base.Controls.Add(this.groupBox3);
            base.Name = "ProjCoordSys";
            base.Size = new Size(320, 480);
            base.Load += new EventHandler(this.ProjCoordSys_Load);
            this.groupBox4.ResumeLayout(false);
            this.cboLineUnitName.Properties.EndInit();
            this.txtValue.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboProjectName.Properties.EndInit();
            this.textEditName.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.textBoxGeoCoodSys.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            this.cboProjectName.Properties.Items.Add("Gauss_Kruger");
            this.cboProjectName.Properties.Items.Add("Lambert_Conformal_Conic");
            this.cboProjectName.Properties.Items.Add("Lambert_Azimuthal_Equal_Area");
            this.cboProjectName.Properties.Items.Add("Mercator");
            this.cboProjectName.Properties.Items.Add("Transverse_Mercator");
        }

        private void method_1()
        {
            this.cboLineUnitName.Properties.Items.Add("自定义");
            this.cboLineUnitName.Properties.Items.Add("Meter");
            this.cboLineUnitName.Properties.Items.Add("Kilometer");
        }

        private void method_2()
        {
            if (this.iprojectedCoordinateSystem_0 == null)
            {
                this.bool_0 = true;
                this.cboProjectName.SelectedIndex = 0;
                this.cboLineUnitName.SelectedIndex = 1;
                this.btnModify.Enabled = false;
                this.iprojectedCoordinateSystem_0 = new ProjectedCoordinateSystemClass();
            }
            else
            {
                this.textEditName.Text = this.iprojectedCoordinateSystem_0.Name;
                this.igeographicCoordinateSystem_0 = this.iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
                this.iprojection_0 = this.iprojectedCoordinateSystem_0.Projection;
                this.iunit_0 = this.iprojectedCoordinateSystem_0.CoordinateUnit;
                ((IProjectedCoordinateSystem4GEN) this.iprojectedCoordinateSystem_0).GetParameters(ref this.iparameter_0);
                this.method_3(this.igeographicCoordinateSystem_0);
                int index = this.cboProjectName.Properties.Items.IndexOf(this.iprojection_0.Name);
                if (index != -1)
                {
                    this.cboProjectName.SelectedIndex = index;
                }
                else
                {
                    this.cboProjectName.Text = this.iprojection_0.Name;
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
                this.method_4(this.iparameter_0);
                this.btnModify.Enabled = true;
            }
            this.bool_0 = true;
        }

        private void method_3(IGeographicCoordinateSystem igeographicCoordinateSystem_1)
        {
            string[] strArray = new string[] { 
                "名称: ", igeographicCoordinateSystem_1.Name, "\r\n缩略名: ", igeographicCoordinateSystem_1.Abbreviation, "\r\n说明: ", igeographicCoordinateSystem_1.Remarks, "\r\n角度单位: ", igeographicCoordinateSystem_1.CoordinateUnit.Name, " (", igeographicCoordinateSystem_1.CoordinateUnit.RadiansPerUnit.ToString(), ")\r\n本初子午线: ", igeographicCoordinateSystem_1.PrimeMeridian.Name, " (", igeographicCoordinateSystem_1.PrimeMeridian.Longitude.ToString(), ")\r\n数据: ", igeographicCoordinateSystem_1.Datum.Name, 
                "\r\n  椭球体: ", igeographicCoordinateSystem_1.Datum.Spheroid.Name, "\r\n    长半轴: ", igeographicCoordinateSystem_1.Datum.Spheroid.SemiMajorAxis.ToString(), "\r\n    短半轴: ", igeographicCoordinateSystem_1.Datum.Spheroid.SemiMinorAxis.ToString(), "\r\n    扁率倒数: ", (1.0 / igeographicCoordinateSystem_1.Datum.Spheroid.Flattening).ToString()
             };
            string str = string.Concat(strArray);
            this.textBoxGeoCoodSys.Text = str;
        }

        private void method_4(IParameter[] iparameter_1)
        {
            this.paramlistView.Items.Clear();
            string[] items = new string[2];
            for (int i = 0; i < iparameter_1.Length; i++)
            {
                if (iparameter_1[i] == null)
                {
                    break;
                }
                items[0] = iparameter_1[i].Name;
                items[1] = iparameter_1[i].Value.ToString();
                this.paramlistView.Items.Add(new ListViewItem(items));
            }
        }

        private void method_5(object sender, ValueChangedEventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(e.NewValue);
                ((IParameterEdit) this.iparameter_0[e.Row]).Value = num;
            }
            catch
            {
                MessageBox.Show("数据格式错!", "投影");
            }
        }

        private void method_6(TextEdit textEdit_0, bool bool_1)
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

        private void ProjCoordSys_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.method_2();
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
                else if (CommonHelper.IsNmuber(this.txtValue.Text))
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
            get
            {
                return this.igeographicCoordinateSystem_0;
            }
        }

        public IParameter[] Paramters
        {
            get
            {
                int index = 0;
                while (index < this.iparameter_0.Length)
                {
                    if (this.iparameter_0[index] == null)
                    {
                        break;
                    }
                    index++;
                }
                IParameter[] parameterArray = new IParameter[index];
                for (int i = 0; i < index; i++)
                {
                    parameterArray[i] = this.iparameter_0[i];
                }
                return parameterArray;
            }
        }

        public IProjectedCoordinateSystem ProjectedCoordinateSystem
        {
            set
            {
                this.iprojectedCoordinateSystem_0 = value;
            }
        }

        public IProjection Projection
        {
            get
            {
                return this.iprojection_0;
            }
        }
    }
}

