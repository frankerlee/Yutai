using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
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
    internal partial class PointLabelSetCtrl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private string string_0 = "";

        public PointLabelSetCtrl()
        {
            this.InitializeComponent();
        }

        private void btnAngles_Click(object sender, EventArgs e)
        {
            IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
            frmLabelRotateAngles angles = new frmLabelRotateAngles {
                Angles = basicOverposterLayerProperties.PointPlacementAngles
            };
            if (angles.ShowDialog() == DialogResult.OK)
            {
                basicOverposterLayerProperties.PointPlacementAngles = angles.Angles;
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
                (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Symbol = this.itextSymbol_0;
            }
        }

        private void btnRotateField_Click(object sender, EventArgs e)
        {
            IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
            frmLableRotateField field = new frmLableRotateField {
                m_pFields = this.igeoFeatureLayer_0.FeatureClass.Fields,
                m_PerpendicularToAngle = basicOverposterLayerProperties.PerpendicularToAngle,
                m_RotationType = basicOverposterLayerProperties.RotationType,
                m_RoteteField = basicOverposterLayerProperties.RotationField
            };
            if (field.ShowDialog() == DialogResult.OK)
            {
                basicOverposterLayerProperties.PerpendicularToAngle = field.m_PerpendicularToAngle;
                basicOverposterLayerProperties.RotationType = field.m_RotationType;
                basicOverposterLayerProperties.RotationField = field.m_RoteteField;
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

 private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
                IPointPlacementPriorities pointPlacementPriorities = basicOverposterLayerProperties.PointPlacementPriorities;
                ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[this.imageComboBoxEdit1.SelectedIndex];
                this.method_0(item.Value.ToString(), pointPlacementPriorities);
                basicOverposterLayerProperties.PointPlacementPriorities = pointPlacementPriorities;
            }
        }

 private void method_0(string string_1, IPointPlacementPriorities ipointPlacementPriorities_0)
        {
            ipointPlacementPriorities_0.AboveLeft = this.method_3(string_1[0]);
            ipointPlacementPriorities_0.AboveCenter = this.method_3(string_1[1]);
            ipointPlacementPriorities_0.AboveRight = this.method_3(string_1[2]);
            ipointPlacementPriorities_0.CenterLeft = this.method_3(string_1[3]);
            ipointPlacementPriorities_0.CenterRight = this.method_3(string_1[5]);
            ipointPlacementPriorities_0.BelowLeft = this.method_3(string_1[6]);
            ipointPlacementPriorities_0.BelowCenter = this.method_3(string_1[7]);
            ipointPlacementPriorities_0.BelowRight = this.method_3(string_1[8]);
        }

        private int method_1(int int_0)
        {
            if (int_0 > 3)
            {
                return 3;
            }
            return int_0;
        }

        private string method_2(IPointPlacementPriorities ipointPlacementPriorities_0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.method_1(ipointPlacementPriorities_0.AboveLeft));
            builder.Append(this.method_1(ipointPlacementPriorities_0.AboveCenter));
            builder.Append(this.method_1(ipointPlacementPriorities_0.AboveRight));
            builder.Append(this.method_1(ipointPlacementPriorities_0.CenterLeft));
            builder.Append(this.method_1(0));
            builder.Append(this.method_1(ipointPlacementPriorities_0.CenterRight));
            builder.Append(this.method_1(ipointPlacementPriorities_0.BelowLeft));
            builder.Append(this.method_1(ipointPlacementPriorities_0.BelowCenter));
            builder.Append(this.method_1(ipointPlacementPriorities_0.BelowRight));
            return builder.ToString();
        }

        private int method_3(char char_0)
        {
            switch (char_0)
            {
                case '0':
                    return 0;

                case '1':
                    return 1;

                case '2':
                    return 2;

                case '3':
                    return 3;

                case '4':
                    return 4;

                case '5':
                    return 5;

                case '6':
                    return 6;

                case '7':
                    return 7;

                case '8':
                    return 8;

                case '9':
                    return 9;
            }
            return 0;
        }

        private void method_4()
        {
            if (((this.igeoFeatureLayer_0 != null) && (this.iannotateLayerProperties_0 != null)) && (this.igeoFeatureLayer_0.FeatureClass != null))
            {
                int num;
                this.bool_0 = false;
                IFields fields = this.igeoFeatureLayer_0.FeatureClass.Fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    IField field = fields.get_Field(num);
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
                this.rdoPointPlacementMethod.SelectedIndex = (int) (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.PointPlacementMethod;
                if (this.rdoPointPlacementMethod.SelectedIndex != 0)
                {
                    if (this.rdoPointPlacementMethod.SelectedIndex == 2)
                    {
                        this.btnAngles.Enabled = true;
                        this.btnRotateField.Enabled = false;
                        this.imageComboBoxEdit1.Enabled = false;
                    }
                    else if (this.rdoPointPlacementMethod.SelectedIndex == 3)
                    {
                        this.btnAngles.Enabled = false;
                        this.btnRotateField.Enabled = true;
                        this.imageComboBoxEdit1.Enabled = false;
                    }
                    else
                    {
                        this.btnAngles.Enabled = false;
                        this.btnRotateField.Enabled = false;
                        this.imageComboBoxEdit1.Enabled = false;
                    }
                }
                else
                {
                    IPointPlacementPriorities pointPlacementPriorities = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties.PointPlacementPriorities;
                    string str2 = this.method_2(pointPlacementPriorities);
                    for (num = 0; num < this.imageComboBoxEdit1.Properties.Items.Count; num++)
                    {
                        ImageComboBoxItem item = this.imageComboBoxEdit1.Properties.Items[num];
                        if (item.Value.ToString() == str2)
                        {
                            this.imageComboBoxEdit1.SelectedIndex = num;
                            break;
                        }
                    }
                    this.btnAngles.Enabled = false;
                    this.btnRotateField.Enabled = false;
                    this.imageComboBoxEdit1.Enabled = true;
                }
                this.bool_0 = true;
            }
        }

        private void PointLabelSetCtrl_Load(object sender, EventArgs e)
        {
            this.bool_2 = true;
            this.method_4();
        }

        private void rdoPointPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPointPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.imageComboBoxEdit1.Enabled = true;
                    this.btnRotateField.Enabled = false;
                    this.btnAngles.Enabled = false;
                    break;

                case 1:
                    this.imageComboBoxEdit1.Enabled = false;
                    this.btnRotateField.Enabled = false;
                    this.btnAngles.Enabled = false;
                    break;

                case 2:
                    this.imageComboBoxEdit1.Enabled = false;
                    this.btnRotateField.Enabled = false;
                    this.btnAngles.Enabled = true;
                    break;

                case 3:
                    this.imageComboBoxEdit1.Enabled = false;
                    this.btnRotateField.Enabled = true;
                    this.btnAngles.Enabled = false;
                    break;
            }
            if (this.bool_0)
            {
                IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
                basicOverposterLayerProperties.PointPlacementMethod = (esriOverposterPointPlacementMethod) this.rdoPointPlacementMethod.SelectedIndex;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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

        public IAnnotateLayerProperties AnnotateLayerProperties
        {
            set
            {
                this.iannotateLayerProperties_0 = value;
                if (this.bool_2)
                {
                    this.method_4();
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

