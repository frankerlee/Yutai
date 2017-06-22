using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmOutline : Form
    {
        private bool bool_0 = false;
        private IFrameElement iframeElement_0 = null;
        private IPageLayout ipageLayout_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;

        public frmOutline()
        {
            this.InitializeComponent();
        }

        private void btnBackgroundInfo_Click(object sender, EventArgs e)
        {
            IStyleGalleryItem selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
            IBackground item = null;
            if (selectStyleGalleryItem != null)
            {
                item = selectStyleGalleryItem.Item as IBackground;
            }
            if (item != null)
            {
                frmElementProperty property = new frmElementProperty();
                BackgroundSymbolPropertyPage page = new BackgroundSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(item))
                {
                    this.bool_0 = false;
                    selectStyleGalleryItem = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                    if (selectStyleGalleryItem != null)
                    {
                        if (selectStyleGalleryItem.Name == "<定制>")
                        {
                            selectStyleGalleryItem.Item = item;
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = item
                            };
                            this.cboBackground.Add(selectStyleGalleryItem);
                            this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                        }
                    }
                    else
                    {
                        selectStyleGalleryItem = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = item
                        };
                        this.cboBackground.Add(selectStyleGalleryItem);
                        this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void btnBackgroundSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    IStyleGalleryItem selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
                    IBackground pSym = null;
                    if (selectStyleGalleryItem != null)
                    {
                        pSym = selectStyleGalleryItem.Item as IBackground;
                    }
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (pSym != null)
                    {
                        selector.SetSymbol(pSym);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolBackgroundClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IBackground;
                        this.bool_0 = false;
                        selectStyleGalleryItem = this.cboBackground.GetStyleGalleryItemAt(this.cboBackground.Items.Count - 1);
                        if (selectStyleGalleryItem != null)
                        {
                            if (selectStyleGalleryItem.Name == "<定制>")
                            {
                                selectStyleGalleryItem.Item = pSym;
                            }
                            else
                            {
                                selectStyleGalleryItem = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = pSym
                                };
                                this.cboBackground.Add(selectStyleGalleryItem);
                                this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                            }
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = pSym
                            };
                            this.cboBackground.Add(selectStyleGalleryItem);
                            this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                        }
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnBorderInfo_Click(object sender, EventArgs e)
        {
            IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
            IBorder item = null;
            if (selectStyleGalleryItem != null)
            {
                item = selectStyleGalleryItem.Item as IBorder;
            }
            if (item != null)
            {
                frmElementProperty property = new frmElementProperty();
                BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(item))
                {
                    this.bool_0 = false;
                    selectStyleGalleryItem = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                    if (selectStyleGalleryItem != null)
                    {
                        if (selectStyleGalleryItem.Name == "<定制>")
                        {
                            selectStyleGalleryItem.Item = item;
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = item
                            };
                            this.cboBorder.Add(selectStyleGalleryItem);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                    }
                    else
                    {
                        selectStyleGalleryItem = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = item
                        };
                        this.cboBorder.Add(selectStyleGalleryItem);
                        this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void btnBorderSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
                    IBorder pSym = null;
                    if (selectStyleGalleryItem != null)
                    {
                        pSym = selectStyleGalleryItem.Item as IBorder;
                    }
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (pSym != null)
                    {
                        selector.SetSymbol(pSym);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolBorderClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IBorder;
                        this.bool_0 = false;
                        selectStyleGalleryItem = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                        if (selectStyleGalleryItem != null)
                        {
                            if (selectStyleGalleryItem.Name == "<定制>")
                            {
                                selectStyleGalleryItem.Item = pSym;
                            }
                            else
                            {
                                selectStyleGalleryItem = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = pSym
                                };
                                this.cboBorder.Add(selectStyleGalleryItem);
                                this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                            }
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = pSym
                            };
                            this.cboBorder.Add(selectStyleGalleryItem);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                if (this.chkNewFrameElement.Checked)
                {
                    IGraphicsContainerSelect select;
                    IEnumElement selectedElements;
                    IGraphicsContainer container;
                    flag = true;
                    IFrameElement element = new FrameElementClass();
                    IEnvelope printableBounds = null;
                    IElement element2 = null;
                    if (this.rdoPlaceSelectElement.Checked)
                    {
                        select = this.ipageLayout_0 as IGraphicsContainerSelect;
                        selectedElements = select.SelectedElements;
                        selectedElements.Reset();
                        element2 = selectedElements.Next();
                        if (element2 == null)
                        {
                            return;
                        }
                        printableBounds = element2.Geometry.Envelope;
                        for (element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                        {
                            printableBounds.Union(element2.Geometry.Envelope);
                        }
                    }
                    else if (this.rdoPlaceAllElement.Checked)
                    {
                        container = this.ipageLayout_0 as IGraphicsContainer;
                        container.Reset();
                        element2 = container.Next();
                        if (element2 == null)
                        {
                            return;
                        }
                        printableBounds = element2.Geometry.Envelope;
                        for (element2 = container.Next(); element2 != null; element2 = container.Next())
                        {
                            printableBounds.Union(element2.Geometry.Envelope);
                        }
                    }
                    else
                    {
                        printableBounds = this.ipageLayout_0.Page.PrintableBounds;
                    }
                    printableBounds.Expand(0.5, 0.5, false);
                    (element as IElement).Geometry = printableBounds;
                    if (this.chkGroup.Checked)
                    {
                        IGroupElement group = new GroupElementClass();
                        container = this.ipageLayout_0 as IGraphicsContainer;
                        container.MoveElementToGroup(element as IElement, group);
                        select = this.ipageLayout_0 as IGraphicsContainerSelect;
                        selectedElements = null;
                        if (this.rdoPlaceAllElement.Checked)
                        {
                            select.SelectAllElements();
                        }
                        selectedElements = select.SelectedElements;
                        selectedElements.Reset();
                        for (element2 = selectedElements.Next(); element2 != null; element2 = selectedElements.Next())
                        {
                            container.MoveElementToGroup(element2, group);
                        }
                        this.iframeElement_0 = group as IFrameElement;
                    }
                    else
                    {
                        this.iframeElement_0 = element;
                    }
                }
                IStyleGalleryItem selectStyleGalleryItem = null;
                selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
                IBorder item = null;
                if (selectStyleGalleryItem != null)
                {
                    item = selectStyleGalleryItem.Item as IBorder;
                    (item as IFrameDecoration).HorizontalSpacing = (double) this.txtGap.Value;
                    (item as IFrameDecoration).VerticalSpacing = (double) this.txtGap.Value;
                    (item as IFrameDecoration).CornerRounding = (short) this.txtRound.Value;
                }
                IBackground background = null;
                selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem != null)
                {
                    background = selectStyleGalleryItem.Item as IBackground;
                    (background as IFrameDecoration).HorizontalSpacing = (double) this.txtGap.Value;
                    (background as IFrameDecoration).VerticalSpacing = (double) this.txtGap.Value;
                    (background as IFrameDecoration).CornerRounding = (short) this.txtRound.Value;
                }
                IShadow shadow = null;
                selectStyleGalleryItem = this.cboShadow.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem != null)
                {
                    shadow = selectStyleGalleryItem.Item as IShadow;
                    (shadow as IFrameDecoration).HorizontalSpacing = (double) this.txtGap.Value;
                    (shadow as IFrameDecoration).VerticalSpacing = (double) this.txtGap.Value;
                    (shadow as IFrameDecoration).CornerRounding = (short) this.txtRound.Value;
                }
                this.iframeElement_0.Border = item;
                this.iframeElement_0.Background = background;
                (this.iframeElement_0 as IFrameProperties).Shadow = shadow;
                if (flag)
                {
                    (this.ipageLayout_0 as IGraphicsContainer).AddElement(this.iframeElement_0 as IElement, 0);
                    ElementOperator.FocusOneElement(this.ipageLayout_0 as IActiveView, this.iframeElement_0 as IElement);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void btnshadowInfo_Click(object sender, EventArgs e)
        {
            IStyleGalleryItem selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
            IShadow item = null;
            if (selectStyleGalleryItem != null)
            {
                item = selectStyleGalleryItem.Item as IShadow;
            }
            if (item != null)
            {
                frmElementProperty property = new frmElementProperty();
                ShadowSymbolPropertyPage page = new ShadowSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(item))
                {
                    this.bool_0 = false;
                    selectStyleGalleryItem = this.cboShadow.GetStyleGalleryItemAt(this.cboShadow.Items.Count - 1);
                    if (selectStyleGalleryItem != null)
                    {
                        if (selectStyleGalleryItem.Name == "<定制>")
                        {
                            selectStyleGalleryItem.Item = item;
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = item
                            };
                            this.cboShadow.Add(selectStyleGalleryItem);
                            this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                        }
                    }
                    else
                    {
                        selectStyleGalleryItem = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = item
                        };
                        this.cboShadow.Add(selectStyleGalleryItem);
                        this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                    }
                    this.bool_0 = true;
                }
            }
        }

        private void btnShadowSelector_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    IStyleGalleryItem selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
                    IShadow pSym = null;
                    if (selectStyleGalleryItem != null)
                    {
                        pSym = selectStyleGalleryItem.Item as IShadow;
                    }
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (pSym != null)
                    {
                        selector.SetSymbol(pSym);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolShadowClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        pSym = selector.GetSymbol() as IShadow;
                        this.bool_0 = false;
                        selectStyleGalleryItem = this.cboBackground.GetStyleGalleryItemAt(this.cboShadow.Items.Count - 1);
                        if (selectStyleGalleryItem != null)
                        {
                            if (selectStyleGalleryItem.Name == "<定制>")
                            {
                                selectStyleGalleryItem.Item = pSym;
                            }
                            else
                            {
                                selectStyleGalleryItem = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = pSym
                                };
                                this.cboShadow.Add(selectStyleGalleryItem);
                                this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                            }
                        }
                        else
                        {
                            selectStyleGalleryItem = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = pSym
                            };
                            this.cboShadow.Add(selectStyleGalleryItem);
                            this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                        }
                        this.bool_0 = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void chkNewFrameElement_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.chkNewFrameElement.Checked)
                {
                    this.chkGroup.Enabled = true;
                }
                else
                {
                    this.chkGroup.Enabled = false;
                    this.chkGroup.Checked = false;
                }
            }
        }

 private void frmOutline_Load(object sender, EventArgs e)
        {
            this.cboBorder.Add(null);
            this.cboBackground.Add(null);
            this.cboShadow.Add(null);
            if (this.istyleGallery_0 != null)
            {
                IStyleGalleryItem item = null;
                IEnumStyleGalleryItem item2 = this.istyleGallery_0.get_Items("Borders", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboBorder.Add(item);
                }
                if (this.cboBorder.Items.Count > 0)
                {
                    this.cboBorder.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Backgrounds", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboBackground.Add(item);
                }
                if (this.cboBackground.Items.Count > 0)
                {
                    this.cboBackground.SelectedIndex = 0;
                }
                item2 = this.istyleGallery_0.get_Items("Shadows", "", "");
                item2.Reset();
                for (item = item2.Next(); item != null; item = item2.Next())
                {
                    this.cboShadow.Add(item);
                }
                if (this.cboShadow.Items.Count > 0)
                {
                    this.cboShadow.SelectedIndex = 0;
                }
            }
            this.method_0();
            this.bool_0 = true;
        }

 private void method_0()
        {
            IElement element2;
            IGraphicsContainerSelect select = this.ipageLayout_0 as IGraphicsContainerSelect;
            if (select.ElementSelectionCount > 0)
            {
                this.rdoPlaceSelectElement.Enabled = true;
                this.rdoPlaceSelectElement.Checked = true;
                this.rdoPlaceAllElement.Checked = false;
                this.rdoPlacePageMargine.Checked = false;
                IEnumElement selectedElements = select.SelectedElements;
                selectedElements.Reset();
                element2 = selectedElements.Next();
                if ((select.ElementSelectionCount == 1) && (element2 is IFrameElement))
                {
                    this.chkNewFrameElement.Checked = false;
                    this.chkNewFrameElement.Enabled = true;
                    this.chkGroup.Checked = false;
                    this.chkGroup.Enabled = false;
                    this.iframeElement_0 = element2 as IFrameElement;
                    this.method_1(element2 as IFrameElement);
                }
                else
                {
                    this.chkNewFrameElement.Checked = true;
                    this.chkNewFrameElement.Enabled = false;
                    this.chkGroup.Checked = false;
                    this.chkGroup.Enabled = true;
                }
            }
            else
            {
                this.rdoPlaceSelectElement.Enabled = false;
                this.rdoPlaceSelectElement.Checked = false;
                this.rdoPlaceAllElement.Checked = true;
                this.rdoPlacePageMargine.Checked = false;
                IGraphicsContainer container = this.ipageLayout_0 as IGraphicsContainer;
                container.Reset();
                element2 = container.Next();
                if (!((container.Next() == null) && (element2 is IFrameElement)))
                {
                    this.chkNewFrameElement.Checked = true;
                    this.chkNewFrameElement.Enabled = false;
                    this.chkGroup.Checked = false;
                    this.chkGroup.Enabled = true;
                }
                else
                {
                    this.chkNewFrameElement.Checked = false;
                    this.chkNewFrameElement.Enabled = true;
                    this.chkGroup.Checked = false;
                    this.chkGroup.Enabled = false;
                    this.iframeElement_0 = element2 as IFrameElement;
                    this.method_1(element2 as IFrameElement);
                }
            }
        }

        private void method_1(IFrameElement iframeElement_1)
        {
            IBorder border = iframeElement_1.Border;
            IBackground background = iframeElement_1.Background;
            IShadow shadow = (iframeElement_1 as IFrameProperties).Shadow;
            IStyleGalleryItem oO = null;
            IFrameDecoration decoration = null;
            if (border == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = border
                };
                decoration = border as IFrameDecoration;
                this.txtGap.Text = decoration.HorizontalSpacing.ToString("0.##");
                this.txtRound.Text = decoration.CornerRounding.ToString("0.##");
            }
            this.cboBorder.SelectStyleGalleryItem(oO);
            if (background == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = background
                };
                if (decoration == null)
                {
                    decoration = background as IFrameDecoration;
                    this.txtGap.Text = decoration.HorizontalSpacing.ToString("0.##");
                    this.txtRound.Text = decoration.CornerRounding.ToString("0.##");
                }
            }
            this.cboBackground.SelectStyleGalleryItem(oO);
            if (shadow == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = shadow
                };
                if (decoration == null)
                {
                    decoration = shadow as IFrameDecoration;
                    this.txtGap.Text = decoration.HorizontalSpacing.ToString("0.##");
                    this.txtRound.Text = decoration.CornerRounding.ToString("0.##");
                }
            }
            this.cboShadow.SelectStyleGalleryItem(oO);
        }

        private void rdoPlaceAllElement_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IGraphicsContainer container = this.ipageLayout_0 as IGraphicsContainer;
                container.Reset();
                IElement element = container.Next();
                if (!((container.Next() == null) && (element is IFrameElement)))
                {
                    this.chkNewFrameElement.Enabled = false;
                    this.chkNewFrameElement.Checked = true;
                    this.chkGroup.Enabled = true;
                }
                else
                {
                    this.chkNewFrameElement.Enabled = true;
                    this.chkGroup.Enabled = this.chkNewFrameElement.Checked;
                    this.chkGroup.Checked = false;
                }
            }
        }

        private void rdoPlacePageMargine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.chkNewFrameElement.Enabled = false;
                this.chkNewFrameElement.Checked = true;
                this.chkGroup.Enabled = false;
                this.chkGroup.Checked = false;
            }
        }

        private void rdoPlaceSelectElement_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IGraphicsContainerSelect select = this.ipageLayout_0 as IGraphicsContainerSelect;
                IEnumElement selectedElements = select.SelectedElements;
                selectedElements.Reset();
                IElement element2 = selectedElements.Next();
                if ((select.ElementSelectionCount == 1) && (element2 is IFrameElement))
                {
                    this.chkNewFrameElement.Enabled = true;
                    this.chkGroup.Enabled = this.chkNewFrameElement.Checked;
                    this.chkGroup.Checked = false;
                }
                else
                {
                    this.chkNewFrameElement.Checked = true;
                    this.chkNewFrameElement.Enabled = false;
                    this.chkGroup.Enabled = true;
                }
            }
        }

        public IPageLayout PageLayout
        {
            set
            {
                this.ipageLayout_0 = value;
            }
        }
    }
}

