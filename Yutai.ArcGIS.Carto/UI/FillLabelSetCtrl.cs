using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class FillLabelSetCtrl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private SimpleButton btnEditSymbol;
        private SimpleButton btnExpress;
        private GroupBox btnLabelExpression;
        private SimpleButton btnScaleSet;
        private SimpleButton btnSQL;
        private ComboBoxEdit cboFields;
        private CheckEdit chkPlaceOnlyInsidePolygon;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private PictureEdit pictureEdit1;
        private PictureEdit pictureEdit2;
        private PictureEdit pictureEdit3;
        private RadioGroup rdoPolygonPlacementMethod;
        private string string_0 = "";
        private SymbolItem symbolItem;

        public FillLabelSetCtrl()
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

        private void chkPlaceOnlyInsidePolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
                basicOverposterLayerProperties.PlaceOnlyInsidePolygon = this.chkPlaceOnlyInsidePolygon.Checked;
            }
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void FillLabelSetCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_2 = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillLabelSetCtrl));
            this.btnSQL = new SimpleButton();
            this.btnScaleSet = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.chkPlaceOnlyInsidePolygon = new CheckEdit();
            this.pictureEdit3 = new PictureEdit();
            this.pictureEdit2 = new PictureEdit();
            this.pictureEdit1 = new PictureEdit();
            this.rdoPolygonPlacementMethod = new RadioGroup();
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.btnExpress = new SimpleButton();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.chkPlaceOnlyInsidePolygon.Properties.BeginInit();
            this.pictureEdit3.Properties.BeginInit();
            this.pictureEdit2.Properties.BeginInit();
            this.pictureEdit1.Properties.BeginInit();
            this.rdoPolygonPlacementMethod.Properties.BeginInit();
            this.btnLabelExpression.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.btnSQL.Location = new Point(0x58, 0x108);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(0x38, 0x18);
            this.btnSQL.TabIndex = 0x17;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.btnScaleSet.Location = new Point(8, 0x108);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(0x40, 0x18);
            this.btnScaleSet.TabIndex = 0x16;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            this.groupBox2.Controls.Add(this.chkPlaceOnlyInsidePolygon);
            this.groupBox2.Controls.Add(this.pictureEdit3);
            this.groupBox2.Controls.Add(this.pictureEdit2);
            this.groupBox2.Controls.Add(this.pictureEdit1);
            this.groupBox2.Controls.Add(this.rdoPolygonPlacementMethod);
            this.groupBox2.Location = new Point(4, 0x8a);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x76);
            this.groupBox2.TabIndex = 0x15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放值属性";
            this.chkPlaceOnlyInsidePolygon.Location = new Point(0x10, 0x60);
            this.chkPlaceOnlyInsidePolygon.Name = "chkPlaceOnlyInsidePolygon";
            this.chkPlaceOnlyInsidePolygon.Properties.Caption = "只将标注放在多边形内部";
            this.chkPlaceOnlyInsidePolygon.Size = new Size(0xa8, 0x13);
            this.chkPlaceOnlyInsidePolygon.TabIndex = 0x12;
            this.chkPlaceOnlyInsidePolygon.CheckedChanged += new EventHandler(this.chkPlaceOnlyInsidePolygon_CheckedChanged);
            this.pictureEdit3.EditValue = resources.GetObject("pictureEdit3.EditValue");
            this.pictureEdit3.Location = new Point(0xb8, 0x10);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit3.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit3.Size = new Size(0x4a, 0x51);
            this.pictureEdit3.TabIndex = 0x11;
            this.pictureEdit3.Visible = false;
            this.pictureEdit2.EditValue = resources.GetObject("pictureEdit2.EditValue");
            this.pictureEdit2.Location = new Point(0xb8, 0x10);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit2.Size = new Size(0x4a, 0x51);
            this.pictureEdit2.TabIndex = 0x10;
            this.pictureEdit2.Visible = false;
            this.pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            this.pictureEdit1.Location = new Point(0xb8, 0x10);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = PictureSizeMode.Clip;
            this.pictureEdit1.Size = new Size(0x4a, 0x51);
            this.pictureEdit1.TabIndex = 15;
            this.pictureEdit1.Visible = false;
            this.rdoPolygonPlacementMethod.Location = new Point(0x10, 0x10);
            this.rdoPolygonPlacementMethod.Name = "rdoPolygonPlacementMethod";
            this.rdoPolygonPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPolygonPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPolygonPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPolygonPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "总是水平"), new RadioGroupItem(null, "总是垂直"), new RadioGroupItem(null, "尽量先水平，然后在垂直") });
            this.rdoPolygonPlacementMethod.Size = new Size(0xa8, 80);
            this.rdoPolygonPlacementMethod.TabIndex = 14;
            this.rdoPolygonPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPolygonPlacementMethod_SelectedIndexChanged);
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(4, 0x42);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(360, 70);
            this.btnLabelExpression.TabIndex = 20;
            this.btnLabelExpression.TabStop = false;
            this.btnLabelExpression.Text = "文本符号";
            this.btnEditSymbol.Location = new Point(0xe8, 0x18);
            this.btnEditSymbol.Name = "btnEditSymbol";
            this.btnEditSymbol.Size = new Size(0x40, 0x18);
            this.btnEditSymbol.TabIndex = 1;
            this.btnEditSymbol.Text = "符号...";
            this.btnEditSymbol.Click += new EventHandler(this.btnEditSymbol_Click);
            this.symbolItem.BackColor = SystemColors.ControlLight;
            this.symbolItem.Location = new Point(0x10, 0x18);
            this.symbolItem.Name = "symbolItem";
            this.symbolItem.Size = new Size(0xa8, 40);
            this.symbolItem.Symbol = null;
            this.symbolItem.TabIndex = 0;
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.btnExpress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x40);
            this.groupBox1.TabIndex = 0x13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注表达式";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(80, 0x18);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x88, 0x17);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.btnExpress.Location = new Point(240, 0x18);
            this.btnExpress.Name = "btnExpress";
            this.btnExpress.Size = new Size(0x40, 0x18);
            this.btnExpress.TabIndex = 4;
            this.btnExpress.Text = "表达式...";
            this.btnExpress.Click += new EventHandler(this.btnExpress_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "标注字段";
            base.Controls.Add(this.btnSQL);
            base.Controls.Add(this.btnScaleSet);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnLabelExpression);
            base.Controls.Add(this.groupBox1);
            base.Name = "FillLabelSetCtrl";
            base.Size = new Size(0x180, 0x128);
            base.Load += new EventHandler(this.FillLabelSetCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkPlaceOnlyInsidePolygon.Properties.EndInit();
            this.pictureEdit3.Properties.EndInit();
            this.pictureEdit2.Properties.EndInit();
            this.pictureEdit1.Properties.EndInit();
            this.rdoPolygonPlacementMethod.Properties.EndInit();
            this.btnLabelExpression.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
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
                IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
                this.rdoPolygonPlacementMethod.SelectedIndex = (int) basicOverposterLayerProperties.PolygonPlacementMethod;
                this.chkPlaceOnlyInsidePolygon.Checked = basicOverposterLayerProperties.PlaceOnlyInsidePolygon;
                this.rdoPolygonPlacementMethod_SelectedIndexChanged(this, new EventArgs());
                this.bool_0 = true;
            }
        }

        private void rdoPolygonPlacementMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.rdoPolygonPlacementMethod.SelectedIndex)
            {
                case 0:
                    this.pictureEdit1.Visible = true;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = false;
                    break;

                case 1:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = true;
                    this.pictureEdit3.Visible = false;
                    break;

                case 2:
                    this.pictureEdit1.Visible = false;
                    this.pictureEdit2.Visible = false;
                    this.pictureEdit3.Visible = true;
                    break;
            }
            if (this.bool_0)
            {
                IBasicOverposterLayerProperties4 basicOverposterLayerProperties = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).BasicOverposterLayerProperties as IBasicOverposterLayerProperties4;
                basicOverposterLayerProperties.PolygonPlacementMethod = (esriOverposterPolygonPlacementMethod) this.rdoPolygonPlacementMethod.SelectedIndex;
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

