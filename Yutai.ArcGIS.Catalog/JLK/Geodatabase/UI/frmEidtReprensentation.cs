namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmEidtReprensentation : Form
    {
        private Button btnCancel;
        private Button btnNext;
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IRepresentationClass irepresentationClass_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
        private ReprensationGeneralPage reprensationGeneralPage_0 = new ReprensationGeneralPage();
        private RepresentationRulesPage representationRulesPage_0 = new RepresentationRulesPage();
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;

        public frmEidtReprensentation()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
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

        private void frmEidtReprensentation_Load(object sender, EventArgs e)
        {
            this.reprensationGeneralPage_0.RepresentationClass = this.irepresentationClass_0;
            this.reprensationGeneralPage_0.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.reprensationGeneralPage_0);
            this.representationRulesPage_0.Dock = DockStyle.Fill;
            this.representationRulesPage_0.FeatureClass = this.ifeatureClass_0;
            this.representationRulesPage_0.RepresentationRules = this.irepresentationRules_0;
            this.tabPage2.Controls.Add(this.representationRulesPage_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmEidtReprensentation));
            this.btnNext = new Button();
            this.btnCancel = new Button();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.btnNext.Location = new Point(0x146, 0x146);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x1c);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "确定";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x18c, 0x146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x1c);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1dc, 320);
            this.tabControl1.TabIndex = 15;
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1d4, 0x127);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1d4, 0x127);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "规则";
            this.tabPage2.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1dc, 0x16e);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEidtReprensentation";
            this.Text = "新建制图表现向导";
            base.Load += new EventHandler(this.frmEidtReprensentation_Load);
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IFeatureClass FeatureClass
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public IRepresentationClass RepresentationClass
        {
            get
            {
                return this.irepresentationClass_0;
            }
            set
            {
                this.irepresentationClass_0 = value;
                this.ifeatureClass_0 = this.irepresentationClass_0.FeatureClass;
                this.irepresentationRules_0 = this.irepresentationClass_0.RepresentationRules;
            }
        }
    }
}

