namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.DataSourcesFile;
    using ESRI.ArcGIS.esriSystem;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class CoverageTolerancePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ICoverage icoverage_0 = null;
        private ICoverageName icoverageName_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label lblDangle;
        private Label lblEdit;
        private Label lblFuzzy;
        private Label lblGrain;
        private Label lblNodeSnap;
        private Label lblSnap;
        private Label lblTicMatch;
        private Label lblWeed;
        private TextEdit textDangle;
        private TextEdit txtEdit;
        private TextEdit txtFuzzy;
        private TextEdit txtGrain;
        private TextEdit txtNodeSnap;
        private TextEdit txtSnap;
        private TextEdit txtTicMatch;
        private TextEdit txtWeed;

        public CoverageTolerancePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            double toleranceValue = 0.0;
            try
            {
                toleranceValue = double.Parse(this.txtFuzzy.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTFuzzy, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.textDangle.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTDangle, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtEdit.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTEdit, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtGrain.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTGrain, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtNodeSnap.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTNodeSnap, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtSnap.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTSnap, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtTicMatch.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTTicMatch, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtWeed.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTWeed, toleranceValue);
            }
            catch
            {
            }
        }

        private void CoverageTolerancePropertyPage_Load(object sender, EventArgs e)
        {
            this.txtFuzzy.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTFuzzy).ToString();
            this.lblFuzzy.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTFuzzy) ? "Verified" : "缺省值";
            this.textDangle.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTDangle).ToString();
            this.lblDangle.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTDangle) ? "Verified" : "缺省值";
            this.txtEdit.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTEdit).ToString();
            this.lblEdit.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTEdit) ? "Verified" : "缺省值";
            this.txtGrain.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTGrain).ToString();
            this.lblGrain.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTGrain) ? "Verified" : "缺省值";
            this.txtNodeSnap.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTNodeSnap).ToString();
            this.lblNodeSnap.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTNodeSnap) ? "Verified" : "缺省值";
            this.txtSnap.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTSnap).ToString();
            this.lblSnap.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTSnap) ? "Verified" : "缺省值";
            this.txtTicMatch.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTTicMatch).ToString();
            this.lblTicMatch.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTTicMatch) ? "Verified" : "缺省值";
            this.txtWeed.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTWeed).ToString();
            this.lblWeed.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTWeed) ? "Verified" : "缺省值";
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
            this.groupBox2 = new GroupBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtFuzzy = new TextEdit();
            this.textDangle = new TextEdit();
            this.txtGrain = new TextEdit();
            this.txtEdit = new TextEdit();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtSnap = new TextEdit();
            this.txtNodeSnap = new TextEdit();
            this.label5 = new Label();
            this.label6 = new Label();
            this.txtWeed = new TextEdit();
            this.txtTicMatch = new TextEdit();
            this.label7 = new Label();
            this.label8 = new Label();
            this.lblFuzzy = new Label();
            this.lblDangle = new Label();
            this.lblEdit = new Label();
            this.lblGrain = new Label();
            this.lblNodeSnap = new Label();
            this.lblSnap = new Label();
            this.lblTicMatch = new Label();
            this.lblWeed = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.txtFuzzy.Properties.BeginInit();
            this.textDangle.Properties.BeginInit();
            this.txtGrain.Properties.BeginInit();
            this.txtEdit.Properties.BeginInit();
            this.txtSnap.Properties.BeginInit();
            this.txtNodeSnap.Properties.BeginInit();
            this.txtWeed.Properties.BeginInit();
            this.txtTicMatch.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lblDangle);
            this.groupBox1.Controls.Add(this.lblFuzzy);
            this.groupBox1.Controls.Add(this.textDangle);
            this.groupBox1.Controls.Add(this.txtFuzzy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "拓扑容限";
            this.groupBox2.Controls.Add(this.lblWeed);
            this.groupBox2.Controls.Add(this.lblTicMatch);
            this.groupBox2.Controls.Add(this.lblSnap);
            this.groupBox2.Controls.Add(this.lblNodeSnap);
            this.groupBox2.Controls.Add(this.lblGrain);
            this.groupBox2.Controls.Add(this.lblEdit);
            this.groupBox2.Controls.Add(this.txtWeed);
            this.groupBox2.Controls.Add(this.txtTicMatch);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtSnap);
            this.groupBox2.Controls.Add(this.txtNodeSnap);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtGrain);
            this.groupBox2.Controls.Add(this.txtEdit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new Point(0x10, 0x5d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(280, 0xd0);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Arcedit容限";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x30, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "模糊值:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 0x11);
            this.label2.TabIndex = 1;
            this.label2.Text = "悬垂值:";
            this.txtFuzzy.EditValue = "";
            this.txtFuzzy.Location = new Point(0x48, 0x10);
            this.txtFuzzy.Name = "txtFuzzy";
            this.txtFuzzy.Size = new Size(120, 0x17);
            this.txtFuzzy.TabIndex = 2;
            this.textDangle.EditValue = "";
            this.textDangle.Location = new Point(0x48, 0x30);
            this.textDangle.Name = "textDangle";
            this.textDangle.Size = new Size(120, 0x17);
            this.textDangle.TabIndex = 3;
            this.txtGrain.EditValue = "";
            this.txtGrain.Location = new Point(80, 0x30);
            this.txtGrain.Name = "txtGrain";
            this.txtGrain.Size = new Size(120, 0x17);
            this.txtGrain.TabIndex = 7;
            this.txtEdit.EditValue = "";
            this.txtEdit.Location = new Point(80, 0x10);
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new Size(120, 0x17);
            this.txtEdit.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x30);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 0x11);
            this.label3.TabIndex = 5;
            this.label3.Text = "粒度:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x30, 0x11);
            this.label4.TabIndex = 4;
            this.label4.Text = "编辑值:";
            this.txtSnap.EditValue = "";
            this.txtSnap.Location = new Point(80, 0x70);
            this.txtSnap.Name = "txtSnap";
            this.txtSnap.Size = new Size(120, 0x17);
            this.txtSnap.TabIndex = 11;
            this.txtNodeSnap.EditValue = "";
            this.txtNodeSnap.Location = new Point(80, 80);
            this.txtNodeSnap.Name = "txtNodeSnap";
            this.txtNodeSnap.Size = new Size(120, 0x17);
            this.txtNodeSnap.TabIndex = 10;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x10, 0x70);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 9;
            this.label5.Text = "捕捉:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 80);
            this.label6.Name = "label6";
            this.label6.Size = new Size(60, 0x11);
            this.label6.TabIndex = 8;
            this.label6.Text = "节点捕捉:";
            this.txtWeed.EditValue = "";
            this.txtWeed.Location = new Point(80, 0xb0);
            this.txtWeed.Name = "txtWeed";
            this.txtWeed.Size = new Size(120, 0x17);
            this.txtWeed.TabIndex = 15;
            this.txtTicMatch.EditValue = "";
            this.txtTicMatch.Location = new Point(80, 0x90);
            this.txtTicMatch.Name = "txtTicMatch";
            this.txtTicMatch.Size = new Size(120, 0x17);
            this.txtTicMatch.TabIndex = 14;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x10, 0xb0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 0x11);
            this.label7.TabIndex = 13;
            this.label7.Text = "Weed:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x90);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x36, 0x11);
            this.label8.TabIndex = 12;
            this.label8.Text = "Tic匹配:";
            this.lblFuzzy.AutoSize = true;
            this.lblFuzzy.Location = new Point(200, 0x10);
            this.lblFuzzy.Name = "lblFuzzy";
            this.lblFuzzy.Size = new Size(0, 0x11);
            this.lblFuzzy.TabIndex = 4;
            this.lblDangle.AutoSize = true;
            this.lblDangle.Location = new Point(200, 0x30);
            this.lblDangle.Name = "lblDangle";
            this.lblDangle.Size = new Size(0, 0x11);
            this.lblDangle.TabIndex = 5;
            this.lblEdit.AutoSize = true;
            this.lblEdit.Location = new Point(0xd0, 0x10);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new Size(0, 0x11);
            this.lblEdit.TabIndex = 0x10;
            this.lblGrain.AutoSize = true;
            this.lblGrain.Location = new Point(0xd0, 0x30);
            this.lblGrain.Name = "lblGrain";
            this.lblGrain.Size = new Size(0, 0x11);
            this.lblGrain.TabIndex = 0x11;
            this.lblNodeSnap.AutoSize = true;
            this.lblNodeSnap.Location = new Point(0xd0, 80);
            this.lblNodeSnap.Name = "lblNodeSnap";
            this.lblNodeSnap.Size = new Size(0, 0x11);
            this.lblNodeSnap.TabIndex = 0x12;
            this.lblSnap.AutoSize = true;
            this.lblSnap.Location = new Point(0xd0, 0x70);
            this.lblSnap.Name = "lblSnap";
            this.lblSnap.Size = new Size(0, 0x11);
            this.lblSnap.TabIndex = 0x13;
            this.lblTicMatch.AutoSize = true;
            this.lblTicMatch.Location = new Point(0xd0, 0x90);
            this.lblTicMatch.Name = "lblTicMatch";
            this.lblTicMatch.Size = new Size(0, 0x11);
            this.lblTicMatch.TabIndex = 20;
            this.lblWeed.AutoSize = true;
            this.lblWeed.Location = new Point(0xd0, 0xb8);
            this.lblWeed.Name = "lblWeed";
            this.lblWeed.Size = new Size(0, 0x11);
            this.lblWeed.TabIndex = 0x15;
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "CoverageTolerancePropertyPage";
            base.Size = new Size(0x138, 0x130);
            base.Load += new EventHandler(this.CoverageTolerancePropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.txtFuzzy.Properties.EndInit();
            this.textDangle.Properties.EndInit();
            this.txtGrain.Properties.EndInit();
            this.txtEdit.Properties.EndInit();
            this.txtSnap.Properties.EndInit();
            this.txtNodeSnap.Properties.EndInit();
            this.txtWeed.Properties.EndInit();
            this.txtTicMatch.Properties.EndInit();
            base.ResumeLayout(false);
        }

        public ICoverageName CoverageName
        {
            set
            {
                this.icoverageName_0 = value;
                this.icoverage_0 = (this.icoverageName_0 as IName).Open() as ICoverage;
            }
        }
    }
}

