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

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LegendItemGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ILegendItem ilegendItem_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private ITextSymbol itextSymbol_0 = null;
        private ITextSymbol itextSymbol_1 = null;
        private ITextSymbol itextSymbol_2 = null;
        private ITextSymbol itextSymbol_3 = null;
        private string string_0 = "常规";

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
                    selector.SetStyleGallery(this.istyleGallery_0);
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

