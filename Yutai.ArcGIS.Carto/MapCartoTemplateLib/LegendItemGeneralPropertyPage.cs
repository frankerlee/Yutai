using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class LegendItemGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
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
        private GroupBox groupBox1;
        private IContainer icontainer_0;
        private ILegendItem ilegendItem_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = null;
        private ITextSymbol itextSymbol_1 = null;
        private ITextSymbol itextSymbol_2 = null;
        private ITextSymbol itextSymbol_3 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string string_0 = "常规";
        private TextEdit txtDefaultPatchHeight;
        private TextEdit txtDefaultPatchWidth;

        public event OnValueChangeEventHandler OnValueChange;

        public LegendItemGeneralPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = true;
                this.ilegendItem_0.ShowLayerName = this.chkShowLayerName.Checked;
                this.ilegendItem_0.ShowHeading = this.chkShowTitle.Checked;
                this.ilegendItem_0.ShowLabels = this.chkShowLabel.Checked;
                this.ilegendItem_0.ShowDescriptions = this.chkShowDescription.Checked;
                if (this.chkShowLayerName.Checked)
                {
                    this.ilegendItem_0.LayerNameSymbol = this.itextSymbol_0;
                }
                if (this.chkShowTitle.Checked)
                {
                    this.ilegendItem_0.HeadingSymbol = this.itextSymbol_1;
                }
                if (this.chkShowLabel.Checked)
                {
                    this.ilegendItem_0.LegendClassFormat.LabelSymbol = this.itextSymbol_2;
                }
                if (this.chkShowDescription.Checked)
                {
                    this.ilegendItem_0.LegendClassFormat.DescriptionSymbol = this.itextSymbol_3;
                }
                if (this.chkOveralpDefaultPatch.Checked)
                {
                    IStyleGalleryItem selectStyleGalleryItem = null;
                    selectStyleGalleryItem = this.cboLinePatches.GetSelectStyleGalleryItem();
                    if (selectStyleGalleryItem != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.LinePatch = selectStyleGalleryItem.Item as ILinePatch;
                    }
                    else
                    {
                        this.ilegendItem_0.LegendClassFormat.LinePatch = null;
                    }
                    selectStyleGalleryItem = this.cboAreaPatches.GetSelectStyleGalleryItem();
                    if (selectStyleGalleryItem != null)
                    {
                        this.ilegendItem_0.LegendClassFormat.AreaPatch = selectStyleGalleryItem.Item as IAreaPatch;
                    }
                    else
                    {
                        this.ilegendItem_0.LegendClassFormat.AreaPatch = null;
                    }
                }
                else
                {
                    this.ilegendItem_0.LegendClassFormat.LinePatch = null;
                    this.ilegendItem_0.LegendClassFormat.AreaPatch = null;
                }
                if (this.chkOveralpDefaultPatchSize.Checked)
                {
                    try
                    {
                        double num = double.Parse(this.txtDefaultPatchWidth.Text);
                        double num2 = double.Parse(this.txtDefaultPatchHeight.Text);
                        this.ilegendItem_0.LegendClassFormat.PatchWidth = num;
                        this.ilegendItem_0.LegendClassFormat.PatchHeight = num2;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    this.ilegendItem_0.LegendClassFormat.PatchWidth = 0.0;
                    this.ilegendItem_0.LegendClassFormat.PatchHeight = 0.0;
                }
            }
        }

        private void btnDescriptionSymbol_Click(object sender, EventArgs e)
        {
            this.method_2(ref this.itextSymbol_3);
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            this.method_2(ref this.itextSymbol_2);
        }

        private void btnLayerNameSymbol_Click(object sender, EventArgs e)
        {
            this.method_2(ref this.itextSymbol_0);
        }

        private void btnTitleSymbol_Click(object sender, EventArgs e)
        {
            this.method_2(ref this.itextSymbol_1);
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
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void chkOveralpDefaultPatchSize_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDefaultPatchWidth.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            this.txtDefaultPatchHeight.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void chkShowDescription_CheckedChanged(object sender, EventArgs e)
        {
            this.btnDescriptionSymbol.Enabled = this.chkShowDescription.Checked;
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void chkShowLabel_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLabelSymbol.Enabled = this.chkShowLabel.Checked;
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void chkShowLayerName_CheckedChanged(object sender, EventArgs e)
        {
            this.btnLayerNameSymbol.Enabled = this.chkShowLayerName.Checked;
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void chkShowTitle_CheckedChanged(object sender, EventArgs e)
        {
            this.btnTitleSymbol.Enabled = this.chkShowTitle.Checked;
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            this.groupBox1 = new GroupBox();
            this.cboAreaPatches = new StyleComboBox(this.icontainer_0);
            this.cboLinePatches = new StyleComboBox(this.icontainer_0);
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

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

        private void LegendItemGeneralPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Line Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboLinePatches.Add(item);
                }
                this.cboLinePatches.SelectedIndex = -1;
                item2 = this.istyleGallery_0.get_Items("Area Patches", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboAreaPatches.Add(item);
                }
                this.cboAreaPatches.SelectedIndex = -1;
            }
            this.method_1();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        private void method_1()
        {
            if (this.ilegendItem_0 != null)
            {
                this.itextSymbol_0 = this.ilegendItem_0.LayerNameSymbol;
                this.itextSymbol_1 = this.ilegendItem_0.HeadingSymbol;
                this.itextSymbol_2 = this.ilegendItem_0.LegendClassFormat.LabelSymbol;
                this.itextSymbol_3 = this.ilegendItem_0.LegendClassFormat.DescriptionSymbol;
                this.chkShowLayerName.Checked = this.ilegendItem_0.ShowLayerName;
                this.btnLayerNameSymbol.Enabled = this.chkShowLayerName.Checked;
                this.chkShowTitle.Checked = this.ilegendItem_0.ShowHeading;
                this.btnTitleSymbol.Enabled = this.chkShowTitle.Checked;
                this.chkShowLabel.Checked = this.ilegendItem_0.ShowLabels;
                this.btnLabelSymbol.Enabled = this.chkShowLabel.Checked;
                this.chkShowDescription.Checked = this.ilegendItem_0.ShowDescriptions;
                this.btnDescriptionSymbol.Enabled = this.chkShowDescription.Checked;
                if ((this.ilegendItem_0.LegendClassFormat.AreaPatch != null) || (this.ilegendItem_0.LegendClassFormat.LinePatch != null))
                {
                    IStyleGalleryItem item;
                    this.chkOveralpDefaultPatch.Checked = true;
                    if (this.ilegendItem_0.LegendClassFormat.LinePatch != null)
                    {
                        item = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = this.ilegendItem_0.LegendClassFormat.LinePatch
                        };
                        this.cboLinePatches.SelectStyleGalleryItem(item);
                    }
                    if (this.ilegendItem_0.LegendClassFormat.AreaPatch != null)
                    {
                        item = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = this.ilegendItem_0.LegendClassFormat.AreaPatch
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
                if ((this.ilegendItem_0.LegendClassFormat.PatchHeight != 0.0) && (this.ilegendItem_0.LegendClassFormat.PatchHeight != 0.0))
                {
                    this.chkOveralpDefaultPatchSize.Checked = true;
                    this.txtDefaultPatchHeight.Text = "24";
                    this.txtDefaultPatchWidth.Text = "36";
                }
                else
                {
                    this.txtDefaultPatchHeight.Text = this.ilegendItem_0.LegendClassFormat.PatchHeight.ToString();
                    this.txtDefaultPatchWidth.Text = this.ilegendItem_0.LegendClassFormat.PatchWidth.ToString();
                }
                this.txtDefaultPatchHeight.Enabled = this.chkOveralpDefaultPatchSize.Checked;
                this.txtDefaultPatchWidth.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            }
        }

        private void method_2(ref ITextSymbol itextSymbol_4)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(itextSymbol_4);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        itextSymbol_4 = selector.GetSymbol() as ITextSymbol;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_1();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.ilegendItem_0 = object_0 as ILegendItem;
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
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
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }
    }
}

