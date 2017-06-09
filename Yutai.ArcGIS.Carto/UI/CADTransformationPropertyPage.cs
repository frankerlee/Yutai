using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public class CADTransformationPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnOpenWFName;
        private CheckEdit checkEdit1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ICadTransformations icadTransformations_0 = null;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private RadioGroup radioGroup1;
        private TextEdit txtFromXCoord1;
        private TextEdit txtFromXCoord2;
        private TextEdit txtFromYCoord1;
        private TextEdit txtFromYCoord2;
        private TextEdit txtRotate;
        private TextEdit txtScale;
        private TextEdit txtToXCoord1;
        private TextEdit txtToXCoord2;
        private TextEdit txtToYCoord1;
        private TextEdit txtToYCoord2;
        private TextEdit txtWordFileName;

        public CADTransformationPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                double angle = 0.0;
                double scale = 0.0;
                try
                {
                    angle = double.Parse(this.txtRotate.Text);
                }
                catch
                {
                }
                try
                {
                    scale = double.Parse(this.txtScale.Text);
                }
                catch
                {
                }
                WKSPoint point = new WKSPoint();
                try
                {
                    point.X = double.Parse(this.txtFromXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point.Y = double.Parse(this.txtFromYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point2 = new WKSPoint();
                try
                {
                    point2.X = double.Parse(this.txtToXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point2.Y = double.Parse(this.txtToYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point3 = new WKSPoint();
                try
                {
                    point3.X = double.Parse(this.txtFromXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point3.Y = double.Parse(this.txtFromYCoord1.Text);
                }
                catch
                {
                }
                WKSPoint point4 = new WKSPoint();
                try
                {
                    point4.X = double.Parse(this.txtToXCoord1.Text);
                }
                catch
                {
                }
                try
                {
                    point4.Y = double.Parse(this.txtToYCoord1.Text);
                }
                catch
                {
                }
                this.icadTransformations_0.EnableTransformations = this.checkEdit1.Checked;
                switch (this.radioGroup1.SelectedIndex)
                {
                    case 0:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByWorldFile;
                        this.icadTransformations_0.WorldFileName = this.txtWordFileName.Text;
                        break;

                    case 1:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByPoints;
                        this.icadTransformations_0.SetFromToTransform(ref point, ref point3, ref point2, ref point4);
                        break;

                    case 2:
                        this.icadTransformations_0.TransformMode = esriCadTransform.esriCadTransformByRst;
                        this.icadTransformations_0.SetTransformation(ref point, ref point3, angle, scale);
                        break;
                }
            }
            return true;
        }

        private void btnOpenWFName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "CAD World Files(*.wld)|*.wld",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtWordFileName.Text = dialog.FileName;
                this.bool_0 = true;
            }
        }

        private void CADTransformationPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_1 = true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkEdit1.Checked)
            {
                this.groupBox1.Visible = true;
                this.panel1.Visible = true;
                this.panel2.Visible = true;
            }
            else
            {
                this.groupBox1.Visible = false;
                this.panel1.Visible = false;
                this.panel2.Visible = false;
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.checkEdit1 = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.panel1 = new Panel();
            this.btnOpenWFName = new SimpleButton();
            this.txtWordFileName = new TextEdit();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtFromXCoord1 = new TextEdit();
            this.txtToXCoord1 = new TextEdit();
            this.label5 = new Label();
            this.txtFromYCoord1 = new TextEdit();
            this.label6 = new Label();
            this.txtToYCoord1 = new TextEdit();
            this.label7 = new Label();
            this.panel3 = new Panel();
            this.txtToYCoord2 = new TextEdit();
            this.label8 = new Label();
            this.txtFromYCoord2 = new TextEdit();
            this.label9 = new Label();
            this.txtToXCoord2 = new TextEdit();
            this.label10 = new Label();
            this.txtFromXCoord2 = new TextEdit();
            this.label11 = new Label();
            this.panel4 = new Panel();
            this.txtScale = new TextEdit();
            this.label15 = new Label();
            this.label14 = new Label();
            this.txtRotate = new TextEdit();
            this.checkEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.txtWordFileName.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.txtFromXCoord1.Properties.BeginInit();
            this.txtToXCoord1.Properties.BeginInit();
            this.txtFromYCoord1.Properties.BeginInit();
            this.txtToYCoord1.Properties.BeginInit();
            this.panel3.SuspendLayout();
            this.txtToYCoord2.Properties.BeginInit();
            this.txtFromYCoord2.Properties.BeginInit();
            this.txtToXCoord2.Properties.BeginInit();
            this.txtFromXCoord2.Properties.BeginInit();
            this.panel4.SuspendLayout();
            this.txtScale.Properties.BeginInit();
            this.txtRotate.Properties.BeginInit();
            base.SuspendLayout();
            this.checkEdit1.Location = new Point(8, 8);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "启用变换";
            this.checkEdit1.Size = new Size(0x58, 0x13);
            this.checkEdit1.TabIndex = 0;
            this.checkEdit1.CheckedChanged += new EventHandler(this.checkEdit1_CheckedChanged);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(0x10, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x80, 120);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变换方式";
            this.radioGroup1.Location = new Point(8, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "Word File文件"), new RadioGroupItem(null, "坐标"), new RadioGroupItem(null, "旋转,缩放,平移") });
            this.radioGroup1.Size = new Size(0x70, 0x58);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.btnOpenWFName);
            this.panel1.Controls.Add(this.txtWordFileName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(8, 0x20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(360, 40);
            this.panel1.TabIndex = 5;
            this.btnOpenWFName.Location = new Point(0x130, 8);
            this.btnOpenWFName.Name = "btnOpenWFName";
            this.btnOpenWFName.Size = new Size(0x30, 0x18);
            this.btnOpenWFName.TabIndex = 6;
            this.btnOpenWFName.Text = "浏览...";
            this.btnOpenWFName.Click += new EventHandler(this.btnOpenWFName_Click);
            this.txtWordFileName.EditValue = "";
            this.txtWordFileName.Location = new Point(120, 8);
            this.txtWordFileName.Name = "txtWordFileName";
            this.txtWordFileName.Properties.ReadOnly = true;
            this.txtWordFileName.Size = new Size(0xb0, 0x17);
            this.txtWordFileName.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x67, 0x11);
            this.label1.TabIndex = 4;
            this.label1.Text = "World File文件名";
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.txtToYCoord1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtFromYCoord1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtToXCoord1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtFromXCoord1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new Point(0x90, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x150, 0xc0);
            this.panel2.TabIndex = 6;
            this.panel2.Visible = false;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 0;
            this.label2.Text = "源";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x68, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 1;
            this.label3.Text = "目的";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 40);
            this.label4.Name = "label4";
            this.label4.Size = new Size(11, 0x11);
            this.label4.TabIndex = 5;
            this.label4.Text = "x";
            this.txtFromXCoord1.EditValue = "";
            this.txtFromXCoord1.Location = new Point(0x18, 40);
            this.txtFromXCoord1.Name = "txtFromXCoord1";
            this.txtFromXCoord1.Properties.ReadOnly = true;
            this.txtFromXCoord1.Size = new Size(0x48, 0x17);
            this.txtFromXCoord1.TabIndex = 6;
            this.txtFromXCoord1.EditValueChanged += new EventHandler(this.txtFromXCoord1_EditValueChanged);
            this.txtToXCoord1.EditValue = "";
            this.txtToXCoord1.Location = new Point(120, 40);
            this.txtToXCoord1.Name = "txtToXCoord1";
            this.txtToXCoord1.Properties.ReadOnly = true;
            this.txtToXCoord1.Size = new Size(0x48, 0x17);
            this.txtToXCoord1.TabIndex = 8;
            this.txtToXCoord1.EditValueChanged += new EventHandler(this.txtToXCoord1_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x68, 40);
            this.label5.Name = "label5";
            this.label5.Size = new Size(11, 0x11);
            this.label5.TabIndex = 7;
            this.label5.Text = "x";
            this.txtFromYCoord1.EditValue = "";
            this.txtFromYCoord1.Location = new Point(0x18, 0x48);
            this.txtFromYCoord1.Name = "txtFromYCoord1";
            this.txtFromYCoord1.Properties.ReadOnly = true;
            this.txtFromYCoord1.Size = new Size(0x48, 0x17);
            this.txtFromYCoord1.TabIndex = 10;
            this.txtFromYCoord1.EditValueChanged += new EventHandler(this.txtFromYCoord1_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(8, 0x48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(11, 0x11);
            this.label6.TabIndex = 9;
            this.label6.Text = "y";
            this.txtToYCoord1.EditValue = "";
            this.txtToYCoord1.Location = new Point(120, 0x48);
            this.txtToYCoord1.Name = "txtToYCoord1";
            this.txtToYCoord1.Properties.ReadOnly = true;
            this.txtToYCoord1.Size = new Size(0x48, 0x17);
            this.txtToYCoord1.TabIndex = 12;
            this.txtToYCoord1.EditValueChanged += new EventHandler(this.txtToYCoord1_EditValueChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x68, 0x48);
            this.label7.Name = "label7";
            this.label7.Size = new Size(11, 0x11);
            this.label7.TabIndex = 11;
            this.label7.Text = "y";
            this.panel3.Controls.Add(this.txtToYCoord2);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txtFromYCoord2);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtToXCoord2);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtFromXCoord2);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new Point(0, 0x68);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(200, 80);
            this.panel3.TabIndex = 0x19;
            this.txtToYCoord2.EditValue = "";
            this.txtToYCoord2.Location = new Point(120, 40);
            this.txtToYCoord2.Name = "txtToYCoord2";
            this.txtToYCoord2.Properties.ReadOnly = true;
            this.txtToYCoord2.Size = new Size(0x48, 0x17);
            this.txtToYCoord2.TabIndex = 0x1c;
            this.txtToYCoord2.EditValueChanged += new EventHandler(this.txtToYCoord2_EditValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x68, 40);
            this.label8.Name = "label8";
            this.label8.Size = new Size(11, 0x11);
            this.label8.TabIndex = 0x1b;
            this.label8.Text = "y";
            this.txtFromYCoord2.EditValue = "";
            this.txtFromYCoord2.Location = new Point(0x18, 40);
            this.txtFromYCoord2.Name = "txtFromYCoord2";
            this.txtFromYCoord2.Properties.ReadOnly = true;
            this.txtFromYCoord2.Size = new Size(0x48, 0x17);
            this.txtFromYCoord2.TabIndex = 0x1a;
            this.txtFromYCoord2.EditValueChanged += new EventHandler(this.txtFromYCoord2_EditValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 40);
            this.label9.Name = "label9";
            this.label9.Size = new Size(11, 0x11);
            this.label9.TabIndex = 0x19;
            this.label9.Text = "y";
            this.txtToXCoord2.EditValue = "";
            this.txtToXCoord2.Location = new Point(120, 8);
            this.txtToXCoord2.Name = "txtToXCoord2";
            this.txtToXCoord2.Properties.ReadOnly = true;
            this.txtToXCoord2.Size = new Size(0x48, 0x17);
            this.txtToXCoord2.TabIndex = 0x18;
            this.txtToXCoord2.EditValueChanged += new EventHandler(this.txtToXCoord2_EditValueChanged);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x68, 8);
            this.label10.Name = "label10";
            this.label10.Size = new Size(11, 0x11);
            this.label10.TabIndex = 0x17;
            this.label10.Text = "x";
            this.txtFromXCoord2.EditValue = "";
            this.txtFromXCoord2.Location = new Point(0x18, 8);
            this.txtFromXCoord2.Name = "txtFromXCoord2";
            this.txtFromXCoord2.Properties.ReadOnly = true;
            this.txtFromXCoord2.Size = new Size(0x48, 0x17);
            this.txtFromXCoord2.TabIndex = 0x16;
            this.txtFromXCoord2.EditValueChanged += new EventHandler(this.txtFromXCoord2_EditValueChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(8, 8);
            this.label11.Name = "label11";
            this.label11.Size = new Size(11, 0x11);
            this.label11.TabIndex = 0x15;
            this.label11.Text = "x";
            this.panel4.Controls.Add(this.txtScale);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.txtRotate);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Location = new Point(200, 0x20);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x88, 80);
            this.panel4.TabIndex = 0x1a;
            this.txtScale.EditValue = "";
            this.txtScale.Location = new Point(0x38, 40);
            this.txtScale.Name = "txtScale";
            this.txtScale.Properties.ReadOnly = true;
            this.txtScale.Size = new Size(0x48, 0x17);
            this.txtScale.TabIndex = 0x1c;
            this.txtScale.EditValueChanged += new EventHandler(this.txtScale_EditValueChanged);
            this.label15.AutoSize = true;
            this.label15.Location = new Point(8, 8);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x2a, 0x11);
            this.label15.TabIndex = 0x19;
            this.label15.Text = "旋转角";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(8, 40);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x1d, 0x11);
            this.label14.TabIndex = 0x1b;
            this.label14.Text = "缩放";
            this.txtRotate.EditValue = "";
            this.txtRotate.Location = new Point(0x38, 8);
            this.txtRotate.Name = "txtRotate";
            this.txtRotate.Properties.ReadOnly = true;
            this.txtRotate.Size = new Size(0x48, 0x17);
            this.txtRotate.TabIndex = 0x1a;
            this.txtRotate.EditValueChanged += new EventHandler(this.txtRotate_EditValueChanged);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.checkEdit1);
            base.Name = "CADTransformationPropertyPage";
            base.Size = new Size(0x1e8, 0x120);
            base.Load += new EventHandler(this.CADTransformationPropertyPage_Load);
            this.checkEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.txtWordFileName.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.txtFromXCoord1.Properties.EndInit();
            this.txtToXCoord1.Properties.EndInit();
            this.txtFromYCoord1.Properties.EndInit();
            this.txtToYCoord1.Properties.EndInit();
            this.panel3.ResumeLayout(false);
            this.txtToYCoord2.Properties.EndInit();
            this.txtFromYCoord2.Properties.EndInit();
            this.txtToXCoord2.Properties.EndInit();
            this.txtFromXCoord2.Properties.EndInit();
            this.panel4.ResumeLayout(false);
            this.txtScale.Properties.EndInit();
            this.txtRotate.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            WKSPoint point;
            WKSPoint point3;
            this.checkEdit1.Checked = this.icadTransformations_0.EnableTransformations;
            if (this.checkEdit1.Checked)
            {
                this.groupBox1.Visible = true;
                this.panel1.Visible = true;
                this.panel2.Visible = true;
            }
            else
            {
                this.groupBox1.Visible = false;
                this.panel1.Visible = false;
                this.panel2.Visible = false;
            }
            this.radioGroup1.SelectedIndex = (int) this.icadTransformations_0.TransformMode;
            switch (this.icadTransformations_0.TransformMode)
            {
                case esriCadTransform.esriCadTransformByWorldFile:
                    this.txtWordFileName.Text = this.icadTransformations_0.WorldFileName;
                    break;

                case esriCadTransform.esriCadTransformByPoints:
                    WKSPoint point2;
                    WKSPoint point4;
                    this.icadTransformations_0.GetFromToTransform(out point, out point2, out point3, out point4);
                    this.txtFromXCoord1.Text = point.X.ToString();
                    this.txtFromYCoord1.Text = point.Y.ToString();
                    this.txtFromXCoord2.Text = point2.X.ToString();
                    this.txtFromYCoord2.Text = point2.Y.ToString();
                    this.txtToXCoord1.Text = point3.X.ToString();
                    this.txtToYCoord1.Text = point3.Y.ToString();
                    this.txtToXCoord2.Text = point4.X.ToString();
                    this.txtToYCoord2.Text = point4.Y.ToString();
                    break;

                case esriCadTransform.esriCadTransformByRst:
                    double num;
                    double num2;
                    this.icadTransformations_0.GetTransformation(out point, out point3, out num, out num2);
                    this.txtFromXCoord1.Text = point.X.ToString();
                    this.txtFromYCoord1.Text = point.Y.ToString();
                    this.txtToXCoord1.Text = point3.X.ToString();
                    this.txtToYCoord1.Text = point3.Y.ToString();
                    this.txtScale.Text = num2.ToString();
                    this.txtRotate.Text = num.ToString();
                    break;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.panel1.Visible = true;
                this.panel2.Visible = false;
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.panel3.Visible = true;
                this.panel4.Visible = false;
            }
            else if (this.radioGroup1.SelectedIndex == 2)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.panel3.Visible = false;
                this.panel4.Visible = true;
            }
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private void txtFromXCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromXCoord1.Text);
                this.txtFromXCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromXCoord1.ForeColor = Color.Red;
            }
        }

        private void txtFromXCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromXCoord2.Text);
                this.txtFromXCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromXCoord2.ForeColor = Color.Red;
            }
        }

        private void txtFromYCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromYCoord1.Text);
                this.txtFromYCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromYCoord1.ForeColor = Color.Red;
            }
        }

        private void txtFromYCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtFromYCoord2.Text);
                this.txtFromYCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtFromYCoord2.ForeColor = Color.Red;
            }
        }

        private void txtRotate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtRotate.Text);
                this.txtRotate.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtRotate.ForeColor = Color.Red;
            }
        }

        private void txtScale_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtScale.Text);
                this.txtScale.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtScale.ForeColor = Color.Red;
            }
        }

        private void txtToXCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToXCoord1.Text);
                this.txtToXCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToXCoord1.ForeColor = Color.Red;
            }
        }

        private void txtToXCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToXCoord2.Text);
                this.txtToXCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToXCoord2.ForeColor = Color.Red;
            }
        }

        private void txtToYCoord1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToYCoord1.Text);
                this.txtToYCoord1.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToYCoord1.ForeColor = Color.Red;
            }
        }

        private void txtToYCoord2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(this.txtToYCoord2.Text);
                this.txtToYCoord2.ForeColor = SystemColors.WindowText;
                if (this.bool_1)
                {
                    this.bool_0 = true;
                }
            }
            catch
            {
                this.txtToYCoord2.ForeColor = Color.Red;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.icadTransformations_0 = value as ICadTransformations;
            }
        }
    }
}

