using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class FrameProprtyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IBackground ibackground_0 = null;
        private IBorder iborder_0 = null;
        private IFrameElement iframeElement_0 = null;
        private IShadow ishadow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private string string_0 = "框架";

        public event OnValueChangeEventHandler OnValueChange;

        public FrameProprtyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.iborder_0 != null)
                {
                    this.iframeElement_0.Border = (this.iborder_0 as IClone).Clone() as IBorder;
                }
                if (this.ibackground_0 != null)
                {
                    this.iframeElement_0.Background = (this.ibackground_0 as IClone) as IBackground;
                }
                if (this.ishadow_0 != null)
                {
                    (this.iframeElement_0 as IFrameProperties).Shadow = (this.ishadow_0 as IClone) as IShadow;
                }
            }
        }

        private void btnBackgroundInfo_Click(object sender, EventArgs e)
        {
            if (this.ibackground_0 != null)
            {
                frmElementProperty property = new frmElementProperty();
                BackgroundSymbolPropertyPage page = new BackgroundSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(this.ibackground_0))
                {
                    this.bool_0 = false;
                    this.method_3();
                    IStyleGalleryItem styleGalleryItemAt = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                    if (styleGalleryItemAt != null)
                    {
                        if (styleGalleryItemAt.Name == "<定制>")
                        {
                            styleGalleryItemAt.Item = this.ibackground_0;
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.ibackground_0
                            };
                            this.cboBackground.Add(styleGalleryItemAt);
                            this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                        }
                    }
                    else
                    {
                        styleGalleryItemAt = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = this.ibackground_0
                        };
                        this.cboBackground.Add(styleGalleryItemAt);
                        this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                    }
                    this.bool_0 = true;
                    this.method_0();
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
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (this.ibackground_0 != null)
                    {
                        selector.SetSymbol(this.ibackground_0);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolBackgroundClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.bool_0 = false;
                        this.ibackground_0 = selector.GetSymbol() as IBackground;
                        this.method_3();
                        IStyleGalleryItem styleGalleryItemAt = this.cboBackground.GetStyleGalleryItemAt(this.cboBackground.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = this.ibackground_0;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = this.ibackground_0
                                };
                                this.cboBackground.Add(styleGalleryItemAt);
                                this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.ibackground_0
                            };
                            this.cboBackground.Add(styleGalleryItemAt);
                            this.cboBackground.SelectedIndex = this.cboBackground.Items.Count - 1;
                        }
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnBorderInfo_Click(object sender, EventArgs e)
        {
            if (this.iborder_0 != null)
            {
                frmElementProperty property = new frmElementProperty();
                BorderSymbolPropertyPage page = new BorderSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(this.iborder_0))
                {
                    this.bool_0 = false;
                    this.method_2();
                    IStyleGalleryItem styleGalleryItemAt = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                    if (styleGalleryItemAt != null)
                    {
                        if (styleGalleryItemAt.Name == "<定制>")
                        {
                            styleGalleryItemAt.Item = this.iborder_0;
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.iborder_0
                            };
                            this.cboBorder.Add(styleGalleryItemAt);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                    }
                    else
                    {
                        styleGalleryItemAt = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = this.iborder_0
                        };
                        this.cboBorder.Add(styleGalleryItemAt);
                        this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                    }
                    this.bool_0 = true;
                    this.method_0();
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
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (this.iborder_0 != null)
                    {
                        selector.SetSymbol(this.iborder_0);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolBorderClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.iborder_0 = selector.GetSymbol() as IBorder;
                        this.bool_0 = false;
                        this.method_2();
                        IStyleGalleryItem styleGalleryItemAt = this.cboBorder.GetStyleGalleryItemAt(this.cboBorder.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = this.iborder_0;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = this.iborder_0
                                };
                                this.cboBorder.Add(styleGalleryItemAt);
                                this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.iborder_0
                            };
                            this.cboBorder.Add(styleGalleryItemAt);
                            this.cboBorder.SelectedIndex = this.cboBorder.Items.Count - 1;
                        }
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnshadowInfo_Click(object sender, EventArgs e)
        {
            if (this.ishadow_0 != null)
            {
                frmElementProperty property = new frmElementProperty();
                ShadowSymbolPropertyPage page = new ShadowSymbolPropertyPage();
                property.AddPage(page);
                if (property.EditProperties(this.ishadow_0))
                {
                    this.bool_0 = false;
                    this.method_4();
                    IStyleGalleryItem styleGalleryItemAt = this.cboShadow.GetStyleGalleryItemAt(this.cboShadow.Items.Count - 1);
                    if (styleGalleryItemAt != null)
                    {
                        if (styleGalleryItemAt.Name == "<定制>")
                        {
                            styleGalleryItemAt.Item = this.ishadow_0;
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.ishadow_0
                            };
                            this.cboShadow.Add(styleGalleryItemAt);
                            this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                        }
                    }
                    else
                    {
                        styleGalleryItemAt = new MyStyleGalleryItem {
                            Name = "<定制>",
                            Item = this.ishadow_0
                        };
                        this.cboShadow.Add(styleGalleryItemAt);
                        this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                    }
                    this.bool_0 = true;
                    this.method_0();
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
                    selector.SetStyleGallery(this.istyleGallery_0);
                    if (this.ishadow_0 != null)
                    {
                        selector.SetSymbol(this.ishadow_0);
                    }
                    else
                    {
                        selector.SetSymbol(new SymbolShadowClass());
                    }
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.bool_0 = false;
                        this.ishadow_0 = selector.GetSymbol() as IShadow;
                        this.method_4();
                        IStyleGalleryItem styleGalleryItemAt = this.cboShadow.GetStyleGalleryItemAt(this.cboShadow.Items.Count - 1);
                        if (styleGalleryItemAt != null)
                        {
                            if (styleGalleryItemAt.Name == "<定制>")
                            {
                                styleGalleryItemAt.Item = this.ishadow_0;
                            }
                            else
                            {
                                styleGalleryItemAt = new MyStyleGalleryItem {
                                    Name = "<定制>",
                                    Item = this.ishadow_0
                                };
                                this.cboShadow.Add(styleGalleryItemAt);
                                this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                            }
                        }
                        else
                        {
                            styleGalleryItemAt = new MyStyleGalleryItem {
                                Name = "<定制>",
                                Item = this.ishadow_0
                            };
                            this.cboShadow.Add(styleGalleryItemAt);
                            this.cboShadow.SelectedIndex = this.cboShadow.Items.Count - 1;
                        }
                        this.bool_0 = true;
                        this.method_0();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
        }

        private void cboBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.cboBackground.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem == null)
                {
                    this.ibackground_0 = null;
                }
                else
                {
                    this.ibackground_0 = (selectStyleGalleryItem.Item as IClone).Clone() as IBackground;
                }
                this.bool_0 = false;
                this.method_3();
                this.bool_0 = true;
                this.method_0();
            }
        }

        private void cboBorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.cboBorder.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem == null)
                {
                    this.iborder_0 = null;
                }
                else
                {
                    this.iborder_0 = (selectStyleGalleryItem.Item as IClone).Clone() as IBorder;
                }
                this.bool_0 = false;
                this.method_2();
                this.bool_0 = true;
                this.method_0();
            }
        }

        private void cboShadow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IStyleGalleryItem selectStyleGalleryItem = this.cboShadow.GetSelectStyleGalleryItem();
                if (selectStyleGalleryItem == null)
                {
                    this.ishadow_0 = null;
                }
                else
                {
                    this.ishadow_0 = (selectStyleGalleryItem.Item as IClone).Clone() as IShadow;
                }
                this.bool_0 = false;
                this.method_4();
                this.bool_0 = true;
                this.method_0();
            }
        }

        private void colorBackground_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ibackground_0 as IFrameDecoration;
                IColor color = decoration.Color;
                this.method_5(this.colorBackground, color);
                decoration.Color = color;
                this.cboBackground.Invalidate();
                this.method_0();
            }
        }

        private void colorBorder_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.iborder_0 as IFrameDecoration;
                IColor color = decoration.Color;
                this.method_5(this.colorBorder, color);
                decoration.Color = color;
                this.cboBorder.Invalidate();
                this.method_0();
            }
        }

        private void colorShadow_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ishadow_0 as IFrameDecoration;
                IColor color = decoration.Color;
                this.method_5(this.colorShadow, color);
                decoration.Color = color;
                this.cboShadow.Invalidate();
                this.method_0();
            }
        }

 private void FrameProprtyPage_Load(object sender, EventArgs e)
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
            if (this.iframeElement_0 != null)
            {
                this.method_1();
                this.bool_0 = true;
            }
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
            IStyleGalleryItem oO = null;
            if (this.iborder_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.iborder_0
                };
            }
            this.cboBorder.SelectStyleGalleryItem(oO);
            if (this.ibackground_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.ibackground_0
                };
            }
            this.cboBackground.SelectStyleGalleryItem(oO);
            if (this.ishadow_0 == null)
            {
                oO = null;
            }
            else
            {
                oO = new MyStyleGalleryItem {
                    Name = "<定制>",
                    Item = this.ishadow_0
                };
            }
            this.cboShadow.SelectStyleGalleryItem(oO);
            this.method_2();
            this.method_3();
            this.method_4();
        }

        private void method_2()
        {
            if (this.iborder_0 == null)
            {
                this.colorBorder.Enabled = false;
                this.txtBorderGapx.Enabled = false;
                this.txtBorderGapy.Enabled = false;
                this.txtBorderCornerRounding.Enabled = false;
            }
            else
            {
                this.colorBorder.Enabled = true;
                this.txtBorderGapx.Enabled = true;
                this.txtBorderGapy.Enabled = true;
                this.txtBorderCornerRounding.Enabled = true;
                IFrameDecoration decoration = this.iborder_0 as IFrameDecoration;
                this.txtBorderGapx.Text = decoration.HorizontalSpacing.ToString("0.##");
                this.txtBorderGapy.Text = decoration.VerticalSpacing.ToString("0.##");
                this.txtBorderCornerRounding.Text = decoration.CornerRounding.ToString();
                this.method_6(this.colorBorder, decoration.Color);
            }
        }

        private void method_3()
        {
            if (this.ibackground_0 == null)
            {
                this.colorBackground.Enabled = false;
                this.txtBackgroundGapx.Enabled = false;
                this.txtBackgroundGapy.Enabled = false;
                this.txtBackgroundCornerRounding.Enabled = false;
            }
            else
            {
                this.colorBackground.Enabled = true;
                this.txtBackgroundGapx.Enabled = true;
                this.txtBackgroundGapy.Enabled = true;
                this.txtBackgroundCornerRounding.Enabled = true;
                IFrameDecoration decoration = this.ibackground_0 as IFrameDecoration;
                this.txtBackgroundGapx.Text = decoration.HorizontalSpacing.ToString("0.##");
                this.txtBackgroundGapy.Text = decoration.VerticalSpacing.ToString("0.##");
                this.txtBackgroundCornerRounding.Text = decoration.CornerRounding.ToString();
                this.method_6(this.colorBackground, decoration.Color);
            }
        }

        private void method_4()
        {
            if (this.ishadow_0 == null)
            {
                this.colorShadow.Enabled = false;
                this.txtShadowGapx.Enabled = false;
                this.txtShadowGapy.Enabled = false;
                this.txtShadowCornerRounding.Enabled = false;
            }
            else
            {
                this.colorShadow.Enabled = true;
                this.txtShadowGapx.Enabled = true;
                this.txtShadowGapy.Enabled = true;
                this.txtShadowCornerRounding.Enabled = true;
                IFrameDecoration decoration = this.ishadow_0 as IFrameDecoration;
                this.txtShadowGapx.Text = decoration.HorizontalSpacing.ToString("0.##");
                this.txtShadowGapy.Text = decoration.VerticalSpacing.ToString("0.##");
                this.txtShadowCornerRounding.Text = decoration.CornerRounding.ToString();
                this.method_6(this.colorShadow, decoration.Color);
            }
        }

        private void method_5(ColorEdit colorEdit_0, IColor icolor_0)
        {
            icolor_0.NullColor = colorEdit_0.Color.IsEmpty;
            if (!colorEdit_0.Color.IsEmpty)
            {
                icolor_0.Transparency = colorEdit_0.Color.A;
                icolor_0.RGB = ColorManage.EsriRGB(colorEdit_0.Color.R, colorEdit_0.Color.G, colorEdit_0.Color.B);
            }
        }

        private void method_6(ColorEdit colorEdit_0, IColor icolor_0)
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
                ColorManage.GetEsriRGB((uint) icolor_0.RGB, out num2, out num3, out num4);
                colorEdit_0.Color = Color.FromArgb(icolor_0.Transparency, num2, num3, num4);
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
            this.iframeElement_0 = object_0 as IFrameElement;
            if (this.iframeElement_0.Border != null)
            {
                this.iborder_0 = (this.iframeElement_0.Border as IClone).Clone() as IBorder;
            }
            if (this.iframeElement_0.Background != null)
            {
                this.ibackground_0 = (this.iframeElement_0.Background as IClone).Clone() as IBackground;
            }
            if ((this.iframeElement_0 as IFrameProperties).Shadow != null)
            {
                this.ishadow_0 = ((this.iframeElement_0 as IFrameProperties).Shadow as IClone).Clone() as IShadow;
            }
        }

        private void txtBackgroundCornerRounding_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ibackground_0 as IFrameDecoration;
                try
                {
                    decoration.CornerRounding = short.Parse(this.txtBackgroundCornerRounding.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtBackgroundGapx_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ibackground_0 as IFrameDecoration;
                try
                {
                    decoration.HorizontalSpacing = double.Parse(this.txtBackgroundGapx.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtBackgroundGapy_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ibackground_0 as IFrameDecoration;
                try
                {
                    decoration.VerticalSpacing = double.Parse(this.txtBackgroundGapy.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtBorderCornerRounding_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.iborder_0 as IFrameDecoration;
                try
                {
                    decoration.CornerRounding = short.Parse(this.txtBorderCornerRounding.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtBorderGapx_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.iborder_0 as IFrameDecoration;
                try
                {
                    decoration.HorizontalSpacing = double.Parse(this.txtBorderGapx.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtBorderGapy_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.iborder_0 as IFrameDecoration;
                try
                {
                    decoration.VerticalSpacing = double.Parse(this.txtBorderGapy.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtShadowCornerRounding_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ishadow_0 as IFrameDecoration;
                try
                {
                    decoration.CornerRounding = short.Parse(this.txtShadowCornerRounding.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtShadowGapx_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ishadow_0 as IFrameDecoration;
                try
                {
                    decoration.HorizontalSpacing = double.Parse(this.txtShadowGapx.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
        }

        private void txtShadowGapy_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                IFrameDecoration decoration = this.ishadow_0 as IFrameDecoration;
                try
                {
                    decoration.VerticalSpacing = double.Parse(this.txtShadowGapy.Text);
                    this.method_0();
                }
                catch
                {
                }
            }
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

