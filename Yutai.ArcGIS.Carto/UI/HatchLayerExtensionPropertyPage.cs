using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    public class HatchLayerExtensionPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnAddHatchClass;
        private SimpleButton btnDeleteAll;
        private SimpleButton btnDeleteHatchClass;
        private CheckEdit chkShowHatches;
        private Container container_0 = null;
        private HatchClassCtrl hatchClassCtrl1;
        private HatchDefinitionCtrl hatchDefinitionCtrl1;
        private ILayerExtensions ilayerExtensions_0 = null;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private TreeView treeView1;
        private TextEdit txtHatchInt;

        public HatchLayerExtensionPropertyPage()
        {
            this.InitializeComponent();
            this.hatchDefinitionCtrl1.ValueChanged += new HatchDefinitionCtrl.ValueChangedHandler(this.method_4);
            this.txtHatchInt.LostFocus += new EventHandler(this.txtHatchInt_LostFocus);
        }

        public bool Apply()
        {
            IHatchLayerExtension extension = null;
            int num;
            for (num = 0; num < this.ilayerExtensions_0.ExtensionCount; num++)
            {
                object obj2 = this.ilayerExtensions_0.get_Extension(num);
                if (obj2 is IHatchLayerExtension)
                {
                    extension = obj2 as IHatchLayerExtension;
                    break;
                }
            }
            if (extension != null)
            {
                extension.RemoveAll();
                for (num = 0; num < this.treeView1.Nodes.Count; num++)
                {
                    TreeNode node = this.treeView1.Nodes[num];
                    extension.AddClass(node.Text, node.Tag as IHatchClass);
                }
                extension.ShowHatches = this.chkShowHatches.Checked;
            }
            return true;
        }

        private void btnAddHatchClass_Click(object sender, EventArgs e)
        {
            frmNewHatchClassName name = new frmNewHatchClassName();
            if (name.ShowDialog() == DialogResult.OK)
            {
                IHatchClass class2 = this.method_3();
                this.method_1(name.HatchClassName, class2, this.treeView1);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            if (this.treeView1.Nodes.Count == 0)
            {
                IHatchClass class2 = this.method_3();
                this.method_1("Hatch Class", class2, this.treeView1);
            }
        }

        private void btnDeleteHatchClass_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);
            if (this.treeView1.Nodes.Count == 0)
            {
                IHatchClass class2 = this.method_3();
                this.method_1("Hatch Class", class2, this.treeView1);
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void HatchLayerExtensionPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void InitializeComponent()
        {
            this.chkShowHatches = new CheckEdit();
            this.treeView1 = new TreeView();
            this.panel1 = new Panel();
            this.hatchClassCtrl1 = new HatchClassCtrl();
            this.panel2 = new Panel();
            this.txtHatchInt = new TextEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.hatchDefinitionCtrl1 = new HatchDefinitionCtrl();
            this.btnAddHatchClass = new SimpleButton();
            this.btnDeleteHatchClass = new SimpleButton();
            this.btnDeleteAll = new SimpleButton();
            this.chkShowHatches.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.txtHatchInt.Properties.BeginInit();
            base.SuspendLayout();
            this.chkShowHatches.Location = new Point(0, 8);
            this.chkShowHatches.Name = "chkShowHatches";
            this.chkShowHatches.Properties.Caption = "在此层中用刻度标注要素";
            this.chkShowHatches.Size = new Size(0x98, 0x13);
            this.chkShowHatches.TabIndex = 0;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(8, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(0x70, 0x90);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.panel1.Controls.Add(this.hatchClassCtrl1);
            this.panel1.Location = new Point(0x88, 0x38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(320, 240);
            this.panel1.TabIndex = 2;
            this.hatchClassCtrl1.HatchClass = null;
            this.hatchClassCtrl1.Location = new Point(0, 0);
            this.hatchClassCtrl1.Name = "hatchClassCtrl1";
            this.hatchClassCtrl1.Size = new Size(320, 240);
            this.hatchClassCtrl1.TabIndex = 0;
            this.panel2.Controls.Add(this.txtHatchInt);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.hatchDefinitionCtrl1);
            this.panel2.Location = new Point(0x80, 0x30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(320, 240);
            this.panel2.TabIndex = 3;
            this.txtHatchInt.EditValue = "1";
            this.txtHatchInt.Location = new Point(0x70, 8);
            this.txtHatchInt.Name = "txtHatchInt";
            this.txtHatchInt.Size = new Size(0x48, 0x17);
            this.txtHatchInt.TabIndex = 5;
            this.txtHatchInt.EditValueChanged += new EventHandler(this.txtHatchInt_EditValueChanged);
            this.txtHatchInt.Leave += new EventHandler(this.txtHatchInt_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xd0, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 4;
            this.label2.Text = "个刻度间隔";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5b, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "放置刻度线，每";
            this.hatchDefinitionCtrl1.HatchDefinition = null;
            this.hatchDefinitionCtrl1.HatchInterval = 1;
            this.hatchDefinitionCtrl1.Location = new Point(0, 40);
            this.hatchDefinitionCtrl1.Name = "hatchDefinitionCtrl1";
            this.hatchDefinitionCtrl1.Size = new Size(320, 240);
            this.hatchDefinitionCtrl1.TabIndex = 2;
            this.btnAddHatchClass.Location = new Point(0x10, 200);
            this.btnAddHatchClass.Name = "btnAddHatchClass";
            this.btnAddHatchClass.Size = new Size(80, 0x18);
            this.btnAddHatchClass.TabIndex = 4;
            this.btnAddHatchClass.Text = "增加类...";
            this.btnAddHatchClass.Click += new EventHandler(this.btnAddHatchClass_Click);
            this.btnDeleteHatchClass.Location = new Point(0x10, 0xe8);
            this.btnDeleteHatchClass.Name = "btnDeleteHatchClass";
            this.btnDeleteHatchClass.Size = new Size(80, 0x18);
            this.btnDeleteHatchClass.TabIndex = 5;
            this.btnDeleteHatchClass.Text = "删除类";
            this.btnDeleteHatchClass.Click += new EventHandler(this.btnDeleteHatchClass_Click);
            this.btnDeleteAll.Location = new Point(0x10, 0x108);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new Size(80, 0x18);
            this.btnDeleteAll.TabIndex = 6;
            this.btnDeleteAll.Text = "删除所有";
            this.btnDeleteAll.Click += new EventHandler(this.btnDeleteAll_Click);
            base.Controls.Add(this.btnDeleteAll);
            base.Controls.Add(this.btnDeleteHatchClass);
            base.Controls.Add(this.btnAddHatchClass);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.chkShowHatches);
            base.Name = "HatchLayerExtensionPropertyPage";
            base.Size = new Size(0x1d8, 0x130);
            base.Load += new EventHandler(this.HatchLayerExtensionPropertyPage_Load);
            this.chkShowHatches.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.txtHatchInt.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            if (this.ilayerExtensions_0 != null)
            {
                int num;
                object obj2;
                IHatchLayerExtension extension = null;
                for (num = 0; num < this.ilayerExtensions_0.ExtensionCount; num++)
                {
                    obj2 = this.ilayerExtensions_0.get_Extension(num);
                    if (obj2 is IHatchLayerExtension)
                    {
                        extension = obj2 as IHatchLayerExtension;
                        break;
                    }
                }
                if (extension != null)
                {
                    IHatchClass class2;
                    this.chkShowHatches.Checked = extension.ShowHatches;
                    obj2 = extension.HatchClassNames();
                    if (extension.HatchClassCount() == 0)
                    {
                        class2 = this.method_3();
                        this.method_1("df", class2, this.treeView1);
                    }
                    else
                    {
                        IEnumerator enumerator = obj2.GetEnumerator();
                        enumerator.Reset();
                        enumerator.MoveNext();
                        for (num = 0; num < extension.HatchClassCount(); num++)
                        {
                            string name = enumerator.Current.ToString();
                            class2 = extension.HatchClass(name);
                            this.method_1(name, (class2 as IClone).Clone() as IHatchClass, this.treeView1);
                            enumerator.MoveNext();
                        }
                    }
                    if (this.treeView1.Nodes.Count > 0)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                    }
                }
            }
        }

        private void method_1(string string_0, IHatchClass ihatchClass_0, TreeView treeView_0)
        {
            TreeNode node = new TreeNode(string_0) {
                Tag = ihatchClass_0
            };
            treeView_0.Nodes.Add(node);
            IEnumHatchDefinition enumHatchDefinitions = ihatchClass_0.HatchTemplate.EnumHatchDefinitions;
            enumHatchDefinitions.Reset();
            IHatchDefinition pHatchDefinition = null;
            int pHatchInterval = 0;
            enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
            while (pHatchDefinition != null)
            {
                TreeNode node2 = new TreeNode("Hatch Def(" + pHatchInterval.ToString() + ")") {
                    Tag = new HatchDef(pHatchInterval, pHatchDefinition)
                };
                node.Nodes.Add(node2);
                enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
            }
        }

        private void method_2(IHatchClass ihatchClass_0, TreeNode treeNode_0)
        {
            treeNode_0.Nodes.Clear();
            IEnumHatchDefinition enumHatchDefinitions = ihatchClass_0.HatchTemplate.EnumHatchDefinitions;
            enumHatchDefinitions.Reset();
            IHatchDefinition pHatchDefinition = null;
            int pHatchInterval = 0;
            enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
            while (pHatchDefinition != null)
            {
                TreeNode node = new TreeNode("Hatch Def(" + pHatchInterval.ToString() + ")") {
                    Tag = new HatchDef(pHatchInterval, pHatchDefinition)
                };
                treeNode_0.Nodes.Add(node);
                enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
            }
        }

        private IHatchClass method_3()
        {
            IHatchClass class2 = new HatchClassClass();
            IHatchInputValue value2 = new HatchInputValueClass {
                Value = 0
            };
            class2.HatchInterval = value2;
            IHatchTemplate template = new HatchTemplateClass();
            ISymbol symbol = new SimpleLineSymbolClass();
            IHatchDefinition hatchDefinition = new HatchLineDefinitionClass {
                HatchSymbol = symbol,
                DisplayPrecision = 0
            };
            (hatchDefinition as IHatchLineDefinition).Length = 0.0;
            hatchDefinition.Offset = 0.0;
            hatchDefinition.Alignment = esriHatchAlignmentType.esriHatchAlignLeft;
            template.Name = "HatchTemplate1";
            template.AddHatchDefinition(1, hatchDefinition);
            class2.HatchTemplate = template;
            return class2;
        }

        private void method_4(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                (this.treeView1.SelectedNode.Tag as HatchDef).HatchDefinition = this.hatchDefinitionCtrl1.HatchDefinition;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                if (selectedNode.Tag is IHatchClass)
                {
                    this.btnDeleteHatchClass.Enabled = true;
                    this.panel1.Visible = true;
                    this.panel2.Visible = false;
                    this.hatchClassCtrl1.HatchClass = selectedNode.Tag as IHatchClass;
                    this.hatchClassCtrl1.Fields = (this.ilayerExtensions_0 as IFeatureLayer).FeatureClass.Fields;
                    this.hatchClassCtrl1.Init();
                }
                else if (selectedNode.Tag is HatchDef)
                {
                    this.btnDeleteHatchClass.Enabled = false;
                    this.panel1.Visible = false;
                    this.panel2.Visible = true;
                    this.hatchDefinitionCtrl1.HatchInterval = (selectedNode.Tag as HatchDef).HatchInterval;
                    this.txtHatchInt.Text = (selectedNode.Tag as HatchDef).HatchInterval.ToString();
                    this.hatchDefinitionCtrl1.HatchDefinition = (selectedNode.Tag as HatchDef).HatchDefinition;
                    this.hatchDefinitionCtrl1.Init();
                }
            }
            else
            {
                this.panel1.Visible = false;
                this.panel2.Visible = false;
            }
        }

        private void txtHatchInt_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtHatchInt_Leave(object sender, EventArgs e)
        {
        }

        private void txtHatchInt_LostFocus(object sender, EventArgs e)
        {
            int multipleHatchInterval = int.Parse(this.txtHatchInt.Text);
            if (this.hatchDefinitionCtrl1.HatchInterval != multipleHatchInterval)
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                IEnumHatchDefinition enumHatchDefinitions = (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.EnumHatchDefinitions;
                enumHatchDefinitions.Reset();
                IHatchDefinition pHatchDefinition = null;
                int pHatchInterval = 0;
                enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
                int index = 0;
                bool flag = true;
                while (pHatchDefinition != null)
                {
                    if (multipleHatchInterval == this.hatchDefinitionCtrl1.HatchInterval)
                    {
                        if (MessageBox.Show("指定的刻度已存在，是否覆盖?", "刻度", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        {
                            this.txtHatchInt.Text = this.hatchDefinitionCtrl1.HatchInterval.ToString();
                            return;
                        }
                        (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.RemoveHatchDefinition(index);
                        (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.AddHatchDefinition(multipleHatchInterval, this.hatchDefinitionCtrl1.HatchDefinition);
                        flag = false;
                        break;
                    }
                    index++;
                    enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
                }
                int num5 = 0;
                while (pHatchDefinition != null)
                {
                    if (pHatchInterval == this.hatchDefinitionCtrl1.HatchInterval)
                    {
                        (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.RemoveHatchDefinition(index);
                        break;
                    }
                    num5++;
                    enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
                }
                if (flag)
                {
                    (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.AddHatchDefinition(multipleHatchInterval, this.hatchDefinitionCtrl1.HatchDefinition);
                }
                TreeNode parent = selectedNode.Parent;
                this.method_2(selectedNode.Parent.Tag as IHatchClass, parent);
                this.treeView1.SelectedNode = parent;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.IsPageDirty;
            }
        }

        public ILayerExtensions LayerExtensions
        {
            set
            {
                this.ilayerExtensions_0 = value;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayerExtensions_0 = value as ILayerExtensions;
            }
        }

        internal class HatchDef
        {
            private IHatchDefinition ihatchDefinition_0 = null;
            private int int_0 = 0;

            public HatchDef(int int_1, IHatchDefinition ihatchDefinition_1)
            {
                this.ihatchDefinition_0 = ihatchDefinition_1;
                this.int_0 = int_1;
            }

            public IHatchDefinition HatchDefinition
            {
                get
                {
                    return this.ihatchDefinition_0;
                }
                set
                {
                    this.ihatchDefinition_0 = value;
                }
            }

            public int HatchInterval
            {
                get
                {
                    return this.int_0;
                }
                set
                {
                    this.int_0 = value;
                }
            }
        }
    }
}

