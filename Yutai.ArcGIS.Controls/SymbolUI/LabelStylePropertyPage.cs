using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.Plugins.Interfaces;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    internal partial class LabelStylePropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool m_IsPageDirty = false;
        private ILabelStyle m_pLabelStyle = null;
        private IBasicOverposterLayerProperties m_pOverposterLayerProperties = null;
        private ITextSymbol m_pTextSymbol = null;

        public event OnValueChangeEventHandler OnValueChange;

        public LabelStylePropertyPage()
        {
            this.InitializeComponent();
        }

        public LabelStylePropertyPage(IAppContext context)
        {
            this.InitializeComponent();
            _context =context;
        }

        public void Apply()
        {
            if (this.m_IsPageDirty)
            {
                this.m_IsPageDirty = false;
            }
        }

        private void btnLabelPlacement_Click(object sender, EventArgs e)
        {
            frmElementProperty property = new frmElementProperty {
                Text = "放值属性"
            };
            LabelPlacementPropertyPage page = new LabelPlacementPropertyPage();
            property.AddPage(page);
            LabelConficPropertyPage page2 = new LabelConficPropertyPage();
            property.AddPage(page2);
            if (property.EditProperties(this.m_pOverposterLayerProperties))
            {
            }
        }

        private void btnLabelSymbol_Click(object sender, EventArgs e)
        {
            if (this.m_pTextSymbol != null)
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                selector.SetStyleGallery(_context.StyleGallery);
                selector.SetSymbol(this.m_pTextSymbol);
                if (selector.ShowDialog() == DialogResult.OK)
                {
                    this.m_pTextSymbol = selector.GetSymbol() as ITextSymbol;
                    this.symbolItem1.Symbol = this.m_pTextSymbol;
                    this.symbolItem1.Invalidate();
                }
            }
        }

        public void Cancel()
        {
            this.m_IsPageDirty = false;
        }

 private void LabelStylePropertyPage_Load(object sender, EventArgs e)
        {
            this.symbolItem1.Symbol = this.m_pTextSymbol;
        }

        public void SetObjects(object @object)
        {
            ILabelStyle style = @object as ILabelStyle;
            if (style != null)
            {
                this.m_pTextSymbol = (style.Symbol as IClone).Clone() as ITextSymbol;
                this.m_pOverposterLayerProperties = (style.BasicOverposterLayerProperties as IClone).Clone() as IBasicOverposterLayerProperties;
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
                return "标注样式";
            }
            set
            {
            }
        }
    }
}

