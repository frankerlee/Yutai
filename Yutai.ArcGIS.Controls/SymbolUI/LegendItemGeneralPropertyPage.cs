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
    public partial class LegendItemGeneralPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ITextSymbol m_pDescriptionSymbol = null;
        private ITextSymbol m_pHeadingSymbol = null;
        private ITextSymbol m_pLabelSymbol = null;
        private ITextSymbol m_pLayerNameSymbol = null;
        private IStyleGallery m_pSG; //= ApplicationBase.StyleGallery;
        private string m_Title = "常规";

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
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.DescriptionSymbol =
                        this.m_pDescriptionSymbol;
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
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch =
                            selectStyleGalleryItem.Item as ILinePatch;
                    }
                    else
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch = null;
                    }
                    selectStyleGalleryItem = this.cboAreaPatches.GetSelectStyleGalleryItem();
                    if (selectStyleGalleryItem != null)
                    {
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch =
                            selectStyleGalleryItem.Item as IAreaPatch;
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
                this.m_pDescriptionSymbol =
                    LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.DescriptionSymbol;
                this.chkShowLayerName.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowLayerName;
                this.btnLayerNameSymbol.Enabled = this.chkShowLayerName.Checked;
                this.chkShowTitle.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowHeading;
                this.btnTitleSymbol.Enabled = this.chkShowTitle.Checked;
                this.chkShowLabel.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowLabels;
                this.btnLabelSymbol.Enabled = this.chkShowLabel.Checked;
                this.chkShowDescription.Checked = LegendItemArrangementPropertyPage.m_pLegendItem.ShowDescriptions;
                this.btnDescriptionSymbol.Enabled = this.chkShowDescription.Checked;
                if ((LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch != null) ||
                    (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch != null))
                {
                    IStyleGalleryItem item;
                    this.chkOveralpDefaultPatch.Checked = true;
                    if (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch != null)
                    {
                        item = new MyStyleGalleryItem
                        {
                            Name = "<定制>",
                            Item = LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.LinePatch
                        };
                        this.cboLinePatches.SelectStyleGalleryItem(item);
                    }
                    if (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.AreaPatch != null)
                    {
                        item = new MyStyleGalleryItem
                        {
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
                if ((LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight != 0.0) &&
                    (LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight != 0.0))
                {
                    this.chkOveralpDefaultPatchSize.Checked = true;
                    this.txtDefaultPatchHeight.Text = "24";
                    this.txtDefaultPatchWidth.Text = "36";
                }
                else
                {
                    this.txtDefaultPatchHeight.Text =
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchHeight.ToString();
                    this.txtDefaultPatchWidth.Text =
                        LegendItemArrangementPropertyPage.m_pLegendItem.LegendClassFormat.PatchWidth.ToString();
                }
                this.txtDefaultPatchHeight.Enabled = this.chkOveralpDefaultPatchSize.Checked;
                this.txtDefaultPatchWidth.Enabled = this.chkOveralpDefaultPatchSize.Checked;
            }
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
                    LegendItemArrangementPropertyPage.m_pLegendItem =
                        (LegendItemArrangementPropertyPage.m_pOldLegendItem as IClone).Clone() as ILegendItem;
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
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}