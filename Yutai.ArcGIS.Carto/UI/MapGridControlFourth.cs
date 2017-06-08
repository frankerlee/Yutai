using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridControlFourth : UserControl
    {
        private bool bool_0 = false;
        private Button button1;
        private Button button2;
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IMapGrid imapGrid_0 = null;

        public MapGridControlFourth()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.button1 = new Button();
            this.checkEdit1 = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.button2 = new Button();
            this.checkEdit2 = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.checkEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.checkEdit2.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checkEdit1);
            this.groupBox1.Location = new Point(8, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe0, 0x58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "方里网边框";
            this.button1.Location = new Point(0x38, 0x30);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x70, 0x20);
            this.button1.TabIndex = 1;
            this.checkEdit1.EditValue = false;
            this.checkEdit1.Location = new Point(0x18, 0x18);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "在格网和轴标注间放置边框";
            this.checkEdit1.Size = new Size(0xb0, 0x13);
            this.checkEdit1.TabIndex = 0;
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Location = new Point(8, 0x70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe0, 0x58);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图廓线";
            this.button2.Location = new Point(0x38, 0x30);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x70, 0x20);
            this.button2.TabIndex = 1;
            this.checkEdit2.EditValue = false;
            this.checkEdit2.Location = new Point(0x18, 0x18);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "在格网外放置边框";
            this.checkEdit2.Size = new Size(0xb0, 0x13);
            this.checkEdit2.TabIndex = 0;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridControlFourth";
            base.Size = new Size(0x108, 0xd8);
            base.VisibleChanged += new EventHandler(this.MapGridControlFourth_VisibleChanged);
            base.Load += new EventHandler(this.MapGridControlFourth_Load);
            this.groupBox1.ResumeLayout(false);
            this.checkEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.checkEdit2.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MapGridControlFourth_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void MapGridControlFourth_VisibleChanged(object sender, EventArgs e)
        {
            if (!base.Visible)
            {
            }
        }

        private void method_0()
        {
            if ((this.imapGrid_0 != null) && !(this.imapGrid_0 is IMeasuredGrid))
            {
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
            }
        }
    }
}

