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
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class PointLabelSetCtrl : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private SimpleButton btnAngles;
        private SimpleButton btnEditSymbol;
        private GroupBox btnLabelExpression;
        private SimpleButton btnRotateField;
        private SimpleButton btnScaleSet;
        private SimpleButton btnSQL;
        private ComboBoxEdit cboFields;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
        private IAnnotationExpressionEngine iannotationExpressionEngine_0 = null;
        private IContainer icontainer_0;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList_0;
        private ITextSymbol itextSymbol_0 = null;
        private Label label1;
        private RadioGroup rdoPointPlacementMethod;
        private SimpleButton simpleButton1;
        private string string_0 = "";
        private SymbolItem symbolItem;

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

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_3);
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

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PointLabelSetCtrl));
            this.btnLabelExpression = new GroupBox();
            this.btnEditSymbol = new SimpleButton();
            this.symbolItem = new SymbolItem();
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.simpleButton1 = new SimpleButton();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnRotateField = new SimpleButton();
            this.btnAngles = new SimpleButton();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.rdoPointPlacementMethod = new RadioGroup();
            this.btnSQL = new SimpleButton();
            this.btnScaleSet = new SimpleButton();
            this.btnLabelExpression.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.rdoPointPlacementMethod.Properties.BeginInit();
            base.SuspendLayout();
            this.btnLabelExpression.Controls.Add(this.btnEditSymbol);
            this.btnLabelExpression.Controls.Add(this.symbolItem);
            this.btnLabelExpression.Location = new Point(8, 80);
            this.btnLabelExpression.Name = "btnLabelExpression";
            this.btnLabelExpression.Size = new Size(360, 0x48);
            this.btnLabelExpression.TabIndex = 5;
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
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 0x40);
            this.groupBox1.TabIndex = 4;
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
            this.groupBox2.Controls.Add(this.btnRotateField);
            this.groupBox2.Controls.Add(this.btnAngles);
            this.groupBox2.Controls.Add(this.imageComboBoxEdit1);
            this.groupBox2.Controls.Add(this.rdoPointPlacementMethod);
            this.groupBox2.Location = new Point(8, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(360, 0x80);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "放值属性";
            this.btnRotateField.Location = new Point(280, 0x60);
            this.btnRotateField.Name = "btnRotateField";
            this.btnRotateField.Size = new Size(0x48, 0x18);
            this.btnRotateField.TabIndex = 5;
            this.btnRotateField.Text = "旋转字段...";
            this.btnRotateField.Click += new EventHandler(this.btnRotateField_Click);
            this.btnAngles.Location = new Point(0x119, 0x40);
            this.btnAngles.Name = "btnAngles";
            this.btnAngles.Size = new Size(0x47, 0x18);
            this.btnAngles.TabIndex = 4;
            this.btnAngles.Text = "角度...";
            this.btnAngles.Click += new EventHandler(this.btnAngles_Click);
            this.imageComboBoxEdit1.EditValue = "001000000";
            this.imageComboBoxEdit1.Location = new Point(8, 0x18);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new object[] { 
                new ImageComboBoxItem("仅在右上", "001000000", 0), new ImageComboBoxItem("", "010000000", 1), new ImageComboBoxItem("", "100000000", 2), new ImageComboBoxItem("", "000001000", 3), new ImageComboBoxItem("", "000000001", 4), new ImageComboBoxItem("", "000000010", 5), new ImageComboBoxItem("", "000000100", 6), new ImageComboBoxItem("", "000100000", 7), new ImageComboBoxItem("", "221000000", 8), new ImageComboBoxItem("", "001002002", 9), new ImageComboBoxItem("", "212000000", 10), new ImageComboBoxItem("", "002001002", 11), new ImageComboBoxItem("", "002002001", 12), new ImageComboBoxItem("", "000000221", 13), new ImageComboBoxItem("", "000000212", 14), new ImageComboBoxItem("", "000000122", 15), 
                new ImageComboBoxItem("", "200200100", 0x10), new ImageComboBoxItem("", "200100200", 0x11), new ImageComboBoxItem("", "100200200", 0x12), new ImageComboBoxItem("", "122000000", 0x13), new ImageComboBoxItem("", "221003003", 20), new ImageComboBoxItem("", "331002002", 0x15), new ImageComboBoxItem("", "002002331", 0x16), new ImageComboBoxItem("", "003003221", 0x17), new ImageComboBoxItem("", "200200133", 0x18), new ImageComboBoxItem("", "300300122", 0x19), new ImageComboBoxItem("", "122300300", 0x1a), new ImageComboBoxItem("", "133200200", 0x1b), new ImageComboBoxItem("", "221302332", 0x1c), new ImageComboBoxItem("", "212202232", 0x1d), new ImageComboBoxItem("", "122203233", 30), new ImageComboBoxItem("", "222103222", 0x1f), 
                new ImageComboBoxItem("", "233203122", 0x20), new ImageComboBoxItem("", "232202212", 0x21), new ImageComboBoxItem("", "332302221", 0x22), new ImageComboBoxItem("", "222301222", 0x23)
             });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList_0;
            this.imageComboBoxEdit1.Properties.SmallImages = this.imageList_0;
            this.imageComboBoxEdit1.Size = new Size(0x68, 0x43);
            this.imageComboBoxEdit1.TabIndex = 3;
            this.imageComboBoxEdit1.SelectedIndexChanged += new EventHandler(this.imageComboBoxEdit1_SelectedIndexChanged);
            this.imageList_0.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList_0.ImageSize = new Size(0x4e, 0x3f);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.rdoPointPlacementMethod.Location = new Point(120, 0x10);
            this.rdoPointPlacementMethod.Name = "rdoPointPlacementMethod";
            this.rdoPointPlacementMethod.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoPointPlacementMethod.Properties.Appearance.Options.UseBackColor = true;
            this.rdoPointPlacementMethod.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoPointPlacementMethod.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在点周围水平偏移标注"), new RadioGroupItem(null, "在点上部放置标注"), new RadioGroupItem(null, "以指定角度放置标注"), new RadioGroupItem(null, "根据字段值设置标注角度") });
            this.rdoPointPlacementMethod.Size = new Size(160, 0x68);
            this.rdoPointPlacementMethod.TabIndex = 2;
            this.rdoPointPlacementMethod.SelectedIndexChanged += new EventHandler(this.rdoPointPlacementMethod_SelectedIndexChanged);
            this.btnSQL.Location = new Point(0x68, 0x128);
            this.btnSQL.Name = "btnSQL";
            this.btnSQL.Size = new Size(0x38, 0x18);
            this.btnSQL.TabIndex = 13;
            this.btnSQL.Text = "SQL查询";
            this.btnSQL.Click += new EventHandler(this.btnSQL_Click);
            this.btnScaleSet.Location = new Point(0x18, 0x128);
            this.btnScaleSet.Name = "btnScaleSet";
            this.btnScaleSet.Size = new Size(0x40, 0x18);
            this.btnScaleSet.TabIndex = 12;
            this.btnScaleSet.Text = "比例范围";
            this.btnScaleSet.Click += new EventHandler(this.btnScaleSet_Click);
            base.Controls.Add(this.btnSQL);
            base.Controls.Add(this.btnScaleSet);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnLabelExpression);
            base.Controls.Add(this.groupBox1);
            base.Name = "PointLabelSetCtrl";
            base.Size = new Size(0x188, 0x150);
            base.Load += new EventHandler(this.PointLabelSetCtrl_Load);
            this.btnLabelExpression.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.imageComboBoxEdit1.Properties.EndInit();
            this.rdoPointPlacementMethod.Properties.EndInit();
            base.ResumeLayout(false);
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

