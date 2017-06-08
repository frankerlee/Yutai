using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    public class frmImportExcelSet : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private Button btnOpen;
        private ComboBox cboX;
        private ComboBox cboY;
        private ComboBox comboBox1;
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;

        public frmImportExcelSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataRowCollection excelRows = ExcelTools.GetExcelRows(this.textBox1.Text, this.comboBox1.Text);
            IPointCollection points = new PolygonClass();
            foreach (DataRow row in excelRows)
            {
                try
                {
                    double num = double.Parse(row[this.cboX.Text].ToString());
                    double num2 = double.Parse(row[this.cboY.Text].ToString());
                    IPoint inPoint = new PointClass {
                        X = num,
                        Y = num2
                    };
                    object before = Missing.Value;
                    points.AddPoint(inPoint, ref before, ref before);
                }
                catch
                {
                }
            }
            (points as IPolygon).Close();
            this.igeometry_0 = points as IGeometry;
            base.DialogResult = DialogResult.OK;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.xls|*.xls"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.comboBox1.Items.Clear();
                this.textBox1.Text = dialog.FileName;
                List<string> excelFileSheet = ExcelTools.GetExcelFileSheet(dialog.FileName);
                if (excelFileSheet != null)
                {
                    for (int i = 0; i < excelFileSheet.Count; i++)
                    {
                        this.comboBox1.Items.Add(excelFileSheet[i]);
                    }
                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                    }
                }
            }
        }

        private void cboX_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) && (this.cboY.SelectedIndex >= 0);
        }

        private void cboY_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) && (this.cboY.SelectedIndex >= 0);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = ((this.comboBox1.SelectedIndex >= 0) && (this.cboX.SelectedIndex >= 0)) && (this.cboY.SelectedIndex >= 0);
            if (this.comboBox1.SelectedIndex >= 0)
            {
                List<string> excelColumns = ExcelTools.GetExcelColumns(this.textBox1.Text, this.comboBox1.Text);
                for (int i = 0; i < excelColumns.Count; i++)
                {
                    this.cboX.Items.Add(excelColumns[i]);
                    this.cboY.Items.Add(excelColumns[i]);
                }
                if (this.cboX.Items.Count > 0)
                {
                    this.cboX.SelectedIndex = 0;
                    this.cboY.SelectedIndex = 0;
                }
                if (this.cboX.Items.Count > 1)
                {
                    this.cboY.SelectedIndex = 1;
                }
            }
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
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.btnOpen = new Button();
            this.textBox1 = new TextBox();
            this.cboX = new ComboBox();
            this.label3 = new Label();
            this.cboY = new ComboBox();
            this.label4 = new Label();
            this.btnCancel = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0x24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "属性页";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0x53, 0x21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xe7, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据文件";
            this.btnOpen.Location = new System.Drawing.Point(320, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(30, 0x17);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.textBox1.Location = new System.Drawing.Point(0x53, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0xe7, 0x15);
            this.textBox1.TabIndex = 2;
            this.cboX.FormattingEnabled = true;
            this.cboX.Location = new System.Drawing.Point(0x53, 0x42);
            this.cboX.Name = "cboX";
            this.cboX.Size = new Size(0xe7, 20);
            this.cboX.TabIndex = 6;
            this.cboX.SelectedIndexChanged += new EventHandler(this.cboX_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 0x45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "纵坐标字段";
            this.cboY.FormattingEnabled = true;
            this.cboY.Location = new System.Drawing.Point(0x53, 0x5e);
            this.cboY.Name = "cboY";
            this.cboY.Size = new Size(0xe7, 20);
            this.cboY.TabIndex = 8;
            this.cboY.SelectedIndexChanged += new EventHandler(this.cboY_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 0x61);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "横坐标字段";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xe5, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(0x86, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x13;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x16a, 0x98);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboY);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboX);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmImportExcelSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "导入XY坐标";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public IGeometry Geometry
        {
            get
            {
                return this.igeometry_0;
            }
            set
            {
                this.igeometry_0 = value;
            }
        }
    }
}

