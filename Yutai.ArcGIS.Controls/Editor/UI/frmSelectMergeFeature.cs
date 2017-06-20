using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmSelectMergeFeature : Form
    {
        private Button btnOK;
        private Button button2;
        private Container components = null;
        private Label label1;
        private ListBox listBox1;
        private string[] m_FeatureInfos = null;
        private int m_SelectIndex = -1;

        public frmSelectMergeFeature()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_SelectIndex = this.listBox1.SelectedIndex;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSelectMergeFeature_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_FeatureInfos.Length; i++)
            {
                this.listBox1.Items.Add(this.m_FeatureInfos[i]);
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectMergeFeature));
            this.listBox1 = new ListBox();
            this.label1 = new Label();
            this.btnOK = new Button();
            this.button2 = new Button();
            base.SuspendLayout();
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(8, 0x20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x98, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择将错误并入的要素";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.FlatStyle = FlatStyle.Flat;
            this.btnOK.Location = new Point(8, 0xd0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.Location = new Point(0x58, 0xd0);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x40, 0x18);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xc0, 0xf5);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listBox1);
            
            base.Name = "frmSelectMergeFeature";
            this.Text = "合并";
            base.Load += new EventHandler(this.frmSelectMergeFeature_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndices.Count == 0)
            {
                this.btnOK.Enabled = false;
            }
            else
            {
                this.btnOK.Enabled = true;
            }
        }

        public string[] FeatureInfos
        {
            set
            {
                this.m_FeatureInfos = value;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.m_SelectIndex;
            }
        }
    }
}

