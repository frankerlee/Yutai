using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmSelectMergeFeature : Form
    {
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

        private void frmSelectMergeFeature_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.m_FeatureInfos.Length; i++)
            {
                this.listBox1.Items.Add(this.m_FeatureInfos[i]);
            }
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
            set { this.m_FeatureInfos = value; }
        }

        public int SelectedIndex
        {
            get { return this.m_SelectIndex; }
        }
    }
}