using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class ReprensationGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private IContainer icontainer_0 = null;
        private IDatasetName idatasetName_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IRepresentationClass irepresentationClass_0 = null;

        public ReprensationGeneralPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.txtRepresentationName.Text.Trim().Length == 0)
            {
                MessageBox.Show("名字不能为空!");
                return false;
            }
            if (this.txtruleIDFldName.Text.Trim().Length == 0)
            {
                MessageBox.Show("RuleID字段不能为空!");
                return false;
            }
            if (this.txtoverrideFldName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Override字段不能为空!");
                return false;
            }
            if (this.bool_0 && (this.txtRepresentationName.Text != this.idatasetName_0.Name))
            {
                try
                {
                    (this.irepresentationClass_0 as IDataset).Rename(this.txtRepresentationName.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    return false;
                }
            }
            return true;
        }

        private IRepresentationWorkspaceExtension method_0(IFeatureClass ifeatureClass_1)
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

        private void method_1()
        {
            if (this.idatasetName_0 != null)
            {
                this.txtruleIDFldName.ReadOnly = true;
                this.txtoverrideFldName.ReadOnly = true;
                this.txtruleIDFldName.Text = (this.idatasetName_0 as IRepresentationClassName).RuleIDFieldName;
                this.txtoverrideFldName.Text = (this.idatasetName_0 as IRepresentationClassName).OverrideFieldName;
                this.txtRepresentationName.Text = this.idatasetName_0.Name;
                this.rdoRequireShapeOverride.Checked = this.irepresentationClass_0.RequireShapeOverride;
            }
            else
            {
                IRepresentationWorkspaceExtension extension = this.method_0(this.ifeatureClass_0);
                string str = this.ifeatureClass_0.AliasName + "_Rep";
                string str2 = "RuleID";
                string str3 = "Override";
                if (extension.get_FeatureClassHasRepresentations(this.ifeatureClass_0))
                {
                    IList<string> list = new List<string>();
                    IEnumDatasetName name = extension.get_FeatureClassRepresentationNames(this.ifeatureClass_0);
                    for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        list.Add(name2.Name);
                    }
                    int num = 1;
                    string item = str;
                    while (list.IndexOf(item) != -1)
                    {
                        item = str + num.ToString();
                        num++;
                    }
                    str = item;
                    num = 1;
                    item = str2;
                    while (this.ifeatureClass_0.FindField(item) != -1)
                    {
                        item = str2 + "_" + num.ToString();
                        num++;
                    }
                    str2 = item;
                    num = 1;
                    item = str3;
                    while (this.ifeatureClass_0.FindField(item) != -1)
                    {
                        item = str3 + "_" + num.ToString();
                        num++;
                    }
                    str3 = item;
                }
                this.txtruleIDFldName.Text = str2;
                this.txtoverrideFldName.Text = str3;
                this.txtRepresentationName.Text = str;
            }
        }

        private void ReprensationGeneralPage_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        public IFeatureClass FeatureClass
        {
            set { this.ifeatureClass_0 = value; }
        }

        public string OverrideFieldName
        {
            get { return this.txtoverrideFldName.Text.Trim(); }
            set { this.txtoverrideFldName.Text = value; }
        }

        public IDatasetName RepClassName
        {
            set
            {
                this.bool_0 = true;
                this.idatasetName_0 = value;
                this.irepresentationClass_0 = (this.idatasetName_0 as IName).Open() as IRepresentationClass;
            }
        }

        public IRepresentationClass RepresentationClass
        {
            set
            {
                this.bool_0 = true;
                this.irepresentationClass_0 = value;
                this.idatasetName_0 = (this.irepresentationClass_0 as IDataset).FullName as IDatasetName;
            }
        }

        public string RepresentationName
        {
            get { return this.txtRepresentationName.Text.Trim(); }
            set { this.txtRepresentationName.Text = value; }
        }

        public bool RequireShapeOverride
        {
            get { return this.rdoRequireShapeOverride.Checked; }
        }

        public string RuleIDFieldName
        {
            get { return this.txtruleIDFldName.Text.Trim(); }
            set { this.txtruleIDFldName.Text = value; }
        }
    }
}