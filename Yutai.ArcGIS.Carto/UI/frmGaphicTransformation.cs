using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmGaphicTransformation : Form
    {
        private bool bool_0 = false;
        private Button btnOK;
        private Button button2;
        private ComboBox cboHCSTransformMethod;
        private ComboBox cboTargetGCS;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IGeoTransformation[] igeoTransformation_0 = new IGeoTransformation[] { new GeocentricTranslationClass(), new MolodenskyTransformationClass(), new AbridgedMolodenskyTransformationClass(), new PositionVectorTransformationClass(), new CoordinateFrameTransformationClass(), new NADCONTransformationClass(), new HARNTransformationClass(), new LongitudeRotationTransformationClass() };
        private IGeoTransformation igeoTransformation_1 = null;
        private int[] int_0 = new int[] { 0x1076, 0x1202, 0x10e6 };
        private ISpatialReference ispatialReference_0 = null;
        private ISpatialReference ispatialReference_1 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private EditListView paramlistView;
        private string[] string_0 = new string[] { "地心装换", "Molodendky装换", "简化Molodendky装换", "位置矢量装换", "坐标框架装换", "基于NADCON装换", "基于HRAN装换", "经度旋转装换" };
        private string[] string_1 = new string[] { "X轴平移", "Y轴平移", "Z轴平移", "X轴旋转", "Y轴旋转", "Z轴旋转", "缩放" };
        private string[] string_2 = new string[] { "X轴平移", "Y轴平移", "Z轴平移" };
        private TextBox txtName;
        private TextBox txtSourGCS;

        public frmGaphicTransformation()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cboTargetGCS.SelectedIndex == -1)
            {
                MessageBox.Show("请选择目标地理框架");
            }
            else if (this.txtSourGCS.Tag != null)
            {
                if (this.cboHCSTransformMethod.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择转转方法");
                }
                else if (this.txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入新建地理坐标转转名称");
                }
                else
                {
                    ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference to = null;
                    if (this.cboTargetGCS.SelectedIndex == 0)
                    {
                        to = factory.CreateGeographicCoordinateSystem(0x1076);
                    }
                    else if (this.cboTargetGCS.SelectedIndex == 1)
                    {
                        to = factory.CreateGeographicCoordinateSystem(0x1202);
                    }
                    else
                    {
                        to = factory.CreateGeographicCoordinateSystem(0x10e6);
                    }
                    if (this.igeoTransformation_1 != null)
                    {
                        this.igeoTransformation_1.Name = this.txtName.Text.Trim();
                    }
                    else
                    {
                        IGeoTransformation geoTransformation = (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
                        geoTransformation.PutSpatialReferences(this.txtSourGCS.Tag as ISpatialReference, to);
                        geoTransformation.Name = this.txtName.Text;
                        this.igeoTransformation_1 = geoTransformation;
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void cboHCSTransformMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.paramlistView.Items.Clear();
                if (this.cboHCSTransformMethod.SelectedIndex != -1)
                {
                    IGeoTransformation geoTransformation;
                    int num;
                    double[] numArray = new double[7];
                    string[] items = new string[2];
                    if (this.igeoTransformation_1 != null)
                    {
                        geoTransformation = this.igeoTransformation_1;
                    }
                    else
                    {
                        geoTransformation = (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
                        ISpatialReference to = (this.cboTargetGCS.SelectedItem as ObjectWrap).Object as ISpatialReference;
                        geoTransformation.PutSpatialReferences(this.txtSourGCS.Tag as ISpatialReference, to);
                    }
                    if (geoTransformation is ICoordinateFrameTransformation)
                    {
                        (geoTransformation as ICoordinateFrameTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                        for (num = 0; num < this.string_1.Length; num++)
                        {
                            items[0] = this.string_1[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IMolodenskyTransformation)
                    {
                        (geoTransformation as IMolodenskyTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2]);
                        for (num = 0; num < this.string_2.Length; num++)
                        {
                            items[0] = this.string_2[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IGeocentricTranslation)
                    {
                        (geoTransformation as IGeocentricTranslation).GetParameters(out numArray[0], out numArray[1], out numArray[2]);
                        for (num = 0; num < this.string_2.Length; num++)
                        {
                            items[0] = this.string_2[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (geoTransformation is IPositionVectorTransformation)
                    {
                        (geoTransformation as IPositionVectorTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                        for (num = 0; num < this.string_1.Length; num++)
                        {
                            items[0] = this.string_1[num];
                            items[1] = numArray[num].ToString();
                            this.paramlistView.Items.Add(new ListViewItem(items));
                        }
                    }
                    else if (!(geoTransformation is IGridTransformation) && !(geoTransformation is ILongitudeRotationTransformation))
                    {
                    }
                }
            }
        }

        private void cboTargetGCS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.cboHCSTransformMethod_SelectedIndexChanged(this, e);
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmGaphicTransformation_Load(object sender, EventArgs e)
        {
            int num;
            for (num = 0; num < this.igeoTransformation_0.Length; num++)
            {
                this.cboHCSTransformMethod.Items.Add(new Class1(this.igeoTransformation_0[num], this.string_0[num]));
            }
            this.cboTargetGCS.Items.Clear();
            ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
            ISpatialReference reference = null;
            for (num = 0; num < this.int_0.Length; num++)
            {
                reference = factory.CreateGeographicCoordinateSystem(this.int_0[num]);
                this.cboTargetGCS.Items.Add(new ObjectWrap(reference));
            }
            if (this.ispatialReference_1 != null)
            {
                this.cboTargetGCS.Text = this.ispatialReference_1.Name;
                this.cboTargetGCS.Enabled = false;
            }
            else
            {
                this.cboTargetGCS.SelectedIndex = 0;
            }
            this.txtName.Text = "新装换";
            this.paramlistView.ValueChanged += new ValueChangedHandler(this.method_0);
            if (this.ispatialReference_0 is IProjectedCoordinateSystem)
            {
                this.txtSourGCS.Text = (this.ispatialReference_0 as IProjectedCoordinateSystem).GeographicCoordinateSystem.Name;
                this.txtSourGCS.Tag = (this.ispatialReference_0 as IProjectedCoordinateSystem).GeographicCoordinateSystem;
            }
            else if (this.ispatialReference_0 is IGeographicCoordinateSystem)
            {
                this.txtSourGCS.Text = this.ispatialReference_0.Name;
                this.txtSourGCS.Tag = this.ispatialReference_0;
            }
            this.bool_0 = true;
            if (this.igeoTransformation_1 != null)
            {
                this.txtName.Text = this.igeoTransformation_1.Name;
                if (this.igeoTransformation_1 is ICoordinateFrameTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 4;
                }
                else if (this.igeoTransformation_1 is IMolodenskyTransformation)
                {
                    if (this.igeoTransformation_1 is MolodenskyTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 1;
                    }
                    else if (this.igeoTransformation_1 is AbridgedMolodenskyTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 2;
                    }
                }
                else if (this.igeoTransformation_1 is IGeocentricTranslation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 0;
                }
                else if (this.igeoTransformation_1 is IPositionVectorTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 3;
                }
                else if (this.igeoTransformation_1 is IGridTransformation)
                {
                    if (this.igeoTransformation_1 is NADCONTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 5;
                    }
                    else if (this.igeoTransformation_1 is HARNTransformationClass)
                    {
                        this.cboHCSTransformMethod.SelectedIndex = 6;
                    }
                }
                else if (this.igeoTransformation_1 is ILongitudeRotationTransformation)
                {
                    this.cboHCSTransformMethod.SelectedIndex = 7;
                }
                this.cboHCSTransformMethod.Enabled = false;
            }
            else
            {
                this.cboHCSTransformMethod.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGaphicTransformation));
            this.label1 = new Label();
            this.txtName = new TextBox();
            this.txtSourGCS = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.cboTargetGCS = new ComboBox();
            this.groupBox1 = new GroupBox();
            this.paramlistView = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.label5 = new Label();
            this.cboHCSTransformMethod = new ComboBox();
            this.label4 = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            this.txtName.Location = new System.Drawing.Point(0x5f, 14);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x91, 0x15);
            this.txtName.TabIndex = 1;
            this.txtSourGCS.Enabled = false;
            this.txtSourGCS.Location = new System.Drawing.Point(0x5f, 0x29);
            this.txtSourGCS.Name = "txtSourGCS";
            this.txtSourGCS.Size = new Size(0x91, 0x15);
            this.txtSourGCS.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 0x2c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "源数据框架";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "目的数据框架";
            this.cboTargetGCS.FormattingEnabled = true;
            this.cboTargetGCS.Items.AddRange(new object[] { "北京54", "西安80", "WGS_84" });
            this.cboTargetGCS.Location = new System.Drawing.Point(0x5f, 0x4a);
            this.cboTargetGCS.Name = "cboTargetGCS";
            this.cboTargetGCS.Size = new Size(0x9b, 20);
            this.cboTargetGCS.TabIndex = 5;
            this.cboTargetGCS.SelectedIndexChanged += new EventHandler(this.cboTargetGCS_SelectedIndexChanged);
            this.groupBox1.Controls.Add(this.paramlistView);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboHCSTransformMethod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(2, 0x6a);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x115, 0xd8);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "转换方法";
            this.paramlistView.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.paramlistView.ComboBoxBgColor = Color.LightBlue;
            this.paramlistView.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.EditBgColor = Color.LightBlue;
            this.paramlistView.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.paramlistView.FullRowSelect = true;
            this.paramlistView.GridLines = true;
            this.paramlistView.Location = new System.Drawing.Point(10, 0x4d);
            this.paramlistView.LockRowCount = 0;
            this.paramlistView.Name = "paramlistView";
            this.paramlistView.Size = new Size(0xf6, 0x7c);
            this.paramlistView.TabIndex = 9;
            this.paramlistView.UseCompatibleStateImageBehavior = false;
            this.paramlistView.View = View.Details;
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.ReadOnly;
            this.lvcolumnHeader_0.Text = "名称";
            this.lvcolumnHeader_0.Width = 0x6c;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_1.Text = "值";
            this.lvcolumnHeader_1.Width = 0x7a;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0x11, 0x37);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "参数";
            this.cboHCSTransformMethod.FormattingEnabled = true;
            this.cboHCSTransformMethod.Location = new System.Drawing.Point(0x44, 0x19);
            this.cboHCSTransformMethod.Name = "cboHCSTransformMethod";
            this.cboHCSTransformMethod.Size = new Size(0x9b, 20);
            this.cboHCSTransformMethod.TabIndex = 7;
            this.cboHCSTransformMethod.SelectedIndexChanged += new EventHandler(this.cboHCSTransformMethod_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x11, 0x1c);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "名称";
            this.btnOK.Location = new System.Drawing.Point(0x86, 0x148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x33, 0x17);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(0xc9, 0x149);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x34, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(300, 360);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.cboTargetGCS);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtSourGCS);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtName);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGaphicTransformation";
            this.Text = "新建地理坐标转换";
            base.Load += new EventHandler(this.frmGaphicTransformation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(object sender, ValueChangedEventArgs e)
        {
            IGeoTransformation geoTransformation;
            double[] numArray = new double[7];
            if (this.igeoTransformation_1 != null)
            {
                geoTransformation = this.igeoTransformation_1;
            }
            else
            {
                geoTransformation = (this.cboHCSTransformMethod.SelectedItem as Class1).GeoTransformation;
            }
            if (geoTransformation is ICoordinateFrameTransformation)
            {
                (geoTransformation as ICoordinateFrameTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as ICoordinateFrameTransformation).PutParameters(numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], numArray[5], numArray[6]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IMolodenskyTransformation)
            {
                (geoTransformation as IMolodenskyTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IMolodenskyTransformation).PutParameters(numArray[0], numArray[1], numArray[2]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IGeocentricTranslation)
            {
                (geoTransformation as IGeocentricTranslation).GetParameters(out numArray[0], out numArray[1], out numArray[2]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IGeocentricTranslation).PutParameters(numArray[0], numArray[1], numArray[2]);
                }
                catch
                {
                }
            }
            else if (geoTransformation is IPositionVectorTransformation)
            {
                (geoTransformation as IPositionVectorTransformation).GetParameters(out numArray[0], out numArray[1], out numArray[2], out numArray[3], out numArray[4], out numArray[5], out numArray[6]);
                try
                {
                    numArray[e.Row] = int.Parse(e.NewValue.ToString());
                    (geoTransformation as IPositionVectorTransformation).PutParameters(numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], numArray[5], numArray[6]);
                }
                catch
                {
                }
            }
        }

        public IGeoTransformation GeoTransformations
        {
            get
            {
                return this.igeoTransformation_1;
            }
            set
            {
                this.igeoTransformation_1 = value;
                this.igeoTransformation_1.GetSpatialReferences(out this.ispatialReference_0, out this.ispatialReference_1);
            }
        }

        public ISpatialReference SourceSpatialReference
        {
            set
            {
                this.ispatialReference_0 = value;
            }
        }

        public ISpatialReference TargetSpatialReference
        {
            set
            {
                this.ispatialReference_1 = value;
            }
        }

        private class Class1
        {
            private IGeoTransformation igeoTransformation_0;
            private string string_0;

            internal Class1(IGeoTransformation igeoTransformation_1)
            {
                this.igeoTransformation_0 = null;
                this.string_0 = "";
                this.igeoTransformation_0 = igeoTransformation_1;
            }

            internal Class1(IGeoTransformation igeoTransformation_1, string string_1)
            {
                this.igeoTransformation_0 = null;
                this.string_0 = "";
                this.igeoTransformation_0 = igeoTransformation_1;
                this.string_0 = string_1;
            }

            public override string ToString()
            {
                return this.string_0;
            }

            internal IGeoTransformation GeoTransformation
            {
                get
                {
                    return this.igeoTransformation_0;
                }
            }
        }
    }
}

