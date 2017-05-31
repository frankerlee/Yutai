namespace Yutai.Catalog
{
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmSetGeometryType : Form
    {
        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private IContainer icontainer_0 = null;
        private Label label1;

        public frmSetGeometryType()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex != -1)
            {
                if (this.comboBox1.SelectedIndex == 0)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPoint;
                }
                else if (this.comboBox1.SelectedIndex == 1)
                {
                    this.ShapeType = esriGeometryType.esriGeometryMultipoint;
                }
                else if (this.comboBox1.SelectedIndex == 2)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPolyline;
                }
                else if (this.comboBox1.SelectedIndex == 3)
                {
                    this.ShapeType = esriGeometryType.esriGeometryPolygon;
                }
                base.DialogResult = DialogResult.OK;
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

        private void frmSetGeometryType_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.button1 = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "要素类型";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "点要素", "多点要素", "线要素", "面要素" });
            this.comboBox1.Location = new System.Drawing.Point(0x49, 0x1c);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xc7, 20);
            this.comboBox1.TabIndex = 1;
            this.button1.Location = new System.Drawing.Point(0x6f, 0x4b);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(0xc5, 0x4b);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0x83);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSetGeometryType";
            this.Text = "设置要素类型";
            base.Load += new EventHandler(this.frmSetGeometryType_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public esriGeometryType ShapeType { get; set; }
    }
}

