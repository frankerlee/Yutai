using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class ReprensationGeneralPage : UserControl
    {
        private bool bool_0 = false;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IDatasetName idatasetName_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IRepresentationClass irepresentationClass_0 = null;
        private Label label1;
        private Label label4;
        private Label label5;
        private RadioButton radioButton2;
        private RadioButton rdoRequireShapeOverride;
        private TextBox txtoverrideFldName;
        private TextBox txtRepresentationName;
        private TextBox txtruleIDFldName;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtruleIDFldName = new TextBox();
            this.txtoverrideFldName = new TextBox();
            this.groupBox1 = new GroupBox();
            this.radioButton2 = new RadioButton();
            this.rdoRequireShapeOverride = new RadioButton();
            this.txtRepresentationName = new TextBox();
            this.label1 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x13, 0x3e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "重载字段";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x13, 0x24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "规则字段";
            this.txtruleIDFldName.Location = new Point(0x65, 0x21);
            this.txtruleIDFldName.Name = "txtruleIDFldName";
            this.txtruleIDFldName.Size = new Size(0xa5, 0x15);
            this.txtruleIDFldName.TabIndex = 5;
            this.txtoverrideFldName.Location = new Point(0x65, 0x3b);
            this.txtoverrideFldName.Name = "txtoverrideFldName";
            this.txtoverrideFldName.Size = new Size(0xa5, 0x15);
            this.txtoverrideFldName.TabIndex = 7;
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.rdoRequireShapeOverride);
            this.groupBox1.Location = new Point(0x15, 0x61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xff, 70);
            this.groupBox1.TabIndex = 0x10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "制图几何对象编辑时的行为";
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(12, 0x2a);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x83, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "改变要素的几何对象";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.rdoRequireShapeOverride.AutoSize = true;
            this.rdoRequireShapeOverride.Checked = true;
            this.rdoRequireShapeOverride.Location = new Point(12, 20);
            this.rdoRequireShapeOverride.Name = "rdoRequireShapeOverride";
            this.rdoRequireShapeOverride.Size = new Size(0xef, 0x10);
            this.rdoRequireShapeOverride.TabIndex = 0;
            this.rdoRequireShapeOverride.TabStop = true;
            this.rdoRequireShapeOverride.Text = "存储变化的几何对象作为制图表现的重载";
            this.rdoRequireShapeOverride.UseVisualStyleBackColor = true;
            this.txtRepresentationName.Location = new Point(0x65, 6);
            this.txtRepresentationName.Name = "txtRepresentationName";
            this.txtRepresentationName.Size = new Size(0xa5, 0x15);
            this.txtRepresentationName.TabIndex = 15;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x53, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "制图表现名称:";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtruleIDFldName);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtoverrideFldName);
            base.Controls.Add(this.txtRepresentationName);
            base.Controls.Add(this.label1);
            base.Name = "ReprensationGeneralPage";
            base.Size = new Size(0x127, 0xd7);
            base.Load += new EventHandler(this.ReprensationGeneralPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private IRepresentationWorkspaceExtension method_0(IFeatureClass ifeatureClass_1)
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
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public string OverrideFieldName
        {
            get
            {
                return this.txtoverrideFldName.Text.Trim();
            }
            set
            {
                this.txtoverrideFldName.Text = value;
            }
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
            get
            {
                return this.txtRepresentationName.Text.Trim();
            }
            set
            {
                this.txtRepresentationName.Text = value;
            }
        }

        public bool RequireShapeOverride
        {
            get
            {
                return this.rdoRequireShapeOverride.Checked;
            }
        }

        public string RuleIDFieldName
        {
            get
            {
                return this.txtruleIDFldName.Text.Trim();
            }
            set
            {
                this.txtruleIDFldName.Text = value;
            }
        }
    }
}

