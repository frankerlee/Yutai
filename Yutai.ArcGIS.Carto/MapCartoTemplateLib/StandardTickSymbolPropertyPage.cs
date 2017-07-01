using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class StandardTickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILineSymbol ilineSymbol_0 = new SimpleLineSymbolClass();
        private IMarkerSymbol imarkerSymbol_0 = new SimpleMarkerSymbolClass();
        private MapCartoTemplateLib.MapTemplate mapTemplate_0 = null;
        private string string_0 = "线";

        public event OnValueChangeEventHandler OnValueChange;

        public StandardTickSymbolPropertyPage()
        {
            this.InitializeComponent();
            (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
            (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Size = 28.0;
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
                this.mapTemplate_0.GridSymbol = this.btnStyle.Style as ISymbol;
                this.mapTemplate_0.BigFontSize = double.Parse(this.cboFontSize.Text);
                this.mapTemplate_0.FontName = this.cboFontName.Text;
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

        public void Hide()
        {
        }

        private void rdoLine_CheckedChanged(object sender, EventArgs e)
        {
            if (this.bool_0 && this.rdoLine.Checked)
            {
                this.bool_1 = true;
                this.btnStyle.Style = this.ilineSymbol_0;
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
                this.btnStyle.Style = this.imarkerSymbol_0;
                if (this.OnValueChange != null)
                {
                    this.OnValueChange();
                }
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.MapTemplate = object_0 as MapCartoTemplateLib.MapTemplate;
        }

        private void StandardTickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.mapTemplate_0.GridSymbol != null)
            {
                this.btnStyle.Style = this.mapTemplate_0.GridSymbol;
                if (this.mapTemplate_0.GridSymbol is IMarkerSymbol)
                {
                    this.rdoTick.Checked = true;
                }
                if (this.mapTemplate_0.GridSymbol is ILineSymbol)
                {
                    this.rdoLine.Checked = true;
                }
            }
            else
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
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
            this.bool_0 = true;
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
            get { return this.string_0; }
            set { this.string_0 = value; }
        }
    }
}