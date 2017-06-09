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
    public class LayerLabelPropertyCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = true;
        private bool bool_3 = false;
        private SimpleButton btnAddClass;
        private SimpleButton btnDeleteClass;
        private SimpleButton btnEditSymbol;
        private GroupBox btnLabelExpression;
        private SimpleButton btnRename;
        private SimpleButton btnScaleSet;
        private SimpleButton btnSQL;
        private ComboBoxEdit cboClass;
        private ComboBoxEdit cboFields;
        private CheckEdit chkLabel;
        private CheckEdit chkLabelFeature;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IAnnotateLayerProperties iannotateLayerProperties_0;
        private IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private Label label2;
        private SimpleButton simpleButton1;
        private string string_0 = "";
        private SymbolItem symbolItem;

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
                        AnnotateLayerPropertiesWrap wrap = this.cboClass.Properties.Items[i] as AnnotateLayerPropertiesWrap;
                        if (wrap.AnnotateLayerProperties.Class == input.InputValue)
                        {
                            MessageBox.Show("类名必须唯一!");
                            return;
                        }
                    }
                    IAnnotateLayerProperties properties = new LabelEngineLayerPropertiesClass {
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
                    ILabelEngineLayerProperties properties3 = this.iannotateLayerProperties_0 as ILabelEngineLayerProperties;
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
            frmInput input = new frmInput("名称:", this.iannotateLayerProperties_0.Class) {
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
            frmAnnoScaleSet set = new frmAnnoScaleSet {
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
            frmAttributeQueryBuilder builder = new frmAttributeQueryBuilder {
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
                this.iannotateLayerProperties_0 = (this.cboClass.SelectedItem as AnnotateLayerPropertiesWrap).AnnotateLayerProperties;
                this.string_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).Expression.Trim();
                this.bool_1 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).IsExpressionSimple;
                this.iannotationExpressionEngine_0 = (this.iannotateLayerProperties_0 as ILabelEngineLayerProperties).ExpressionParser;
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

        protected override void Dispose(bool bool_4)
        {
            if (bool_4 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_4);
        }

        private void InitializeComponent()
        {
            this.chkLabelFeature = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.simpleButton1 = new SimpleButton();
            this.label1 = new Label();
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.label2 = new Label();
            this.cboClass = new ComboBoxEdit();
            this.chkLabel = new CheckEdit();
            this.btnAddClass = new SimpleButton();
            this.btnDeleteClass = new SimpleButton();
            this.btnRename = new SimpleButton();
            this.btnSQL = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.btnScaleSet = new SimpleButton();
            this.chkLabelFeature.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.btnLabelExpression.SuspendLayout();
            this.cboClass.Properties.BeginInit();
            this.chkLabel.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.chkLabelFeature.Location = new Point(0x10, 8);
            this.chkLabelFeature.Name = "chkLabelFeature";
            this.chkLabelFeature.Properties.Caption = "标注要素";
            this.chkLabelFeature.Size = new Size(0x58, 0x13);
            this.chkLabelFeature.TabIndex = 0;
            this.chkLabelFeature.CheckedChanged += new EventHandler(this.chkLabelFeature_CheckedChanged);
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(320, 0x40);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标注表达式";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(80, 0x18);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(0x88, 0x17);
            this.cboFields.TabIndex = 5;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.simpleButton1.Location = new Point(240, 0x18);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x40, 0x18);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "表达式...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "标注字段";
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(0x10, 0xa8);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(320, 0x48);
            this.btnLabelExpression.TabIndex = 3;
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
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x17, 0x11);
            this.label2.TabIndex = 4;
            this.label2.Text = "类:";
            this.cboClass.EditValue = "";
            this.cboClass.Location = new Point(0x30, 0x20);
            this.cboClass.Name = "cboClass";
            this.cboClass.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboClass.Size = new Size(0x88, 0x17);
            this.cboClass.TabIndex = 6;
            this.cboClass.SelectedIndexChanged += new EventHandler(this.cboClass_SelectedIndexChanged);
            this.chkLabel.Location = new Point(0xc0, 0x20);
            this.chkLabel.Name = "chkLabel";
            this.chkLabel.Properties.Caption = "标注这个类的要素";
            this.chkLabel.Size = new Size(0x80, 0x13);
            this.chkLabel.TabIndex = 7;
            this.chkLabel.CheckedChanged += new EventHandler(this.chkLabel_CheckedChanged);
            this.btnAddClass.Location = new Point(0x10, 0x40);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new Size(0x38, 0x18);
            this.btnAddClass.TabIndex = 8;
            this.btnAddClass.Text = "添加...";
            this.btnAddClass.Click += new EventHandler(this.btnAddClass_Click);
            this.btnDeleteClass.Location = new Point(80, 0x40);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new Size(0x38, 0x18);
            this.btnDeleteClass.TabIndex = 9;
            this.btnDeleteClass.Text = "删除";
            this.btnDeleteClass.Click += new EventHandler(this.btnDeleteClass_Click);
            this.btnRename.Location = new Point(0x90, 0x40);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new Size(0x38, 0x18);
            this.btnRename.TabIndex = 10;
            this.btnRename.Text = "重命名";
            this.btnRename.Click += new EventHandler(this.btnRename_Click);
            this.btnSQL.Location = new Point(0xd0, 0x40);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(0x38, 0x18);
            this.btnSQL.TabIndex = 11;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.groupBox2.Controls.Add(this.btnScaleSet);
            this.groupBox2.Location = new Point(0x10, 0xf8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xd8, 0x30);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他选项";
            this.btnScaleSet.Location = new Point(0x30, 0x10);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(0x40, 0x18);
            this.btnScaleSet.TabIndex = 0;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnSQL);
            base.Controls.Add(this.btnRename);
            base.Controls.Add(this.btnDeleteClass);
            base.Controls.Add(this.btnAddClass);
            base.Controls.Add(this.chkLabel);
            base.Controls.Add(this.cboClass);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnLabelExpression);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.chkLabelFeature);
            base.Name = "LayerLabelPropertyCtrl";
            base.Size = new Size(0x198, 0x138);
            base.Load += new EventHandler(this.LayerLabelPropertyCtrl_Load);
            this.chkLabelFeature.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.btnLabelExpression.ResumeLayout(false);
            this.cboClass.Properties.EndInit();
            this.chkLabel.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
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
                        if (((((field.Type == esriFieldType.esriFieldTypeDate) || (field.Type == esriFieldType.esriFieldTypeDouble)) || ((field.Type == esriFieldType.esriFieldTypeGlobalID) || (field.Type == esriFieldType.esriFieldTypeGUID))) || (((field.Type == esriFieldType.esriFieldTypeInteger) || (field.Type == esriFieldType.esriFieldTypeOID)) || ((field.Type == esriFieldType.esriFieldTypeSingle) || (field.Type == esriFieldType.esriFieldTypeSmallInteger)))) || (field.Type == esriFieldType.esriFieldTypeString))
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
                        this.cboClass.Properties.Items.Add(new AnnotateLayerPropertiesWrap((properties as IClone).Clone() as IAnnotateLayerProperties, num2, false));
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
            set
            {
            }
        }

        public IGeoFeatureLayer GeoFeatureLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value;
                this.iannotateLayerPropertiesCollection2_0 = this.igeoFeatureLayer_0.AnnotationProperties as IAnnotateLayerPropertiesCollection2;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_3;
            }
        }

        public object SelectItem
        {
            set
            {
                this.igeoFeatureLayer_0 = value as IGeoFeatureLayer;
                if (this.igeoFeatureLayer_0 != null)
                {
                    this.iannotateLayerPropertiesCollection2_0 = this.igeoFeatureLayer_0.AnnotationProperties as IAnnotateLayerPropertiesCollection2;
                }
            }
        }

        internal class AnnotateLayerPropertiesWrap
        {
            private bool bool_0 = true;
            private bool bool_1 = false;
            private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
            private int int_0 = -1;

            internal AnnotateLayerPropertiesWrap(IAnnotateLayerProperties iannotateLayerProperties_1, int int_1, bool bool_2)
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
                get
                {
                    return this.iannotateLayerProperties_0;
                }
            }

            public int ID
            {
                get
                {
                    return this.int_0;
                }
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public bool Update
            {
                get
                {
                    return this.bool_1;
                }
                set
                {
                    this.bool_1 = value;
                }
            }
        }
    }
}

