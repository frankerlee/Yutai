using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
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
    public partial class OtherGridPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public OtherGridPropertyPage()
        {
            this.InitializeComponent();
            SimpleMarkerSymbolClass class2 = new SimpleMarkerSymbolClass
            {
                Style = esriSimpleMarkerStyle.esriSMSCross,
                Size = 28.0
            };
            this.imarkerSymbol_0 = class2;
            CartographicLineSymbolClass class3 = new CartographicLineSymbolClass
            {
                Cap = esriLineCapStyle.esriLCSSquare
            };
            RgbColorClass class4 = new RgbColorClass
            {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class3.Color = class4;
            class3.Join = esriLineJoinStyle.esriLJSMitre;
            class3.Width = 1.0;
            this.ilineSymbol_0 = class3;
            this.cboFontName.Items.Clear();
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                this.cboFontName.Items.Add(fonts.Families[i].Name);
            }
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.rdoNone.Checked)
                {
                    this.mapTemplate_0.GridSymbol = null;
                }
                else
                {
                    this.mapTemplate_0.GridSymbol = this.btnStyle.Style as ISymbol;
                }
                this.mapTemplate_0.BigFontSize = double.Parse(this.cboFontSize.Text);
                this.mapTemplate_0.SmallFontSize = this.mapTemplate_0.BigFontSize + 3.0;
                this.mapTemplate_0.FontName = this.cboFontName.Text;
                this.mapTemplate_0.AnnoUnit = (this.cboAnnoUnit.SelectedIndex == 0)
                    ? esriUnits.esriMeters
                    : esriUnits.esriKilometers;
                this.mapTemplate_0.AnnoUnitZoomScale = Convert.ToDouble(this.txtAnnUnitScale.Text);
                this.MapTemplate.DrawCornerShortLine = this.chkDrawCornerShortLine.Checked;
                this.MapTemplate.DrawRoundText = this.chkRoundText.Checked;
                this.MapTemplate.DrawCornerText = this.chkDrawRoundText.Checked;
                this.MapTemplate.DrawRoundLineShortLine = this.chkDrawRoundLineShortLine.Checked;
                this.MapTemplate.DrawJWD = this.chkDrawJWD.Checked;
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(ApplicationBase.StyleGallery);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.bool_1 = true;
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.rdoTick.Checked)
                    {
                        this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                    }
                    else
                    {
                        this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                    }
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
        }

        public void Cancel()
        {
        }

        private void cboFontName_SelectedIndexChanged(object sender, EventArgs e)
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

        private void cboFontSize_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkDrawRoundText_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_1 = true;
                if (this.chkDrawCornerShortLine.Checked)
                {
                    if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
                    {
                        if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
                        {
                            this.chkDrawRoundText.Enabled = true;
                        }
                        else
                        {
                            this.chkDrawRoundText.Enabled = false;
                            this.chkDrawRoundText.Checked = false;
                        }
                    }
                    else
                    {
                        this.chkDrawJWD.Enabled = true;
                    }
                }
                else if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
                {
                    this.chkDrawRoundText.Checked = false;
                    this.chkDrawRoundText.Enabled = false;
                }
                else
                {
                    this.chkDrawJWD.Checked = false;
                    this.chkDrawJWD.Enabled = false;
                }
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void chkRoundText_CheckedChanged(object sender, EventArgs e)
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

        private void method_0()
        {
            this.btnStyle.Style = this.mapTemplate_0.GridSymbol;
            if (this.mapTemplate_0.GridSymbol is IMarkerSymbol)
            {
                this.rdoTick.Checked = true;
            }
            else if (this.mapTemplate_0.GridSymbol is ILineSymbol)
            {
                this.rdoLine.Checked = true;
            }
            else
            {
                this.rdoNone.Checked = true;
            }
            if (this.mapTemplate_0.NewMapFrameTypeVal == MapFrameType.MFTRect)
            {
                this.chkDrawJWD.Checked = false;
                this.chkDrawJWD.Enabled = false;
            }
            else
            {
                this.chkDrawJWD.Enabled = true;
            }
            string fontName = this.mapTemplate_0.FontName;
            for (int i = 0; i < this.cboFontName.Items.Count; i++)
            {
                if (fontName == this.cboFontName.Items[i].ToString())
                {
                    this.cboFontName.SelectedIndex = i;
                    break;
                }
            }
            this.cboFontSize.Text = this.mapTemplate_0.BigFontSize.ToString();
            this.cboAnnoUnit.SelectedIndex = (this.mapTemplate_0.AnnoUnit == esriUnits.esriMeters) ? 0 : 1;
            this.txtAnnUnitScale.Text = this.mapTemplate_0.AnnoUnitZoomScale.ToString();
            this.chkDrawCornerShortLine.Checked = this.MapTemplate.DrawCornerShortLine;
            this.chkDrawRoundLineShortLine.Checked = this.MapTemplate.DrawRoundLineShortLine;
            this.chkRoundText.Checked = this.MapTemplate.DrawRoundText;
            if (this.MapTemplate.NewMapFrameTypeVal == MapFrameType.MFTRect)
            {
                if (this.MapTemplate.MapFramingType == MapFramingType.StandardFraming)
                {
                    this.chkDrawRoundText.Enabled = true;
                    this.chkDrawRoundText.Checked = this.MapTemplate.DrawCornerText;
                }
                else
                {
                    this.chkDrawRoundText.Enabled = false;
                    this.chkDrawRoundText.Checked = false;
                }
                this.chkDrawJWD.Enabled = false;
                this.chkDrawJWD.Checked = false;
            }
            else
            {
                this.chkDrawJWD.Enabled = true;
                this.chkDrawJWD.Checked = this.MapTemplate.DrawJWD;
                this.chkDrawRoundText.Enabled = false;
                this.chkDrawRoundText.Checked = false;
            }
        }

        private void OtherGridPropertyPage_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.bool_0 = true;
        }

        private void OtherGridPropertyPage_VisibleChanged(object sender, EventArgs e)
        {
            if (base.Visible)
            {
                this.bool_0 = false;
                this.method_0();
                this.bool_0 = true;
            }
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = true;
                this.btnStyle.Style = this.ilineSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoNone.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = false;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        private void rdoTick_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoTick.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Enabled = true;
                this.btnStyle.Style = this.imarkerSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            get { return this.mapTemplate_0; }
            set { this.mapTemplate_0 = value; }
        }

        public string Title
        {
            get { return "其他"; }
            set { }
        }
    }
}