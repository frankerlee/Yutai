using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class MapTemplateGeneralPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;


        public event OnValueChangeEventHandler OnValueChange;

        public MapTemplateGeneralPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                this.MapTemplate.Width = double.Parse(this.txtWidth.Text);
                this.MapTemplate.Height = double.Parse(this.txtHeight.Text);
                this.MapTemplate.Scale = double.Parse(this.txtScale.Text);
                if (this.rdoRect.Checked)
                {
                    this.MapTemplate.MapFrameType = MapFrameType.MFTRect;
                }
                else
                {
                    this.MapTemplate.MapFrameType = MapFrameType.MFTTrapezoid;
                }
                this.MapTemplate.NewMapFrameTypeVal = this.MapTemplate.MapFrameType;
                this.MapTemplate.XInterval = double.Parse(this.txtXInterval.Text);
                this.MapTemplate.YInterval = double.Parse(this.txtYInterval.Text);
                this.MapTemplate.Name = this.txtName.Text;
                this.MapTemplate.LeftInOutSpace = double.Parse(this.txtLeftSpace.Text);
                this.MapTemplate.RightInOutSpace = double.Parse(this.txtRightSpace.Text);
                this.MapTemplate.TopInOutSpace = double.Parse(this.txtTopSpace.Text);
                this.MapTemplate.BottomInOutSpace = double.Parse(this.txtBottomSpace.Text);
                this.MapTemplate.BorderSymbol = this.styleButton1.Style as ISymbol;
                this.MapTemplate.OutBorderWidth = double.Parse(this.txtOutBorderWidth.Text);
                if (this.chkMapGrid.Checked)
                {
                    this.method_0();
                }
                else
                {
                    this.MapTemplate.MapGrid = null;
                }
                this.MapTemplate.FixedWidthAndBottomSpace = this.checkBox1.Checked;
            }
        }

        public bool CanApply()
        {
            try
            {
                if (double.Parse(this.txtWidth.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtHeight.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtScale.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtXInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtYInterval.Text) <= 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtOutBorderWidth.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtBottomSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtTopSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtLeftSpace.Text) < 0.0)
                {
                    return false;
                }
                if (double.Parse(this.txtRightSpace.Text) < 0.0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
            }
            return false;
        }

        public void Cancel()
        {
        }

        private void cboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboStyle.SelectedIndex == 0)
            {
                this.groupBoxPage.Visible = true;
                this.groupBoxPage.Enabled = true;
            }
            else if (this.cboStyle.SelectedIndex == 1)
            {
                this.groupBoxPage.Visible = true;
                this.groupBoxPage.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkMapGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void MapTemplateGeneralPage_Load(object sender, EventArgs e)
        {
            this.cboStyle.SelectedIndex = 0;
            CartographicLineSymbolClass class2 = new CartographicLineSymbolClass
            {
                Cap = esriLineCapStyle.esriLCSSquare
            };
            RgbColorClass class3 = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class2.Color = class3;
            class2.Join = esriLineJoinStyle.esriLJSMitre;
            class2.Width = 1.0;
            this.ilineSymbol_0 = class2;
            SimpleFillSymbolClass class4 = new SimpleFillSymbolClass();
            RgbColorClass class5 = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class4.Color = class5;
            class4.Style = esriSimpleFillStyle.esriSFSSolid;
            SimpleLineSymbolClass class6 = new SimpleLineSymbolClass
            {
                Width = 0.0
            };
            RgbColorClass class7 = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class6.Color = class7;
            class4.Outline = class6;
            this.ifillSymbol_0 = class4;
            this.styleButton1.Style = this.ilineSymbol_0;
            if (this.MapTemplate != null)
            {
                this.txtName.Text = this.MapTemplate.Name;
                if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
                {
                    this.groupBoxPage.Enabled = true;
                    this.panelScale.Enabled = true;
                }
                else
                {
                    this.groupBoxPage.Enabled = false;
                    this.panelScale.Enabled = false;
                }
                if (this.MapTemplate.MapFrameType == MapFrameType.MFTRect)
                {
                    this.rdoRect.Checked = true;
                }
                else
                {
                    this.rdoTrapezoid.Checked = true;
                }
                this.txtLeftSpace.Text = this.MapTemplate.LeftInOutSpace.ToString();
                this.txtRightSpace.Text = this.MapTemplate.RightInOutSpace.ToString();
                this.txtTopSpace.Text = this.MapTemplate.TopInOutSpace.ToString();
                this.txtBottomSpace.Text = this.MapTemplate.BottomInOutSpace.ToString();
                this.txtOutBorderWidth.Text = this.MapTemplate.OutBorderWidth.ToString();
                this.styleButton1.Style = this.MapTemplate.BorderSymbol;
                this.cboStyle.SelectedIndex = (int) this.MapTemplate.TemplateSizeStyle;
                this.txtWidth.Text = this.MapTemplate.Width.ToString();
                this.txtHeight.Text = this.MapTemplate.Height.ToString();
                this.txtScale.Text = this.MapTemplate.Scale.ToString();
                this.txtXInterval.Text = this.MapTemplate.XInterval.ToString();
                this.txtYInterval.Text = this.MapTemplate.YInterval.ToString();
                if (this.MapTemplate.BorderSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                    this.ilineSymbol_0 = this.MapTemplate.BorderSymbol as ILineSymbol;
                }
                else if (this.MapTemplate.BorderSymbol is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.MapTemplate.BorderSymbol as IFillSymbol;
                    this.rdoFill.Checked = true;
                }
                else
                {
                    this.rdoNoOutLine.Checked = true;
                    this.styleButton1.Enabled = false;
                }
                this.styleButton1.Style = this.MapTemplate.BorderSymbol;
                this.cboStyle.SelectedIndex = (int) this.MapTemplate.TemplateSizeStyle;
                this.chkMapGrid.Checked = this.MapTemplate.MapGrid != null;
                this.checkBox1.Checked = this.MapTemplate.FixedWidthAndBottomSpace;
            }
            this.bool_0 = true;
        }

        private void MapTemplateGeneralPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.bool_0 = false;
                if (this.MapTemplate != null)
                {
                    this.txtName.Text = this.MapTemplate.Name;
                    this.txtLeftSpace.Text = this.MapTemplate.LeftInOutSpace.ToString();
                    this.txtRightSpace.Text = this.MapTemplate.RightInOutSpace.ToString();
                    this.txtTopSpace.Text = this.MapTemplate.TopInOutSpace.ToString();
                    this.txtBottomSpace.Text = this.MapTemplate.BottomInOutSpace.ToString();
                    this.txtOutBorderWidth.Text = this.MapTemplate.OutBorderWidth.ToString();
                    this.styleButton1.Style = this.MapTemplate.BorderSymbol;
                    this.cboStyle.SelectedIndex = (int) this.MapTemplate.TemplateSizeStyle;
                    this.txtWidth.Text = this.MapTemplate.Width.ToString();
                    this.txtHeight.Text = this.MapTemplate.Height.ToString();
                    this.txtScale.Text = this.MapTemplate.Scale.ToString();
                    this.txtXInterval.Text = this.MapTemplate.XInterval.ToString();
                    this.txtYInterval.Text = this.MapTemplate.YInterval.ToString();
                    if (this.MapTemplate.BorderSymbol is ILineSymbol)
                    {
                        this.rdoLine.Checked = true;
                        this.ilineSymbol_0 = this.MapTemplate.BorderSymbol as ILineSymbol;
                    }
                    if (this.MapTemplate.BorderSymbol is IFillSymbol)
                    {
                        this.ifillSymbol_0 = this.MapTemplate.BorderSymbol as IFillSymbol;
                        this.rdoFill.Checked = true;
                    }
                    this.cboStyle.SelectedIndex = (int) this.MapTemplate.TemplateSizeStyle;
                    this.chkMapGrid.Checked = this.MapTemplate.MapGrid != null;
                }
                this.bool_0 = true;
            }
        }

        private void method_0()
        {
            IMapGrid grid = new MeasuredGridClass();
            if (this.MapFrame != null)
            {
                grid.SetDefaults(this.MapFrame);
            }
            (grid as IMeasuredGrid).XOrigin = 0.0;
            (grid as IMeasuredGrid).Units = esriUnits.esriMeters;
            (grid as IMeasuredGrid).YOrigin = 0.0;
            (grid as IMeasuredGrid).XIntervalSize = 200.0;
            (grid as IMeasuredGrid).YIntervalSize = 200.0;
            (grid as IMeasuredGrid).FixedOrigin = true;
            IGridLabel labelFormat = grid.LabelFormat;
            ITextSymbol symbol = new TextSymbolClass
            {
                Font = labelFormat.Font,
                Color = labelFormat.Color,
                Text = labelFormat.DisplayName,
                VerticalAlignment = esriTextVerticalAlignment.esriTVABottom
            };
            labelFormat.Font = symbol.Font;
            labelFormat.Color = symbol.Color;
            labelFormat.LabelOffset = 6.0;
            grid.LabelFormat = labelFormat;
            if (labelFormat is IMixedFontGridLabel2)
            {
                (labelFormat as IMixedFontGridLabel2).NumGroupedDigits = 0;
            }
            this.MapTemplate.MapGrid = grid;
        }

        private void method_1(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void method_2(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void method_3(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoFill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoFill.Checked)
            {
                this.styleButton1.Enabled = true;
                if (this.styleButton1.Style is ILineSymbol)
                {
                    this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                }
                this.styleButton1.Style = this.ifillSymbol_0;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                this.styleButton1.Enabled = true;
                if (this.styleButton1.Style is IFillSymbol)
                {
                    this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                }
                this.styleButton1.Style = this.ilineSymbol_0;
                this.txtOutBorderWidth.Text = (this.ilineSymbol_0.Width*0.0352777778).ToString("0.##");
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoNoOutLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoNoOutLine.Checked)
            {
                this.styleButton1.Enabled = false;
                this.styleButton1.Style = null;
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoRect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
            {
                this.groupBoxPage.Enabled = this.rdoRect.Checked;
            }
            this.MapTemplate.NewMapFrameTypeVal = this.rdoRect.Checked
                ? MapFrameType.MFTRect
                : MapFrameType.MFTTrapezoid;
            this.bool_1 = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.styleButton1.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.styleButton1.Style = selector.GetSymbol();
                    if (this.rdoLine.Checked)
                    {
                        this.ilineSymbol_0 = this.styleButton1.Style as ILineSymbol;
                        this.txtOutBorderWidth.Text = (this.ilineSymbol_0.Width*0.0352777778).ToString("0.##");
                    }
                    else
                    {
                        this.ifillSymbol_0 = this.styleButton1.Style as IFillSymbol;
                    }
                    this.bool_1 = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtOutBorderWidth_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                if (this.rdoLine.Checked)
                {
                    ILineSymbol style = this.styleButton1.Style as ILineSymbol;
                    if (style != null)
                    {
                        try
                        {
                            double num = Convert.ToDouble(this.txtOutBorderWidth.Text);
                            style.Width = num/0.0352777778;
                            this.styleButton1.Style = style;
                        }
                        catch
                        {
                        }
                    }
                }
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtScale_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtTopSpace_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtXInterval_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void txtYInterval_TextChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public IMapFrame MapFrame { get; set; }

        public MapTemplate MapTemplate { get; set; }


        public string Title
        {
            get { return "常规"; }
            set { }
        }

        public bool UseMapGrid
        {
            get { return this.chkMapGrid.Checked; }
        }
    }
}