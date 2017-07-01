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
    internal partial class MapGridBorderPropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IMapGrid imapGrid_0 = null;

        public MapGridBorderPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnCalibratedMapBorder_Click(object sender, EventArgs e)
        {
            if (this.imapGrid_0.Border is ICalibratedMapGridBorder)
            {
                frmCalibratedMapBorder border = new frmCalibratedMapBorder
                {
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
                    (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol =
                        this.btnSimpleBorderLine.Style as ILineSymbol;
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
                    (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol =
                        this.btnSimpleBorderLineSymbol.Style as ILineSymbol;
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
                ISimpleMapGridBorder border = new SimpleMapGridBorderClass
                {
                    LineSymbol = this.btnSimpleBorderLine.Style as ILineSymbol
                };
                this.imapGrid_0.Border = border as IMapGridBorder;
            }
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
                        this.btnSimpleBorderLineSymbol.Style =
                            (this.imapGrid_0.Border as ISimpleMapGridBorder).LineSymbol;
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

        private void MapGridBorderPropertyPage_Load(object sender, EventArgs e)
        {
            ILineSymbol symbol = new SimpleLineSymbolClass
            {
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
                    ISimpleMapGridBorder border = new SimpleMapGridBorderClass
                    {
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
            get { return (this.rdoGenerateGraphicsType.SelectedIndex == 0); }
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