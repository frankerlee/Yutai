using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerLabelPropertyCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = true;
        private bool bool_3 = false;
        private Container container_0 = null;
        private IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITextSymbol itextSymbol_0 = null;
        private string string_0 = "";

        public LayerLabelPropertyCtrl()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_3)
            {
                int num;
                AnnotateLayerPropertiesWrap wrap;
                this.igeoFeatureLayer_0.DisplayAnnotation = this.bool_2;
                try
                {
                    num = 0;
                    while (num < this.ilist_0.Count)
                    {
                        wrap = this.ilist_0[num] as AnnotateLayerPropertiesWrap;
                        this.iannotateLayerPropertiesCollection2_0.Remove(wrap.ID);
                        num++;
                    }
                }
                catch
                {
                }
                for (num = 0; num < this.cboClass.Properties.Items.Count; num++)
                {
                    wrap = this.cboClass.Properties.Items[num] as AnnotateLayerPropertiesWrap;
                    if (wrap.IsNew)
                    {
                        this.iannotateLayerPropertiesCollection2_0.Add(wrap.AnnotateLayerProperties);
                    }
                    else if (wrap.Update)
                    {
                        this.iannotateLayerPropertiesCollection2_0.Replace(wrap.ID, wrap.AnnotateLayerProperties);
                    }
                }
            }
            return true;
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            frmInput input = new frmInput("类名称:", "");
            if (input.ShowDialog() == DialogResult.OK)
            {
                if (input.InputValue.Trim().Length == 0)
                {
                    MessageBox.Show("非法类名!");
                }
                else
                {
                    for (int i = 0; i < this.cboClass.Properties.Items.Count; i++)
                    {
                        AnnotateLayerPropertiesWrap wrap =
                            this.cboClass.Properties.Items[i] as AnnotateLayerPropertiesWrap;
                        if (wrap.AnnotateLayerProperties.Class == input.InputValue)
                        {
                            MessageBox.Show("类名必须唯一!");
                            return;
                        }
                    }
                    IAnnotateLayerProperties properties = new LabelEngineLayerPropertiesClass
                    {
                        Class = input.InputValue,
                        FeatureLinked = this.iannotateLayerProperties_0.FeatureLinked,
                        FeatureLayer = this.iannotateLayerProperties_0.FeatureLayer,
                        AddUnplacedToGraphicsContainer = this.iannotateLayerProperties_0.AddUnplacedToGraphicsContainer,
                        AnnotationMaximumScale = this.iannotateLayerProperties_0.AnnotationMaximumScale,
                        AnnotationMinimumScale = this.iannotateLayerProperties_0.AnnotationMinimumScale,
                        CreateUnplacedElements = this.iannotateLayerProperties_0.CreateUnplacedElements,
                        DisplayAnnotation = this.iannotateLayerProperties_0.DisplayAnnotation,
                        LabelWhichFeatures = this.iannotateLayerProperties_0.LabelWhichFeatures,
                        Priority = this.iannotateLayerProperties_0.Priority,
                        UseOutput = this.iannotateLayerProperties_0.UseOutput,
                        WhereClause = this.iannotateLayerProperties_0.WhereClause
                    };
                    ILabelEngineLayerProperties properties2 = properties as ILabelEngineLayerProperties;
                    ILabelEngineLayerProperties properties3 =
                        this.iannotateLayerProperties_0 as ILabelEngineLayerProperties;
                    properties2.Expression = properties3.Expression;
                    properties2.IsExpressionSimple = properties3.IsExpressionSimple;
                    int num2 = this.method_2();
                    properties2.SymbolID = num2;
                    properties2.Symbol = (properties3.Symbol as IClone).Clone() as ITextSymbol;
                    this.cboClass.Properties.Items.Add(new AnnotateLayerPropertiesWrap(properties, num2, true));
                    this.bool_3 = true;
                }
            }
        }

        private void btnDeleteClass_Click(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex != -1)
            {
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                if (!selectedItem.IsNew)
                {
                    this.ilist_0.Add(selectedItem);
                    this.bool_3 = true;
                }
                this.cboClass.Properties.Items.RemoveAt(this.cboClass.SelectedIndex);
                this.cboClass.SelectedIndex = 0;
            }
        }

        private void btnEditSymbol_Click(object sender, EventArgs e)
        {
            frmTextSymbolEdit edit = new frmTextSymbolEdit();
            edit.SetSymbol(this.itextSymbol_0 as ISymbol);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                this.itextSymbol_0 = edit.GetSymbol() as ITextSymbol;
                this.symbolItem.Symbol = this.itextSymbol_0;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Symbol = this.itextSymbol_0;
                this.bool_3 = true;
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                selectedItem.Update = true;
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            frmInput input = new frmInput("名称:", this.iannotateLayerProperties_0.Class)
            {
                Text = "输入新类名"
            };
            if (input.ShowDialog() == DialogResult.OK)
            {
                if (input.InputValue.Trim().Length == 0)
                {
                    MessageBox.Show("非法类名!");
                }
                else
                {
                    AnnotateLayerPropertiesWrap selectedItem;
                    for (int i = 0; i < this.cboClass.Properties.Items.Count; i++)
                    {
                        selectedItem = this.cboClass.Properties.Items[i] as AnnotateLayerPropertiesWrap;
                        if (selectedItem.AnnotateLayerProperties.Class == input.InputValue)
                        {
                            MessageBox.Show("类名必须唯一!");
                            return;
                        }
                    }
                    selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                    this.iannotateLayerProperties_0.Class = input.InputValue;
                    this.bool_3 = true;
                    selectedItem.Update = true;
                }
            }
        }

        private void btnScaleSet_Click(object sender, EventArgs e)
        {
            frmAnnoScaleSet set = new frmAnnoScaleSet
            {
                AnnotateLayerProperties = this.iannotateLayerProperties_0
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                selectedItem.Update = true;
                this.bool_3 = true;
            }
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder
            {
                Table = this.igeoFeatureLayer_0.FeatureClass as ITable,
                WhereCaluse = this.iannotateLayerProperties_0.WhereClause
            };
            if (builder.ShowDialog() == DialogResult.OK)
            {
                this.iannotateLayerProperties_0.WhereClause = builder.WhereCaluse;
                this.bool_3 = true;
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                selectedItem.Update = true;
            }
        }

        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedItem != null)
            {
                this.bool_0 = false;
                this.iannotateLayerProperties_0 =
                    (this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap).AnnotateLayerProperties;
                this.string_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression.Trim();
                this.bool_1 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).IsExpressionSimple;
                this.iannotationExpressionEngine_0 =
                    (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).ExpressionParser;
                this.chkLabel.Checked = this.iannotateLayerProperties_0.DisplayAnnotation;
                if (this.string_0.IndexOf("[", 1) != -1)
                {
                    this.cboFields.Enabled = false;
                    this.cboFields.Text = "表达式";
                }
                else
                {
                    IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                    string name = this.string_0.Substring(1, this.string_0.Length - 2).Trim();
                    int index = fields.FindField(name);
                    if (index == -1)
                    {
                        this.cboFields.Enabled = false;
                        this.cboFields.Text = "表达式";
                    }
                    else
                    {
                        this.cboFields.Enabled = true;
                        this.cboFields.Text = fields.get_Field(index).AliasName;
                    }
                }
                this.symbolItem.ScaleRatio = 0.5;
                this.itextSymbol_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Symbol;
                this.symbolItem.Symbol = this.itextSymbol_0;
                this.symbolItem.Invalidate();
                this.btnDeleteClass.Enabled = this.cboClass.Properties.Items.Count > 1;
                this.btnRename.Enabled = true;
                this.btnSQL.Enabled = true;
                this.bool_0 = true;
            }
            else
            {
                this.btnDeleteClass.Enabled = false;
                this.btnRename.Enabled = true;
                this.btnSQL.Enabled = false;
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_3 = true;
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                int index = fields.FindFieldByAliasName(this.cboFields.Text);
                if (index != -1)
                {
                    IField field = fields.get_Field(index);
                    this.string_0 = "[" + field.Name + "]";
                    (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression = this.string_0;
                    AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                    selectedItem.Update = true;
                }
            }
        }

        private void chkLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.iannotateLayerProperties_0.DisplayAnnotation = this.chkLabel.Checked;
                this.bool_3 = true;
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                selectedItem.Update = true;
            }
        }

        private void chkLabelFeature_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_3 = true;
                this.bool_2 = this.chkLabelFeature.Checked;
            }
        }

        private void LayerLabelPropertyCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.igeoFeatureLayer_0 != null)
            {
                if (this.igeoFeatureLayer_0.FeatureClass != null)
                {
                    int num;
                    IFields fields = this.method_1().Fields;
                    this.bool_2 = this.igeoFeatureLayer_0.DisplayAnnotation;
                    this.chkLabelFeature.Checked = this.bool_2;
                    for (num = 0; num < fields.FieldCount; num++)
                    {
                        IField field = fields.get_Field(num);
                        if (((((field.Type == esriFieldType.esriFieldTypeDate) ||
                               (field.Type == esriFieldType.esriFieldTypeDouble)) ||
                              ((field.Type == esriFieldType.esriFieldTypeGlobalID) ||
                               (field.Type == esriFieldType.esriFieldTypeGUID))) ||
                             (((field.Type == esriFieldType.esriFieldTypeInteger) ||
                               (field.Type == esriFieldType.esriFieldTypeOID)) ||
                              ((field.Type == esriFieldType.esriFieldTypeSingle) ||
                               (field.Type == esriFieldType.esriFieldTypeSmallInteger)))) ||
                            (field.Type == esriFieldType.esriFieldTypeString))
                        {
                            this.cboFields.Properties.Items.Add(field.AliasName);
                        }
                    }
                    this.cboClass.Properties.Items.Clear();
                    for (num = 0; num < this.iannotateLayerPropertiesCollection2_0.Count; num++)
                    {
                        IAnnotateLayerProperties properties;
                        int num2;
                        this.iannotateLayerPropertiesCollection2_0.QueryItem(num, out properties, out num2);
                        this.cboClass.Properties.Items.Add(
                            new AnnotateLayerPropertiesWrap((properties as IClone).Clone() as IAnnotateLayerProperties,
                                num2, false));
                    }
                }
                this.bool_0 = true;
                if (this.cboClass.Properties.Items.Count > 0)
                {
                    this.cboClass.SelectedIndex = 0;
                }
            }
        }

        private ITable method_1()
        {
            if (this.igeoFeatureLayer_0 == null)
            {
                return null;
            }
            if (this.igeoFeatureLayer_0 is IDisplayTable)
            {
                return (this.igeoFeatureLayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.igeoFeatureLayer_0 is IAttributeTable)
            {
                return (this.igeoFeatureLayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.igeoFeatureLayer_0 != null)
            {
                return (this.igeoFeatureLayer_0.FeatureClass as ITable);
            }
            return (this.igeoFeatureLayer_0 as ITable);
        }

        private int method_2()
        {
            IList list = new ArrayList();
            for (int i = 0; i < this.cboClass.Properties.Items.Count; i++)
            {
                AnnotateLayerPropertiesWrap wrap = this.cboClass.Properties.Items[i] as AnnotateLayerPropertiesWrap;
                list.Add(wrap.ID);
            }
            int num2 = 0;
            while (list.IndexOf(num2) != -1)
            {
                num2++;
            }
            return num2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmExpressionSet set = new frmExpressionSet
            {
                LabelExpression = this.string_0,
                AnnotationExpressionEngine = this.iannotationExpressionEngine_0,
                IsExpressionSimple = this.bool_1,
                GeoFeatureLayer = this.igeoFeatureLayer_0
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                this.string_0 = set.LabelExpression;
                this.iannotationExpressionEngine_0 = set.AnnotationExpressionEngine;
                this.bool_1 = set.IsExpressionSimple;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression = this.string_0;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).ExpressionParser =
                    this.iannotationExpressionEngine_0;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).IsExpressionSimple = this.bool_1;
                this.bool_3 = true;
                AnnotateLayerPropertiesWrap selectedItem = this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap;
                selectedItem.Update = true;
                int index = this.string_0.IndexOf("[", 1);
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                this.bool_0 = false;
                if (index != -1)
                {
                    this.cboFields.Enabled = false;
                    this.cboFields.Text = "表达式";
                }
                else
                {
                    string name = this.string_0.Substring(1, this.string_0.Length - 2).Trim();
                    index = fields.FindField(name);
                    if (index == -1)
                    {
                        this.cboFields.Enabled = false;
                        this.cboFields.Text = "表达式";
                    }
                    else
                    {
                        this.cboFields.Enabled = true;
                        this.cboFields.Text = fields.get_Field(index).AliasName;
                    }
                }
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set { }
        }

        public IGeoFeatureLayer GeoFeatureLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value;
                this.iannotateLayerPropertiesCollection2_0 =
                    this.igeoFeatureLayer_0.AnnotationProperties as IAnnotateLayerPropertiesCollection2;
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_3; }
        }

        public object SelectItem
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.igeoFeatureLayer_0 != null)
                {
                    this.iannotateLayerPropertiesCollection2_0 =
                        this.igeoFeatureLayer_0.AnnotationProperties as IAnnotateLayerPropertiesCollection2;
                }
            }
        }

        internal partial class AnnotateLayerPropertiesWrap
        {
            private bool bool_0 = true;
            private bool bool_1 = false;
            private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
            private int int_0 = -1;

            internal AnnotateLayerPropertiesWrap(IAnnotateLayerProperties iannotateLayerProperties_1, int int_1,
                bool bool_2)
            {
                this.iannotateLayerProperties_0 = iannotateLayerProperties_1;
                this.bool_0 = bool_2;
                this.int_0 = int_1;
            }

            public override string ToString()
            {
                return this.iannotateLayerProperties_0.Class;
            }

            public IAnnotateLayerProperties AnnotateLayerProperties
            {
                get { return this.iannotateLayerProperties_0; }
            }

            public int ID
            {
                get { return this.int_0; }
            }

            public bool IsNew
            {
                get { return this.bool_0; }
                set { this.bool_0 = value; }
            }

            public bool Update
            {
                get { return this.bool_1; }
                set { this.bool_1 = value; }
            }
        }
    }
}