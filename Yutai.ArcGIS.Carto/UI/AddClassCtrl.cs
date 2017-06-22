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
    internal partial class AddClassCtrl : UserControl
    {
        private Container container_0 = null;
        private IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0 = null;
        private TreeNode treeNode_0 = null;

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

