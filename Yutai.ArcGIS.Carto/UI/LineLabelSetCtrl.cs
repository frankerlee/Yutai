using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Query.UI;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LineLabelSetCtrl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private Container container_0 = null;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private string string_0 = "";

        public LineLabelSetCtrl()
        {
            this.InitializeComponent();
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
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Symbol = this.itextSymbol_0;
            }
        }

        private void btnExpress_Click(object sender, EventArgs e)
        {
            frmExpressionSet set = new frmExpressionSet {
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
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).ExpressionParser = this.iannotationExpressionEngine_0;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).IsExpressionSimple = this.bool_1;
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

        private void btnScaleSet_Click(object sender, EventArgs e)
        {
            new frmAnnoScaleSet { AnnotateLayerProperties = this.iannotateLayerProperties_0 }.ShowDialog();
        }

        private void btnSQL_Click(object sender, EventArgs e)
        {
            frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
                Table = this.igeoFeatureLayer_0.FeatureClass as ITable,
                WhereCaluse = this.iannotateLayerProperties_0.WhereClause
            };
            if (builder.ShowDialog() == DialogResult.OK)
            {
                this.iannotateLayerProperties_0.WhereClause = builder.WhereCaluse;
            }
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                int index = fields.FindFieldByAliasName(this.cboFields.Text);
                if (index != -1)
                {
                    IField field = fields.get_Field(index);
                    this.string_0 = "[" + field.Name + "]";
                    (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression = this.string_0;
                }
            }
        }

        private void chkBelow_Right_CheckedChanged(object sender, EventArgs e)
        {
            ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
            lineLabelPosition.Right = this.chkBelow_Right.Checked;
            if (!this.chkBelow_Right.Checked)
            {
                lineLabelPosition.Below = false;
            }
            (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
        }

        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
        {
            ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
            lineLabelPosition.OnTop = this.chkOnTop.Checked;
            (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
        }

        private void chkTop_Left_CheckedChanged(object sender, EventArgs e)
        {
            ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
            lineLabelPosition.Left = this.chkTop_Left.Checked;
            if (!this.chkTop_Left.Checked)
            {
                lineLabelPosition.Above = false;
            }
            (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
        }

 private void LineLabelSetCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_2 = true;
        }

        private void method_0()
        {
            if (((this.igeoFeatureLayer_0 != null) && (this.iannotateLayerProperties_0 != null)) && (this.igeoFeatureLayer_0.FeatureClass != null))
            {
                this.bool_0 = false;
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((((field.Type == esriFieldType.esriFieldTypeDate) || (field.Type == esriFieldType.esriFieldTypeDouble)) || ((field.Type == esriFieldType.esriFieldTypeGlobalID) || (field.Type == esriFieldType.esriFieldTypeGUID))) || (((field.Type == esriFieldType.esriFieldTypeInteger) || (field.Type == esriFieldType.esriFieldTypeOID)) || ((field.Type == esriFieldType.esriFieldTypeSingle) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)))) || (field.Type == esriFieldType.esriFieldTypeString))
                    {
                        this.cboFields.Properties.Items.Add(field.AliasName);
                    }
                }
                this.string_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression.Trim();
                this.bool_1 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).IsExpressionSimple;
                this.iannotationExpressionEngine_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).ExpressionParser;
                if (this.string_0.IndexOf("[", 1) != -1)
                {
                    this.cboFields.Enabled = false;
                    this.cboFields.Text = "表达式";
                }
                else
                {
                    fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
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
                ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
                if (lineLabelPosition.Horizontal)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 0;
                }
                else if (lineLabelPosition.Parallel)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 1;
                }
                else if (lineLabelPosition.ProduceCurvedLabels)
                {
                    this.rdoLineLabelPosition.SelectedIndex = 2;
                }
                else
                {
                    this.rdoLineLabelPosition.SelectedIndex = 3;
                }
                this.txtOffset.Text = lineLabelPosition.Offset.ToString();
                this.chkBelow_Right.Checked = lineLabelPosition.Below ? lineLabelPosition.Below : lineLabelPosition.Right;
                this.chkTop_Left.Checked = lineLabelPosition.Above ? lineLabelPosition.Above : lineLabelPosition.Left;
                this.chkOnTop.Checked = lineLabelPosition.OnTop;
                this.bool_0 = true;
            }
        }

        private void rdoLineLabelPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
                lineLabelPosition.Horizontal = this.rdoLineLabelPosition.SelectedIndex == 0;
                lineLabelPosition.Parallel = this.rdoLineLabelPosition.SelectedIndex == 1;
                lineLabelPosition.ProduceCurvedLabels = this.rdoLineLabelPosition.SelectedIndex == 2;
                lineLabelPosition.Perpendicular = this.rdoLineLabelPosition.SelectedIndex == 3;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
            }
        }

        private void txtOffset_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double num = double.Parse(this.txtOffset.Text);
                ILineLabelPosition lineLabelPosition = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition;
                lineLabelPosition.Offset = num;
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.LineLabelPosition = lineLabelPosition;
            }
            catch
            {
            }
        }

        public IAnnotateLayerProperties AnnotateLayerProperties
        {
            set
            {
                this.iannotateLayerProperties_0 = value;
                if (this.bool_2)
                {
                    this.method_0();
                }
            }
        }

        public IGeoFeatureLayer GeoFeatureLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value;
            }
        }
    }
}

