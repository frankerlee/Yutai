using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.Library
{
    public class frmCircleInput : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private IContainer icontainer_0 = null;
        private IGeometry igeometry_0 = null;
        private IMap imap_0 = null;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtRadio;
        private TextBox txtXCoor;
        private TextBox txtYCoor;

        public frmCircleInput()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double num = double.Parse(this.txtXCoor.Text);
            double num2 = double.Parse(this.txtYCoor.Text);
            double radius = double.Parse(this.txtRadio.Text);
            if (this.imap_0.MapUnits != esriUnits.esriKilometers)
            {
                num *= 1000.0;
                num2 *= 1000.0;
                radius *= 1000.0;
            }
            IPoint centerPoint = new PointClass {
                X = num,
                Y = num2
            };
            ICircularArc arc = new CircularArcClass();
            (arc as IConstructCircularArc).ConstructCircle(centerPoint, radius, false);
            ISegmentCollection segments = new PolygonClass();
            object before = Missing.Value;
            segments.AddSegment(arc as ISegment, ref before, ref before);
            this.igeometry_0 = segments as IGeometry;
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCircleInput_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtYCoor = new TextBox();
            this.txtXCoor = new TextBox();
            this.label5 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtRadio = new TextBox();
            this.label9 = new Label();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xaf, 0x81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 0x18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.Location = new System.Drawing.Point(80, 0x81);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0x17;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0xd7, 0x39);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 0x16;
            this.label4.Text = "公里";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0xd4, 20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0x15;
            this.label3.Text = "公里";
            this.txtYCoor.Location = new System.Drawing.Point(0x6a, 0x36);
            this.txtYCoor.Name = "txtYCoor";
            this.txtYCoor.Size = new Size(100, 0x15);
            this.txtYCoor.TabIndex = 20;
            this.txtXCoor.Location = new System.Drawing.Point(0x6a, 0x11);
            this.txtXCoor.Name = "txtXCoor";
            this.txtXCoor.Size = new Size(100, 0x15);
            this.txtXCoor.TabIndex = 0x13;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0xd7, 0x5b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x1b;
            this.label5.Text = "公里";
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x41, 12);
            this.label7.TabIndex = 0x16;
            this.label7.Text = "圆心纵坐标";
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 20);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 12);
            this.label8.TabIndex = 0x15;
            this.label8.Text = "圆心横坐标";
            this.txtRadio.Location = new System.Drawing.Point(0x6a, 0x59);
            this.txtRadio.Name = "txtRadio";
            this.txtRadio.Size = new Size(100, 0x15);
            this.txtRadio.TabIndex = 0x1c;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 0x5c);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 0x1b;
            this.label9.Text = "半径";
            base.ClientSize = new Size(0x100, 0xb7);
            base.Controls.Add(this.txtRadio);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.txtYCoor);
            base.Controls.Add(this.txtXCoor);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label5);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCircleInput";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "输入圆心,半径";
            base.Load += new EventHandler(this.frmCircleInput_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void method_0(object sender, EventArgs e)
        {
            double.Parse(this.txtXCoor.Text);
            double.Parse(this.txtYCoor.Text);
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

        public IMap Map
        {
            set
            {
                this.imap_0 = value;
            }
        }
    }
}

