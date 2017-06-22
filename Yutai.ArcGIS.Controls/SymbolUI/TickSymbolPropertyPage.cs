using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    public partial class TickSymbolPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private ILineSymbol m_TickLineSymbol = null;
        private IMarkerSymbol m_TickMarkSymbol = null;
        private string m_Title = "线";

        public event OnValueChangeEventHandler OnValueChange;

        public TickSymbolPropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = null;
                }
                else if (this.radioGroup1.SelectedIndex == 0)
                {
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = this.m_TickLineSymbol;
                }
                else
                {
                    GridAxisPropertyPage.m_pMapGrid.LineSymbol = null;
                    GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol = this.m_TickMarkSymbol;
                }
            }
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            frmSymbolSelector selector = new frmSymbolSelector();
            if (selector != null)
            {
                selector.SetStyleGallery(GridAxisPropertyPage.m_pSG);
                selector.SetSymbol(this.btnStyle.Style);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.btnStyle.Style = selector.GetSymbol();
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.m_TickLineSymbol = this.btnStyle.Style as ILineSymbol;
                    }
                    else if (this.radioGroup1.SelectedIndex == 1)
                    {
                        this.m_TickMarkSymbol = this.btnStyle.Style as IMarkerSymbol;
                    }
                    this.m_IsPageDirty = true;
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
                this.btnStyle.Style = this.m_TickLineSymbol;
                this.btnStyle.Enabled = true;
            }
            else
            {
                this.btnStyle.Style = this.m_TickMarkSymbol;
                this.btnStyle.Enabled = true;
            }
            this.btnStyle.Invalidate();
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public void SetObjects(object @object)
        {
        }

        private void TickSymbolPropertyPage_Load(object sender, EventArgs e)
        {
            if (GridAxisPropertyPage.m_pMapGrid != null)
            {
                if (GridAxisPropertyPage.m_pMapGrid.LineSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 0;
                    this.m_TickLineSymbol = GridAxisPropertyPage.m_pMapGrid.LineSymbol;
                    this.m_TickMarkSymbol = new SimpleMarkerSymbolClass();
                    (this.m_TickMarkSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                    this.btnStyle.Style = this.m_TickLineSymbol;
                }
                else if (GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol != null)
                {
                    this.radioGroup1.SelectedIndex = 1;
                    this.m_TickMarkSymbol = GridAxisPropertyPage.m_pMapGrid.TickMarkSymbol;
                    this.m_TickLineSymbol = new SimpleLineSymbolClass();
                    this.btnStyle.Style = this.m_TickMarkSymbol;
                }
                else
                {
                    this.radioGroup1.SelectedIndex = 2;
                    this.btnStyle.Enabled = false;
                }
                this.m_CanDo = true;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.m_IsPageDirty;
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
                return this.m_Title;
            }
            set
            {
                this.m_Title = value;
            }
        }
    }
}

