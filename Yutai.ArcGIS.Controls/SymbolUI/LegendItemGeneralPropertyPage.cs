using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public class LegendItemGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private SimpleButton btnDescriptionSymbol;
        private SimpleButton btnLabelSymbol;
        private SimpleButton btnLayerNameSymbol;
        private SimpleButton btnTitleSymbol;
        private StyleComboBox cboAreaPatches;
        private StyleComboBox cboLinePatches;
        private CheckEdit chkOveralpDefaultPatch;
        private CheckEdit chkOveralpDefaultPatchSize;
        private CheckEdit chkShowDescription;
        private CheckEdit chkShowLabel;
        private CheckEdit chkShowLayerName;
        private CheckEdit chkShowTitle;
        private IContainer components;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ITextSymbol m_pDescriptionSymbol = null;
        private ITextSymbol m_pHeadingSymbol = null;
        private ITextSymbol m_pLabelSymbol = null;
        private ITextSymbol m_pLayerNameSymbol = null;
        private IStyleGallery m_pSG ;//= ApplicationBase.StyleGallery;
        private string m_Title = "常规";
        private TextEdit txtDefaultPatchHeight;
        private TextEdit txtDefaultPatchWidth;
        private IAppContext _context;

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public LegendItemGeneralPropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context = context;
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = true;
            }
        }

        private void btnDescriptionSymbol_Click(object sender, EventArgs e)
        {
            if (LegendItemArrangementPropertyPage.m_pLegendItem != null)
            {
                this.SelectTextSymbol(ref this.m_pDescriptionSymbol);
                if (this.chkShowDescription.Checked)
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.DescriptionSymbol = this.m_pDescriptionSymbol;
                }
            }
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            if (LegendItemArrangementPropertyPage.m_pLegendItem != null)
            {
                this.SelectTextSymbol(ref this.m_pLabelSymbol);
                if (this.chkShowLabel.Checked)
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LabelSymbol = this.m_pLabelSymbol;
                }
            }
        }

        private void btnLayerNameSymbol_Click(object sender, EventArgs e)
        {
            if (LegendItemArrangementPropertyPage.m_pLegendItem != null)
            {
                this.SelectTextSymbol(ref this.m_pLayerNameSymbol);
                if (this.chkShowLayerName.Checked)
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.LayerNameSymbol = this.m_pLayerNameSymbol;
                }
            }
        }

        private void btnTitleSymbol_Click(object sender, EventArgs e)
        {
            if (LegendItemArrangementPropertyPage.m_pLegendItem != null)
            {
                this.SelectTextSymbol(ref this.m_pHeadingSymbol);
                if (this.chkShowTitle.Checked)
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.HeadingSymbol = this.m_pHeadingSymbol;
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboLinePatches_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkOveralpDefaultPatch_CheckedChanged(object sender, EventArgs e)
        {
            this.cboLinePatches.Enabled = this.chkOveralpDefaultPatch.Checked;
            this.cboAreaPatches.Enabled = this.chkOveralpDefaultPatch.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                if (this.chkOveralpDefaultPatch.Checked)
                {
                    IStyleGalleryItem selectStyleGalleryItem = null;
                    selectStyleGalleryItem = this.cboLinePatches.GetSelectStyleGalleryItem();
                    if (selectStyleGalleryItem != null)
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch = selectStyleGalleryItem.Item as ILinePatch;
                    }
                    else
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch = null;
                    }
                    selectStyleGalleryItem = this.cboAreaPatches.GetSelectStyleGalleryItem();
                    if (selectStyleGalleryItem != null)
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch = selectStyleGalleryItem.Item as IAreaPatch;
                    }
                    else
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch = null;
                    }
                }
                else
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch = null;
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch = null;
                }
                this.ValueChanged();
            }
        }

        private void chkOveralpDefaultPatchSize_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDefaultPatchWidth.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            this.txtDefaultPatchHeight.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                if (this.chkOveralpDefaultPatchSize.Checked)
                {
                    try
                    {
                        double num = double.Parse(this.txtDefaultPatchWidth.Text);
                        double num2 = double.Parse(this.txtDefaultPatchHeight.Text);
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchWidth = num;
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight = num2;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchWidth = 0.0;
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight = 0.0;
                }
                this.ValueChanged();
            }
        }

        private void chkShowDescription_CheckedChanged(object sender, EventArgs e)
        {
            this.btnDescriptionSymbol.Enabled = this.chkShowDescription.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                LegendItemArrangementPropertyPage.m_pLegendItem.ShowDescriptions = this.chkShowDescription.Checked;
                this.ValueChanged();
            }
        }

        private void chkShowLabel_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLabelSymbol.Enabled = this.chkShowLabel.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                LegendItemArrangementPropertyPage.m_pLegendItem.ShowLabels = this.chkShowLabel.Checked;
                this.ValueChanged();
            }
        }

        private void chkShowLayerName_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLayerNameSymbol.Enabled = this.chkShowLayerName.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                LegendItemArrangementPropertyPage.m_pLegendItem.ShowLayerName = this.chkShowLayerName.Checked;
                this.ValueChanged();
            }
        }

        private void chkShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            this.btnTitleSymbol.Enabled = this.chkShowTitle.Checked;
            if (this.m_CanDo && (LegendItemArrangementPropertyPage.m_pLegendItem != null))
            {
                LegendItemArrangementPropertyPage.m_pLegendItem.ShowHeading = this.chkShowTitle.Checked;
                this.ValueChanged();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Hide()
        {
        }

        private void Init()
        {
            if (LegendItemArrangementPropertyPage.m_pLegendItem != null)
            {
                this.m_pLayerNameSymbol = LegendItemArrangementPropertyPage.m_pLegendItem.LayerNameSymbol;
                this.m_pHeadingSymbol = LegendItemArrangementPropertyPage.m_pLegendItem.HeadingSymbol;
                this.m_pLabelSymbol = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LabelSymbol;
                this.m_pDescriptionSymbol = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.DescriptionSymbol;
                this.chkShowLayerName.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowLayerName;
                this.btnLayerNameSymbol.Enabled = this.chkShowLayerName.Checked;
                this.chkShowTitle.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowHeading;
                this.btnTitleSymbol.Enabled = this.chkShowTitle.Checked;
                this.chkShowLabel.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowLabels;
                this.btnLabelSymbol.Enabled = this.chkShowLabel.Checked;
                this.chkShowDescription.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowDescriptions;
                this.btnDescriptionSymbol.Enabled = this.chkShowDescription.Checked;
                if ((LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch != null) || (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch != null))
                {
                    IStyleGalleryItem item;
                    this.chkOveralpDefaultPatch.Checked = true;
                    if (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch != null)
                    {
                        item = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch
                        };
                        this.cboLinePatches.SelectStyleGalleryItem(item);
                    }
                    if (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch != null)
                    {
                        item = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch
                        };
                        this.cboAreaPatches.SelectStyleGalleryItem(item);
                    }
                }
                else
                {
                    this.chkOveralpDefaultPatch.Checked = false;
                }
                this.cboLinePatches.Enabled = this.chkOveralpDefaultPatch.Checked;
                this.cboAreaPatches.Enabled = this.chkOveralpDefaultPatch.Checked;
                if ((LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight != 0.0) && (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight != 0.0))
                {
                    this.chkOveralpDefaultPatchSize.Checked = true;
                    this.txtDefaultPatchHeight.Text = "24";
                    this.txtDefaultPatchWidth.Text = "36";
                }
                else
                {
                    this.txtDefaultPatchHeight.Text = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight.ToString();
                    this.txtDefaultPatchWidth.Text = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchWidth.ToString();
                }
                this.txtDefaultPatchHeight.Enabled = this.chkOveralpDefaultPatchSize.Checked;
                this.txtDefaultPatchWidth.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.groupBox1 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.components);
            this.cboLinePatches = new StyleComboBox(this.components);
            this.txtDefaultPatchHeight = new TextEdit();
            this.txtDefaultPatchWidth = new TextEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.chkOveralpDefaultPatchSize = new CheckEdit();
            this.chkOveralpDefaultPatch = new CheckEdit();
            this.btnDescriptionSymbol = new SimpleButton();
            this.chkShowDescription = new CheckEdit();
            this.btnTitleSymbol = new SimpleButton();
            this.chkShowTitle = new CheckEdit();
            this.btnLabelSymbol = new SimpleButton();
            this.btnLayerNameSymbol = new SimpleButton();
            this.chkShowLabel = new CheckEdit();
            this.chkShowLayerName = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.txtDefaultPatchHeight.Properties.BeginInit();
            this.txtDefaultPatchWidth.Properties.BeginInit();
            this.chkOveralpDefaultPatchSize.Properties.BeginInit();
            this.chkOveralpDefaultPatch.Properties.BeginInit();
            this.chkShowDescription.Properties.BeginInit();
            this.chkShowTitle.Properties.BeginInit();
            this.chkShowLabel.Properties.BeginInit();
            this.chkShowLayerName.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboAreaPatches);
            this.groupBox1.Controls.Add(this.cboLinePatches);
            this.groupBox1.Controls.Add(this.txtDefaultPatchHeight);
            this.groupBox1.Controls.Add(this.txtDefaultPatchWidth);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkOveralpDefaultPatchSize);
            this.groupBox1.Controls.Add(this.chkOveralpDefaultPatch);
            this.groupBox1.Controls.Add(this.btnDescriptionSymbol);
            this.groupBox1.Controls.Add(this.chkShowDescription);
            this.groupBox1.Controls.Add(this.btnTitleSymbol);
            this.groupBox1.Controls.Add(this.chkShowTitle);
            this.groupBox1.Controls.Add(this.btnLabelSymbol);
            this.groupBox1.Controls.Add(this.btnLayerNameSymbol);
            this.groupBox1.Controls.Add(this.chkShowLabel);
            this.groupBox1.Controls.Add(this.chkShowLayerName);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x120, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "外观";
            this.cboAreaPatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboAreaPatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboAreaPatches.DropDownWidth = 160;
            this.cboAreaPatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboAreaPatches.Location = new Point(0x30, 200);
            this.cboAreaPatches.Name = "cboAreaPatches";
            this.cboAreaPatches.Size = new Size(0x40, 0x1f);
            this.cboAreaPatches.TabIndex = 0x11;
            this.cboLinePatches.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboLinePatches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLinePatches.DropDownWidth = 160;
            this.cboLinePatches.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboLinePatches.ItemHeight = 20;
            this.cboLinePatches.Location = new Point(0x30, 0xa8);
            this.cboLinePatches.Name = "cboLinePatches";
            this.cboLinePatches.Size = new Size(0x40, 0x1a);
            this.cboLinePatches.TabIndex = 0x10;
            this.cboLinePatches.SelectedIndexChanged += new EventHandler(this.cboLinePatches_SelectedIndexChanged);
            this.txtDefaultPatchHeight.EditValue = "";
            this.txtDefaultPatchHeight.Location = new Point(0xb8, 0xca);
            this.txtDefaultPatchHeight.Name = "txtDefaultPatchHeight";
            this.txtDefaultPatchHeight.Size = new Size(0x40, 0x15);
            this.txtDefaultPatchHeight.TabIndex = 15;
            this.txtDefaultPatchWidth.EditValue = "";
            this.txtDefaultPatchWidth.Location = new Point(0xb8, 170);
            this.txtDefaultPatchWidth.Name = "txtDefaultPatchWidth";
            this.txtDefaultPatchWidth.Size = new Size(0x40, 0x15);
            this.txtDefaultPatchWidth.TabIndex = 14;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x90, 210);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 0x11);
            this.label4.TabIndex = 13;
            this.label4.Text = "高度";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x90, 0xb2);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 0x11);
            this.label3.TabIndex = 12;
            this.label3.Text = "宽度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0xca);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x11, 0x11);
            this.label2.TabIndex = 11;
            this.label2.Text = "面";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 170);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 0x11);
            this.label1.TabIndex = 10;
            this.label1.Text = "线";
            this.chkOveralpDefaultPatchSize.EditValue = false;
            this.chkOveralpDefaultPatchSize.Location = new Point(0x90, 0x8a);
            this.chkOveralpDefaultPatchSize.Name = "chkOveralpDefaultPatchSize";
            this.chkOveralpDefaultPatchSize.Properties.Caption = "覆盖默认区块尺寸";
            this.chkOveralpDefaultPatchSize.Size = new Size(0x80, 0x13);
            this.chkOveralpDefaultPatchSize.TabIndex = 9;
            this.chkOveralpDefaultPatchSize.CheckedChanged += new EventHandler(this.chkOveralpDefaultPatchSize_CheckedChanged);
            this.chkOveralpDefaultPatch.EditValue = false;
            this.chkOveralpDefaultPatch.Location = new Point(8, 0x8a);
            this.chkOveralpDefaultPatch.Name = "chkOveralpDefaultPatch";
            this.chkOveralpDefaultPatch.Properties.Caption = "覆盖默认区块";
            this.chkOveralpDefaultPatch.Size = new Size(0x68, 0x13);
            this.chkOveralpDefaultPatch.TabIndex = 8;
            this.chkOveralpDefaultPatch.CheckedChanged += new EventHandler(this.chkOveralpDefaultPatch_CheckedChanged);
            this.btnDescriptionSymbol.Location = new Point(0x90, 0x65);
            this.btnDescriptionSymbol.Name = "btnDescriptionSymbol";
            this.btnDescriptionSymbol.Size = new Size(0x60, 0x18);
            this.btnDescriptionSymbol.TabIndex = 7;
            this.btnDescriptionSymbol.Text = "说明符号...";
            this.btnDescriptionSymbol.Click += new EventHandler(this.btnDescriptionSymbol_Click);
            this.chkShowDescription.EditValue = false;
            this.chkShowDescription.Location = new Point(0x90, 0x4a);
            this.chkShowDescription.Name = "chkShowDescription";
            this.chkShowDescription.Properties.Caption = "显示说明";
            this.chkShowDescription.Size = new Size(0x58, 0x13);
            this.chkShowDescription.TabIndex = 6;
            this.chkShowDescription.CheckedChanged += new EventHandler(this.chkShowDescription_CheckedChanged);
            this.btnTitleSymbol.Location = new Point(0x10, 0x65);
            this.btnTitleSymbol.Name = "btnTitleSymbol";
            this.btnTitleSymbol.Size = new Size(0x60, 0x18);
            this.btnTitleSymbol.TabIndex = 5;
            this.btnTitleSymbol.Text = "标题符号...";
            this.btnTitleSymbol.Click += new EventHandler(this.btnTitleSymbol_Click);
            this.chkShowTitle.EditValue = false;
            this.chkShowTitle.Location = new Point(8, 0x4a);
            this.chkShowTitle.Name = "chkShowTitle";
            this.chkShowTitle.Properties.Caption = "显示标题";
            this.chkShowTitle.Size = new Size(0x58, 0x13);
            this.chkShowTitle.TabIndex = 4;
            this.chkShowTitle.CheckedChanged += new EventHandler(this.chkShowTitle_CheckedChanged);
            this.btnLabelSymbol.Location = new Point(0x90, 0x2b);
            this.btnLabelSymbol.Name = "btnLabelSymbol";
            this.btnLabelSymbol.Size = new Size(0x60, 0x18);
            this.btnLabelSymbol.TabIndex = 3;
            this.btnLabelSymbol.Text = "标注符号...";
            this.btnLabelSymbol.Click += new EventHandler(this.btnLabelSymbol_Click);
            this.btnLayerNameSymbol.Location = new Point(0x10, 0x2b);
            this.btnLayerNameSymbol.Name = "btnLayerNameSymbol";
            this.btnLayerNameSymbol.Size = new Size(0x60, 0x18);
            this.btnLayerNameSymbol.TabIndex = 2;
            this.btnLayerNameSymbol.Text = "图层名称符号...";
            this.btnLayerNameSymbol.Click += new EventHandler(this.btnLayerNameSymbol_Click);
            this.chkShowLabel.EditValue = false;
            this.chkShowLabel.Location = new Point(0x90, 0x15);
            this.chkShowLabel.Name = "chkShowLabel";
            this.chkShowLabel.Properties.Caption = "显示标注";
            this.chkShowLabel.Size = new Size(0x58, 0x13);
            this.chkShowLabel.TabIndex = 1;
            this.chkShowLabel.CheckedChanged += new EventHandler(this.chkShowLabel_CheckedChanged);
            this.chkShowLayerName.EditValue = false;
            this.chkShowLayerName.Location = new Point(8, 0x15);
            this.chkShowLayerName.Name = "chkShowLayerName";
            this.chkShowLayerName.Properties.Caption = "显示图层名";
            this.chkShowLayerName.Size = new Size(0x58, 0x13);
            this.chkShowLayerName.TabIndex = 0;
            this.chkShowLayerName.CheckedChanged += new EventHandler(this.chkShowLayerName_CheckedChanged);
            base.Controls.Add(this.groupBox1);
            base.Name = "LegendItemGeneralPropertyPage";
            base.Size = new Size(320, 0x120);
            base.Load += new EventHandler(this.LegendItemGeneralPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtDefaultPatchHeight.Properties.EndInit();
            this.txtDefaultPatchWidth.Properties.EndInit();
            this.chkOveralpDefaultPatchSize.Properties.EndInit();
            this.chkOveralpDefaultPatch.Properties.EndInit();
            this.chkShowDescription.Properties.EndInit();
            this.chkShowTitle.Properties.EndInit();
            this.chkShowLabel.Properties.EndInit();
            this.chkShowLayerName.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LegendItemGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_pSG != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.m_pSG.get_Items("Line Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboLinePatches.Add(item);
                }
                this.cboLinePatches.SelectedIndex = -1;
                item2 = this.m_pSG.get_Items("Area Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboAreaPatches.Add(item);
                }
                this.cboAreaPatches.SelectedIndex = -1;
            }
            this.Init();
            this.m_CanDo = true;
        }

        private void SelectTextSymbol(ref ITextSymbol pTextSymbol)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(pTextSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pTextSymbol = selector.GetSymbol() as ITextSymbol;
                        this.ValueChanged();
                    }
                }
            }
            catch
            {
            }
        }

        public void SetObjects(object @object)
        {
            if (LegendItemArrangementPropertyPage.m_pOldLegendItem == null)
            {
                LegendItemArrangementPropertyPage.m_pOldLegendItem = @object as ILegendItem;
                if (LegendItemArrangementPropertyPage.m_pOldLegendItem != null)
                {
                    LegendItemArrangementPropertyPage.m_pLegendItem = (LegendItemArrangementPropertyPage.m_pOldLegendItem as IClone).Clone() as ILegendItem;
                }
            }
        }

        private void ValueChanged()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

