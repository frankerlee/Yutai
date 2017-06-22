using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
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
                if (this.mapTemplateElement_0.Element is IFillShapeElement)
                {
                    (this.mapTemplateElement_0.Element as IFillShapeElement).Symbol = this.styleButton1.Style as IFillSymbol;
                }
                else if (this.mapTemplateElement_0.Element is ILineElement)
                {
                    (this.mapTemplateElement_0.Element as ILineElement).Symbol = this.styleButton1.Style as ILineSymbol;
                }
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 private void ElementSymbolSetPage_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

 private void method_0()
        {
            this.bool_0 = false;
            if (this.mapTemplateElement_0.Style != null)
            {
                this.styleButton1.Style = this.mapTemplateElement_0.Style;
            }
            else if (this.mapTemplateElement_0.Element is IFillShapeElement)
            {
                this.styleButton1.Style = (this.mapTemplateElement_0.Element as IFillShapeElement).Symbol;
            }
            else if (this.mapTemplateElement_0.Element is ILineElement)
            {
                this.styleButton1.Style = (this.mapTemplateElement_0.Element as ILineElement).Symbol;
            }
            this.bool_0 = true;
        }

        public void ResetControl()
        {
            this.bool_0 = false;
            this.method_0();
            this.bool_0 = true;
        }

        public void SetObjects(object object_0)
        {
            this.mapTemplateElement_0 = object_0 as MapCartoTemplateLib.MapTemplateElement;
            this.styleButton1.Style = this.mapTemplateElement_0.Style;
        }

        private void styleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSymbolSelector selector = new frmSymbolSelector();
                if (selector != null)
                {
                    selector.SetStyleGallery(ApplicationBase.StyleGallery);
                    selector.SetSymbol(this.styleButton1.Style);
                    if (selector.ShowDialog() == DialogResult.OK)
                    {
                        this.styleButton1.Style = selector.GetSymbol();
                        this.mapTemplateElement_0.Style = this.styleButton1.Style;
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
            get
            {
                return this.bool_1;
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

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            set
            {
                this.mapTemplateElement_0 = value;
                this.method_0();
            }
        }

        public string Title
        {
            get
            {
                return "符号";
            }
            set
            {
            }
        }
    }
}

