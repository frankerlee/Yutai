namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using stdole;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class AnnoClassSetCtrl : UserControl
    {
        private SimpleButton btnDelete;
        private SimpleButton btnLabelExpress;
        private SimpleButton btnNew;
        private SimpleButton btnReName;
        private System.Windows.Forms.ComboBox cboFontName;
        private System.Windows.Forms.ComboBox cboFontName_rel;
        private ComboBoxEdit cboFontSize;
        private ComboBoxEdit cboFontSize_rel;
        private ComboBoxEdit cboLabelField;
        private CheckBox chkBold;
        private CheckBox chkBold_rel;
        private CheckEdit chkCanPalceOuter;
        private CheckEdit chkHorPlace;
        private CheckBox chkIta_rel;
        private CheckBox chkItalic;
        private CheckEdit chkOverPlace;
        private CheckBox chkUnderline;
        private CheckBox chkUnderLine_rel;
        private ColorEdit colorEdit1;
        private ColorEdit colorEdit2;
        private GroupBox gropScaleset;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupPlaceSet;
        private IContainer icontainer_0;
        private ImageComboBoxEdit imageComboBoxEdit1;
        private ImageList imageList_0;
        private ImageList imageList_1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListBoxControl listBoxControl1;
        private Panel panel1;
        private Panel panel2;
        private Panel panelScaleSet;
        private RadioGroup rdoDisplayScale;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private SimpleButton simpleButton3;
        private SimpleButton simpleButton4;
        private TextEdit textEdit1;
        private TextEdit txtMaxScale;
        private TextEdit txtMinScale;

        public AnnoClassSetCtrl()
        {
            this.InitializeComponent();
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
                this.cboFontName_rel.Items.Add(fonts.Families[i].Name);
            }
        }

        private void AnnoClassSetCtrl_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                int selectedIndex = this.listBoxControl1.SelectedIndex;
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Remove(selectedItem.SymbolIdentifier.ID);
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Remove(selectedItem.SymbolIdentifier.ID);
                if (selectedIndex == 0)
                {
                    this.listBoxControl1.SelectedIndex = selectedIndex;
                }
                else
                {
                    this.listBoxControl1.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void btnLabelExpress_Click(object sender, EventArgs e)
        {
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmInput input = new frmInput("名称:", "") {
                Text = "输入"
            };
            if (input.ShowDialog() == DialogResult.OK)
            {
                if (input.InputValue.Trim().Length == 0)
                {
                    MessageBox.Show("非法类名!");
                }
                else
                {
                    ISymbolIdentifier2 identifier;
                    for (int i = 0; i < this.listBoxControl1.Items.Count; i++)
                    {
                        SymbolIdentifierWrap wrap = this.listBoxControl1.Items[i] as SymbolIdentifierWrap;
                        if (wrap.AnnotateLayerProperties.Class == input.InputValue)
                        {
                            MessageBox.Show("类名必须唯一!");
                            return;
                        }
                    }
                    ITextSymbol symbol = new TextSymbolClass();
                    int symbolID = this.method_4(NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl);
                    NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.set_Symbol(symbolID, symbol as ISymbol);
                    IAnnotateLayerProperties item = new LabelEngineLayerPropertiesClass {
                        Class = input.InputValue,
                        FeatureLinked = NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature,
                        AddUnplacedToGraphicsContainer = false,
                        CreateUnplacedElements = true,
                        DisplayAnnotation = true,
                        UseOutput = true
                    };
                    ILabelEngineLayerProperties properties2 = item as ILabelEngineLayerProperties;
                    if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                    {
                        new AnnotationVBScriptEngineClass();
                        IFeatureClass relatedFeatureClass = NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass;
                        properties2.Expression = "[" + relatedFeatureClass.ObjectClassID + "]";
                        properties2.IsExpressionSimple = true;
                    }
                    properties2.Offset = 0.0;
                    properties2.SymbolID = symbolID;
                    properties2.Symbol = symbol;
                    NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Add(item);
                    NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.GetSymbolIdentifier(symbolID, out identifier);
                    this.listBoxControl1.Items.Add(new SymbolIdentifierWrap(item, identifier));
                }
            }
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                frmInput input = new frmInput("名称:", selectedItem.AnnotateLayerProperties.Class) {
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
                        for (int i = 0; i < this.listBoxControl1.Items.Count; i++)
                        {
                            SymbolIdentifierWrap wrap2 = this.listBoxControl1.Items[i] as SymbolIdentifierWrap;
                            if (wrap2.AnnotateLayerProperties.Class == input.InputValue)
                            {
                                MessageBox.Show("类名必须唯一!");
                                return;
                            }
                        }
                        selectedItem.AnnotateLayerProperties.Class = input.InputValue;
                        this.method_5(selectedItem);
                    }
                }
            }
        }

        private void cboFontName_rel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Name = this.cboFontName.Text;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void cboFontSize_rel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Size = double.Parse(this.cboFontSize.Text);
                this.method_5(selectedItem);
            }
        }

        private void cboLabelField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Bold = this.chkBold.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkBold_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkIta_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Italic = this.chkItalic.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkUnderline_CheckedChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                font.Underline = this.chkUnderline.Checked;
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font = font;
                this.method_5(selectedItem);
            }
        }

        private void chkUnderLine_rel_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void colorEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                IColor color = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Color;
                this.method_3(this.colorEdit1, color);
                (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Color = color;
                this.method_5(selectedItem);
            }
        }

        private void colorEdit2_EditValueChanged(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Init()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
            }
            else
            {
                this.panel2.Visible = false;
                this.panel1.Visible = true;
            }
            if (NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Count == 0)
            {
                ITextSymbol symbol = new TextSymbolClass();
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.set_Symbol(0, symbol as ISymbol);
                IAnnotateLayerProperties item = new LabelEngineLayerPropertiesClass {
                    Class = "要素类 1",
                    FeatureLinked = NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature,
                    AddUnplacedToGraphicsContainer = false,
                    CreateUnplacedElements = true,
                    DisplayAnnotation = true,
                    UseOutput = true
                };
                ILabelEngineLayerProperties properties2 = item as ILabelEngineLayerProperties;
                if (NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                {
                    new AnnotationVBScriptEngineClass();
                    IFeatureClass relatedFeatureClass = NewObjectClassHelper.m_pObjectClassHelper.RelatedFeatureClass;
                    properties2.Expression = "[" + relatedFeatureClass.ObjectClassID + "]";
                    properties2.IsExpressionSimple = true;
                }
                properties2.Offset = 0.0;
                properties2.SymbolID = 0;
                properties2.Symbol = symbol;
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Add(item);
            }
            this.listBoxControl1.Items.Clear();
            for (int i = 0; i < NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Count; i++)
            {
                IAnnotateLayerProperties properties3;
                int num2;
                ISymbolIdentifier2 identifier;
                NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.QueryItem(i, out properties3, out num2);
                NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.GetSymbolIdentifier(num2, out identifier);
                this.listBoxControl1.Items.Add(new SymbolIdentifierWrap(properties3, identifier));
            }
            this.listBoxControl1.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            ResourceManager manager = new ResourceManager(typeof(AnnoClassSetCtrl));
            this.label1 = new Label();
            this.listBoxControl1 = new ListBoxControl();
            this.btnNew = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnReName = new SimpleButton();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.imageList_1 = new ImageList(this.icontainer_0);
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.simpleButton2 = new SimpleButton();
            this.simpleButton1 = new SimpleButton();
            this.cboFontSize = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.chkUnderline = new CheckBox();
            this.chkItalic = new CheckBox();
            this.chkBold = new CheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.gropScaleset = new GroupBox();
            this.panelScaleSet = new Panel();
            this.txtMaxScale = new TextEdit();
            this.txtMinScale = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.rdoDisplayScale = new RadioGroup();
            this.panel2 = new Panel();
            this.btnLabelExpress = new SimpleButton();
            this.cboLabelField = new ComboBoxEdit();
            this.label5 = new Label();
            this.groupBox2 = new GroupBox();
            this.simpleButton3 = new SimpleButton();
            this.simpleButton4 = new SimpleButton();
            this.cboFontSize_rel = new ComboBoxEdit();
            this.colorEdit2 = new ColorEdit();
            this.chkUnderLine_rel = new CheckBox();
            this.chkIta_rel = new CheckBox();
            this.chkBold_rel = new CheckBox();
            this.cboFontName_rel = new System.Windows.Forms.ComboBox();
            this.groupPlaceSet = new GroupBox();
            this.chkCanPalceOuter = new CheckEdit();
            this.chkOverPlace = new CheckEdit();
            this.chkHorPlace = new CheckEdit();
            this.label6 = new Label();
            this.imageComboBoxEdit1 = new ImageComboBoxEdit();
            this.textEdit1 = new TextEdit();
            this.label4 = new Label();
            ((ISupportInitialize) this.listBoxControl1).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cboFontSize.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.gropScaleset.SuspendLayout();
            this.panelScaleSet.SuspendLayout();
            this.txtMaxScale.Properties.BeginInit();
            this.txtMinScale.Properties.BeginInit();
            this.rdoDisplayScale.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.cboLabelField.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.cboFontSize_rel.Properties.BeginInit();
            this.colorEdit2.Properties.BeginInit();
            this.groupPlaceSet.SuspendLayout();
            this.chkCanPalceOuter.Properties.BeginInit();
            this.chkOverPlace.Properties.BeginInit();
            this.chkHorPlace.Properties.BeginInit();
            this.imageComboBoxEdit1.Properties.BeginInit();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2a, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "注记类";
            this.listBoxControl1.ItemHeight = 0x11;
            this.listBoxControl1.Location = new Point(8, 0x20);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new Size(0x130, 0x70);
            this.listBoxControl1.TabIndex = 1;
            this.listBoxControl1.SelectedIndexChanged += new EventHandler(this.listBoxControl1_SelectedIndexChanged);
            this.btnNew.Location = new Point(0x150, 40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(0x30, 0x18);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "新建";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnDelete.Location = new Point(0x150, 0x48);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x30, 0x18);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnReName.Location = new Point(0x150, 0x68);
            this.btnReName.Name = "btnReName";
            this.btnReName.Size = new Size(0x30, 0x18);
            this.btnReName.TabIndex = 4;
            this.btnReName.Text = "重命名";
            this.btnReName.Click += new EventHandler(this.btnReName_Click);
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Magenta;
            this.imageList_1.ImageSize = new Size(0x51, 0x3a);
            this.imageList_1.ImageStream = (ImageListStreamer) manager.GetObject("imageList2.ImageStream");
            this.imageList_1.TransparentColor = Color.Transparent;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.gropScaleset);
            this.panel1.Location = new Point(8, 0x98);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x180, 0x108);
            this.panel1.TabIndex = 8;
            this.groupBox1.Controls.Add(this.simpleButton2);
            this.groupBox1.Controls.Add(this.simpleButton1);
            this.groupBox1.Controls.Add(this.cboFontSize);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.chkUnderline);
            this.groupBox1.Controls.Add(this.chkItalic);
            this.groupBox1.Controls.Add(this.chkBold);
            this.groupBox1.Controls.Add(this.cboFontName);
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x180, 0x58);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文本符号";
            this.simpleButton2.Location = new Point(0x148, 0x30);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x30, 0x18);
            this.simpleButton2.TabIndex = 0x10;
            this.simpleButton2.Text = "引导线";
            this.simpleButton1.Location = new Point(0x110, 0x30);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x30, 0x18);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "符号";
            this.cboFontSize.EditValue = "5";
            this.cboFontSize.Location = new Point(0x138, 0x10);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize.Size = new Size(0x40, 0x17);
            this.cboFontSize.TabIndex = 14;
            this.cboFontSize.SelectedIndexChanged += new EventHandler(this.cboFontSize_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(120, 0x30);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 13;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.chkUnderline.Appearance = Appearance.Button;
            this.chkUnderline.ImageIndex = 2;
            this.chkUnderline.ImageList = this.imageList_0;
            this.chkUnderline.Location = new Point(240, 0x30);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Size = new Size(0x1c, 0x18);
            this.chkUnderline.TabIndex = 7;
            this.chkUnderline.CheckedChanged += new EventHandler(this.chkUnderline_CheckedChanged);
            this.chkItalic.Appearance = Appearance.Button;
            this.chkItalic.ImageIndex = 1;
            this.chkItalic.ImageList = this.imageList_0;
            this.chkItalic.Location = new Point(0xd0, 0x30);
            this.chkItalic.Name = "chkItalic";
            this.chkItalic.Size = new Size(0x1c, 0x18);
            this.chkItalic.TabIndex = 6;
            this.chkItalic.CheckedChanged += new EventHandler(this.chkItalic_CheckedChanged);
            this.chkBold.Appearance = Appearance.Button;
            this.chkBold.ImageIndex = 0;
            this.chkBold.ImageList = this.imageList_0;
            this.chkBold.Location = new Point(0xb0, 0x30);
            this.chkBold.Name = "chkBold";
            this.chkBold.Size = new Size(0x1c, 0x18);
            this.chkBold.TabIndex = 5;
            this.chkBold.CheckedChanged += new EventHandler(this.chkBold_CheckedChanged);
            this.cboFontName.Location = new Point(120, 0x10);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new Size(0xb8, 20);
            this.cboFontName.TabIndex = 2;
            this.cboFontName.SelectedIndexChanged += new EventHandler(this.cboFontName_SelectedIndexChanged);
            this.gropScaleset.Controls.Add(this.panelScaleSet);
            this.gropScaleset.Controls.Add(this.rdoDisplayScale);
            this.gropScaleset.Location = new Point(0, 0x60);
            this.gropScaleset.Name = "gropScaleset";
            this.gropScaleset.Size = new Size(0x180, 160);
            this.gropScaleset.TabIndex = 7;
            this.gropScaleset.TabStop = false;
            this.gropScaleset.Text = "比例范围";
            this.panelScaleSet.Controls.Add(this.txtMaxScale);
            this.panelScaleSet.Controls.Add(this.txtMinScale);
            this.panelScaleSet.Controls.Add(this.label3);
            this.panelScaleSet.Controls.Add(this.label2);
            this.panelScaleSet.Location = new Point(8, 80);
            this.panelScaleSet.Name = "panelScaleSet";
            this.panelScaleSet.Size = new Size(0x170, 0x48);
            this.panelScaleSet.TabIndex = 5;
            this.txtMaxScale.EditValue = "";
            this.txtMaxScale.Location = new Point(80, 0x29);
            this.txtMaxScale.Name = "txtMaxScale";
            this.txtMaxScale.Size = new Size(240, 0x17);
            this.txtMaxScale.TabIndex = 8;
            this.txtMaxScale.EditValueChanged += new EventHandler(this.txtMaxScale_EditValueChanged);
            this.txtMinScale.EditValue = "";
            this.txtMinScale.Location = new Point(80, 9);
            this.txtMinScale.Name = "txtMinScale";
            this.txtMinScale.Size = new Size(240, 0x17);
            this.txtMinScale.TabIndex = 7;
            this.txtMinScale.EditValueChanged += new EventHandler(this.txtMinScale_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0, 0x29);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x42, 0x11);
            this.label3.TabIndex = 6;
            this.label3.Text = "放大超过1:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x42, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "缩小超过1:";
            this.rdoDisplayScale.Location = new Point(8, 0x18);
            this.rdoDisplayScale.Name = "rdoDisplayScale";
            this.rdoDisplayScale.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.rdoDisplayScale.Properties.Appearance.Options.UseBackColor = true;
            this.rdoDisplayScale.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoDisplayScale.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在所有比例尺下都显示"), new RadioGroupItem(null, "不显示注记，当") });
            this.rdoDisplayScale.Size = new Size(0xa8, 0x30);
            this.rdoDisplayScale.TabIndex = 0;
            this.rdoDisplayScale.SelectedIndexChanged += new EventHandler(this.rdoDisplayScale_SelectedIndexChanged);
            this.panel2.Controls.Add(this.btnLabelExpress);
            this.panel2.Controls.Add(this.cboLabelField);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupPlaceSet);
            this.panel2.Location = new Point(8, 0x90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x198, 0x128);
            this.panel2.TabIndex = 9;
            this.btnLabelExpress.Location = new Point(0x130, 8);
            this.btnLabelExpress.Name = "btnLabelExpress";
            this.btnLabelExpress.Size = new Size(0x48, 0x18);
            this.btnLabelExpress.TabIndex = 0x10;
            this.btnLabelExpress.Text = "表达式";
            this.btnLabelExpress.Click += new EventHandler(this.btnLabelExpress_Click);
            this.cboLabelField.EditValue = "";
            this.cboLabelField.Location = new Point(80, 8);
            this.cboLabelField.Name = "cboLabelField";
            this.cboLabelField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelField.Size = new Size(200, 0x17);
            this.cboLabelField.TabIndex = 11;
            this.cboLabelField.SelectedIndexChanged += new EventHandler(this.cboLabelField_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0, 0x10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x36, 0x11);
            this.label5.TabIndex = 10;
            this.label5.Text = "标注字段";
            this.groupBox2.Controls.Add(this.simpleButton3);
            this.groupBox2.Controls.Add(this.simpleButton4);
            this.groupBox2.Controls.Add(this.cboFontSize_rel);
            this.groupBox2.Controls.Add(this.colorEdit2);
            this.groupBox2.Controls.Add(this.chkUnderLine_rel);
            this.groupBox2.Controls.Add(this.chkIta_rel);
            this.groupBox2.Controls.Add(this.chkBold_rel);
            this.groupBox2.Controls.Add(this.cboFontName_rel);
            this.groupBox2.Location = new Point(0, 0x24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x180, 0x54);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文本符号";
            this.simpleButton3.Location = new Point(0x148, 0x30);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x30, 0x18);
            this.simpleButton3.TabIndex = 0x10;
            this.simpleButton3.Text = "引导线";
            this.simpleButton3.Visible = false;
            this.simpleButton4.Location = new Point(0x110, 0x30);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new Size(0x30, 0x18);
            this.simpleButton4.TabIndex = 15;
            this.simpleButton4.Text = "符号";
            this.simpleButton4.Visible = false;
            this.cboFontSize_rel.EditValue = "5";
            this.cboFontSize_rel.Location = new Point(0x138, 0x10);
            this.cboFontSize_rel.Name = "cboFontSize_rel";
            this.cboFontSize_rel.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFontSize_rel.Properties.Items.AddRange(new object[] { 
                "5", "6", "7", "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", 
                "34", "36", "48", "72"
             });
            this.cboFontSize_rel.Size = new Size(0x40, 0x17);
            this.cboFontSize_rel.TabIndex = 14;
            this.cboFontSize_rel.SelectedIndexChanged += new EventHandler(this.cboFontSize_rel_SelectedIndexChanged);
            this.colorEdit2.EditValue = Color.Empty;
            this.colorEdit2.Location = new Point(120, 0x30);
            this.colorEdit2.Name = "colorEdit2";
            this.colorEdit2.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit2.Size = new Size(0x30, 0x17);
            this.colorEdit2.TabIndex = 13;
            this.colorEdit2.EditValueChanged += new EventHandler(this.colorEdit2_EditValueChanged);
            this.chkUnderLine_rel.Appearance = Appearance.Button;
            this.chkUnderLine_rel.ImageIndex = 2;
            this.chkUnderLine_rel.ImageList = this.imageList_0;
            this.chkUnderLine_rel.Location = new Point(240, 0x30);
            this.chkUnderLine_rel.Name = "chkUnderLine_rel";
            this.chkUnderLine_rel.Size = new Size(0x1c, 0x18);
            this.chkUnderLine_rel.TabIndex = 7;
            this.chkUnderLine_rel.CheckedChanged += new EventHandler(this.chkUnderLine_rel_CheckedChanged);
            this.chkIta_rel.Appearance = Appearance.Button;
            this.chkIta_rel.ImageIndex = 1;
            this.chkIta_rel.ImageList = this.imageList_0;
            this.chkIta_rel.Location = new Point(0xd0, 0x30);
            this.chkIta_rel.Name = "chkIta_rel";
            this.chkIta_rel.Size = new Size(0x1c, 0x18);
            this.chkIta_rel.TabIndex = 6;
            this.chkIta_rel.CheckedChanged += new EventHandler(this.chkIta_rel_CheckedChanged);
            this.chkBold_rel.Appearance = Appearance.Button;
            this.chkBold_rel.ImageIndex = 0;
            this.chkBold_rel.ImageList = this.imageList_0;
            this.chkBold_rel.Location = new Point(0xb0, 0x30);
            this.chkBold_rel.Name = "chkBold_rel";
            this.chkBold_rel.Size = new Size(0x1c, 0x18);
            this.chkBold_rel.TabIndex = 5;
            this.chkBold_rel.CheckedChanged += new EventHandler(this.chkBold_rel_CheckedChanged);
            this.cboFontName_rel.Location = new Point(120, 0x10);
            this.cboFontName_rel.Name = "cboFontName_rel";
            this.cboFontName_rel.Size = new Size(0xb8, 20);
            this.cboFontName_rel.TabIndex = 2;
            this.cboFontName_rel.SelectedIndexChanged += new EventHandler(this.cboFontName_rel_SelectedIndexChanged);
            this.groupPlaceSet.Controls.Add(this.chkCanPalceOuter);
            this.groupPlaceSet.Controls.Add(this.chkOverPlace);
            this.groupPlaceSet.Controls.Add(this.chkHorPlace);
            this.groupPlaceSet.Controls.Add(this.label6);
            this.groupPlaceSet.Controls.Add(this.imageComboBoxEdit1);
            this.groupPlaceSet.Controls.Add(this.textEdit1);
            this.groupPlaceSet.Controls.Add(this.label4);
            this.groupPlaceSet.Location = new Point(0, 0x80);
            this.groupPlaceSet.Name = "groupPlaceSet";
            this.groupPlaceSet.Size = new Size(0x180, 0x88);
            this.groupPlaceSet.TabIndex = 8;
            this.groupPlaceSet.TabStop = false;
            this.groupPlaceSet.Text = "放置属性";
            this.chkCanPalceOuter.Location = new Point(0x80, 0x30);
            this.chkCanPalceOuter.Name = "chkCanPalceOuter";
            this.chkCanPalceOuter.Properties.Caption = "可以在外部放置标注";
            this.chkCanPalceOuter.Size = new Size(0x88, 0x13);
            this.chkCanPalceOuter.TabIndex = 9;
            this.chkOverPlace.Location = new Point(0x84, 0x48);
            this.chkOverPlace.Name = "chkOverPlace";
            this.chkOverPlace.Properties.Caption = "堆叠标注";
            this.chkOverPlace.Size = new Size(120, 0x13);
            this.chkOverPlace.TabIndex = 8;
            this.chkHorPlace.Location = new Point(0x80, 0x18);
            this.chkHorPlace.Name = "chkHorPlace";
            this.chkHorPlace.Properties.Caption = "首先尽量水平";
            this.chkHorPlace.Size = new Size(120, 0x13);
            this.chkHorPlace.TabIndex = 7;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x10);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 0x11);
            this.label6.TabIndex = 6;
            this.label6.Text = "位置";
            this.imageComboBoxEdit1.EditValue = 0;
            this.imageComboBoxEdit1.Location = new Point(0x10, 40);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.imageComboBoxEdit1.Properties.Items.AddRange(new object[] { new ImageComboBoxItem("水平", 0, 0), new ImageComboBoxItem("平直", 1, 1), new ImageComboBoxItem("弯曲", 2, 2), new ImageComboBoxItem("水平偏移", 3, 3), new ImageComboBoxItem("边界", 4, 4) });
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList_1;
            this.imageComboBoxEdit1.Size = new Size(0x68, 0x3e);
            this.imageComboBoxEdit1.TabIndex = 5;
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(0x88, 0x68);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(0x88, 0x17);
            this.textEdit1.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x58, 0x70);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 0x11);
            this.label4.TabIndex = 2;
            this.label4.Text = "偏移:";
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnReName);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.listBoxControl1);
            base.Controls.Add(this.label1);
            base.Name = "AnnoClassSetCtrl";
            base.Size = new Size(0x1c8, 0x1c8);
            base.Load += new EventHandler(this.AnnoClassSetCtrl_Load);
            ((ISupportInitialize) this.listBoxControl1).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.cboFontSize.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.gropScaleset.ResumeLayout(false);
            this.panelScaleSet.ResumeLayout(false);
            this.txtMaxScale.Properties.EndInit();
            this.txtMinScale.Properties.EndInit();
            this.rdoDisplayScale.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.cboLabelField.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.cboFontSize_rel.Properties.EndInit();
            this.colorEdit2.Properties.EndInit();
            this.groupPlaceSet.ResumeLayout(false);
            this.chkCanPalceOuter.Properties.EndInit();
            this.chkOverPlace.Properties.EndInit();
            this.chkHorPlace.Properties.EndInit();
            this.imageComboBoxEdit1.Properties.EndInit();
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.listBoxControl1.ItemCount > 1) && (this.listBoxControl1.SelectedIndex != -1))
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
            if (this.listBoxControl1.SelectedIndex == -1)
            {
                this.groupBox1.Enabled = false;
            }
            else
            {
                int num;
                this.groupBox1.Enabled = true;
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                ITextSymbol symbol = selectedItem.SymbolIdentifier.Symbol as ITextSymbol;
                stdole.IFontDisp font = (selectedItem.SymbolIdentifier.Symbol as ITextSymbol).Font;
                if (!NewObjectClassHelper.m_pObjectClassHelper.IsRelatedFeature)
                {
                    if ((selectedItem.AnnotateLayerProperties.AnnotationMaximumScale != 0.0) || (selectedItem.AnnotateLayerProperties.AnnotationMinimumScale != 0.0))
                    {
                        this.rdoDisplayScale.SelectedIndex = 1;
                        this.panelScaleSet.Enabled = true;
                        this.txtMaxScale.Text = selectedItem.AnnotateLayerProperties.AnnotationMaximumScale.ToString();
                        this.txtMinScale.Text = selectedItem.AnnotateLayerProperties.AnnotationMinimumScale.ToString();
                    }
                    else
                    {
                        this.rdoDisplayScale.SelectedIndex = 0;
                        this.panelScaleSet.Enabled = false;
                    }
                    this.method_0(this.colorEdit1, symbol.Color);
                    num = 0;
                    while (num < this.cboFontName.Items.Count)
                    {
                        if (symbol.Font.Name == this.cboFontName.Items[num].ToString())
                        {
                            this.cboFontName.SelectedIndex = num;
                            break;
                        }
                        num++;
                    }
                }
                else
                {
                    this.method_0(this.colorEdit2, symbol.Color);
                    for (num = 0; num < this.cboFontName_rel.Items.Count; num++)
                    {
                        if (symbol.Font.Name == this.cboFontName_rel.Items[num].ToString())
                        {
                            this.cboFontName_rel.SelectedIndex = num;
                            break;
                        }
                    }
                    this.cboFontSize_rel.Text = symbol.Size.ToString();
                    this.chkBold_rel.Checked = font.Bold;
                    this.chkIta_rel.Checked = font.Italic;
                    this.chkUnderLine_rel.Checked = font.Underline;
                    return;
                }
                this.cboFontSize.Text = symbol.Size.ToString();
                this.chkBold.Checked = font.Bold;
                this.chkItalic.Checked = font.Italic;
                this.chkUnderline.Checked = font.Underline;
            }
        }

        private void method_0(ColorEdit colorEdit_0, IColor icolor_0)
        {
            if (icolor_0.NullColor)
            {
                colorEdit_0.Color = Color.Empty;
            }
            else
            {
                int num2;
                int num3;
                int num4;
                int rGB = icolor_0.RGB;
                this.method_1((uint) rGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
            }
        }

        private void method_1(uint uint_0, out int int_0, out int int_1, out int int_2)
        {
            uint num = uint_0 & 0xff0000;
            int_2 = (int) (num >> 0x10);
            num = uint_0 & 0xff00;
            int_1 = (int) (num >> 8);
            num = uint_0 & 0xff;
            int_0 = (int) num;
        }

        private int method_2(int int_0, int int_1, int int_2)
        {
            uint num = 0;
            num = (uint) (0 | int_2);
            num = num << 8;
            num |= (uint) int_1;
            num = num << 8;
            num |= (uint) int_0;
            return (int) num;
        }

        private void method_3(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = this.method_2(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private int method_4(ISymbolCollection2 isymbolCollection2_0)
        {
            isymbolCollection2_0.Reset();
            ISymbolIdentifier2 identifier = isymbolCollection2_0.Next() as ISymbolIdentifier2;
            IList list = new ArrayList();
            while (identifier != null)
            {
                list.Add(identifier.ID);
                identifier = isymbolCollection2_0.Next() as ISymbolIdentifier2;
            }
            int num = 0;
            if (list.Count != 0)
            {
                while (list.IndexOf(num) != -1)
                {
                    num++;
                }
                return num;
            }
            return num;
        }

        private void method_5(SymbolIdentifierWrap symbolIdentifierWrap_0)
        {
            NewObjectClassHelper.m_pObjectClassHelper.m_pSymbolColl.Replace(symbolIdentifierWrap_0.SymbolIdentifier.ID, symbolIdentifierWrap_0.SymbolIdentifier.Symbol);
            NewObjectClassHelper.m_pObjectClassHelper.m_pAnnoPropertiesColn.Replace(this.listBoxControl1.SelectedIndex, symbolIdentifierWrap_0.AnnotateLayerProperties);
        }

        private void rdoDisplayScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            SymbolIdentifierWrap selectedItem;
            if (this.rdoDisplayScale.SelectedIndex == 0)
            {
                this.panelScaleSet.Enabled = false;
                if (this.listBoxControl1.SelectedIndex != -1)
                {
                    selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                    if ((selectedItem.AnnotateLayerProperties.AnnotationMaximumScale != 0.0) || (selectedItem.AnnotateLayerProperties.AnnotationMinimumScale != 0.0))
                    {
                        selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = 0.0;
                        selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = 0.0;
                        this.method_5(selectedItem);
                    }
                }
            }
            else
            {
                this.panelScaleSet.Enabled = true;
                if (this.listBoxControl1.SelectedIndex != -1)
                {
                    selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                    double num = 0.0;
                    double num2 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtMinScale.Text);
                        num2 = double.Parse(this.txtMaxScale.Text);
                    }
                    catch
                    {
                    }
                    if ((num != 0.0) || (num2 != 0.0))
                    {
                        selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = num2;
                        selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = num;
                        this.method_5(selectedItem);
                    }
                }
            }
        }

        private void txtMaxScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtMaxScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                }
                if (num != 0.0)
                {
                    selectedItem.AnnotateLayerProperties.AnnotationMaximumScale = num;
                    this.method_5(selectedItem);
                }
            }
        }

        private void txtMinScale_EditValueChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedIndex != -1)
            {
                SymbolIdentifierWrap selectedItem = this.listBoxControl1.SelectedItem as SymbolIdentifierWrap;
                double num = 0.0;
                try
                {
                    num = double.Parse(this.txtMinScale.Text);
                }
                catch
                {
                    MessageBox.Show("请输入数字!");
                }
                if (num != 0.0)
                {
                    selectedItem.AnnotateLayerProperties.AnnotationMinimumScale = num;
                    this.method_5(selectedItem);
                }
            }
        }

        internal class SymbolIdentifierWrap
        {
            private IAnnotateLayerProperties iannotateLayerProperties_0 = null;
            private ISymbolIdentifier2 isymbolIdentifier2_0 = null;

            public SymbolIdentifierWrap(IAnnotateLayerProperties iannotateLayerProperties_1, ISymbolIdentifier2 isymbolIdentifier2_1)
            {
                this.iannotateLayerProperties_0 = iannotateLayerProperties_1;
                this.isymbolIdentifier2_0 = isymbolIdentifier2_1;
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

            public ISymbolIdentifier2 SymbolIdentifier
            {
                get
                {
                    return this.isymbolIdentifier2_0;
                }
            }
        }
    }
}

