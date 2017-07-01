using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class TickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private ILineSymbol ilineSymbol_0 = null;
        private IMarkerSymbol imarkerSymbol_0 = null;
        protected IMapGrid m_pMapGrid = null;


        public event OnValueChangeEventHandler OnValueChange;

        public TickSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    this.m_pMapGrid.TickMarkSymbol = null;
                    this.m_pMapGrid.LineSymbol = null;
                }
                else if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.m_pMapGrid.TickMarkSymbol = null;
                    this.m_pMapGrid.LineSymbol = this.ilineSymbol_0;
                }
                else
                {
                    this.m_pMapGrid.LineSymbol = null;
                    this.m_pMapGrid.TickMarkSymbol = this.imarkerSymbol_0;
                }
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
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.ilineSymbol_0 = this.btnStyle.Style as ILineSymbol;
                    }
                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        this.imarkerSymbol_0 = this.btnStyle.Style as IMarkerSymbol;
                    }
                    this.bool_1 = true;
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

        public void Hide()
        {
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 2)
            {
                this.btnStyle.Style = null;
                this.btnStyle.Enabled = false;
            }
            else if (this.radioGroup1.SelectedIndex == 0)
            {
                this.btnStyle.Style = this.ilineSymbol_0;
                this.btnStyle.Enabled = true;
            }
            else
            {
                this.btnStyle.Style = this.imarkerSymbol_0;
                this.btnStyle.Enabled = true;
            }
            this.btnStyle.Invalidate();
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
            this.m_pMapGrid = this.MapTemplate.MapGrid;
        }

        private void TickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            this.m_pMapGrid = this.MapTemplate.MapGrid;
            if (this.m_pMapGrid != null)
            {
                if (this.m_pMapGrid.LineSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                    this.ilineSymbol_0 = this.m_pMapGrid.LineSymbol;
                    this.imarkerSymbol_0 = new SimpleMarkerSymbolClass();
                    (this.imarkerSymbol_0 as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                    this.btnStyle.Style = this.ilineSymbol_0;
                }
                else if (this.m_pMapGrid.TickMarkSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 1;
                    this.imarkerSymbol_0 = this.m_pMapGrid.TickMarkSymbol;
                    this.ilineSymbol_0 = new SimpleLineSymbolClass();
                    this.btnStyle.Style = this.imarkerSymbol_0;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                    this.btnStyle.Enabled = false;
                }
                this.bool_0 = true;
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

        public MapCartoTemplateLib.MapTemplate MapTemplate { get; set; }

        public string Title { get; set; }
    }
}