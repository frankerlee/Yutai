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
    public class FrameProprtyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private SimpleButton btnBackgroundInfo;
        private SimpleButton btnBackgroundSelector;
        private SimpleButton btnBorderInfo;
        private SimpleButton btnBorderSelector;
        private SimpleButton btnshadowInfo;
        private SimpleButton btnShadowSelector;
        private StyleComboBox cboBackground;
        private StyleComboBox cboBorder;
        private StyleComboBox cboShadow;
        private ColorEdit colorBackground;
        private ColorEdit colorBorder;
        private ColorEdit colorShadow;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IBackground ibackground_0 = null;
        private IBorder iborder_0 = null;
        private IContainer icontainer_0;
        private IFrameElement iframeElement_0 = null;
        private ImageList imageList_0;
        private IShadow ishadow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private string string_0 = "框架";
        private SpinEdit txtBackgroundCornerRounding;
        private SpinEdit txtBackgroundGapx;
        private SpinEdit txtBackgroundGapy;
        private SpinEdit txtBorderCornerRounding;
        private SpinEdit txtBorderGapx;
        private SpinEdit txtBorderGapy;
        private SpinEdit txtShadowCornerRounding;
        private SpinEdit txtShadowGapx;
        private SpinEdit txtShadowGapy;

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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_2);
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

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrameProprtyPage));
            this.groupBox1 = new GroupBox();
            this.txtBorderCornerRounding = new SpinEdit();
            this.label5 = new Label();
            this.txtBorderGapy = new SpinEdit();
            this.label4 = new Label();
            this.label3 = new Label();
            this.txtBorderGapx = new SpinEdit();
            this.label2 = new Label();
            this.label1 = new Label();
            this.colorBorder = new ColorEdit();
            this.btnBorderInfo = new SimpleButton();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnBorderSelector = new SimpleButton();
            this.cboBorder = new StyleComboBox(this.icontainer_0);
            this.groupBox2 = new GroupBox();
            this.txtBackgroundCornerRounding = new SpinEdit();
            this.label6 = new Label();
            this.txtBackgroundGapy = new SpinEdit();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtBackgroundGapx = new SpinEdit();
            this.label9 = new Label();
            this.label10 = new Label();
            this.colorBackground = new ColorEdit();
            this.btnBackgroundInfo = new SimpleButton();
            this.btnBackgroundSelector = new SimpleButton();
            this.cboBackground = new StyleComboBox(this.icontainer_0);
            this.groupBox3 = new GroupBox();
            this.txtShadowCornerRounding = new SpinEdit();
            this.label11 = new Label();
            this.txtShadowGapy = new SpinEdit();
            this.label12 = new Label();
            this.label13 = new Label();
            this.txtShadowGapx = new SpinEdit();
            this.label14 = new Label();
            this.label15 = new Label();
            this.colorShadow = new ColorEdit();
            this.btnshadowInfo = new SimpleButton();
            this.btnShadowSelector = new SimpleButton();
            this.cboShadow = new StyleComboBox(this.icontainer_0);
            this.groupBox1.SuspendLayout();
            this.txtBorderCornerRounding.Properties.BeginInit();
            this.txtBorderGapy.Properties.BeginInit();
            this.txtBorderGapx.Properties.BeginInit();
            this.colorBorder.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtBackgroundCornerRounding.Properties.BeginInit();
            this.txtBackgroundGapy.Properties.BeginInit();
            this.txtBackgroundGapx.Properties.BeginInit();
            this.colorBackground.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtShadowCornerRounding.Properties.BeginInit();
            this.txtShadowGapy.Properties.BeginInit();
            this.txtShadowGapx.Properties.BeginInit();
            this.colorShadow.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.txtBorderCornerRounding);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBorderGapy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBorderGapx);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.colorBorder);
            this.groupBox1.Controls.Add(this.btnBorderInfo);
            this.groupBox1.Controls.Add(this.btnBorderSelector);
            this.groupBox1.Controls.Add(this.cboBorder);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x180, 0x68);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边框";
            int[] bits = new int[4];
            this.txtBorderCornerRounding.EditValue = new decimal(bits);
            this.txtBorderCornerRounding.Location = new Point(280, 0x48);
            this.txtBorderCornerRounding.Name = "txtBorderCornerRounding";
            this.txtBorderCornerRounding.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBorderCornerRounding.Properties.UseCtrlIncrement = false;
            this.txtBorderCornerRounding.Size = new Size(0x30, 0x17);
            this.txtBorderCornerRounding.TabIndex = 0x15;
            this.txtBorderCornerRounding.EditValueChanged += new EventHandler(this.txtBorderCornerRounding_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(240, 0x48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 0x11);
            this.label5.TabIndex = 20;
            this.label5.Text = "圆角:";
            bits = new int[4];
            this.txtBorderGapy.EditValue = new decimal(bits);
            this.txtBorderGapy.Location = new Point(0x98, 0x48);
            this.txtBorderGapy.Name = "txtBorderGapy";
            this.txtBorderGapy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBorderGapy.Properties.UseCtrlIncrement = false;
            this.txtBorderGapy.Size = new Size(0x30, 0x17);
            this.txtBorderGapy.TabIndex = 0x13;
            this.txtBorderGapy.EditValueChanged += new EventHandler(this.txtBorderGapy_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x80, 0x48);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 0x11);
            this.label4.TabIndex = 0x12;
            this.label4.Text = "Y:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x30, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 0x11);
            this.label3.TabIndex = 0x11;
            this.label3.Text = "X:";
            bits = new int[4];
            this.txtBorderGapx.EditValue = new decimal(bits);
            this.txtBorderGapx.Location = new Point(0x48, 0x48);
            this.txtBorderGapx.Name = "txtBorderGapx";
            this.txtBorderGapx.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBorderGapx.Properties.UseCtrlIncrement = false;
            this.txtBorderGapx.Size = new Size(0x30, 0x17);
            this.txtBorderGapx.TabIndex = 0x10;
            this.txtBorderGapx.EditValueChanged += new EventHandler(this.txtBorderGapx_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 0x11);
            this.label2.TabIndex = 15;
            this.label2.Text = "间隔:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(280, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 0x11);
            this.label1.TabIndex = 14;
            this.label1.Text = "颜色:";
            this.colorBorder.EditValue = Color.Empty;
            this.colorBorder.Location = new Point(320, 0x20);
            this.colorBorder.Name = "colorBorder";
            this.colorBorder.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorBorder.Size = new Size(0x30, 0x17);
            this.colorBorder.TabIndex = 13;
            this.colorBorder.EditValueChanged += new EventHandler(this.colorBorder_EditValueChanged);
            this.btnBorderInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBorderInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderInfo.Appearance.Options.UseBackColor = true;
            this.btnBorderInfo.Appearance.Options.UseForeColor = true;
            this.btnBorderInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBorderInfo.ImageIndex = 1;
            this.btnBorderInfo.ImageList = this.imageList_0;
            this.btnBorderInfo.Location = new Point(0x100, 0x27);
            this.btnBorderInfo.Name = "btnBorderInfo";
            this.btnBorderInfo.Size = new Size(0x10, 0x10);
            this.btnBorderInfo.TabIndex = 12;
            this.btnBorderInfo.Click += new EventHandler(this.btnBorderInfo_Click);
            this.imageList_0.ImageSize = new Size(10, 10);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.btnBorderSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBorderSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderSelector.Appearance.Options.UseBackColor = true;
            this.btnBorderSelector.Appearance.Options.UseForeColor = true;
            this.btnBorderSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBorderSelector.ImageIndex = 0;
            this.btnBorderSelector.ImageList = this.imageList_0;
            this.btnBorderSelector.Location = new Point(0x100, 0x18);
            this.btnBorderSelector.Name = "btnBorderSelector";
            this.btnBorderSelector.Size = new Size(0x10, 0x10);
            this.btnBorderSelector.TabIndex = 11;
            this.btnBorderSelector.Click += new EventHandler(this.btnBorderSelector_Click);
            this.cboBorder.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBorder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBorder.DropDownWidth = 160;
            this.cboBorder.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBorder.Location = new Point(0x10, 0x18);
            this.cboBorder.Name = "cboBorder";
            this.cboBorder.Size = new Size(240, 0x1f);
            this.cboBorder.TabIndex = 10;
            this.cboBorder.SelectedIndexChanged += new EventHandler(this.cboBorder_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.txtBackgroundCornerRounding);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtBackgroundGapy);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtBackgroundGapx);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.colorBackground);
            this.groupBox2.Controls.Add(this.btnBackgroundInfo);
            this.groupBox2.Controls.Add(this.btnBackgroundSelector);
            this.groupBox2.Controls.Add(this.cboBackground);
            this.groupBox2.Location = new Point(8, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x180, 0x68);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "背景";
            bits = new int[4];
            this.txtBackgroundCornerRounding.EditValue = new decimal(bits);
            this.txtBackgroundCornerRounding.Location = new Point(280, 0x48);
            this.txtBackgroundCornerRounding.Name = "txtBackgroundCornerRounding";
            this.txtBackgroundCornerRounding.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBackgroundCornerRounding.Properties.UseCtrlIncrement = false;
            this.txtBackgroundCornerRounding.Size = new Size(0x30, 0x17);
            this.txtBackgroundCornerRounding.TabIndex = 0x15;
            this.txtBackgroundCornerRounding.EditValueChanged += new EventHandler(this.txtBackgroundCornerRounding_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(240, 0x48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 0x11);
            this.label6.TabIndex = 20;
            this.label6.Text = "圆角:";
            bits = new int[4];
            this.txtBackgroundGapy.EditValue = new decimal(bits);
            this.txtBackgroundGapy.Location = new Point(0x98, 0x48);
            this.txtBackgroundGapy.Name = "txtBackgroundGapy";
            this.txtBackgroundGapy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBackgroundGapy.Properties.UseCtrlIncrement = false;
            this.txtBackgroundGapy.Size = new Size(0x30, 0x17);
            this.txtBackgroundGapy.TabIndex = 0x13;
            this.txtBackgroundGapy.EditValueChanged += new EventHandler(this.txtBackgroundGapy_EditValueChanged);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x80, 0x48);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x11, 0x11);
            this.label7.TabIndex = 0x12;
            this.label7.Text = "Y:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x30, 0x48);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x11, 0x11);
            this.label8.TabIndex = 0x11;
            this.label8.Text = "X:";
            bits = new int[4];
            this.txtBackgroundGapx.EditValue = new decimal(bits);
            this.txtBackgroundGapx.Location = new Point(0x48, 0x48);
            this.txtBackgroundGapx.Name = "txtBackgroundGapx";
            this.txtBackgroundGapx.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtBackgroundGapx.Properties.UseCtrlIncrement = false;
            this.txtBackgroundGapx.Size = new Size(0x30, 0x17);
            this.txtBackgroundGapx.TabIndex = 0x10;
            this.txtBackgroundGapx.EditValueChanged += new EventHandler(this.txtBackgroundGapx_EditValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 0x48);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 0x11);
            this.label9.TabIndex = 15;
            this.label9.Text = "间隔:";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(280, 0x20);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 0x11);
            this.label10.TabIndex = 14;
            this.label10.Text = "颜色:";
            this.colorBackground.EditValue = Color.Empty;
            this.colorBackground.Location = new Point(320, 0x20);
            this.colorBackground.Name = "colorBackground";
            this.colorBackground.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorBackground.Size = new Size(0x30, 0x17);
            this.colorBackground.TabIndex = 13;
            this.colorBackground.EditValueChanged += new EventHandler(this.colorBackground_EditValueChanged);
            this.btnBackgroundInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundInfo.Appearance.Options.UseBackColor = true;
            this.btnBackgroundInfo.Appearance.Options.UseForeColor = true;
            this.btnBackgroundInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundInfo.ImageIndex = 1;
            this.btnBackgroundInfo.ImageList = this.imageList_0;
            this.btnBackgroundInfo.Location = new Point(0x100, 0x27);
            this.btnBackgroundInfo.Name = "btnBackgroundInfo";
            this.btnBackgroundInfo.Size = new Size(0x10, 0x10);
            this.btnBackgroundInfo.TabIndex = 12;
            this.btnBackgroundInfo.Click += new EventHandler(this.btnBackgroundInfo_Click);
            this.btnBackgroundSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundSelector.Appearance.Options.UseBackColor = true;
            this.btnBackgroundSelector.Appearance.Options.UseForeColor = true;
            this.btnBackgroundSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundSelector.ImageIndex = 0;
            this.btnBackgroundSelector.ImageList = this.imageList_0;
            this.btnBackgroundSelector.Location = new Point(0x100, 0x18);
            this.btnBackgroundSelector.Name = "btnBackgroundSelector";
            this.btnBackgroundSelector.Size = new Size(0x10, 0x10);
            this.btnBackgroundSelector.TabIndex = 11;
            this.btnBackgroundSelector.Click += new EventHandler(this.btnBackgroundSelector_Click);
            this.cboBackground.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBackground.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBackground.DropDownWidth = 160;
            this.cboBackground.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBackground.Location = new Point(0x10, 0x18);
            this.cboBackground.Name = "cboBackground";
            this.cboBackground.Size = new Size(240, 0x1f);
            this.cboBackground.TabIndex = 10;
            this.cboBackground.SelectedIndexChanged += new EventHandler(this.cboBackground_SelectedIndexChanged);
            this.groupBox3.Controls.Add(this.txtShadowCornerRounding);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtShadowGapy);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtShadowGapx);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.colorShadow);
            this.groupBox3.Controls.Add(this.btnshadowInfo);
            this.groupBox3.Controls.Add(this.btnShadowSelector);
            this.groupBox3.Controls.Add(this.cboShadow);
            this.groupBox3.Location = new Point(8, 0xe8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x180, 0x68);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "阴影";
            bits = new int[4];
            this.txtShadowCornerRounding.EditValue = new decimal(bits);
            this.txtShadowCornerRounding.Location = new Point(280, 0x48);
            this.txtShadowCornerRounding.Name = "txtShadowCornerRounding";
            this.txtShadowCornerRounding.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtShadowCornerRounding.Properties.UseCtrlIncrement = false;
            this.txtShadowCornerRounding.Size = new Size(0x30, 0x17);
            this.txtShadowCornerRounding.TabIndex = 0x15;
            this.txtShadowCornerRounding.EditValueChanged += new EventHandler(this.txtShadowCornerRounding_EditValueChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(240, 0x48);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x23, 0x11);
            this.label11.TabIndex = 20;
            this.label11.Text = "圆角:";
            bits = new int[4];
            this.txtShadowGapy.EditValue = new decimal(bits);
            this.txtShadowGapy.Location = new Point(0x98, 0x48);
            this.txtShadowGapy.Name = "txtShadowGapy";
            this.txtShadowGapy.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtShadowGapy.Properties.UseCtrlIncrement = false;
            this.txtShadowGapy.Size = new Size(0x30, 0x17);
            this.txtShadowGapy.TabIndex = 0x13;
            this.txtShadowGapy.EditValueChanged += new EventHandler(this.txtShadowGapy_EditValueChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x80, 0x48);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x11, 0x11);
            this.label12.TabIndex = 0x12;
            this.label12.Text = "Y:";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x30, 0x48);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 0x11);
            this.label13.TabIndex = 0x11;
            this.label13.Text = "X:";
            bits = new int[4];
            this.txtShadowGapx.EditValue = new decimal(bits);
            this.txtShadowGapx.Location = new Point(0x48, 0x48);
            this.txtShadowGapx.Name = "txtShadowGapx";
            this.txtShadowGapx.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtShadowGapx.Properties.UseCtrlIncrement = false;
            this.txtShadowGapx.Size = new Size(0x30, 0x17);
            this.txtShadowGapx.TabIndex = 0x10;
            this.txtShadowGapx.EditValueChanged += new EventHandler(this.txtShadowGapx_EditValueChanged);
            this.label14.AutoSize = true;
            this.label14.Location = new Point(8, 0x48);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x23, 0x11);
            this.label14.TabIndex = 15;
            this.label14.Text = "间隔:";
            this.label15.AutoSize = true;
            this.label15.Location = new Point(280, 0x20);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x23, 0x11);
            this.label15.TabIndex = 14;
            this.label15.Text = "颜色:";
            this.colorShadow.EditValue = Color.Empty;
            this.colorShadow.Location = new Point(320, 0x20);
            this.colorShadow.Name = "colorShadow";
            this.colorShadow.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorShadow.Size = new Size(0x30, 0x17);
            this.colorShadow.TabIndex = 13;
            this.colorShadow.EditValueChanged += new EventHandler(this.colorShadow_EditValueChanged);
            this.btnshadowInfo.Appearance.BackColor = SystemColors.Window;
            this.btnshadowInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnshadowInfo.Appearance.Options.UseBackColor = true;
            this.btnshadowInfo.Appearance.Options.UseForeColor = true;
            this.btnshadowInfo.ButtonStyle = BorderStyles.Simple;
            this.btnshadowInfo.ImageIndex = 1;
            this.btnshadowInfo.ImageList = this.imageList_0;
            this.btnshadowInfo.Location = new Point(0x100, 0x27);
            this.btnshadowInfo.Name = "btnshadowInfo";
            this.btnshadowInfo.Size = new Size(0x10, 0x10);
            this.btnshadowInfo.TabIndex = 12;
            this.btnshadowInfo.Click += new EventHandler(this.btnshadowInfo_Click);
            this.btnShadowSelector.Appearance.BackColor = SystemColors.Window;
            this.btnShadowSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnShadowSelector.Appearance.Options.UseBackColor = true;
            this.btnShadowSelector.Appearance.Options.UseForeColor = true;
            this.btnShadowSelector.ButtonStyle = BorderStyles.Simple;
            this.btnShadowSelector.ImageIndex = 0;
            this.btnShadowSelector.ImageList = this.imageList_0;
            this.btnShadowSelector.Location = new Point(0x100, 0x18);
            this.btnShadowSelector.Name = "btnShadowSelector";
            this.btnShadowSelector.Size = new Size(0x10, 0x10);
            this.btnShadowSelector.TabIndex = 11;
            this.btnShadowSelector.Click += new EventHandler(this.btnShadowSelector_Click);
            this.cboShadow.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboShadow.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboShadow.DropDownWidth = 160;
            this.cboShadow.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboShadow.Location = new Point(0x10, 0x18);
            this.cboShadow.Name = "cboShadow";
            this.cboShadow.Size = new Size(240, 0x1f);
            this.cboShadow.TabIndex = 10;
            this.cboShadow.SelectedIndexChanged += new EventHandler(this.cboShadow_SelectedIndexChanged);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "FrameProprtyPage";
            base.Size = new Size(0x198, 0x160);
            base.Load += new EventHandler(this.FrameProprtyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtBorderCornerRounding.Properties.EndInit();
            this.txtBorderGapy.Properties.EndInit();
            this.txtBorderGapx.Properties.EndInit();
            this.colorBorder.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtBackgroundCornerRounding.Properties.EndInit();
            this.txtBackgroundGapy.Properties.EndInit();
            this.txtBackgroundGapx.Properties.EndInit();
            this.colorBackground.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtShadowCornerRounding.Properties.EndInit();
            this.txtShadowGapy.Properties.EndInit();
            this.txtShadowGapx.Properties.EndInit();
            this.colorShadow.Properties.EndInit();
            base.ResumeLayout(false);
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
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

