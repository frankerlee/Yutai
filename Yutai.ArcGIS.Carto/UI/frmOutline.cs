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
    public class frmOutline : Form
    {
        private bool bool_0 = false;
        private SimpleButton btnBackgroundInfo;
        private SimpleButton btnBackgroundSelector;
        private SimpleButton btnBorderInfo;
        private SimpleButton btnBorderSelector;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnshadowInfo;
        private SimpleButton btnShadowSelector;
        private StyleComboBox cboBackground;
        private StyleComboBox cboBorder;
        private StyleComboBox cboShadow;
        private CheckEdit chkGroup;
        private CheckEdit chkNewFrameElement;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private IContainer icontainer_0;
        private IFrameElement iframeElement_0 = null;
        private ImageList imageList_0;
        private IPageLayout ipageLayout_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label2;
        private RadioButton rdoPlaceAllElement;
        private RadioButton rdoPlacePageMargine;
        private RadioButton rdoPlaceSelectElement;
        private SpinEdit txtGap;
        private SpinEdit txtRound;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
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

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutline));
            this.groupBox1 = new GroupBox();
            this.rdoPlacePageMargine = new RadioButton();
            this.rdoPlaceAllElement = new RadioButton();
            this.rdoPlaceSelectElement = new RadioButton();
            this.txtRound = new SpinEdit();
            this.txtGap = new SpinEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.chkGroup = new CheckEdit();
            this.chkNewFrameElement = new CheckEdit();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.groupBox2 = new GroupBox();
            this.btnBorderInfo = new SimpleButton();
            this.btnBorderSelector = new SimpleButton();
            this.cboBorder = new StyleComboBox(this.icontainer_0);
            this.groupBox3 = new GroupBox();
            this.btnBackgroundInfo = new SimpleButton();
            this.btnBackgroundSelector = new SimpleButton();
            this.cboBackground = new StyleComboBox(this.icontainer_0);
            this.groupBox4 = new GroupBox();
            this.btnshadowInfo = new SimpleButton();
            this.btnShadowSelector = new SimpleButton();
            this.cboShadow = new StyleComboBox(this.icontainer_0);
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.txtRound.Properties.BeginInit();
            this.txtGap.Properties.BeginInit();
            this.chkGroup.Properties.BeginInit();
            this.chkNewFrameElement.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoPlacePageMargine);
            this.groupBox1.Controls.Add(this.rdoPlaceAllElement);
            this.groupBox1.Controls.Add(this.rdoPlaceSelectElement);
            this.groupBox1.Controls.Add(this.txtRound);
            this.groupBox1.Controls.Add(this.txtGap);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkGroup);
            this.groupBox1.Controls.Add(this.chkNewFrameElement);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "放置";
            this.rdoPlacePageMargine.Location = new System.Drawing.Point(8, 0x40);
            this.rdoPlacePageMargine.Name = "rdoPlacePageMargine";
            this.rdoPlacePageMargine.Size = new Size(0x70, 0x18);
            this.rdoPlacePageMargine.TabIndex = 0x31;
            this.rdoPlacePageMargine.Text = "放在页边距内";
            this.rdoPlacePageMargine.CheckedChanged += new EventHandler(this.rdoPlacePageMargine_CheckedChanged);
            this.rdoPlaceAllElement.Location = new System.Drawing.Point(8, 40);
            this.rdoPlaceAllElement.Name = "rdoPlaceAllElement";
            this.rdoPlaceAllElement.Size = new Size(0xa8, 0x18);
            this.rdoPlaceAllElement.TabIndex = 0x30;
            this.rdoPlaceAllElement.Text = "沿所有元素的周边放置";
            this.rdoPlaceAllElement.CheckedChanged += new EventHandler(this.rdoPlaceAllElement_CheckedChanged);
            this.rdoPlaceSelectElement.Location = new System.Drawing.Point(8, 0x10);
            this.rdoPlaceSelectElement.Name = "rdoPlaceSelectElement";
            this.rdoPlaceSelectElement.Size = new Size(160, 0x18);
            this.rdoPlaceSelectElement.TabIndex = 0x2e;
            this.rdoPlaceSelectElement.Text = "沿选中元素周边放置";
            this.rdoPlaceSelectElement.CheckedChanged += new EventHandler(this.rdoPlaceSelectElement_CheckedChanged);
            int[] bits = new int[4];
            this.txtRound.EditValue = new decimal(bits);
            this.txtRound.Location = new System.Drawing.Point(0x70, 0xa7);
            this.txtRound.Name = "txtRound";
            this.txtRound.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtRound.Size = new Size(0x30, 0x15);
            this.txtRound.TabIndex = 6;
            int[] bits2 = new int[4];
            bits2[0] = 10;
            this.txtGap.EditValue = new decimal(bits2);
            this.txtGap.Location = new System.Drawing.Point(0x10, 0xa7);
            this.txtGap.Name = "txtGap";
            this.txtGap.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtGap.Size = new Size(0x30, 0x15);
            this.txtGap.TabIndex = 5;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x70, 0x91);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "圆角";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x91);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "间隔";
            this.chkGroup.Location = new System.Drawing.Point(8, 0x70);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Properties.Caption = "将边框和元素合并";
            this.chkGroup.Size = new Size(0x98, 0x13);
            this.chkGroup.TabIndex = 2;
            this.chkNewFrameElement.Location = new System.Drawing.Point(8, 0x58);
            this.chkNewFrameElement.Name = "chkNewFrameElement";
            this.chkNewFrameElement.Properties.Caption = "创建单独的边框元素";
            this.chkNewFrameElement.Size = new Size(0x90, 0x13);
            this.chkNewFrameElement.TabIndex = 1;
            this.chkNewFrameElement.CheckedChanged += new EventHandler(this.chkNewFrameElement_CheckedChanged);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.groupBox2.Controls.Add(this.btnBorderInfo);
            this.groupBox2.Controls.Add(this.btnBorderSelector);
            this.groupBox2.Controls.Add(this.cboBorder);
            this.groupBox2.Location = new System.Drawing.Point(0xd8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(280, 60);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边框";
            this.btnBorderInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBorderInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderInfo.Appearance.Options.UseBackColor = true;
            this.btnBorderInfo.Appearance.Options.UseForeColor = true;
            this.btnBorderInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBorderInfo.ImageIndex = 1;
            this.btnBorderInfo.ImageList = this.imageList_0;
            this.btnBorderInfo.Location = new System.Drawing.Point(0xfc, 0x24);
            this.btnBorderInfo.Name = "btnBorderInfo";
            this.btnBorderInfo.Size = new Size(0x10, 0x10);
            this.btnBorderInfo.TabIndex = 0x21;
            this.btnBorderInfo.Click += new EventHandler(this.btnBorderInfo_Click);
            this.btnBorderSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBorderSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderSelector.Appearance.Options.UseBackColor = true;
            this.btnBorderSelector.Appearance.Options.UseForeColor = true;
            this.btnBorderSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBorderSelector.ImageIndex = 0;
            this.btnBorderSelector.ImageList = this.imageList_0;
            this.btnBorderSelector.Location = new System.Drawing.Point(0xfc, 20);
            this.btnBorderSelector.Name = "btnBorderSelector";
            this.btnBorderSelector.Size = new Size(0x10, 0x10);
            this.btnBorderSelector.TabIndex = 0x20;
            this.btnBorderSelector.Click += new EventHandler(this.btnBorderSelector_Click);
            this.cboBorder.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBorder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBorder.DropDownWidth = 160;
            this.cboBorder.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBorder.Location = new System.Drawing.Point(12, 20);
            this.cboBorder.Name = "cboBorder";
            this.cboBorder.Size = new Size(240, 0x1f);
            this.cboBorder.TabIndex = 0x1f;
            this.groupBox3.Controls.Add(this.btnBackgroundInfo);
            this.groupBox3.Controls.Add(this.btnBackgroundSelector);
            this.groupBox3.Controls.Add(this.cboBackground);
            this.groupBox3.Location = new System.Drawing.Point(0xd8, 0x4e);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(280, 60);
            this.groupBox3.TabIndex = 0x29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "背景";
            this.btnBackgroundInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundInfo.Appearance.Options.UseBackColor = true;
            this.btnBackgroundInfo.Appearance.Options.UseForeColor = true;
            this.btnBackgroundInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundInfo.ImageIndex = 1;
            this.btnBackgroundInfo.ImageList = this.imageList_0;
            this.btnBackgroundInfo.Location = new System.Drawing.Point(0xfc, 0x22);
            this.btnBackgroundInfo.Name = "btnBackgroundInfo";
            this.btnBackgroundInfo.Size = new Size(0x10, 0x10);
            this.btnBackgroundInfo.TabIndex = 0x27;
            this.btnBackgroundInfo.Click += new EventHandler(this.btnBackgroundInfo_Click);
            this.btnBackgroundSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundSelector.Appearance.Options.UseBackColor = true;
            this.btnBackgroundSelector.Appearance.Options.UseForeColor = true;
            this.btnBackgroundSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundSelector.ImageIndex = 0;
            this.btnBackgroundSelector.ImageList = this.imageList_0;
            this.btnBackgroundSelector.Location = new System.Drawing.Point(0xfc, 0x12);
            this.btnBackgroundSelector.Name = "btnBackgroundSelector";
            this.btnBackgroundSelector.Size = new Size(0x10, 0x10);
            this.btnBackgroundSelector.TabIndex = 0x26;
            this.btnBackgroundSelector.Click += new EventHandler(this.btnBackgroundSelector_Click);
            this.cboBackground.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBackground.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBackground.DropDownWidth = 160;
            this.cboBackground.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBackground.Location = new System.Drawing.Point(12, 0x12);
            this.cboBackground.Name = "cboBackground";
            this.cboBackground.Size = new Size(240, 0x1f);
            this.cboBackground.TabIndex = 0x25;
            this.groupBox4.Controls.Add(this.btnshadowInfo);
            this.groupBox4.Controls.Add(this.btnShadowSelector);
            this.groupBox4.Controls.Add(this.cboShadow);
            this.groupBox4.Location = new System.Drawing.Point(0xd8, 0x94);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(280, 60);
            this.groupBox4.TabIndex = 0x2a;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "阴影";
            this.btnshadowInfo.Appearance.BackColor = SystemColors.Window;
            this.btnshadowInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnshadowInfo.Appearance.Options.UseBackColor = true;
            this.btnshadowInfo.Appearance.Options.UseForeColor = true;
            this.btnshadowInfo.ButtonStyle = BorderStyles.Simple;
            this.btnshadowInfo.ImageIndex = 1;
            this.btnshadowInfo.ImageList = this.imageList_0;
            this.btnshadowInfo.Location = new System.Drawing.Point(0xfc, 0x20);
            this.btnshadowInfo.Name = "btnshadowInfo";
            this.btnshadowInfo.Size = new Size(0x10, 0x10);
            this.btnshadowInfo.TabIndex = 0x2a;
            this.btnshadowInfo.Click += new EventHandler(this.btnshadowInfo_Click);
            this.btnShadowSelector.Appearance.BackColor = SystemColors.Window;
            this.btnShadowSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnShadowSelector.Appearance.Options.UseBackColor = true;
            this.btnShadowSelector.Appearance.Options.UseForeColor = true;
            this.btnShadowSelector.ButtonStyle = BorderStyles.Simple;
            this.btnShadowSelector.ImageIndex = 0;
            this.btnShadowSelector.ImageList = this.imageList_0;
            this.btnShadowSelector.Location = new System.Drawing.Point(0xfc, 0x10);
            this.btnShadowSelector.Name = "btnShadowSelector";
            this.btnShadowSelector.Size = new Size(0x10, 0x10);
            this.btnShadowSelector.TabIndex = 0x29;
            this.btnShadowSelector.Click += new EventHandler(this.btnShadowSelector_Click);
            this.cboShadow.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboShadow.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboShadow.DropDownWidth = 160;
            this.cboShadow.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboShadow.Location = new System.Drawing.Point(12, 0x10);
            this.cboShadow.Name = "cboShadow";
            this.cboShadow.Size = new Size(240, 0x1f);
            this.cboShadow.TabIndex = 40;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(0x160, 0xd8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0x2b;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x1a8, 0xd8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 0x2c;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1f8, 0x105);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Icon = ((System.Drawing.Icon)resources.GetObject("$Icon"));
            base.Name = "frmOutline";
            this.Text = "轮廓线";
            base.Load += new EventHandler(this.frmOutline_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtRound.Properties.EndInit();
            this.txtGap.Properties.EndInit();
            this.chkGroup.Properties.EndInit();
            this.chkNewFrameElement.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
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

