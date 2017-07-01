using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;
using IPropertyPage = Yutai.ArcGIS.Common.BaseClasses.IPropertyPage;
using IPropertyPageEvents = Yutai.ArcGIS.Common.BaseClasses.IPropertyPageEvents;
using OnValueChangeEventHandler = Yutai.ArcGIS.Common.BaseClasses.OnValueChangeEventHandler;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public partial class ElementSymbolSetPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;

        public event OnValueChangeEventHandler OnValueChange;

        public ElementSymbolSetPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            if (this.bool_1)
            {
                this.bool_1 = false;
                ElementWizardHelp.Style = this.styleButton1.Style;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

        private void ElementSymbolSetPage_Load(object sender, EventArgs e)
        {
            this.styleButton1.Style = ElementWizardHelp.Style;
            this.bool_0 = true;
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.styleButton1.Style = ElementWizardHelp.Style;
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(new MyStyleGallery());
                    selector.SetSymbol(this.styleButton1.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.styleButton1.Style = selector.GetSymbol();
                        ElementWizardHelp.Style = this.styleButton1.Style;
                        this.bool_1 = true;
                        if (this.OnValueChange != null)
                        {
                            this.OnValueChange();
                        }
                    }
                }
            }
            catch
            {
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

        public string Title
        {
            get { return "符号"; }
            set { }
        }
    }
}