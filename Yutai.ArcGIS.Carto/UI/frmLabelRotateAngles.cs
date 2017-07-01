using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmLabelRotateAngles : Form
    {
        private Container container_0 = null;
        private double[] double_0 = null;

        public frmLabelRotateAngles()
        {
            this.InitializeComponent();
        }

        private void btnAddAngle_Click(object sender, EventArgs e)
        {
            try
            {
                double item = double.Parse(this.txtAngle.Text);
                this.listPointPlacementAngles.Items.Add(item);
            }
            catch
            {
            }
        }

        private void btnDeleteLayer_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if (selectedIndex != -1)
            {
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if ((selectedIndex > -1) && (selectedIndex < (this.listPointPlacementAngles.Items.Count - 1)))
            {
                object item = this.listPointPlacementAngles.Items[selectedIndex];
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                if (this.listPointPlacementAngles.Items.Count == selectedIndex)
                {
                    this.listPointPlacementAngles.Items.Add(item);
                }
                else
                {
                    this.listPointPlacementAngles.Items.Insert(selectedIndex + 1, item);
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.listPointPlacementAngles.SelectedIndex;
            if (selectedIndex > 0)
            {
                object item = this.listPointPlacementAngles.Items[selectedIndex];
                this.listPointPlacementAngles.Items.RemoveAt(selectedIndex);
                this.listPointPlacementAngles.Items.Insert(selectedIndex - 1, item);
            }
        }

        private void frmLabelRotateAngles_Load(object sender, EventArgs e)
        {
            if (this.double_0 != null)
            {
                this.listPointPlacementAngles.Items.Clear();
                for (int i = 0; i < this.double_0.Length; i++)
                {
                    this.listPointPlacementAngles.Items.Add(this.double_0[i]);
                }
            }
        }

        public object Angles
        {
            get
            {
                this.double_0 = new double[this.listPointPlacementAngles.Items.Count];
                for (int i = 0; i < this.listPointPlacementAngles.Items.Count; i++)
                {
                    this.double_0[i] = (double) this.listPointPlacementAngles.Items[i];
                }
                return this.double_0;
            }
            set { this.double_0 = value as double[]; }
        }
    }
}