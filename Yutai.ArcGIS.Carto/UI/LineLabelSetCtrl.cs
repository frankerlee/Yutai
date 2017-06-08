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
    public class LineLabelSetCtrl : UserControl
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
        private CheckEdit chkBelow_Right;
        private CheckEdit chkOnTop;
        private CheckEdit chkTop_Left;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private Label label2;
        private Label label4;
        private RadioGroup rdoLineLabelPosition;
        private string string_0 = "";
        private SymbolItem symbolItem;
        private TextEdit txtOffset;

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

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        private void InitializeComponent()
        {
            this.btnSQL = new SimpleButton();
            this.btnScaleSet = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.label4 = new Label();
            this.txtOffset = new TextEdit();
            this.label2 = new Label();
            this.chkBelow_Right = new CheckEdit();
            this.chkOnTop = new CheckEdit();
            this.chkTop_Left = new CheckEdit();
            this.groupBox4 = new GroupBox();
            this.rdoLineLabelPosition = new RadioGroup();
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.btnExpress = new SimpleButton();
            this.label1 = new Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.txtOffset.Properties.BeginInit();
            this.chkBelow_Right.Properties.BeginInit();
            this.chkOnTop.Properties.BeginInit();
            this.chkTop_Left.Properties.BeginInit();
            this.groupBox4.SuspendLayout();
            this.rdoLineLabelPosition.Properties.BeginInit();
            this.btnLabelExpression.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            base.SuspendLayout();
            this.btnSQL.Location = new Point(0x70, 0x128);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(0x38, 0x18);
            this.btnSQL.TabIndex = 0x12;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.btnScaleSet.Location = new Point(0x20, 0x128);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(0x40, 0x18);
            this.btnScaleSet.TabIndex = 0x11;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new Point(0x10, 0x98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x88);
            this.groupBox2.TabIndex = 0x10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放值属性";
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtOffset);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.chkBelow_Right);
            this.groupBox3.Controls.Add(this.chkOnTop);
            this.groupBox3.Controls.Add(this.chkTop_Left);
            this.groupBox3.Location = new Point(0x90, 0x10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xc0, 0x70);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "位置";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x80, 0x52);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x36, 0x11);
            this.label4.TabIndex = 8;
            this.label4.Text = "地图单位";
            this.txtOffset.EditValue = "0";
            this.txtOffset.Location = new Point(0x3f, 0x4f);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(0x38, 0x17);
            this.txtOffset.TabIndex = 7;
            this.txtOffset.EditValueChanged += new EventHandler(this.txtOffset_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x52);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 0x11);
            this.label2.TabIndex = 6;
            this.label2.Text = "偏移";
            this.chkBelow_Right.Location = new Point(0x10, 0x37);
            this.chkBelow_Right.Name = "chkBelow_Right";
            this.chkBelow_Right.Properties.Caption = "下";
            this.chkBelow_Right.Size = new Size(0x38, 0x13);
            this.chkBelow_Right.TabIndex = 4;
            this.chkBelow_Right.CheckedChanged += new EventHandler(this.chkBelow_Right_CheckedChanged);
            this.chkOnTop.Location = new Point(0x10, 0x23);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Properties.Caption = "在线上";
            this.chkOnTop.Size = new Size(0x38, 0x13);
            this.chkOnTop.TabIndex = 3;
            this.chkOnTop.CheckedChanged += new EventHandler(this.chkOnTop_CheckedChanged);
            this.chkTop_Left.Location = new Point(0x10, 0x10);
            this.chkTop_Left.Name = "chkTop_Left";
            this.chkTop_Left.Properties.Caption = "上";
            this.chkTop_Left.Size = new Size(0x38, 0x13);
            this.chkTop_Left.TabIndex = 2;
            this.chkTop_Left.CheckedChanged += new EventHandler(this.chkTop_Left_CheckedChanged);
            this.groupBox4.Controls.Add(this.rdoLineLabelPosition);
            this.groupBox4.Location = new Point(0x18, 0x10);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x68, 0x70);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "方向";
            this.rdoLineLabelPosition.Location = new Point(8, 0x10);
            this.rdoLineLabelPosition.Name = "rdoLineLabelPosition";
            this.rdoLineLabelPosition.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoLineLabelPosition.Properties.Appearance.Options.UseBackColor = true;
            this.rdoLineLabelPosition.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoLineLabelPosition.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "水平放置"), new RadioGroupItem(null, "平行放置"), new RadioGroupItem(null, "弯曲放置"), new RadioGroupItem(null, "垂直放置") });
            this.rdoLineLabelPosition.Size = new Size(0x58, 80);
            this.rdoLineLabelPosition.TabIndex = 0;
            this.rdoLineLabelPosition.SelectedIndexChanged += new EventHandler(this.rdoLineLabelPosition_SelectedIndexChanged);
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(0x10, 0x49);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(360, 0x48);
            this.btnLabelExpression.TabIndex = 15;
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
            this.groupBox1.Location = new Point(0x10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x40);
            this.groupBox1.TabIndex = 14;
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
            base.Name = "LineLabelSetCtrl";
            base.Size = new Size(0x180, 0x148);
            base.Load += new EventHandler(this.LineLabelSetCtrl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.txtOffset.Properties.EndInit();
            this.chkBelow_Right.Properties.EndInit();
            this.chkOnTop.Properties.EndInit();
            this.chkTop_Left.Properties.EndInit();
            this.groupBox4.ResumeLayout(false);
            this.rdoLineLabelPosition.Properties.EndInit();
            this.btnLabelExpression.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            base.ResumeLayout(false);
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

