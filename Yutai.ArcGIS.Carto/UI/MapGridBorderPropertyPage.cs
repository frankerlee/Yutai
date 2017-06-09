using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class MapGridBorderPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnCalibratedMapBorder;
        private StyleButton btnOutlineSymbol;
        private StyleButton btnSimpleBorderLine;
        private StyleButton btnSimpleBorderLineSymbol;
        private CheckEdit chkOutline;
        private CheckEdit chkUseSimpleBorder;
        private Container container_0 = null;
        private GroupBox groupBox2;
        private GroupBox groupBoxGraticuleBorder;
        private GroupBox groupBoxGridBorder;
        private GroupBox groupBoxGridProperty;
        private IMapGrid imapGrid_0 = null;
        private RadioGroup rdoGenerateGraphicsType;
        private RadioGroup rdoGraticuleBorderType;

        public MapGridBorderPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnCalibratedMapBorder_Click(object sender, EventArgs e)
        {
            if (this.imapGrid_0.Border is ICalibratedMapGridBorder)
            {
                frmCalibratedMapBorder border = new frmCalibratedMapBorder {
                    CalibratedMapGridBorder = this.imapGrid_0.Border as ICalibratedMapGridBorder
                };
                if (border.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void btnOutlineSymbol_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            selector.SetSymbol(this.btnOutlineSymbol.Style);
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.btnOutlineSymbol.Style = selector.GetSymbol();
            }
        }

        private void btnSimpleBorderLine_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            ILineSymbol style = this.btnSimpleBorderLine.Style as ILineSymbol;
            if (style == null)
            {
                style = new SimpleLineSymbolClass();
            }
            selector.SetSymbol(style);
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.btnSimpleBorderLine.Style = selector.GetSymbol();
                if (this.imapGrid_0.Border is ISimpleMapGridBorder)
                {
                    (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol = this.btnSimpleBorderLine.Style as ILineSymbol;
                }
            }
        }

        private void btnSimpleBorderLineSymbol_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            ILineSymbol style = this.btnSimpleBorderLineSymbol.Style as ILineSymbol;
            if (style == null)
            {
                style = new SimpleLineSymbolClass();
            }
            selector.SetSymbol(style);
            selector.SetStyleGallery(ApplicationBase.StyleGallery);
            if (selector.ShowDialog() == DialogResult.OK)
            {
                this.btnSimpleBorderLineSymbol.Style = selector.GetSymbol();
                if (this.imapGrid_0.Border is ISimpleMapGridBorder)
                {
                    (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol = this.btnSimpleBorderLineSymbol.Style as ILineSymbol;
                }
            }
        }

        private void chkOutline_CheckedChanged(object sender, EventArgs e)
        {
            this.btnOutlineSymbol.Enabled = this.chkOutline.Checked;
        }

        private void chkUseSimpleBorder_CheckedChanged(object sender, EventArgs e)
        {
            this.btnSimpleBorderLine.Enabled = this.chkUseSimpleBorder.Checked;
            if (this.chkUseSimpleBorder.Checked && !(this.imapGrid_0.Border is ISimpleMapGridBorder))
            {
                ISimpleMapGridBorder border = new SimpleMapGridBorderClass {
                    LineSymbol = this.btnSimpleBorderLine.Style as ILineSymbol
                };
                this.imapGrid_0.Border = border as IMapGridBorder;
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        public void Init()
        {
            if (this.imapGrid_0 != null)
            {
                if (this.imapGrid_0 is IGraticule)
                {
                    this.groupBoxGraticuleBorder.Visible = true;
                    this.groupBoxGridBorder.Visible = false;
                    if (this.imapGrid_0.Border is ICalibratedMapGridBorder)
                    {
                        this.rdoGraticuleBorderType.SelectedIndex = 1;
                        this.btnSimpleBorderLineSymbol.Enabled = false;
                        this.btnSimpleBorderLineSymbol.Style = new SimpleLineSymbolClass();
                        this.btnCalibratedMapBorder.Enabled = true;
                    }
                    else if (this.imapGrid_0.Border is ISimpleMapGridBorder)
                    {
                        this.rdoGraticuleBorderType.SelectedIndex = 0;
                        this.btnSimpleBorderLineSymbol.Enabled = true;
                        this.btnCalibratedMapBorder.Enabled = false;
                        this.btnSimpleBorderLineSymbol.Style = (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol;
                    }
                }
                else
                {
                    this.groupBoxGraticuleBorder.Visible = false;
                    this.groupBoxGridBorder.Visible = true;
                    if (this.imapGrid_0.Border is ISimpleMapGridBorder)
                    {
                        this.chkUseSimpleBorder.Checked = true;
                        this.btnSimpleBorderLine.Style = (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol;
                        this.btnSimpleBorderLine.Enabled = true;
                    }
                    else
                    {
                        this.chkUseSimpleBorder.Checked = false;
                        this.btnSimpleBorderLine.Style = new SimpleLineSymbolClass();
                        this.btnSimpleBorderLine.Enabled = false;
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.btnOutlineSymbol = new StyleButton();
            this.chkOutline = new CheckEdit();
            this.groupBoxGridBorder = new GroupBox();
            this.btnSimpleBorderLine = new StyleButton();
            this.chkUseSimpleBorder = new CheckEdit();
            this.groupBoxGridProperty = new GroupBox();
            this.rdoGenerateGraphicsType = new RadioGroup();
            this.groupBoxGraticuleBorder = new GroupBox();
            this.btnCalibratedMapBorder = new SimpleButton();
            this.btnSimpleBorderLineSymbol = new StyleButton();
            this.rdoGraticuleBorderType = new RadioGroup();
            this.groupBox2.SuspendLayout();
            this.chkOutline.Properties.BeginInit();
            this.groupBoxGridBorder.SuspendLayout();
            this.chkUseSimpleBorder.Properties.BeginInit();
            this.groupBoxGridProperty.SuspendLayout();
            this.rdoGenerateGraphicsType.Properties.BeginInit();
            this.groupBoxGraticuleBorder.SuspendLayout();
            this.rdoGraticuleBorderType.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.btnOutlineSymbol);
            this.groupBox2.Controls.Add(this.chkOutline);
            this.groupBox2.Location = new Point(8, 0x85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x110, 0x33);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图廓线";
            this.btnOutlineSymbol.Enabled = false;
            this.btnOutlineSymbol.Location = new Point(0xa8, 0x10);
            this.btnOutlineSymbol.Name = "btnOutlineSymbol";
            this.btnOutlineSymbol.Size = new Size(0x58, 0x18);
            this.btnOutlineSymbol.Style = null;
            this.btnOutlineSymbol.TabIndex = 1;
            this.btnOutlineSymbol.Click += new EventHandler(this.btnOutlineSymbol_Click);
            this.chkOutline.Location = new Point(8, 0x10);
            this.chkOutline.Name = "chkOutline";
            this.chkOutline.Properties.Caption = "在格网外放置边框";
            this.chkOutline.Size = new Size(0x80, 0x13);
            this.chkOutline.TabIndex = 0;
            this.chkOutline.CheckedChanged += new EventHandler(this.chkOutline_CheckedChanged);
            this.groupBoxGridBorder.Controls.Add(this.btnSimpleBorderLine);
            this.groupBoxGridBorder.Controls.Add(this.chkUseSimpleBorder);
            this.groupBoxGridBorder.Location = new Point(8, 8);
            this.groupBoxGridBorder.Name = "groupBoxGridBorder";
            this.groupBoxGridBorder.Size = new Size(0x110, 120);
            this.groupBoxGridBorder.TabIndex = 2;
            this.groupBoxGridBorder.TabStop = false;
            this.groupBoxGridBorder.Text = "格网边框";
            this.btnSimpleBorderLine.Location = new Point(0x30, 0x38);
            this.btnSimpleBorderLine.Name = "btnSimpleBorderLine";
            this.btnSimpleBorderLine.Size = new Size(0x70, 0x20);
            this.btnSimpleBorderLine.Style = null;
            this.btnSimpleBorderLine.TabIndex = 1;
            this.btnSimpleBorderLine.Click += new EventHandler(this.btnSimpleBorderLine_Click);
            this.chkUseSimpleBorder.Location = new Point(0x18, 0x18);
            this.chkUseSimpleBorder.Name = "chkUseSimpleBorder";
            this.chkUseSimpleBorder.Properties.Caption = "在格网和轴标注间放置边框";
            this.chkUseSimpleBorder.Size = new Size(0xb0, 0x13);
            this.chkUseSimpleBorder.TabIndex = 0;
            this.chkUseSimpleBorder.CheckedChanged += new EventHandler(this.chkUseSimpleBorder_CheckedChanged);
            this.groupBoxGridProperty.Controls.Add(this.rdoGenerateGraphicsType);
            this.groupBoxGridProperty.Location = new Point(8, 0xc0);
            this.groupBoxGridProperty.Name = "groupBoxGridProperty";
            this.groupBoxGridProperty.Size = new Size(0x110, 0x40);
            this.groupBoxGridProperty.TabIndex = 4;
            this.groupBoxGridProperty.TabStop = false;
            this.groupBoxGridProperty.Text = "格网属性";
            this.rdoGenerateGraphicsType.Location = new Point(0x10, 0x10);
            this.rdoGenerateGraphicsType.Name = "rdoGenerateGraphicsType";
            this.rdoGenerateGraphicsType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoGenerateGraphicsType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoGenerateGraphicsType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoGenerateGraphicsType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "作为可以编辑的静态图形存储"), new RadioGroupItem(null, "作为与数据框同步更新的固定格网存储") });
            this.rdoGenerateGraphicsType.Size = new Size(0xf8, 40);
            this.rdoGenerateGraphicsType.SelectedIndex = 1;
            this.rdoGenerateGraphicsType.TabIndex = 0;
            this.groupBoxGraticuleBorder.Controls.Add(this.btnCalibratedMapBorder);
            this.groupBoxGraticuleBorder.Controls.Add(this.btnSimpleBorderLineSymbol);
            this.groupBoxGraticuleBorder.Controls.Add(this.rdoGraticuleBorderType);
            this.groupBoxGraticuleBorder.Location = new Point(8, 8);
            this.groupBoxGraticuleBorder.Name = "groupBoxGraticuleBorder";
            this.groupBoxGraticuleBorder.Size = new Size(0x110, 120);
            this.groupBoxGraticuleBorder.TabIndex = 5;
            this.groupBoxGraticuleBorder.TabStop = false;
            this.groupBoxGraticuleBorder.Text = "经纬网边框";
            this.btnCalibratedMapBorder.Location = new Point(0x48, 0x56);
            this.btnCalibratedMapBorder.Name = "btnCalibratedMapBorder";
            this.btnCalibratedMapBorder.Size = new Size(80, 0x16);
            this.btnCalibratedMapBorder.TabIndex = 4;
            this.btnCalibratedMapBorder.Text = "属性...";
            this.btnCalibratedMapBorder.Click += new EventHandler(this.btnCalibratedMapBorder_Click);
            this.btnSimpleBorderLineSymbol.Location = new Point(0x40, 0x29);
            this.btnSimpleBorderLineSymbol.Name = "btnSimpleBorderLineSymbol";
            this.btnSimpleBorderLineSymbol.Size = new Size(0x68, 0x16);
            this.btnSimpleBorderLineSymbol.Style = null;
            this.btnSimpleBorderLineSymbol.TabIndex = 3;
            this.btnSimpleBorderLineSymbol.Click += new EventHandler(this.btnSimpleBorderLineSymbol_Click);
            this.rdoGraticuleBorderType.Location = new Point(8, 12);
            this.rdoGraticuleBorderType.Name = "rdoGraticuleBorderType";
            this.rdoGraticuleBorderType.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoGraticuleBorderType.Properties.Appearance.Options.UseBackColor = true;
            this.rdoGraticuleBorderType.Properties.Appearance.Options.UseTextOptions = true;
            this.rdoGraticuleBorderType.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            this.rdoGraticuleBorderType.Properties.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.rdoGraticuleBorderType.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoGraticuleBorderType.Properties.GlyphAlignment = HorzAlignment.Default;
            this.rdoGraticuleBorderType.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "在经纬网边界放置简单边框"), new RadioGroupItem(null, "在经纬网边界放置校准边框") });
            this.rdoGraticuleBorderType.Size = new Size(0xc0, 0x54);
            this.rdoGraticuleBorderType.TabIndex = 2;
            this.rdoGraticuleBorderType.SelectedIndexChanged += new EventHandler(this.rdoGraticuleBorderType_SelectedIndexChanged);
            base.Controls.Add(this.groupBoxGraticuleBorder);
            base.Controls.Add(this.groupBoxGridProperty);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBoxGridBorder);
            base.Name = "MapGridBorderPropertyPage";
            base.Size = new Size(0x128, 0x110);
            base.Load += new EventHandler(this.MapGridBorderPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkOutline.Properties.EndInit();
            this.groupBoxGridBorder.ResumeLayout(false);
            this.chkUseSimpleBorder.Properties.EndInit();
            this.groupBoxGridProperty.ResumeLayout(false);
            this.rdoGenerateGraphicsType.Properties.EndInit();
            this.groupBoxGraticuleBorder.ResumeLayout(false);
            this.rdoGraticuleBorderType.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void MapGridBorderPropertyPage_Load(object sender, EventArgs e)
        {
            ILineSymbol symbol = new SimpleLineSymbolClass {
                Width = 1.0
            };
            this.btnOutlineSymbol.Style = symbol;
            this.Init();
            this.bool_0 = true;
        }

        private void rdoGraticuleBorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoGraticuleBorderType.SelectedIndex == 0)
            {
                this.btnSimpleBorderLineSymbol.Enabled = true;
                this.btnCalibratedMapBorder.Enabled = false;
                if (!(this.imapGrid_0.Border is ISimpleMapGridBorder))
                {
                    ISimpleMapGridBorder border = new SimpleMapGridBorderClass {
                        LineSymbol = this.btnSimpleBorderLineSymbol.Style as ILineSymbol
                    };
                    this.imapGrid_0.Border = border as IMapGridBorder;
                }
            }
            else
            {
                this.btnSimpleBorderLineSymbol.Enabled = false;
                this.btnCalibratedMapBorder.Enabled = true;
                if (!(this.imapGrid_0.Border is ICalibratedMapGridBorder))
                {
                    this.imapGrid_0.Border = new CalibratedMapGridBorderClass();
                }
            }
        }

        public bool IsGenerateGraphics
        {
            get
            {
                return (this.rdoGenerateGraphicsType.SelectedIndex == 0);
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
                if (this.bool_0)
                {
                    this.Init();
                }
            }
        }

        public ILineSymbol OutlineSymbol
        {
            get
            {
                if (this.chkOutline.Checked)
                {
                    return (this.btnOutlineSymbol.Style as ILineSymbol);
                }
                return null;
            }
        }
    }
}

