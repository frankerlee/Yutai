using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class AddClassCtrl : UserControl
    {
        private SimpleButton btnAdd;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0 = null;
        private Label label1;
        private TreeNode treeNode_0 = null;
        private TextEdit txtClassName;

        public AddClassCtrl()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string str = this.txtClassName.Text.Trim();
            if (str.Length != 0)
            {
                if (this.method_0(str))
                {
                    MessageBox.Show("类名已存在!");
                }
                else
                {
                    IAnnotateLayerProperties properties;
                    int num;
                    this.iannotateLayerPropertiesCollection2_0.QueryItem(0, out properties, out num);
                    IAnnotateLayerProperties properties2 = new LabelEngineLayerPropertiesClass {
                        Class = str,
                        FeatureLinked = properties.FeatureLinked,
                        FeatureLayer = properties.FeatureLayer,
                        AddUnplacedToGraphicsContainer = properties.AddUnplacedToGraphicsContainer,
                        AnnotationMaximumScale = properties.AnnotationMaximumScale,
                        AnnotationMinimumScale = properties.AnnotationMinimumScale,
                        CreateUnplacedElements = properties.CreateUnplacedElements,
                        DisplayAnnotation = properties.DisplayAnnotation,
                        LabelWhichFeatures = properties.LabelWhichFeatures,
                        Priority = properties.Priority,
                        UseOutput = properties.UseOutput,
                        WhereClause = properties.WhereClause
                    };
                    ILabelEngineLayerProperties properties3 = properties2 as ILabelEngineLayerProperties;
                    ILabelEngineLayerProperties properties4 = properties as ILabelEngineLayerProperties;
                    properties3.Expression = properties4.Expression;
                    properties3.IsExpressionSimple = properties4.IsExpressionSimple;
                    num = this.method_1();
                    properties3.SymbolID = num;
                    properties3.Symbol = (properties4.Symbol as IClone).Clone() as ITextSymbol;
                    TreeNode node = new TreeNode(str) {
                        Checked = properties2.DisplayAnnotation,
                        Tag = new frmLabelManager.AnnotateLayerPropertiesWrap(properties2, num, true)
                    };
                    this.treeNode_0.Nodes.Add(node);
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

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.txtClassName = new TextEdit();
            this.btnAdd = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtClassName.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 0x48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加标注类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "类名";
            this.txtClassName.EditValue = "";
            this.txtClassName.Location = new Point(0x38, 0x18);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new Size(0x80, 0x17);
            this.txtClassName.TabIndex = 1;
            this.btnAdd.Location = new Point(0xd0, 0x18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x40, 0x18);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            base.Controls.Add(this.groupBox1);
            base.Name = "AddClassCtrl";
            base.Size = new Size(360, 0xe8);
            this.groupBox1.ResumeLayout(false);
            this.txtClassName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private bool method_0(string string_0)
        {
            for (int i = 0; i < this.iannotateLayerPropertiesCollection2_0.Count; i++)
            {
                IAnnotateLayerProperties properties;
                int num2;
                this.iannotateLayerPropertiesCollection2_0.QueryItem(i, out properties, out num2);
                if (properties.Class == string_0)
                {
                    return true;
                }
            }
            return false;
        }

        private int method_1()
        {
            int num2;
            IList list = new ArrayList();
            for (int i = 0; i < this.iannotateLayerPropertiesCollection2_0.Count; i++)
            {
                IAnnotateLayerProperties properties;
                this.iannotateLayerPropertiesCollection2_0.QueryItem(i, out properties, out num2);
                list.Add(num2);
            }
            num2 = 0;
            while (list.IndexOf(num2) != -1)
            {
                num2++;
            }
            return num2;
        }

        public IAnnotateLayerPropertiesCollection2 AnnoLayerPropsColl
        {
            set
            {
                this.iannotateLayerPropertiesCollection2_0 = value;
            }
        }

        public TreeNode Node
        {
            set
            {
                this.treeNode_0 = value;
            }
        }
    }
}

