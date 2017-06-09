using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class GeometryNewConnectivityPropertyPage : UserControl
    {
        private ComboBoxEdit cboConnectivityRule;
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IGeometricNetwork igeometricNetwork_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ListView listView1;
        private SpinEdit spinEdit1;
        private SpinEdit spinEdit2;
        private SpinEdit spinEdit3;
        private SpinEdit spinEdit4;
        private TreeView treeView1;
        private TreeView treeView2;

        public GeometryNewConnectivityPropertyPage()
        {
            this.InitializeComponent();
        }

        private void cboConnectivityRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            if (this.cboConnectivityRule.SelectedIndex != -1)
            {
                ListViewItem item;
                ISubtypes featureClass = (this.cboConnectivityRule.SelectedItem as FeatureClassWrap).FeatureClass as ISubtypes;
                string[] items = new string[2];
                if (featureClass.HasSubtype)
                {
                    int num;
                    IEnumSubtype subtypes = featureClass.Subtypes;
                    subtypes.Reset();
                    for (string str = subtypes.Next(out num); str != null; str = subtypes.Next(out num))
                    {
                        items[0] = str;
                        items[1] = num.ToString();
                        item = new ListViewItem(items) {
                            Tag = num
                        };
                        this.listView1.Items.Add(item);
                    }
                }
                else
                {
                    items[0] = this.cboConnectivityRule.Text;
                    items[1] = "0";
                    item = new ListViewItem(items) {
                        Tag = 0
                    };
                    this.listView1.Items.Add(item);
                }
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

        private void GeometryNewConnectivityPropertyPage_Load(object sender, EventArgs e)
        {
            IFeatureClass class3;
            IEnumFeatureClass class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexJunction);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTSimpleEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            class2 = this.igeometricNetwork_0.get_ClassesByType(esriFeatureType.esriFTComplexEdge);
            class2.Reset();
            for (class3 = class2.Next(); class3 != null; class3 = class2.Next())
            {
                this.cboConnectivityRule.Properties.Items.Add(new FeatureClassWrap(class3));
            }
            this.method_0(this.igeometricNetwork_0);
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.cboConnectivityRule = new ComboBoxEdit();
            this.label2 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label3 = new Label();
            this.treeView1 = new TreeView();
            this.groupBox1 = new GroupBox();
            this.spinEdit3 = new SpinEdit();
            this.spinEdit4 = new SpinEdit();
            this.label7 = new Label();
            this.label8 = new Label();
            this.checkEdit2 = new CheckEdit();
            this.spinEdit2 = new SpinEdit();
            this.spinEdit1 = new SpinEdit();
            this.label6 = new Label();
            this.label5 = new Label();
            this.checkEdit1 = new CheckEdit();
            this.label4 = new Label();
            this.treeView2 = new TreeView();
            this.cboConnectivityRule.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.spinEdit3.Properties.BeginInit();
            this.spinEdit4.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.spinEdit2.Properties.BeginInit();
            this.spinEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "连通性规则";
            this.cboConnectivityRule.EditValue = "";
            this.cboConnectivityRule.Location = new Point(8, 0x18);
            this.cboConnectivityRule.Name = "cboConnectivityRule";
            this.cboConnectivityRule.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboConnectivityRule.Size = new Size(0xc0, 0x17);
            this.cboConnectivityRule.TabIndex = 1;
            this.cboConnectivityRule.SelectedIndexChanged += new EventHandler(this.cboConnectivityRule_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4f, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "要素类子类型";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.Location = new Point(8, 0x48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xd8, 0x60);
            this.listView1.TabIndex = 3;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "描述";
            this.columnHeader_0.Width = 0x5d;
            this.columnHeader_1.Text = "代码";
            this.columnHeader_1.Width = 0x69;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 0xb0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x5b, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "网络中的子类型";
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(8, 200);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(0xd0, 0x60);
            this.treeView1.TabIndex = 5;
            this.groupBox1.Controls.Add(this.spinEdit3);
            this.groupBox1.Controls.Add(this.spinEdit4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkEdit2);
            this.groupBox1.Controls.Add(this.spinEdit2);
            this.groupBox1.Controls.Add(this.spinEdit1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkEdit1);
            this.groupBox1.Location = new Point(240, 0x18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd8, 0x98);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对应关系";
            int[] bits = new int[4];
            this.spinEdit3.EditValue = new decimal(bits);
            this.spinEdit3.Location = new Point(120, 120);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit3.Size = new Size(80, 0x17);
            this.spinEdit3.TabIndex = 9;
            bits = new int[4];
            this.spinEdit4.EditValue = new decimal(bits);
            this.spinEdit4.Location = new Point(0x10, 120);
            this.spinEdit4.Name = "spinEdit4";
            this.spinEdit4.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit4.Size = new Size(80, 0x17);
            this.spinEdit4.TabIndex = 8;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x80, 0x68);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 0x11);
            this.label7.TabIndex = 7;
            this.label7.Text = "最大";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x68);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x1d, 0x11);
            this.label8.TabIndex = 6;
            this.label8.Text = "最小";
            this.checkEdit2.Location = new Point(0x10, 80);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "一条边可以连接的点数";
            this.checkEdit2.Size = new Size(160, 0x13);
            this.checkEdit2.TabIndex = 5;
            bits = new int[4];
            this.spinEdit2.EditValue = new decimal(bits);
            this.spinEdit2.Location = new Point(0x70, 0x38);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit2.Size = new Size(80, 0x17);
            this.spinEdit2.TabIndex = 4;
            bits = new int[4];
            this.spinEdit1.EditValue = new decimal(bits);
            this.spinEdit1.Location = new Point(8, 0x38);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.spinEdit1.Size = new Size(80, 0x17);
            this.spinEdit1.TabIndex = 3;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(120, 40);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 2;
            this.label6.Text = "最大";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(8, 40);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 0x11);
            this.label5.TabIndex = 1;
            this.label5.Text = "最小";
            this.checkEdit1.Location = new Point(8, 0x11);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "一个连接点可以连接的边数";
            this.checkEdit1.Size = new Size(0xb8, 0x13);
            this.checkEdit1.TabIndex = 0;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(240, 0xb8);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x42, 0x11);
            this.label4.TabIndex = 7;
            this.label4.Text = "连接子类型";
            this.treeView2.ImageIndex = -1;
            this.treeView2.Location = new Point(240, 200);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = -1;
            this.treeView2.Size = new Size(0xd8, 0x60);
            this.treeView2.TabIndex = 8;
            base.Controls.Add(this.treeView2);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.cboConnectivityRule);
            base.Controls.Add(this.label1);
            base.Name = "GeometryNewConnectivityPropertyPage";
            base.Size = new Size(0x1d8, 320);
            base.Load += new EventHandler(this.GeometryNewConnectivityPropertyPage_Load);
            this.cboConnectivityRule.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.spinEdit3.Properties.EndInit();
            this.spinEdit4.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.spinEdit2.Properties.EndInit();
            this.spinEdit1.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count != 1)
            {
            }
        }

        private void method_0(IGeometricNetwork igeometricNetwork_1)
        {
            try
            {
                IFeatureClassContainer featureDataset = (IFeatureClassContainer) igeometricNetwork_1.FeatureDataset;
                IEnumRule rules = igeometricNetwork_1.Rules;
                rules.Reset();
                for (IRule rule2 = rules.Next(); rule2 != null; rule2 = rules.Next())
                {
                    if (rule2 is IConnectivityRule)
                    {
                        IConnectivityRule rule3 = (IConnectivityRule) rule2;
                        if (rule3.Category == -1)
                        {
                            IJunctionConnectivityRule2 rule4 = (IJunctionConnectivityRule2) rule3;
                            featureDataset.get_ClassByID(rule4.EdgeClassID);
                            featureDataset.get_ClassByID(rule4.JunctionClassID);
                        }
                        else if (rule3.Type == esriRuleType.esriRTEdgeConnectivity)
                        {
                            IEdgeConnectivityRule rule5 = (IEdgeConnectivityRule) rule3;
                            featureDataset.get_ClassByID(rule5.FromEdgeClassID);
                            featureDataset.get_ClassByID(rule5.ToEdgeClassID);
                            featureDataset.get_ClassByID(rule5.DefaultJunctionClassID);
                            for (int i = 0; i < rule5.JunctionCount; i++)
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public IGeometricNetwork GeometricNetwork
        {
            set
            {
                this.igeometricNetwork_0 = value;
            }
        }

        internal class ConnectivityRuleWrap
        {
            private IConnectivityRule iconnectivityRule_0 = null;

            public ConnectivityRuleWrap(IConnectivityRule iconnectivityRule_1)
            {
                this.iconnectivityRule_0 = iconnectivityRule_1;
            }

            public override string ToString()
            {
                return this.iconnectivityRule_0.Helpstring;
            }

            public IConnectivityRule ConnectivityRule
            {
                get
                {
                    return this.iconnectivityRule_0;
                }
            }
        }

        internal class FeatureClassWrap
        {
            private IFeatureClass ifeatureClass_0 = null;

            public FeatureClassWrap(IFeatureClass ifeatureClass_1)
            {
                this.ifeatureClass_0 = ifeatureClass_1;
            }

            public override string ToString()
            {
                return this.ifeatureClass_0.AliasName;
            }

            public IFeatureClass FeatureClass
            {
                get
                {
                    return this.ifeatureClass_0;
                }
            }
        }
    }
}

