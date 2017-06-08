namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.Editors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SelectLoadDataControl : UserControl
    {
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private ListBoxControl SourceDatalistBox;
        private TextEdit txtInputFeatureClass;

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
            frmOpenFile file = new frmOpenFile {
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SelectLoadDataControl));
            this.btnSelectInputFeatures = new SimpleButton();
            this.SourceDatalistBox = new ListBoxControl();
            this.label1 = new Label();
            this.btnDelete = new SimpleButton();
            this.label2 = new Label();
            this.txtInputFeatureClass = new TextEdit();
            this.btnAdd = new SimpleButton();
            ((ISupportInitialize) this.SourceDatalistBox).BeginInit();
            this.txtInputFeatureClass.Properties.BeginInit();
            base.SuspendLayout();
            this.btnSelectInputFeatures.Image = (Image) manager.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(0x108, 0x20);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(0x18, 0x18);
            this.btnSelectInputFeatures.TabIndex = 3;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.SourceDatalistBox.ItemHeight = 15;
            this.SourceDatalistBox.Location = new Point(8, 0x58);
            this.SourceDatalistBox.Name = "SourceDatalistBox";
            this.SourceDatalistBox.SelectionMode = SelectionMode.MultiExtended;
            this.SourceDatalistBox.Size = new Size(0xf8, 0x88);
            this.SourceDatalistBox.TabIndex = 4;
            this.SourceDatalistBox.SelectedIndexChanged += new EventHandler(this.SourceDatalistBox_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x40);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "源数据列表";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) manager.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(0x108, 120);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x18, 0x18);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "输入数据";
            this.txtInputFeatureClass.EditValue = "";
            this.txtInputFeatureClass.Location = new Point(8, 0x20);
            this.txtInputFeatureClass.Name = "txtInputFeatureClass";
            this.txtInputFeatureClass.Properties.ReadOnly = true;
            this.txtInputFeatureClass.Size = new Size(0xf8, 0x15);
            this.txtInputFeatureClass.TabIndex = 10;
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = (Image) manager.GetObject("btnAdd.Image");
            this.btnAdd.Location = new Point(0x108, 0x58);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x18, 0x18);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.txtInputFeatureClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.SourceDatalistBox);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Name = "SelectLoadDataControl";
            base.Size = new Size(0x130, 0x100);
            base.Load += new EventHandler(this.SelectLoadDataControl_Load);
            ((ISupportInitialize) this.SourceDatalistBox).EndInit();
            this.txtInputFeatureClass.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
            get
            {
                return this.SourceDatalistBox.Items;
            }
        }
    }
}

