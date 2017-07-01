using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class RepresentationPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;

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
            frmNewReprensentationWizard wizard = new frmNewReprensentationWizard
            {
                FeatureClass = this.ifeatureClass_0
            };
            if (wizard.ShowDialog() == DialogResult.OK)
            {
                IDatasetName fullName = (wizard.RepresentationClass as IDataset).FullName as IDatasetName;
                ListViewItem item =
                    new ListViewItem(new string[]
                    {
                        fullName.Name, (fullName as IRepresentationClassName).RuleIDFieldName,
                        (fullName as IRepresentationClassName).OverrideFieldName
                    })
                    {
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
            new frmEidtReprensentation {RepresentationClass = dataset as IRepresentationClass}.ShowDialog();
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
        }

        public IRepresentationWorkspaceExtension GetRepWSExtFromFClass(IFeatureClass ifeatureClass_1)
        {
            try
            {
                IDataset dataset = ifeatureClass_1 as IDataset;
                IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                UID gUID = new UIDClass
                {
                    Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                };
                return (workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension);
            }
            catch
            {
            }
            return null;
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
                    ListViewItem item = new ListViewItem(items)
                    {
                        Tag = name2
                    };
                    this.listView1.Items.Add(item);
                    name2 = name.Next();
                }
            }
        }

        public IFeatureClass FeatureClass
        {
            set { this.ifeatureClass_0 = value; }
        }
    }
}