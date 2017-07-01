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
    public partial class HatchLayerExtensionPropertyPage : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILayerExtensions ilayerExtensions_0 = null;

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

        private void HatchLayerExtensionPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            object extension;
            if (this.ilayerExtensions_0 != null)
            {
                IHatchLayerExtension hatchLayerExtension = null;
                int i = 0;
                while (true)
                {
                    if (i < this.ilayerExtensions_0.ExtensionCount)
                    {
                        extension = this.ilayerExtensions_0.Extension[i];
                        if (extension is IHatchLayerExtension)
                        {
                            hatchLayerExtension = extension as IHatchLayerExtension;
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (hatchLayerExtension != null)
                {
                    this.chkShowHatches.Checked = hatchLayerExtension.ShowHatches;
                    extension = hatchLayerExtension.HatchClassNames();
                    if (hatchLayerExtension.HatchClassCount() != 0)
                    {
                        IEnumerator enumerator = ((System.Array) extension).GetEnumerator();
                        enumerator.Reset();
                        enumerator.MoveNext();
                        for (i = 0; i < hatchLayerExtension.HatchClassCount(); i++)
                        {
                            string str = enumerator.Current.ToString();
                            IHatchClass hatchClass = hatchLayerExtension.HatchClass(str);
                            this.method_1(str, (hatchClass as IClone).Clone() as IHatchClass, this.treeView1);
                            enumerator.MoveNext();
                        }
                    }
                    else
                    {
                        this.method_1("df", this.method_3(), this.treeView1);
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
            TreeNode node = new TreeNode(string_0)
            {
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
                TreeNode node2 = new TreeNode("Hatch Def(" + pHatchInterval.ToString() + ")")
                {
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
                TreeNode node = new TreeNode("Hatch Def(" + pHatchInterval.ToString() + ")")
                {
                    Tag = new HatchDef(pHatchInterval, pHatchDefinition)
                };
                treeNode_0.Nodes.Add(node);
                enumHatchDefinitions.Next(ref pHatchInterval, ref pHatchDefinition);
            }
        }

        private IHatchClass method_3()
        {
            IHatchClass class2 = new HatchClassClass();
            IHatchInputValue value2 = new HatchInputValueClass
            {
                Value = 0
            };
            class2.HatchInterval = value2;
            IHatchTemplate template = new HatchTemplateClass();
            ISymbol symbol = new SimpleLineSymbolClass();
            IHatchDefinition hatchDefinition = new HatchLineDefinitionClass
            {
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
                (this.treeView1.SelectedNode.Tag as HatchDef).HatchDefinition =
                    this.hatchDefinitionCtrl1.HatchDefinition;
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
                IEnumHatchDefinition enumHatchDefinitions =
                    (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.EnumHatchDefinitions;
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
                        (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.AddHatchDefinition(
                            multipleHatchInterval, this.hatchDefinitionCtrl1.HatchDefinition);
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
                    (selectedNode.Parent.Tag as IHatchClass).HatchTemplate.AddHatchDefinition(multipleHatchInterval,
                        this.hatchDefinitionCtrl1.HatchDefinition);
                }
                TreeNode parent = selectedNode.Parent;
                this.method_2(selectedNode.Parent.Tag as IHatchClass, parent);
                this.treeView1.SelectedNode = parent;
            }
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public bool IsPageDirty
        {
            get { return this.IsPageDirty; }
        }

        public ILayerExtensions LayerExtensions
        {
            set { this.ilayerExtensions_0 = value; }
        }

        public object SelectItem
        {
            set { this.ilayerExtensions_0 = value as ILayerExtensions; }
        }

        internal partial class HatchDef
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
                get { return this.ihatchDefinition_0; }
                set { this.ihatchDefinition_0 = value; }
            }

            public int HatchInterval
            {
                get { return this.int_0; }
                set { this.int_0 = value; }
            }
        }
    }
}