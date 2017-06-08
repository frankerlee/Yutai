using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NAGeneralPropertyPage : UserControl
    {
        private CheckEdit checkEdit;
        private Container components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private bool m_CanDo = false;
        private bool m_IsDirty = false;
        private RadioGroup rdoTrackFeatureSet;
        private TextEdit txtSnapTol;

        public NAGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.m_IsDirty)
            {
                this.m_IsDirty = false;
                NetworkAnalyst.TraceIndeterminateFlow = this.checkEdit.Checked;
                try
                {
                    double num = double.Parse(this.txtSnapTol.Text);
                    if (num > 0.0)
                    {
                        NetworkAnalyst.SnapTolerance = num;
                    }
                }
                catch
                {
                }
            }
            return true;
        }

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.m_IsDirty = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Init()
        {
            this.m_CanDo = false;
            this.txtSnapTol.Text = NetworkAnalyst.SnapTolerance.ToString();
            this.checkEdit.Checked = NetworkAnalyst.TraceIndeterminateFlow;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.checkEdit = new CheckEdit();
            this.rdoTrackFeatureSet = new RadioGroup();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtSnapTol = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.checkEdit.Properties.BeginInit();
            this.rdoTrackFeatureSet.Properties.BeginInit();
            this.txtSnapTol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.checkEdit);
            this.groupBox1.Controls.Add(this.rdoTrackFeatureSet);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 0xc0);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "追踪要素";
            this.checkEdit.Location = new System.Drawing.Point(8, 0x70);
            this.checkEdit.Name = "checkEdit";
            this.checkEdit.Properties.Caption = "定向追踪任务包含未确定和未初始化流向的边";
            this.checkEdit.Size = new Size(0x108, 0x13);
            this.checkEdit.TabIndex = 1;
            this.checkEdit.CheckedChanged += new EventHandler(this.checkEdit_CheckedChanged);
            this.rdoTrackFeatureSet.Location = new System.Drawing.Point(0x18, 0x18);
            this.rdoTrackFeatureSet.Name = "rdoTrackFeatureSet";
            this.rdoTrackFeatureSet.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoTrackFeatureSet.Properties.Appearance.Options.UseBackColor = true;
            this.rdoTrackFeatureSet.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoTrackFeatureSet.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "全部要素"), new RadioGroupItem(null, "选中要素"), new RadioGroupItem(null, "未选中要素") });
            this.rdoTrackFeatureSet.Size = new Size(0xc0, 0x48);
            this.rdoTrackFeatureSet.TabIndex = 0;
            this.rdoTrackFeatureSet.SelectedIndexChanged += new EventHandler(this.rdoTrackFeatureSet_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0xe8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x80, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "标记和障碍的捕捉容差";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x108, 0xe8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "像素";
            this.txtSnapTol.EditValue = "10";
            this.txtSnapTol.Location = new System.Drawing.Point(160, 0xe8);
            this.txtSnapTol.Name = "txtSnapTol";
            this.txtSnapTol.Size = new Size(0x58, 0x17);
            this.txtSnapTol.TabIndex = 3;
            this.txtSnapTol.EditValueChanged += new EventHandler(this.txtSnapTol_EditValueChanged);
            base.Controls.Add(this.txtSnapTol);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "NAGeneralPropertyPage";
            base.Size = new Size(0x138, 0x150);
            base.Load += new EventHandler(this.NAGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.checkEdit.Properties.EndInit();
            this.rdoTrackFeatureSet.Properties.EndInit();
            this.txtSnapTol.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NAGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void rdoTrackFeatureSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_IsDirty = true;
        }

        private void txtSnapTol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                try
                {
                    if (double.Parse(this.txtSnapTol.Text) > 0.0)
                    {
                        this.txtSnapTol.ForeColor = Color.Black;
                        this.m_IsDirty = true;
                    }
                    else
                    {
                        this.txtSnapTol.ForeColor = Color.Red;
                    }
                }
                catch
                {
                    this.txtSnapTol.ForeColor = Color.Red;
                }
            }
        }
    }
}

