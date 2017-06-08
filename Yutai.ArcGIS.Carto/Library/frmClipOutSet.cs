using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.Library
{
    public class frmClipOutSet : Form
    {
        private Button btnCancle;
        private Button btnOK;
        private Button btnOutSet;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private int int_0 = 0;
        private int int_1 = 0;
        private IWorkspace iworkspace_0 = null;
        private Label label1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private TextBox textBox1;

        public frmClipOutSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.int_1 = 0;
                Directory.CreateDirectory(this.textBox1.Text);
                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                this.iworkspace_0 = factory.OpenFromFile(this.textBox1.Text, 0);
            }
            else if (this.radioButton2.Checked)
            {
                this.int_1 = 1;
            }
            else if (this.radioButton3.Checked)
            {
                this.int_1 = 2;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnOutSet_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog;
            if (this.radioButton1.Checked)
            {
                dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog.SelectedPath;
                }
            }
            else if (this.radioButton2.Checked)
            {
                dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog.SelectedPath;
                }
            }
            else if (this.radioButton3.Checked)
            {
                SaveFileDialog dialog2 = new SaveFileDialog {
                    Filter = "*.vct|*.vct"
                };
                if (dialog2.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = dialog2.FileName;
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
            this.groupBox1 = new GroupBox();
            this.btnOutSet = new Button();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.radioButton3 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出类型";
            this.groupBox1.Controls.Add(this.btnOutSet);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(290, 0x7e);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出设置";
            this.btnOutSet.Location = new Point(0xf7, 0x4a);
            this.btnOutSet.Name = "btnOutSet";
            this.btnOutSet.Size = new Size(0x25, 0x17);
            this.btnOutSet.TabIndex = 6;
            this.btnOutSet.Text = "...";
            this.btnOutSet.UseVisualStyleBackColor = true;
            this.btnOutSet.Click += new EventHandler(this.btnOutSet_Click);
            this.textBox1.Location = new Point(0x4c, 0x4a);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0xa5, 0x15);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x11, 0x4d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "输出位置";
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(0xbd, 0x2a);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0x29, 0x10);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "VCT";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x66, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x41, 0x10);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "mapinfo";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(0x13, 0x2a);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x4d, 0x10);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "shapefile";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0x7c, 0x9a);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.Location = new Point(0xde, 0x9a);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x4b, 0x17);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x135, 0xc2);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmClipOutSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "裁剪输出";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox1.Tag = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.textBox1.Text.Length > 0;
        }

        public string OutPath
        {
            get
            {
                return this.textBox1.Text;
            }
        }

        public IWorkspace OutWorspace
        {
            get
            {
                return this.iworkspace_0;
            }
        }

        public int Type
        {
            get
            {
                return this.int_1;
            }
        }
    }
}

