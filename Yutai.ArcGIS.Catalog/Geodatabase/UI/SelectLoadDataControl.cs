using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class SelectLoadDataControl : UserControl
    {
        private Container container_0 = null;

        public SelectLoadDataControl()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.SourceDatalistBox.Items.IndexOf(this.txtInputFeatureClass.Tag) != -1)
            {
                MessageBox.Show("要素类已存在!");
            }
            else
            {
                this.SourceDatalistBox.Items.Add(this.txtInputFeatureClass.Tag);
                this.txtInputFeatureClass.Tag = null;
                this.txtInputFeatureClass.Text = "";
                this.btnAdd.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.SourceDatalistBox.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int index = this.SourceDatalistBox.SelectedIndices[i];
                this.SourceDatalistBox.Items.RemoveAt(index);
            }
        }

        private void btnSelectInputFeatures_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile
            {
                Text = "选择要素"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.DoModalOpen() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count == 0)
                {
                    return;
                }
                IGxObject obj2 = items.get_Element(0) as IGxObject;
                this.txtInputFeatureClass.Text = obj2.FullName;
                this.txtInputFeatureClass.Tag = obj2;
            }
            if (this.txtInputFeatureClass.Text.Length > 0)
            {
                this.btnAdd.Enabled = true;
            }
        }

        private void SelectLoadDataControl_Load(object sender, EventArgs e)
        {
        }

        private void SourceDatalistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SourceDatalistBox.SelectedIndices.Count > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

        public IList Items
        {
            get { return this.SourceDatalistBox.Items; }
        }
    }
}