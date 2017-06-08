using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridControlSecond : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IMapFrame imapFrame_0 = null;
        private IMapGrid imapGrid_0 = null;
        private Label label1;
        private Label label2;
        private Label lblXUnit;
        private Label lblYUnit;
        private RadioGroup radioGroup1;
        private TextEdit txtXSpace;
        private TextEdit txtYSpace;

        public MapGridControlSecond()
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

        public void Do()
        {
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.lblYUnit = new Label();
            this.lblXUnit = new Label();
            this.txtYSpace = new TextEdit();
            this.txtXSpace = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtYSpace.Properties.BeginInit();
            this.txtXSpace.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 0x68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "外观";
            this.radioGroup1.Location = new Point(0x10, 0x18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "只显示标注"), new RadioGroupItem(null, "刻度标志和标记"), new RadioGroupItem(null, "经纬网格和标注") });
            this.radioGroup1.Size = new Size(160, 0x48);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.lblYUnit);
            this.groupBox2.Controls.Add(this.lblXUnit);
            this.groupBox2.Controls.Add(this.txtYSpace);
            this.groupBox2.Controls.Add(this.txtXSpace);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new Point(8, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(240, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "间隔";
            this.lblYUnit.AutoSize = true;
            this.lblYUnit.Location = new Point(0x98, 0x30);
            this.lblYUnit.Name = "lblYUnit";
            this.lblYUnit.Size = new Size(0, 0x11);
            this.lblYUnit.TabIndex = 5;
            this.lblXUnit.AutoSize = true;
            this.lblXUnit.Location = new Point(0x98, 0x18);
            this.lblXUnit.Name = "lblXUnit";
            this.lblXUnit.Size = new Size(0, 0x11);
            this.lblXUnit.TabIndex = 4;
            this.txtYSpace.EditValue = "";
            this.txtYSpace.Location = new Point(40, 0x30);
            this.txtYSpace.Name = "txtYSpace";
            this.txtYSpace.Size = new Size(0x68, 0x15);
            this.txtYSpace.TabIndex = 3;
            this.txtYSpace.EditValueChanged += new EventHandler(this.txtYSpace_EditValueChanged);
            this.txtXSpace.EditValue = "";
            this.txtXSpace.Location = new Point(40, 0x18);
            this.txtXSpace.Name = "txtXSpace";
            this.txtXSpace.Size = new Size(0x68, 0x15);
            this.txtXSpace.TabIndex = 2;
            this.txtXSpace.EditValueChanged += new EventHandler(this.txtXSpace_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y轴:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x1d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "X轴:";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "MapGridControlSecond";
            base.Size = new Size(0x100, 0xd8);
            base.VisibleChanged += new EventHandler(this.MapGridControlSecond_VisibleChanged);
            base.Load += new EventHandler(this.MapGridControlSecond_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtYSpace.Properties.EndInit();
            this.txtXSpace.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MapGridControlSecond_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void MapGridControlSecond_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void method_0()
        {
            if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
            {
                this.txtXSpace.Text = (this.imapGrid_0 as IMeasuredGrid).XIntervalSize.ToString();
                this.txtYSpace.Text = (this.imapGrid_0 as IMeasuredGrid).YIntervalSize.ToString();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.imapGrid_0 != null) && !(this.imapGrid_0 is IMeasuredGrid))
            {
            }
        }

        private void txtXSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(this.txtXSpace.Text);
                if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
                {
                    (this.imapGrid_0 as IMeasuredGrid).XIntervalSize = num;
                }
            }
            catch
            {
            }
        }

        private void txtYSpace_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(this.txtYSpace.Text);
                if ((this.imapGrid_0 != null) && (this.imapGrid_0 is IMeasuredGrid))
                {
                    (this.imapGrid_0 as IMeasuredGrid).YIntervalSize = num;
                }
            }
            catch
            {
            }
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.imapFrame_0 = value;
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
                if (this.bool_0)
                {
                    this.method_0();
                }
            }
        }
    }
}

