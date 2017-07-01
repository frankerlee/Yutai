using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.Historical;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;


namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class DestinationPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_CanDo = false;
        private bool m_IsPageDirty = false;
        private IStyleGallery m_pSG = null;
        private IRealTimeDestination m_RealTimeDestination = null;
        private string m_Title = "常规";

        public event OnValueChangeEventHandler OnValueChange;

        public DestinationPropertyPage()
        {
            this.InitializeComponent();
        }

        event Common.BaseClasses.OnValueChangeEventHandler IPropertyPageEvents.OnValueChange
        {
            add { throw new NotImplementedException(); }

            remove { throw new NotImplementedException(); }
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
                this.m_RealTimeDestination.BearingToDestinationSymbol =
                    (this.btnBearingSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_RealTimeDestination.DestinationTextSymbol =
                    (this.btnLabelSymbol.Style as IClone).Clone() as ITextSymbol;
                this.m_RealTimeDestination.DestinationSymbol = (this.btnSymbol.Style as IClone).Clone() as IMarkerSymbol;
                this.m_RealTimeDestination.DestinationLabel = this.txtLabel.Text;
                this.m_RealTimeDestination.ShowBearingToDestination = this.chkShowBearingSymbol.Checked;
            }
        }

        private void btnBearingSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.BearingToDestinationSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnBearingSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.DestinationTextSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnLabelSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnSymbol_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(this.m_pSG);
                    selector.SetSymbol(this.m_RealTimeDestination.DestinationSymbol);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.btnSymbol.Style = selector.GetSymbol();
                        this.ValueChange();
                    }
                }
            }
            catch
            {
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

        private void chkShowBearingSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void DestinationPropertyPage_Load(object sender, EventArgs e)
        {
            if (this.m_RealTimeDestination != null)
            {
                this.btnBearingSymbol.Style = this.m_RealTimeDestination.BearingToDestinationSymbol;
                this.btnLabelSymbol.Style = this.m_RealTimeDestination.DestinationTextSymbol;
                this.btnSymbol.Style = this.m_RealTimeDestination.DestinationSymbol;
                this.txtLabel.Text = this.m_RealTimeDestination.DestinationLabel;
                this.chkShowBearingSymbol.Checked = this.m_RealTimeDestination.ShowBearingToDestination;
                this.m_CanDo = true;
            }
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object @object)
        {
            this.m_RealTimeDestination = @object as IRealTimeDestination;
        }

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.ValueChange();
            }
        }

        private void ValueChange()
        {
            this.m_IsPageDirty = true;
            if (this.OnValueChange != null)
            {
                this.OnValueChange();
            }
        }

        public bool IsPageDirty
        {
            get { return this.m_IsPageDirty; }
        }

        int IPropertyPage.Height
        {
            get { return base.Height; }
        }

        int IPropertyPage.Width
        {
            get { return base.Width; }
        }

        public IRealTimeDestination RealTimeDestination
        {
            set { this.m_RealTimeDestination = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.m_pSG = value; }
        }

        public string Title
        {
            get { return this.m_Title; }
            set { this.m_Title = value; }
        }
    }
}