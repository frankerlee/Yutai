using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class LegendFrameUserControl : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnBackgroundInfo;
        private SimpleButton btnBackgroundSelector;
        private SimpleButton btnBorderInfo;
        private SimpleButton btnBorderSelector;
        private SimpleButton btnshadowInfo;
        private SimpleButton btnShadowSelector;
        private StyleComboBox cboBackground;
        private StyleComboBox cboBorder;
        private StyleComboBox cboShadow;
        private IBackground ibackground_0 = null;
        private IBorder iborder_0 = null;
        private IContainer icontainer_0;
        private IFrameElement iframeElement_0 = null;
        private ImageList imageList_0;
        private IShadow ishadow_0 = null;
        private IStyleGallery istyleGallery_0 = ApplicationBase.StyleGallery;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label6;
        private Label label9;
        private SpinEdit txtCornerRounding;
        private SpinEdit txtGap;

        public LegendFrameUserControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendFrameUserControl));
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnBorderInfo = new SimpleButton();
            this.btnBorderSelector = new SimpleButton();
            this.cboBorder = new StyleComboBox(this.icontainer_0);
            this.label16 = new Label();
            this.label17 = new Label();
            this.label18 = new Label();
            this.btnBackgroundInfo = new SimpleButton();
            this.btnBackgroundSelector = new SimpleButton();
            this.cboBackground = new StyleComboBox(this.icontainer_0);
            this.btnshadowInfo = new SimpleButton();
            this.btnShadowSelector = new SimpleButton();
            this.cboShadow = new StyleComboBox(this.icontainer_0);
            this.txtCornerRounding = new SpinEdit();
            this.label6 = new Label();
            this.txtGap = new SpinEdit();
            this.label9 = new Label();
            this.txtCornerRounding.Properties.BeginInit();
            this.txtGap.Properties.BeginInit();
            base.SuspendLayout();
            this.imageList_0.ImageSize = new Size(10, 10);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.btnBorderInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBorderInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderInfo.Appearance.Options.UseBackColor = true;
            this.btnBorderInfo.Appearance.Options.UseForeColor = true;
            this.btnBorderInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBorderInfo.ImageIndex = 1;
            this.btnBorderInfo.ImageList = this.imageList_0;
            this.btnBorderInfo.Location = new Point(0xf8, 0x29);
            this.btnBorderInfo.Name = "btnBorderInfo";
            this.btnBorderInfo.Size = new Size(0x10, 0x10);
            this.btnBorderInfo.TabIndex = 0x12;
            this.btnBorderSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBorderSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBorderSelector.Appearance.Options.UseBackColor = true;
            this.btnBorderSelector.Appearance.Options.UseForeColor = true;
            this.btnBorderSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBorderSelector.ImageIndex = 0;
            this.btnBorderSelector.ImageList = this.imageList_0;
            this.btnBorderSelector.Location = new Point(0xf8, 0x19);
            this.btnBorderSelector.Name = "btnBorderSelector";
            this.btnBorderSelector.Size = new Size(0x10, 0x10);
            this.btnBorderSelector.TabIndex = 0x11;
            this.cboBorder.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBorder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBorder.DropDownWidth = 160;
            this.cboBorder.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBorder.Location = new Point(8, 0x19);
            this.cboBorder.Name = "cboBorder";
            this.cboBorder.Size = new Size(240, 0x1f);
            this.cboBorder.TabIndex = 0x10;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(8, 8);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 0x11);
            this.label16.TabIndex = 0x13;
            this.label16.Text = "边框";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(8, 0x40);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x1d, 0x11);
            this.label17.TabIndex = 20;
            this.label17.Text = "背景";
            this.label18.AutoSize = true;
            this.label18.Location = new Point(8, 0x80);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x1d, 0x11);
            this.label18.TabIndex = 0x15;
            this.label18.Text = "阴影";
            this.btnBackgroundInfo.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundInfo.Appearance.Options.UseBackColor = true;
            this.btnBackgroundInfo.Appearance.Options.UseForeColor = true;
            this.btnBackgroundInfo.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundInfo.ImageIndex = 1;
            this.btnBackgroundInfo.ImageList = this.imageList_0;
            this.btnBackgroundInfo.Location = new Point(0xf8, 0x68);
            this.btnBackgroundInfo.Name = "btnBackgroundInfo";
            this.btnBackgroundInfo.Size = new Size(0x10, 0x10);
            this.btnBackgroundInfo.TabIndex = 0x18;
            this.btnBackgroundSelector.Appearance.BackColor = SystemColors.Window;
            this.btnBackgroundSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnBackgroundSelector.Appearance.Options.UseBackColor = true;
            this.btnBackgroundSelector.Appearance.Options.UseForeColor = true;
            this.btnBackgroundSelector.ButtonStyle = BorderStyles.Simple;
            this.btnBackgroundSelector.ImageIndex = 0;
            this.btnBackgroundSelector.ImageList = this.imageList_0;
            this.btnBackgroundSelector.Location = new Point(0xf8, 0x58);
            this.btnBackgroundSelector.Name = "btnBackgroundSelector";
            this.btnBackgroundSelector.Size = new Size(0x10, 0x10);
            this.btnBackgroundSelector.TabIndex = 0x17;
            this.cboBackground.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboBackground.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBackground.DropDownWidth = 160;
            this.cboBackground.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboBackground.Location = new Point(8, 0x58);
            this.cboBackground.Name = "cboBackground";
            this.cboBackground.Size = new Size(240, 0x1f);
            this.cboBackground.TabIndex = 0x16;
            this.btnshadowInfo.Appearance.BackColor = SystemColors.Window;
            this.btnshadowInfo.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnshadowInfo.Appearance.Options.UseBackColor = true;
            this.btnshadowInfo.Appearance.Options.UseForeColor = true;
            this.btnshadowInfo.ButtonStyle = BorderStyles.Simple;
            this.btnshadowInfo.ImageIndex = 1;
            this.btnshadowInfo.ImageList = this.imageList_0;
            this.btnshadowInfo.Location = new Point(0xf8, 0xa8);
            this.btnshadowInfo.Name = "btnshadowInfo";
            this.btnshadowInfo.Size = new Size(0x10, 0x10);
            this.btnshadowInfo.TabIndex = 0x1b;
            this.btnShadowSelector.Appearance.BackColor = SystemColors.Window;
            this.btnShadowSelector.Appearance.ForeColor = SystemColors.ControlLightLight;
            this.btnShadowSelector.Appearance.Options.UseBackColor = true;
            this.btnShadowSelector.Appearance.Options.UseForeColor = true;
            this.btnShadowSelector.ButtonStyle = BorderStyles.Simple;
            this.btnShadowSelector.ImageIndex = 0;
            this.btnShadowSelector.ImageList = this.imageList_0;
            this.btnShadowSelector.Location = new Point(0xf8, 0x98);
            this.btnShadowSelector.Name = "btnShadowSelector";
            this.btnShadowSelector.Size = new Size(0x10, 0x10);
            this.btnShadowSelector.TabIndex = 0x1a;
            this.cboShadow.DrawMode = DrawMode.OwnerDrawVariable;
            this.cboShadow.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboShadow.DropDownWidth = 160;
            this.cboShadow.Font = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.cboShadow.Location = new Point(8, 0x98);
            this.cboShadow.Name = "cboShadow";
            this.cboShadow.Size = new Size(240, 0x1f);
            this.cboShadow.TabIndex = 0x19;
            int[] bits = new int[4];
            this.txtCornerRounding.EditValue = new decimal(bits);
            this.txtCornerRounding.Location = new Point(0xb8, 0xc4);
            this.txtCornerRounding.Name = "txtCornerRounding";
            this.txtCornerRounding.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtCornerRounding.Properties.UseCtrlIncrement = false;
            this.txtCornerRounding.Size = new Size(0x30, 0x17);
            this.txtCornerRounding.TabIndex = 0x1f;
            this.txtCornerRounding.EditValueChanged += new EventHandler(this.txtCornerRounding_EditValueChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x90, 200);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 0x11);
            this.label6.TabIndex = 30;
            this.label6.Text = "圆角:";
            bits = new int[4];
            bits[0] = 10;
            this.txtGap.EditValue = new decimal(bits);
            this.txtGap.Location = new Point(0x30, 0xc4);
            this.txtGap.Name = "txtGap";
            this.txtGap.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtGap.Properties.UseCtrlIncrement = false;
            this.txtGap.Size = new Size(0x30, 0x17);
            this.txtGap.TabIndex = 0x1d;
            this.txtGap.EditValueChanged += new EventHandler(this.txtGap_EditValueChanged);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(8, 200);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 0x11);
            this.label9.TabIndex = 0x1c;
            this.label9.Text = "间隔:";
            base.Controls.Add(this.txtCornerRounding);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.txtGap);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.btnshadowInfo);
            base.Controls.Add(this.btnShadowSelector);
            base.Controls.Add(this.cboShadow);
            base.Controls.Add(this.btnBackgroundInfo);
            base.Controls.Add(this.btnBackgroundSelector);
            base.Controls.Add(this.cboBackground);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.label17);
            base.Controls.Add(this.label16);
            base.Controls.Add(this.btnBorderInfo);
            base.Controls.Add(this.btnBorderSelector);
            base.Controls.Add(this.cboBorder);
            base.Name = "LegendFrameUserControl";
            base.Size = new Size(0x120, 0xf8);
            base.Load += new EventHandler(this.LegendFrameUserControl_Load);
            this.txtCornerRounding.Properties.EndInit();
            this.txtGap.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void LegendFrameUserControl_Load(object sender, EventArgs e)
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
                this.method_0();
                this.bool_0 = true;
            }
        }

        private void method_0()
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
        }

        private void txtCornerRounding_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtCornerRounding.ForeColor = Color.Black;
                short num = short.Parse(this.txtCornerRounding.Text);
                if ((num < 0) || (num > 100))
                {
                    this.txtCornerRounding.ForeColor = Color.Red;
                }
                else
                {
                    if (this.iborder_0 != null)
                    {
                        (this.iborder_0 as IFrameDecoration).CornerRounding = num;
                    }
                    if (this.ibackground_0 != null)
                    {
                        (this.ibackground_0 as IFrameDecoration).CornerRounding = num;
                    }
                    if (this.ishadow_0 != null)
                    {
                        (this.ishadow_0 as IFrameDecoration).CornerRounding = num;
                    }
                }
            }
            catch
            {
                this.txtCornerRounding.ForeColor = Color.Red;
            }
        }

        private void txtGap_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtGap.ForeColor = Color.Black;
                double num = double.Parse(this.txtGap.Text);
                if (this.iborder_0 != null)
                {
                    (this.iborder_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.iborder_0 as IFrameDecoration).VerticalSpacing = num;
                }
                if (this.ibackground_0 != null)
                {
                    (this.ibackground_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.ibackground_0 as IFrameDecoration).VerticalSpacing = num;
                }
                if (this.ishadow_0 != null)
                {
                    (this.ishadow_0 as IFrameDecoration).HorizontalSpacing = num;
                    (this.ishadow_0 as IFrameDecoration).VerticalSpacing = num;
                }
            }
            catch
            {
                this.txtGap.ForeColor = Color.Red;
            }
        }

        public IFrameElement LegendFrame
        {
            set
            {
                this.iframeElement_0 = value;
                this.iborder_0 = this.iframeElement_0.Border;
                this.ibackground_0 = this.iframeElement_0.Background;
                this.ishadow_0 = (this.iframeElement_0 as IFrameProperties).Shadow;
            }
        }
    }
}

