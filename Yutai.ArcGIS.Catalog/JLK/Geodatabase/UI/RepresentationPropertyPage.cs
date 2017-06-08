namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RepresentationPropertyPage : UserControl
    {
        private Button btnDelete;
        private Button btnNew;
        private Button btnProperty;
        private Button btnReName;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private Label label1;
        private ListView listView1;

        public RepresentationPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                IDatasetName tag = item.Tag as IDatasetName;
                ((tag as IName).Open() as IDataset).Delete();
                this.listView1.Items.Remove(item);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewReprensentationWizard wizard = new frmNewReprensentationWizard {
                FeatureClass = this.ifeatureClass_0
            };
            if (wizard.ShowDialog() == DialogResult.OK)
            {
                IDatasetName fullName = (wizard.RepresentationClass as IDataset).FullName as IDatasetName;
                ListViewItem item = new ListViewItem(new string[] { fullName.Name, (fullName as IRepresentationClassName).RuleIDFieldName, (fullName as IRepresentationClassName).OverrideFieldName }) {
                    Tag = fullName
                };
                this.listView1.Items.Add(item);
            }
        }

        private void btnProperty_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            IDatasetName tag = item.Tag as IDatasetName;
            IDataset dataset = (tag as IName).Open() as IDataset;
            new frmEidtReprensentation { RepresentationClass = dataset as IRepresentationClass }.ShowDialog();
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public IRepresentationWorkspaceExtension GetRepWSExtFromFClass(IFeatureClass ifeatureClass_1)
        {
            try
            {
                IDataset dataset = ifeatureClass_1 as IDataset;
                IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.label1 = new Label();
            this.btnNew = new Button();
            this.btnDelete = new Button();
            this.btnProperty = new Button();
            this.btnReName = new Button();
            base.SuspendLayout();
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(14, 0x1b);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x125, 0xf2);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "名称";
            this.columnHeader_0.Width = 0x56;
            this.columnHeader_1.Text = "RuleID";
            this.columnHeader_1.Width = 0x4b;
            this.columnHeader_2.Text = "Override";
            this.columnHeader_2.Width = 0x66;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "制图表现列表";
            this.btnNew.Location = new Point(0x139, 0x1b);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x39, 0x20);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnDelete.Location = new Point(0x139, 0x41);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x39, 0x20);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnProperty.Location = new Point(0x139, 0x67);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(0x39, 0x20);
            this.btnProperty.TabIndex = 4;
            this.btnProperty.Text = "属性";
            this.btnProperty.UseVisualStyleBackColor = true;
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.btnReName.Location = new Point(0x139, 0x8d);
            this.btnReName.Name = "btnReName";
            this.btnReName.Size = new Size(0x39, 0x20);
            this.btnReName.TabIndex = 5;
            this.btnReName.Text = "重命名";
            this.btnReName.UseVisualStyleBackColor = true;
            this.btnReName.Click += new EventHandler(this.btnReName_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnReName);
            base.Controls.Add(this.btnProperty);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.listView1);
            base.Name = "RepresentationPropertyPage";
            base.Size = new Size(0x17e, 0x141);
            base.Load += new EventHandler(this.RepresentationPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
            this.btnProperty.Enabled = this.listView1.SelectedItems.Count == 1;
            this.btnReName.Enabled = this.listView1.SelectedItems.Count == 1;
        }

        private void RepresentationPropertyPage_Load(object sender, EventArgs e)
        {
            IRepresentationWorkspaceExtension repWSExtFromFClass = this.GetRepWSExtFromFClass(this.ifeatureClass_0);
            if (repWSExtFromFClass.get_FeatureClassHasRepresentations(this.ifeatureClass_0))
            {
                IEnumDatasetName name = repWSExtFromFClass.get_FeatureClassRepresentationNames(this.ifeatureClass_0);
                IDatasetName name2 = name.Next();
                string[] items = new string[3];
                while (name2 != null)
                {
                    items[0] = name2.Name;
                    items[1] = (name2 as IRepresentationClassName).RuleIDFieldName;
                    items[2] = (name2 as IRepresentationClassName).OverrideFieldName;
                    ListViewItem item = new ListViewItem(items) {
                        Tag = name2
                    };
                    this.listView1.Items.Add(item);
                    name2 = name.Next();
                }
            }
        }

        public IFeatureClass FeatureClass
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }
    }
}

