using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public partial class ElementTypeSelectPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
     
        public event OnValueChangeEventHandler OnValueChange;

        public ElementTypeSelectPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            switch (this.cboMapTemplateElementType.SelectedIndex)
            {
                case 0:
                    this.MapTemplateElement = new MapTemplateTextElement(this.MapTemplate);
                    break;

                case 1:
                    this.MapTemplateElement = new MapTemplateScaleTextElement(this.MapTemplate);
                    break;

                case 2:
                    this.MapTemplateElement = new MapTemplateScaleBarElement(this.MapTemplate);
                    break;

                case 3:
                    this.MapTemplateElement = new MapTemplateLegendElement(this.MapTemplate);
                    break;

                case 4:
                    this.MapTemplateElement = new MapTemplatePictureElement(this.MapTemplate);
                    break;

                case 5:
                    this.MapTemplateElement = new MapTemplateOLEElement(this.MapTemplate);
                    break;

                case 6:
                    this.MapTemplateElement = new MapTemplateNorthElement(this.MapTemplate);
                    break;

                case 7:
                    this.MapTemplateElement = new MapTemplateJoinTableElement(this.MapTemplate);
                    break;

                case 8:
                    this.MapTemplateElement = new MapTemplateCustomLegendElement(this.MapTemplate);
                    break;

                case 9:
                    this.MapTemplateElement = new MapTemplateTableElement(this.MapTemplate);
                    break;
            }
        }

        public void Cancel()
        {
            this.bool_1 = false;
        }

 private void ElementTypeSelectPage_Load(object sender, EventArgs e)
        {
            this.cboMapTemplateElementType.SelectedIndex = 0;
            this.bool_0 = true;
        }

 private void method_0(object sender, EventArgs e)
        {
        }

        public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
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

        public MapCartoTemplateLib.MapTemplate MapTemplate
        {
            [CompilerGenerated]
            protected get
            {
                return this.mapTemplate_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateElement MapTemplateElement
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateElement_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.mapTemplateElement_0 = value;
            }
        }

        public string Title
        {
            get
            {
                return "类型";
            }
            set
            {
            }
        }
    }
}

